using DeepClone.Model;
using Natasha;
using Natasha.CSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DeepClone.Template
{
    public class CloneClassTemplate : ICloneTemplate
    {

        internal readonly static int HashCode;
        private CtorTempalte CtorHandler;
        static CloneClassTemplate() => HashCode = typeof(CloneClassTemplate).GetHashCode();




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

            var instanceName = "oldSource";
            info.FatherType = info.FatherType == typeof(object) ? info.CurrentType : info.FatherType;
            if (info.CurrentType != info.FatherType)
            {
                instanceName = "old";
            }
            CtorHandler = new CtorTempalte(info.CurrentType, instanceName);


            StringBuilder scriptBuilder = new StringBuilder();
            var memberBuilder = new StringBuilder();
            var builder = FastMethodOperator.UseDomain(info.CurrentType.GetDomain());
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

                        //简单类型直接赋值（值类型）
                        memberBuilder.Append($"{member.MemberName}={instanceName}.{member.MemberName},");

                    }
                    else if (member.MemberType == typeof(object))
                    {

                        //如果是object类型，那么使用object克隆方法
                        memberBuilder.Append($"{member.MemberName}=ObjectCloneOperator.Clone({instanceName}.{member.MemberName}),");

                    }
                    else
                    {

                        //如果是运行时类型
                        //精确添加Using防止二义性引用
                        builder.Using(member.MemberType);
                        memberBuilder.Append($"{member.MemberName}=CloneOperator.Clone({instanceName}.{member.MemberName}),");

                    }

                }
            }


            List<NBuildInfo> infos = new List<NBuildInfo>();
            foreach (var fieldInfo in info.CurrentType.GetFields(BindingFlags.Instance | BindingFlags.Public))
            {

                //获取NeetCtor注解
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

                //获取NeetCtor注解
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
            return builder
                .Using("DeepClone")
                .Using(info.CurrentType)
                .Param(info.FatherType, "oldSource")
                .Body(scriptBuilder.ToString())
                .Return(info.FatherType)
                .Compile();
        }

    }

}