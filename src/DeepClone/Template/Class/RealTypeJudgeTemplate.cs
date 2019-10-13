using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeepClone.Builder;
using DeepClone.Model;
using Natasha;
using Natasha.Operator;

namespace DeepClone.Template
{
    public class RealTypeJudgeTemplate : ICloneTemplate
    {
        private static readonly HashSet<Type> BuiltTypes;
        static RealTypeJudgeTemplate() => BuiltTypes = new HashSet<Type>();
        private readonly CloneClassTemplate _cloneClassTemplate = new CloneClassTemplate();
        public Delegate TypeRouter(NBuildInfo info)
        {
            BuiltTypes.Add(info.DeclaringType);
            var cloneMethod = _cloneClassTemplate.TypeRouter(info).Method;
            return FastMethodOperator.New
                 .Using("DeepClone")
                 .Param(info.DeclaringType, "old")
                 .MethodBody($"return old == default ? old : RealTypeCloneOperator.IsRealType(old) ? {cloneMethod.DeclaringType.Name}.NatashaDynamicMethod(old) : RealTypeCloneOperator.Clone(old);")
                 .Return(info.DeclaringType).Complie();
        }

        public bool MatchType(Type type) => _cloneClassTemplate.MatchType(type) &&
                                            !BuiltTypes.Contains(type) &&
                                            !BuiltTypes.Any(type.IsSubclassOf);
    }
}
