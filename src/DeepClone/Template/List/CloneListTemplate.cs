﻿using DeepClone.Model;
using Natasha;
using Natasha.CSharp;
using System;
using System.Collections;
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

                    scriptBuilder.Append("ObjectCloneOperator.Clone(item)");

                }
                else
                {

                    scriptBuilder.Append("CloneOperator.Clone(item)");

                }
                scriptBuilder.Append("));");


                scriptBuilder.AppendLine(@"}return default;");
                var action = FastMethodOperator.UseDomain(info.CurrentType.GetDomain())
                                .Using("DeepClone")
                                .Using("System.Linq")
                                .Param(info.FatherType, "old")
                                .Body(scriptBuilder.ToString())
                                .Return(info.FatherType)
                                .Compile();
                return action;

            }
            else
            {

                throw new Exception("暂不支持");

            }
        }
    }
}
