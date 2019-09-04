using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

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

            ////多维数组
            //var muiltArray = new int[,] { { 1, 2, 3 }, { 1, 1, 1 } };
            

            //var muiltArrayNew = DeepClone.CloneOperator.Clone(muiltArray);


            ////交错数组
            //var staArrayInt = new int[][] { new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 } };
            //var staArrayIntNew = DeepClone.CloneOperator.Clone(staArrayInt);
            //Assert.NotNull(staArrayIntNew);
            //Assert.NotSame(staArrayInt, staArrayIntNew);
            //Assert.True(staArrayInt.Length== staArrayIntNew.Length);
            //Assert.NotSame(staArrayInt[0], staArrayIntNew[0]);
            //Assert.NotSame(staArrayInt[1], staArrayIntNew[1]);
        }
    }
}
