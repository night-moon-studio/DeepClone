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
    public class CloneClassTemplate : ICloneTemplate
    {

        internal readonly static int HashCode;
        private CloneCtorTempalte CtorHandler;
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

            CtorHandler = new CloneCtorTempalte(info.DeclaringType);


            var builder = FastMethodOperator.New;
            //构造函数处理: 不存在public无参构造函数无法克隆;
            if (info.DeclaringType.GetConstructor(new Type[0]) == null)
            {
                return default;
            }


            List<NBuildInfo> infos = new List<NBuildInfo>();
            StringBuilder scriptBuilder = new StringBuilder();
            var memberBuilder = new StringBuilder();
            foreach (var fieldInfo in info.DeclaringType.GetFields(BindingFlags.Instance | BindingFlags.Public))
            {

                var ctorAttr = fieldInfo.GetCustomAttribute<NeedCtorAttribute>();
                if (ctorAttr != default)
                {
                    NBuildInfo tempInfo = fieldInfo;
                    tempInfo.DeclaringAvailableName = ctorAttr.Name == default ? fieldInfo.Name.ToUpper() : ctorAttr.Name.ToUpper();
                    infos.Add(tempInfo);
                }


                if (!fieldInfo.IsInitOnly)
                {
                    
                    if (fieldInfo.FieldType.IsSimpleType())
                    {
                        memberBuilder.Append($"{fieldInfo.Name}=old.{fieldInfo.Name},");
                    }
                    else
                    {
                        builder.Using(fieldInfo.FieldType);
                        memberBuilder.Append($"{fieldInfo.Name}=CloneOperator.Clone(old.{fieldInfo.Name}),");
                    }

                }

            }


            foreach (var propertyInfo in info.DeclaringType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {

                if (propertyInfo.CanWrite && propertyInfo.CanRead)
                {

                    var ctorAttr = propertyInfo.GetCustomAttribute<NeedCtorAttribute>();
                    if (ctorAttr != default)
                    {
                        NBuildInfo tempInfo = propertyInfo;
                        tempInfo.DeclaringAvailableName = ctorAttr.Name == default ? propertyInfo.Name.ToUpper() : ctorAttr.Name.ToUpper();
                        infos.Add(tempInfo);
                    }


                    if (propertyInfo.PropertyType.IsSimpleType())
                    {
                        memberBuilder.Append($"{propertyInfo.Name}=old.{propertyInfo.Name},");
                    }
                    else
                    {
                        builder.Using(propertyInfo.PropertyType);
                        memberBuilder.Append($"{propertyInfo.Name}=CloneOperator.Clone(old.{propertyInfo.Name}),");
                    }

                }

            }
            string readonlyScript = CtorHandler.GetCtor(infos);
            scriptBuilder.Insert(0,$"if(old!=default){{ return new {info.DeclaringTypeName}({readonlyScript}) {{");
            if (memberBuilder.Length>0)
            {
                memberBuilder.Length -= 1;
                scriptBuilder.Append(memberBuilder);
            }
            

            scriptBuilder.Append("};}return default;");

            var func = builder
                .Using("DeepClone")
                .Param(info.DeclaringType, "old")
                .MethodBody(scriptBuilder.ToString())
                .Return(info.DeclaringType)
                .Complie();
            return func;
        }

    }

}