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

        public TestModel Self;

        public List<TestModel> SelfList;


        public class InnerClass
        {
            public string Name;
            public List<TestModel> SelfList;
        }
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
                Model2=new TestModel2 { A="A",B=1},
                Self = new TestModel {  B="E"},
                SelfList = new List<TestModel> { new TestModel { B = "l" } }

            };
            var testModel=CloneOperator.Clone(model);
            Assert.NotSame(model, testModel);
            Assert.Equal(model.B, testModel.B);
            Assert.NotNull(model.Model1);
            Assert.NotSame(model.Model2, testModel.Model2);
            Assert.Equal(model.Model2.A, testModel.Model2.A);
            Assert.Equal(model.Model2.B, testModel.Model2.B);
            Assert.NotSame(model.Self, testModel.Self);
            Assert.Equal(model.Self.B, testModel.Self.B);
            Assert.NotSame(model.SelfList, testModel.SelfList);
            Assert.Equal(model.SelfList[0].B, testModel.SelfList[0].B);
        }

        [Fact]
        public void ClassInnerTest()
        {
            TestModel.InnerClass model = new TestModel.InnerClass();
            model.Name = "abc";
            model.SelfList = new List<TestModel> { new TestModel { B = "l" } };
            var testModel = CloneOperator.Clone(model);
            Assert.NotSame(model, testModel);
            Assert.NotSame(model.SelfList, testModel.SelfList);
            Assert.Equal(model.SelfList[0].B, testModel.SelfList[0].B);
        }
    }


}
