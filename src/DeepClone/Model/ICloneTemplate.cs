using Natasha;
using System;

namespace DeepClone.Model
{
    public interface ICloneTemplate
    {

        Delegate TypeRouter(NBuildInfo info);

        bool MatchType(Type type);

    }
}
