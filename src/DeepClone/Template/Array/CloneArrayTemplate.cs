using System;
using System.Collections;
using System.Text;
using DeepClone.Model;
using Natasha;
using Natasha.Operator;

namespace DeepClone.Template
{
    public class CloneArrayTemplate : ICloneTemplate
    {
        internal readonly static int HashCode;
        static CloneArrayTemplate() => HashCode = typeof(CloneArrayTemplate).GetHashCode();

        public override int GetHashCode() => HashCode;


        private readonly static Type arrayType = typeof(Array);
        private readonly static Type arrayListType = typeof(ArrayList);
        public bool MatchType(Type type) => type.IsArray;


        public Delegate TypeRouter(Model.BuilderInfo info)
        {
            var sb = new StringBuilder();

            if (info.ArrayBaseType.IsOnceType())
            {
                sb.AppendLine($@"
                        var newIns = new {info.ElementTypeName}[oldIns.Length];
                        Array.Copy(oldIns, newIns, newIns.Length);
                        return newIns;");
            }
            else if (info.ArrayDimensions > 1) // 多维数组
            {
                // 循环变量数组
                var varNameArr = new string[info.ArrayDimensions];
                // 多维数组长度数组
                var multiArrLenArr = new string[info.ArrayDimensions];

                for (int i = 0; i < info.ArrayDimensions; i++)
                {
                    var varName = $"_{i}";
                    varNameArr[i] = varName;
                    multiArrLenArr[i] = $"oldIns.GetLength({i})";

                    sb.AppendLine($"for (int {varName} = 0; {varName} < oldIns.GetLength({i}); {varName}++)");
                }
                var varNameStr = string.Join(",", varNameArr);
                sb.AppendLine($@"newIns[{varNameStr}] = CloneOperator.Clone(oldIns[{varNameStr}]);return newIns;");

                var multiArrTypeStr = string.Join(",", multiArrLenArr);
                sb.Insert(0, $"{info.DeclaringType} newIns = new {info.ElementTypeName}[{multiArrTypeStr}];");
            }
            else // 1维数组 || 多维锯齿数组
            {
                sb.AppendLine($@"
{info.DeclaringTypeName} newIns = 
    ({info.DeclaringTypeName})Array.CreateInstance(
    typeof({info.ElementTypeName})
    , oldIns.Length
);
for (int i = 0; i < newIns.Length; i++)
    newIns[i] = CloneOperator.Clone(oldIns[i]);
return newIns;
");
            }

            var tempBuilder = FastMethodOperator.New;

            var action = tempBuilder
                            .Using("DeepClone")
                            .Using("Natasha")
                            .Using(typeof(Array))
                            .OopName($"DeepClone{Guid.NewGuid().ToString().Replace("-", string.Empty)}")
                            .MethodName("Clone")
                            .Param(info.DeclaringType, "oldIns")
                            .MethodBody(sb.ToString())
                            .Return(info.DeclaringType)
                            .Complie();

            return action;
        }
    }
}
