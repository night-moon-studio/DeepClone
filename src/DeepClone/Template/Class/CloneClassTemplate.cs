using DeepClone.Model;
using Natasha.Operator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DeepClone.Template
{
    public class CloneClassTemplate : ICloneTemplate
    {

        internal readonly static int HashCode;
        static CloneClassTemplate() => HashCode = typeof(CloneClassTemplate).GetHashCode();




        public override int GetHashCode() => HashCode;




        public bool MatchType(Type type)
        {
            return type.IsClass
                && type != typeof(object)
                && type != typeof(string)
                && type != typeof(List<>)
                && type != typeof(Dictionary<,>)
                && !type.IsArray;
        }

        public Delegate TypeRouter(BuilderInfo info)
        {
            //构造函数处理: 不存在public无参构造函数无法克隆;
            if (info.DeclaringType.GetConstructor(new Type[0]) == null)
            {
                return default;
            }
            var builder = new StringBuilder($"new {info.DeclaringTypeName} {{");
            foreach (var fieldInfo in info.DeclaringType.GetFields(BindingFlags.Instance | BindingFlags.Public))
            {
                if (fieldInfo.Attributes.HasFlag(FieldAttributes.InitOnly))
                {
                    continue;
                }
                if (fieldInfo.DeclaringType.IsValueType || fieldInfo.FieldType == typeof(string))
                {
                    builder.Append($"{fieldInfo.Name}=oldModel.{fieldInfo.Name},");
                    continue;
                }
                builder.Append($"{fieldInfo.Name}=CloneOperator.Clone(oldModel.{fieldInfo.Name}),");
            }

            foreach (var propertyInfo in info.DeclaringType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (propertyInfo.Attributes.HasFlag(FieldAttributes.InitOnly))
                {
                    continue;
                }
                if (propertyInfo.DeclaringType.IsValueType || propertyInfo.PropertyType == typeof(string))
                {
                    builder.Append($"{propertyInfo.Name}=oldModel.{propertyInfo.Name},");
                    continue;
                }
                builder.Append($"{propertyInfo.Name}=CloneOperator.Clone(oldModel.{propertyInfo.Name}),");
            }

            builder.Append("}};");

            var func = FastMethodOperator.New
                .Param(info.DeclaringType, "oldModel")
                .MethodBody(builder.ToString())
                .Return(info.DeclaringType)
                .Complie();
            return func;
        }

    }

}