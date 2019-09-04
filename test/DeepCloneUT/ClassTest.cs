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

        public string B;

        public TestModel1 Model1;

        public TestModel2 Model2;
    }

    public class TestModel1
    {
        public TestModel1() { }

        public TestModel1(string a) { }
    }

    public class TestModel2
    {
        public string A;

        public int B { get; set; }
    }

    public class ClassTest
    {
        [Fact]
        public void ClassCloneTest()
        {
            var model = new TestModel
            {
                B = "B",
                Model1 = new TestModel1("Model1"),
                Model2=new TestModel2 { A="A",B=1}
            };
            var testModel=CloneOperator.Clone(model);
            Assert.NotSame(model, testModel);
            Assert.Equal(model.B, testModel.B);
            Assert.NotNull(model.Model1);
            Assert.NotSame(model.Model2, testModel.Model2);
            Assert.Equal(model.Model2.A, testModel.Model2.A);
            Assert.Equal(model.Model2.B, testModel.Model2.B);
        }
    }
}
