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

    public class CloneListTemplate : ICloneTemplate
    {

        internal readonly static int HashCode;
        static CloneListTemplate() => HashCode = typeof(CloneListTemplate).GetHashCode();

        public override int GetHashCode() => HashCode;




        public bool MatchType(Type type)
        {
            return type.IsImplementFrom<IEnumerable>() && !type.IsImplementFrom<IDictionary>() && !type.IsArray;
        }




        public Delegate TypeRouter(Model.BuilderInfo info)
        {

            if (info.DeclaringType.IsGenericType)
            {

                StringBuilder scriptBuilder = new StringBuilder();
                scriptBuilder.AppendLine(@"if(old!=default){ return ");

                // 初始化目标，区分实体类和接口
                if (!info.DeclaringType.IsInterface)
                {

                    scriptBuilder.AppendLine($"new {info.DeclaringTypeName}");

                }


                scriptBuilder.AppendLine("(old.Select(item=>");
                var parameters = info.DeclaringType.GetGenericArguments();
                if (parameters[0].IsSimpleType())
                {

                    scriptBuilder.Append("item");

                }
                else
                {

                    scriptBuilder.Append("CloneOperator.Clone(item)");

                }
                scriptBuilder.Append("));");


                scriptBuilder.AppendLine(@"return old;");
                var action = FastMethodOperator.New
                                .Using("DeepClone")
                                .Using("System.Linq")
                                .Param(info.DeclaringType, "oldlist")
                                .MethodBody(scriptBuilder.ToString())
                                .Return(info.DeclaringType)
                                .Complie();
                return action;

            }
            else
            {

                throw new Exception("暂不支持");

            }
        }
    }
}
