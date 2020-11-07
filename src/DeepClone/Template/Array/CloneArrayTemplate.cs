﻿using DeepClone.Model;
using Natasha;
using Natasha.CSharp;
using System;
using System.Linq;
using System.Text;

namespace DeepClone.Template
{
    public class CloneArrayTemplate : ICloneTemplate
    {
        internal readonly static int HashCode;
        static CloneArrayTemplate() => HashCode = typeof(CloneArrayTemplate).GetHashCode();

        public override int GetHashCode() => HashCode;

        public bool MatchType(Type type) => type.IsArray;


        public Delegate TypeRouter(NBuildInfo info)
        {

            string methodBody;

            // 简单类型
            // SimpleType
            if (info.ArrayBaseType.IsSimpleType())
            {

                methodBody = GenerateSimpleTypeClone(info);

            }
            else if (info.ArrayBaseType == typeof(object))
            {

                // 多维+复杂
                // MultiD+Complex
                if (info.ArrayDimensions > 1)
                {
                    methodBody = GenerateMultiDWithObjectClone(info);
                }
                // 1维+复杂 及 锯齿+复杂
                // 1D+Complex and Jagged+Complex
                else
                {
                    methodBody = GenerateJaggedWithObjectClone2(info);
                }

            }
            else
            {

                // 多维+复杂
                // MultiD+Complex
                if (info.ArrayDimensions > 1)
                {
                    methodBody = GenerateMultiDWithComplexClone(info);
                }
                // 1维+复杂 及 锯齿+复杂
                // 1D+Complex and Jagged+Complex
                else
                {
                    methodBody = GenerateJaggedWithComplexClone2(info);
                }

            }


            return FastMethodOperator.UseDomain(info.CurrentType.GetDomain())
                            .Using("DeepClone")
                            .Using(typeof(Array))
                            .Param(info.FatherType, "oldIns")
                            .Body(methodBody)
                            .Return(info.FatherType)
                            .Compile();
        }





        /// <summary>
        /// 1维+复杂 及 锯齿+复杂
        /// 1D+Complex 及 Jagged+Complex
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GenerateJaggedWithComplexClone2(NBuildInfo info)
        {
            var methodBody = $@"
                    if(oldIns==default) return default;
                    {info.CurrentTypeName} newIns = 
                        ({info.CurrentTypeName})Array.CreateInstance(
                                                        typeof({info.ElementTypeName})
                                                        , oldIns.Length
                                                        );
                    for (int i = 0; i < newIns.Length; i++)
                        newIns[i] = CloneOperator.Clone(oldIns[i]);
                    return newIns;
                ";
            return methodBody;
        }




        /// <summary>
        /// 1维+复杂 及 锯齿+复杂
        /// 1D+Complex 及 Jagged+Complex
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GenerateJaggedWithObjectClone2(NBuildInfo info)
        {
            var methodBody = $@"
                    if(oldIns==default) return default;
                    {info.CurrentTypeName} newIns = 
                        ({info.CurrentTypeName})Array.CreateInstance(
                                                        typeof({info.ElementTypeName})
                                                        , oldIns.Length
                                                        );
                    for (int i = 0; i < newIns.Length; i++)
                        newIns[i] = ObjectCloneOperator.Clone(oldIns[i]);
                    return newIns;
                ";
            return methodBody;
        }





        /// <summary>
        /// 多维+复杂
        /// MultiD+Complex
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GenerateMultiDWithComplexClone(NBuildInfo info)
        {
            var sb = new StringBuilder();
            var varNameArr = new string[info.ArrayDimensions];
            var multiArrLenArr = new string[info.ArrayDimensions];

            for (int i = 0; i < info.ArrayDimensions; i++)
            {
                var varName = $"_{i}";
                varNameArr[i] = varName;
                multiArrLenArr[i] = $"oldIns.GetLength({i})";

                sb.AppendLine($"for (int {varName} = 0; {varName} < oldIns.GetLength({i}); {varName}++)");
            }
            var varNameStr = string.Join(",", varNameArr);
            var multiArrTypeStr = string.Join(",", multiArrLenArr);

            var methodBody = $@"
                if(oldIns==default) return default;
                {info.CurrentTypeName} newIns = new {info.ElementTypeName}[{multiArrTypeStr}];
                {sb.ToString()}
                    newIns[{varNameStr}] = CloneOperator.Clone(oldIns[{varNameStr}]);
                return newIns;
            ";
            return methodBody;
        }




        /// <summary>
        /// 多维+复杂
        /// MultiD+Complex
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GenerateMultiDWithObjectClone(NBuildInfo info)
        {
            var sb = new StringBuilder();
            var varNameArr = new string[info.ArrayDimensions];
            var multiArrLenArr = new string[info.ArrayDimensions];

            for (int i = 0; i < info.ArrayDimensions; i++)
            {
                var varName = $"_{i}";
                varNameArr[i] = varName;
                multiArrLenArr[i] = $"oldIns.GetLength({i})";

                sb.AppendLine($"for (int {varName} = 0; {varName} < oldIns.GetLength({i}); {varName}++)");
            }
            var varNameStr = string.Join(",", varNameArr);
            var multiArrTypeStr = string.Join(",", multiArrLenArr);

            var methodBody = $@"
                if(oldIns==default) return default;
                {info.CurrentTypeName} newIns = new {info.ElementTypeName}[{multiArrTypeStr}];
                {sb.ToString()}
                    newIns[{varNameStr}] = ObjectCloneOperator.Clone(oldIns[{varNameStr}]);
                return newIns;
            ";
            return methodBody;
        }




        /// <summary>
        /// 简单类型
        /// SimpleType
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GenerateSimpleTypeClone(NBuildInfo info)
        {
            var sb = new StringBuilder();
            // 多维 & 1维
            // MultiD & 1D
            if (info.ArrayDimensions >= 1 && info.ArrayLayer == 1)
            {
                // 生成多维数组结构
                // Generate a multi array structure
                var multiArrLenArr = new string[info.ArrayDimensions];
                for (int i = 0; i < info.ArrayDimensions; i++)
                {
                    multiArrLenArr[i] = $"oldIns.GetLength({i})";
                }
                var multiArrTypeStr = string.Join(",", multiArrLenArr);

                sb.AppendLine($@"
                        if(oldIns==default) return default;
                        {info.CurrentTypeName} newIns = new {info.ElementTypeName}[{multiArrTypeStr}];
                        Array.Copy(oldIns, newIns, newIns.Length);
                        return newIns;
                    ");
            }
            // 锯齿
            // Jagged
            else
            {
                // 生成锯齿数组结构
                // Generating jagged array structure
                var arrItem = Enumerable.Repeat("[]", info.ArrayLayer - 1);
                sb.AppendLine($@"
                        if(oldIns==default) return default;
                        {info.CurrentTypeName} newIns = new {info.ArrayBaseTypeName}[oldIns.Length]{string.Join(string.Empty, arrItem)};
                        Array.Copy(oldIns, newIns, newIns.Length);
                        return newIns;
                    ");
            }
            return sb.ToString();
        }
    }
}
