using DeepClone.Model;
using Natasha;
using Natasha.Operator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DeepClone.Template.Class
{
    public class CloneDicTemplate : ICloneTemplate
    {
        internal readonly static int HashCode;
        static CloneDicTemplate() => HashCode = typeof(CloneDicTemplate).GetHashCode();

        public override int GetHashCode() => HashCode;

        public bool MatchType(Type type)
        {
            return type is IDictionary;
        }

        public Delegate TypeRouter(Model.BuilderInfo info)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($@"{info.DeclaringType.GetDevelopName()} newDic = new {info.DeclaringType.GetDevelopName()}(oldDic.Count);");
            if (info.DeclaringType.IsGenericType)
            {
                sb.AppendLine($@"foreach(KeyValuePair<{info.DeclaringType.GetGenericArguments()[0].GetDevelopName()},{info.DeclaringType.GetGenericArguments()[1].GetDevelopName()}> item in oldDic) {{");
                // 复制值
                if (info.DeclaringType.GetGenericArguments()[0].IsOnceType() && info.DeclaringType.GetGenericArguments()[1].IsOnceType())
                {
                    sb.AppendLine($@"newDic.Add(item.Key,item.Value);");
                }
                else if (info.DeclaringType.GetGenericArguments()[0].IsOnceType() && !info.DeclaringType.GetGenericArguments()[1].IsOnceType())
                {
                    sb.AppendLine($@" var value =CloneOperator.Clone(item.Value);");
                    sb.AppendLine($@"newDic.Add(item.Key,value);");
                }
                else if (!info.DeclaringType.GetGenericArguments()[0].IsOnceType() && info.DeclaringType.GetGenericArguments()[1].IsOnceType())
                {
                    sb.AppendLine($@" var key =CloneOperator.Clone(item.Key);");
                    sb.AppendLine($@"newDic.Add(key,item.Value);");
                }
                else if (!info.DeclaringType.GetGenericArguments()[0].IsOnceType() && !info.DeclaringType.GetGenericArguments()[1].IsOnceType())
                {
                    sb.AppendLine($@" var key =CloneOperator.Clone(item.Key);");
                    sb.AppendLine($@" var value =CloneOperator.Clone(item.Value);");
                    sb.AppendLine($@"newDic.Add(key,value);");
                }
            }
            else
            {
                //sb.AppendLine($@"foreach(DictionaryEntry item in oldDic) {{");
                throw new Exception("暂不支持非泛型字典的拷贝");
            }
            sb.AppendLine(@"}");
            sb.AppendLine(@"return newDic;");

            var action = FastMethodOperator.New
                            .Using("DeepClone")
                            .Using("Natasha")
                            .Using(typeof(IDictionary))
                            .OopName($"DeepClone{Guid.NewGuid().ToString().Replace("-", string.Empty)}")
                            .MethodName("Clone")
                            .Param(info.DeclaringType, "oldDic")
                            .MethodBody(sb.ToString())
                            .Return(info.DeclaringType)
                            .Complie();

            return action;
        }
    }
}
