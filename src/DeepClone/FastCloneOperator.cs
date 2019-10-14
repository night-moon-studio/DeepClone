using DeepClone.Builder;
using System;

namespace DeepClone
{
    public static class FastCloneOperator
    {

        public static T Clone<T>(T instance)
        {

            return FastCloneOperator<T>.Clone(instance);

        }

    }



    public static class FastCloneOperator<T>
    {
        public readonly static Func<T, T> Clone;
        static FastCloneOperator()
        {
            Clone = FastCloneBuilder<T>.Create();   //这里要对接CloneBuilder
        }

    }

}
