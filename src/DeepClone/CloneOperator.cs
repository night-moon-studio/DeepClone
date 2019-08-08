using System;

namespace DeepClone
{
    public static class CloneOperator
    {

        public static T Clone<T>(T instance)
        {

            return CloneOperator<T>.Clone(instance);

        }

    }



    public static class CloneOperator<T>
    {

        private readonly static Func<T, T> _func;
        static CloneOperator() => _func = null;   //这里要对接CloneBuilder


        public static T Clone(T instance)
        {

            return _func(instance);

        }

    }

}
