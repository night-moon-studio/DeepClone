using System;

namespace DeepClone.Model
{
    public interface ICloneTemplate
    {

        Delegate TypeRouter(BuilderInfo info);

        bool MatchType(Type type);

    }
}
