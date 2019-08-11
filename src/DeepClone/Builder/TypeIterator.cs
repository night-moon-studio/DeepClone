using System;
using System.Collections.Generic;
using System.Text;

namespace DeepClone.Builder
{

    #region 总体约束
    //类型分发方法约束
    public interface ICloneBuilder
    {
        void TypeRouter(Type type);
    }
    #endregion



    #region 集合构建模块
    //集合构建约束
    public interface ICollectionBuilder : ICloneBuilder
    {
        string OnceTypeCollectionBuilder();
        string OtherTypeCollectionBuilder();
    }


    //集合构建具体实现
    public class CloneCollectionBuilder : ICollectionBuilder
    {
        public string OnceTypeCollectionBuilder()
        {
            throw new NotImplementedException();
        }

        public string OtherTypeCollectionBuilder()
        {
            throw new NotImplementedException();
        }

        public void TypeRouter(Type type)
        {
            throw new NotImplementedException();
        }
    }
    #endregion



    #region 分发引擎
    //类型分发引擎
    public class TypeIterator<
        TCollectionBuidler,
        IDictionaryBuilder,
        TArrayBuiler,
        TStructBuilder,
        TClassBuilder...> : ICloneBuilder 
        where TCollectionBuidler : ICloneBuilder
        where IDictionaryBuilder : ICloneBuilder
        where ...
    {

        private ICollectionBuilder _collectionBuilder;


        public void TypeRouter(Type type)
        {

            if ( is Collection)
            {
                _collectionBuilder.TypeRouter(type);

            }
            else if ( is Dictionary)
            {
                ...
            }
            else
            {
                ...
            }
        }


    }
    #endregion

}
