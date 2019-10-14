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

    public class FastCloneListTemplate : ICloneTemplate
    {

        internal readonly static int HashCode;
        static FastCloneListTemplate() => HashCode = typeof(FastCloneListTemplate).GetHashCode();

        public override int GetHashCode() => HashCode;




        public bool MatchType(Type type)
        {
            return type.IsImplementFrom<IEnumerable>() && !type.IsImplementFrom<IDictionary>() && !type.IsArray;
        }




        public Delegate TypeRouter(NBuildInfo info)
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
                else if (parameters[0] == typeof(object))
                {

                    scriptBuilder.Append("FastObjectCloneOperator.Clone(item)");

                }
                else
                {

                    scriptBuilder.Append("FastCloneOperator.Clone(item)");

                }
                scriptBuilder.Append("));");


                scriptBuilder.AppendLine(@"}return default;");
                var action = FastMethodOperator.New
                                .Using("DeepClone")
                                .Using("System.Linq")
                                .Param(info.DeclaringType, "old")
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
