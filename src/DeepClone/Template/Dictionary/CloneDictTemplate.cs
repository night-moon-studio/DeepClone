using DeepClone.Model;
using Natasha;
using Natasha.Operator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DeepClone.Template
{
    public class CloneDictTemplate : ICloneTemplate
    {

        internal readonly static int HashCode;
        static CloneDictTemplate() => HashCode = typeof(CloneDictTemplate).GetHashCode();




        public override int GetHashCode() => HashCode;




        public bool MatchType(Type type)
        {

            return type.IsImplementFrom<IDictionary>();

        }




        public Delegate TypeRouter(Model.BuilderInfo info)
        {

            StringBuilder scriptBuilder = new StringBuilder();
            scriptBuilder.AppendLine(@"if(old!=default){ return ");


            //初始化目标，区分实体类和接口
            if (!info.DeclaringType.IsInterface)
            {

                scriptBuilder.AppendLine($"new {info.DeclaringTypeName}");

            }
           scriptBuilder.AppendLine(" (old.Select(item=>{{return  new KeyValuePair(");


            //克隆Key
            var keyType = info.DeclaringType.GetGenericArguments()[0];
            if (keyType.IsSimpleType())
            {

                scriptBuilder.Append($"item.Key,");

            }
            else
            {

                scriptBuilder.AppendLine($"CloneOperator.Clone(item.Key),");

            }


            //克隆Value
            var valueType = info.DeclaringType.GetGenericArguments()[1];
            if (valueType.IsSimpleType())
            {

                scriptBuilder.Append($"item.Value");

            }
            else
            {

                scriptBuilder.AppendLine($"CloneOperator.Clone(item.Value)");

            }


            //补全括号，返回默认值。
            scriptBuilder.AppendLine(");}));}return default;");


            var action = FastMethodOperator.New
                            .Using("DeepClone")
                            .Using("System.Linq")
                            .Using(typeof(IDictionary))
                            .Using(typeof(KeyValuePair<,>))
                            .Param(info.DeclaringType, "old")
                            .MethodBody(scriptBuilder.ToString())
                            .Return(info.DeclaringType)
                            .Complie();
            return action;

        }

    }

}
