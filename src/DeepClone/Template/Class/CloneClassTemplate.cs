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
            foreach (var memeberInfo in info.DeclaringType.GetMembers(BindingFlags.Instance | BindingFlags.Public).Where(item=>item.MemberType==MemberTypes.Field|| item.MemberType==MemberTypes.Property))
            {
                if (memeberInfo.DeclaringType.IsValueType || (memeberInfo.MemberType==MemberTypes.Field &&(memeberInfo as FieldInfo).FieldType == typeof(string)))
                {
                    builder.Append($"{memeberInfo.Name}=oldModel.{memeberInfo.Name}");
                    continue;
                }
                if (memeberInfo.DeclaringType.IsValueType || (memeberInfo.MemberType == MemberTypes.Field && (memeberInfo as PropertyInfo).PropertyType == typeof(string)))
                {
                    builder.Append($"{memeberInfo.Name}=oldModel.{memeberInfo.Name}");
                    continue;
                }
                builder.Append($"{memeberInfo.Name}=CloneOperator.Clone(oldModel.{memeberInfo.Name})");
            }

            builder.Append("}};");

            return FastMethodOperator.New
                .OopName($"{info.DeclaringTypeName}Clone")
                .Param(info.DeclaringType, "oldModel")
                .OopBody(builder)
                .Return(info.DeclaringType)
                .Complie();
        }

    }

}