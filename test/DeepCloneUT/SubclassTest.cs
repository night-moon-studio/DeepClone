using System.Collections.Generic;
using DeepClone;
using Xunit;

namespace DeepCloneUT
{
    public class Parent
    {
        public string A { get; set; }
        public string B { get; set; }
    }

    public class Child<T> : Parent
    {
        public new int B { get; set; }
    }
    public class Child : Parent
    {
        public new int B { get; set; }
    }
    public class Child2 : Parent
    {
        public new int B { get; set; }
    }

    public class Child3 : Child
    {
        public new string B { get; set; }
    }

    public class SupportSubclassTest : Prepare
    {
        [Fact]
        public void ListCloneWithChildClassInstanceTest()
        {
            var list = new List<Parent>(){
                new Parent(){A="1",B="1"},
                new Child(){A="1",B=1},
                new Child2(){A="1",B=10},
                new Child2(){A="10",B=100},
                new Child(){A="100",B=1},
                new Child3(){A="1",B="2"}
            };
            var cloneList = CloneOperator.Clone(list);
            Assert.IsType<Parent>(cloneList[0]);
            Assert.IsType<Child>(cloneList[1]);
            Assert.IsType<Child2>(cloneList[2]);
            Assert.IsType<Child2>(cloneList[3]);
            Assert.IsType<Child>(cloneList[4]);
            Assert.IsType<Child3>(cloneList[5]);
            Assert.Equal(list[1].GetType(), cloneList[1].GetType());
            Assert.Equal(((Child)list[1]).B, ((Child)cloneList[1]).B);
            Assert.Equal(default, cloneList[1].B);
            Assert.Equal(1, ((Child)cloneList[1]).B);
            Assert.Equal(10, ((Child2)cloneList[2]).B);
            Assert.Equal(100, ((Child2)cloneList[3]).B);
            Assert.Equal("10", ((Child2)cloneList[3]).A);
            Assert.Equal("100", ((Child)cloneList[4]).A);
            Assert.Equal("1", ((Child3)cloneList[5]).A);
            Assert.Equal("2", ((Child3)cloneList[5]).B);
        }
        [Fact]
        public void ListCloneWithChildGenericClassInstanceTest()
        {
            var list = new List<Parent>(){
                new Parent(){A="1",B="1"},
                new Child<string>(){A="1",B=1},
            };
            var cloneList = CloneOperator.Clone(list);
            Assert.IsType<Child<string>>(list[1]);
            Assert.IsType<Child<string>>(cloneList[1]);
            Assert.Equal(list[1].GetType(), cloneList[1].GetType());
            Assert.Equal(((Parent)list[1]).B, ((Parent)cloneList[1]).B);
            Assert.Equal(default, cloneList[1].B);
            Assert.Equal(1, ((Child<string>)cloneList[1]).B);
        }

        [Fact]

        public void CloneWithChildClassInstanceTest()
        {
            Parent p = new Child() { A = "1", B = 1 };
            Parent cloneP = CloneOperator.Clone(p);
            Assert.IsType<Child>(p);
            Assert.Equal(default, p.B);
            Assert.Equal(1, ((Child)cloneP).B);
            Assert.IsType<Child>(cloneP);
            Assert.NotSame(p, cloneP);
        }

    }
}