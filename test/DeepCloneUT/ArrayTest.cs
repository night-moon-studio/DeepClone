using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DeepCloneUT
{
    public class ArrayTest
    {
        [Fact]
        public void ArrayCloneTest()
        {
            //一维数组
            var arrayInt = new int[] { 1,2,3,4,5,6};
            var arrayIntNew = DeepClone.CloneOperator.Clone(arrayInt);
            Assert.NotNull(arrayIntNew);
            Assert.NotSame(arrayInt, arrayIntNew);
            Assert.True(arrayInt.Length== arrayIntNew.Length);

            //自定义类型数组
            var arrayTestModel = new TestModel[] { new TestModel {
                
            } };
            var arrayTestModelNew = DeepClone.CloneOperator.Clone(arrayTestModel);
            Assert.NotNull(arrayTestModelNew);
            Assert.NotSame(arrayTestModel, arrayTestModelNew);
            Assert.True(arrayTestModel.Length==arrayTestModelNew.Length);
            Assert.NotSame(arrayTestModel[0], arrayTestModelNew[0]);

            //多为数组
            var muiltArrayInt = new int[][] { new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 } };

            var muiltArrayIntNew = DeepClone.CloneOperator.Clone(muiltArrayInt);
            Assert.NotNull(muiltArrayIntNew);
            Assert.NotSame(muiltArrayInt, muiltArrayIntNew);
            Assert.True(muiltArrayInt.Length== muiltArrayIntNew.Length);
            Assert.NotSame(muiltArrayInt[0], muiltArrayIntNew[0]);
            Assert.NotSame(muiltArrayInt[1], muiltArrayIntNew[1]);
            //锯齿数组
            var muilt1ArrayInt = new int[][] { new int[] { 1, 2, 3 }, new int[] { 1 } };
            var muilt1ArrayIntNew = DeepClone.CloneOperator.Clone(muilt1ArrayInt);

            Assert.NotNull(muilt1ArrayIntNew);
            Assert.NotSame(muilt1ArrayInt, muilt1ArrayIntNew);
            Assert.True(muilt1ArrayInt.Length == muilt1ArrayIntNew.Length);
            Assert.NotSame(muilt1ArrayInt[0], muilt1ArrayIntNew[0]);
            Assert.NotSame(muilt1ArrayInt[1], muilt1ArrayIntNew[1]);
        }
    }
}
