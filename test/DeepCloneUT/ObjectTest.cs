using DeepClone;
using NatashaUT.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DeepCloneUT
{
    public class ObjectTest
    {
        public class TestModel 
        {
            public int A { get; set; }
        }

        [Fact]
        public void ObjectCloneTest() 
        {
            //System.ValueType
            Object obj = 1;
            var objNew=FastObjectCloneOperator.Clone(obj);
            Assert.Same(obj, objNew);
            Assert.Equal(obj,objNew);
            //class
            object objA = new TestModel() { A = 2 };
            object objB = FastObjectCloneOperator.Clone(objA);
            Assert.NotNull(objB);
            Assert.NotSame(objA, objB);
            Assert.Equal(((TestModel)objA).A, ((TestModel)objB).A);
            //dict
            object dictA = new Dictionary<string, string>();
            object dictB = FastObjectCloneOperator.Clone(dictA);
            Assert.NotNull(dictB);
            Assert.NotSame(dictA, dictB);
            //list
            object listA = new List<string>();
            object listB = FastObjectCloneOperator.Clone(listA);
            Assert.NotNull(listB);
            Assert.NotSame(listA, listB);
            //arr
            object arrA = new string[0];
            object arrB = FastObjectCloneOperator.Clone(arrA);
            Assert.NotNull(arrB);
            Assert.NotSame(arrA, arrB);
        }
    }
}
