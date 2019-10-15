using DeepClone.Model;
using Natasha;
using Natasha.Operator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DeepClone.Template
{
    public class FullCloneClassTemplate : ICloneTemplate
    {

        internal readonly static int HashCode;
        private FullCloneCtorTempalte CtorHandler;
        static FullCloneClassTemplate() => HashCode = typeof(FullCloneClassTemplate).GetHashCode();




        public override int GetHashCode() => HashCode;




        public bool MatchType(Type type)
        {
            return type.IsClass
                && type != typeof(object)
                && !type.IsImplementFrom<IEnumerable>()
                && !type.IsSimpleType()
                && !type.IsArray
                && !type.IsInterface;
        }

        public Delegate TypeRouter(NBuildInfo info)
        {

            CtorHandler = new FullCloneCtorTempalte(info.CurrentType);

            var instanceName = "oldSource";
            info.FatherType = info.FatherType == typeof(object) ? info.CurrentType : info.FatherType;
            if (info.CurrentType != info.FatherType)
            {
                instanceName = "old";
            }

            StringBuilder scriptBuilder = new StringBuilder();
            var memberBuilder = new StringBuilder();
            var builder = FastMethodOperator.New;
            //构造函数处理: 不存在public无参构造函数无法克隆;
            if (info.CurrentType.GetConstructor(new Type[0]) == null)
            {
                return default;
            }


            var members = NBuildInfo.GetInfos(info.CurrentType);
            foreach (var item in members)
            {

                var member = item.Value;
                if (member == null)
                {
                    continue;
                }


                if (member.CanWrite && member.CanRead && !member.IsStatic)
                {
                    if (member.MemberType.IsSimpleType())
                    {
                        memberBuilder.Append($"{member.MemberName}={instanceName}.{member.MemberName},");
                    }
                    else if (member.MemberType == typeof(object))
                    {
                        memberBuilder.Append($"{member.MemberName}=FullObjectCloneOperator.Clone({instanceName}.{member.MemberName}),");
                    }
                    else
                    {
                        builder.Using(member.MemberType);
                        memberBuilder.Append($"{member.MemberName}=CloneOperator.Clone({instanceName}.{member.MemberName}),");
                    }

                }
            }


            List<NBuildInfo> infos = new List<NBuildInfo>();
            foreach (var fieldInfo in info.CurrentType.GetFields(BindingFlags.Instance | BindingFlags.Public))
            {

                var ctorAttr = fieldInfo.GetCustomAttribute<NeedCtorAttribute>();
                if (ctorAttr != default)
                {
                    NBuildInfo tempInfo = fieldInfo;
                    tempInfo.MemberTypeAvailableName = ctorAttr.Name == default ? fieldInfo.Name.ToUpper() : ctorAttr.Name.ToUpper();
                    infos.Add(tempInfo);
                }

            }

            foreach (var propertyInfo in info.CurrentType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {

                var ctorAttr = propertyInfo.GetCustomAttribute<NeedCtorAttribute>();
                if (ctorAttr != default)
                {
                    NBuildInfo tempInfo = propertyInfo;
                    tempInfo.MemberTypeAvailableName = ctorAttr.Name == default ? propertyInfo.Name.ToUpper() : ctorAttr.Name.ToUpper();
                    infos.Add(tempInfo);
                }

            }

            string readonlyScript = CtorHandler.GetCtor(infos);
            if (info.CurrentType == info.FatherType)
            {
                scriptBuilder.Insert(0, $"if(oldSource!=default){{ return new {info.CurrentTypeName}({readonlyScript}) {{");
            }
            else
            {
                scriptBuilder.Insert(0, $"if(oldSource!=default){{ var old = ({info.CurrentTypeName})oldSource; return new {info.CurrentTypeName}({readonlyScript}) {{");
            }
           
            if (memberBuilder.Length > 0)
            {
                memberBuilder.Length -= 1;
                scriptBuilder.Append(memberBuilder);
            }

          
            scriptBuilder.Append("};}return default;");
            var func = builder
                .Using("DeepClone")
                .Using(info.CurrentType)
                .Param(info.FatherType, "oldSource")
                .MethodBody(scriptBuilder.ToString())
                .Return(info.FatherType)
                .Complie();
            return func;
        }

    }

}