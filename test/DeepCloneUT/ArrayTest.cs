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
            ////一维数组
            //var arrayInt = new int[] { 1,2,3,4,5,6};
            //var arrayIntNew = DeepClone.CloneOperator.Clone(arrayInt);
            //Assert.NotNull(arrayIntNew);
            //Assert.NotSame(arrayInt, arrayIntNew);
            //Assert.True(arrayInt.Length== arrayIntNew.Length);

            ////自定义类型数组
            //var arrayTestModel = new TestModel[] { new TestModel {
                
            //} };
            //var arrayTestModelNew = DeepClone.CloneOperator.Clone(arrayTestModel);
            //Assert.NotNull(arrayTestModelNew);
            //Assert.NotSame(arrayTestModel, arrayTestModelNew);
            //Assert.True(arrayTestModel.Length==arrayTestModelNew.Length);
            //Assert.NotSame(arrayTestModel[0], arrayTestModelNew[0]);

            //////多维数组
            //var muliteArray = new int[,] { { 1, 2, 3 }, { 1, 1, 1 } };
            //var muliteArrayNew = DeepClone.CloneOperator.Clone(muliteArray);
            //Assert.NotNull(muliteArrayNew);
            //Assert.NotSame(muliteArray, muliteArrayNew);
            //Assert.True(muliteArray.Length == muliteArrayNew.Length);

            //////多维数组
            //var threeArray = new int[,,] { { { 1,1},{1,2 },{ 2, 3 } }, { { 2,1}, { 3,2}, { 4,5} } };
            //var threeArrayNew = DeepClone.CloneOperator.Clone(threeArray);
            //Assert.NotNull(threeArrayNew);
            //Assert.NotSame(threeArray, threeArrayNew);
            //Assert.True(threeArray.Length == threeArrayNew.Length);

            ////交错数组
            //var staArrayInt = new int[][] { new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 } };
        
            //var staArrayIntNew = DeepClone.CloneOperator.Clone(staArrayInt);
            //Assert.NotNull(staArrayIntNew);
            //Assert.NotSame(staArrayInt, staArrayIntNew);
            //Assert.True(staArrayInt.Length == staArrayIntNew.Length);
            //Assert.NotSame(staArrayInt[0], staArrayIntNew[0]);
            //Assert.NotSame(staArrayInt[1], staArrayIntNew[1]);
        }
    }
}
