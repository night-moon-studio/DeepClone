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

    public class FullCloneListTemplate : ICloneTemplate
    {

        internal readonly static int HashCode;
        static FullCloneListTemplate() => HashCode = typeof(FullCloneListTemplate).GetHashCode();

        public override int GetHashCode() => HashCode;




        public bool MatchType(Type type)
        {
            return type.IsImplementFrom<IEnumerable>() && !type.IsImplementFrom<IDictionary>() && !type.IsArray;
        }




        public Delegate TypeRouter(NBuildInfo info)
        {

            if (info.CurrentType.IsGenericType)
            {

                StringBuilder scriptBuilder = new StringBuilder();
                scriptBuilder.AppendLine(@"if(old!=default){ return ");

                // 初始化目标，区分实体类和接口
                if (!info.CurrentType.IsInterface)
                {

                    scriptBuilder.AppendLine($"new {info.CurrentTypeName}");

                }


                scriptBuilder.AppendLine("(old.Select(item=>");
                var parameters = info.CurrentType.GetGenericArguments();
                if (parameters[0].IsSimpleType())
                {

                    scriptBuilder.Append("item");

                }
                else if (parameters[0] == typeof(object))
                {

                    scriptBuilder.Append("FullObjectCloneOperator.Clone(item)");

                }
                else
                {

                    scriptBuilder.Append("CloneOperator.Clone(item)");

                }
                scriptBuilder.Append("));");


                scriptBuilder.AppendLine(@"}return default;");
                var action = FastMethodOperator.New
                                .Using("DeepClone")
                                .Using("System.Linq")
                                .Param(info.CurrentType, "old")
                                .MethodBody(scriptBuilder.ToString())
                                .Return(info.CurrentType)
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
