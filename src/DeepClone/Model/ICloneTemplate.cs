using System;

namespace DeepClone.Model
{
    public interface ICloneTemplate
    {

        Delegate TypeRouter(Type type);

        bool MatchType(Type type);

    }
}
