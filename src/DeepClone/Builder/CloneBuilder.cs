using Natasha;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Natasha.Operator;

namespace DeepClone.Builder
{

    public class CloneBuilder<T>
    {

        private static Func<T,T> ValueTypeFunc = (value) => value;


        public static Func<T,T> Create()
        {
            var type = typeof(T);
           
            if (type.IsValueType || type==typeof(string))
            {
                return ValueTypeFunc;
            }
            StringBuilder stringBuilder = new StringBuilder();
            if (type.MemberType == MemberTypes.NestedType)
            {
                stringBuilder.Append($"new {nameof(T)}(){{");
                foreach (var prpoertyInfo in type.GetProperties(BindingFlags.Public|BindingFlags.Instance))
                {
                    stringBuilder.Append($"{prpoertyInfo.Name}=CloneOperator.Clone(model.{prpoertyInfo.Name}),");
                }
                stringBuilder.Append("}}");
                return null;//(Func<T, T>) Natasha.NewMethod.From(stringBuilder.ToString());
            }

            var typeName = type.GetDevelopName();
            if (type.IsArray)
            {

            }

            if (typeName == typeof(List<>).FullName)
            {
            }

            if (typeName == typeof(Dictionary<,>).FullName)
            {
            }

            if (typeName == typeof(LinkedList<>).FullName)
            {

            }

            if (typeName == typeof(Tuple).FullName)
            {

            }
            return ValueTypeFunc;
        }
    }

}
