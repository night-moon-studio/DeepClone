using DeepClone.Model;
using Natasha;
using Natasha.Operator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DeepClone.Template.List
{
    public class CloneListTemplate : ICloneTemplate
    {
        internal readonly static int HashCode;
        static CloneListTemplate() => HashCode = typeof(CloneListTemplate).GetHashCode();

        public override int GetHashCode() => HashCode;

        public bool MatchType(Type type)
        {
            return type.IsGenericType && type != typeof(object) && type != typeof(string);
        }

        public Delegate TypeRouter(Model.BuilderInfo info)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($@"{info.DeclaringType.GetDevelopName()} newlist = new {info.DeclaringType.GetDevelopName()}(oldlist.Count);");
            if (info.DeclaringType.IsGenericType)
            {
                sb.AppendLine($@"foreach ({info.DeclaringType.GetGenericArguments()[0].GetDevelopName()} li in oldlist){{");
                // 值类型
                if (info.ArrayBaseType.IsOnceType())
                {
                    sb.Append("newlist.Add(li);");
                }
                else//类
                {
                    sb.Append("var tempinstance = CloneOperator.Clone(li);");
                    sb.Append("newlist.Add(tempinstance);");
                }
            }
            else
            {
                throw new Exception("暂不支持");
            }
            sb.AppendLine(@"}");
            sb.AppendLine(@"return newlist;");

            var action = FastMethodOperator.New
                            .Using("DeepClone")
                            .Using("Natasha")
                            .Using(typeof(Array))
                            .Using(typeof(IList))
                            .OopName($"DeepClone{Guid.NewGuid().ToString().Replace("-", string.Empty)}")
                            .MethodName("Clone")
                            .Param(info.DeclaringType, "oldlist")
                            .MethodBody(sb.ToString())
                            .Return(info.DeclaringType)
                            .Complie();
            return action;

        }
    }
}
