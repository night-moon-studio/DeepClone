using DeepClone;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DeepCloneUT
{
    public class TestModel
    {
        public readonly string A;
    }

    public class TestModel1
    {
        private TestModel1() { }

        public TestModel1(string a) { }
    }

    public class ClassTest
    {
        [Fact]
        public void ClassCloneTest()
        {
            CloneOperator.Clone(new TestModel());
            CloneOperator.Clone(new TestModel1(""));
        }
    }
}
