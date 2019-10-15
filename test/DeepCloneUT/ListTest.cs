using DeepClone;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DeepCloneUT
{
    public class TestList
    {
        public readonly string A;

        public string B;

        public TestList1 List1;

        public TestList2 List2;
    }

    public class TestList1
    {
        private TestList1() { }

        public TestList1(string a) { }
    }

    public class TestList2
    {
        public string A;

        public int B { get; set; }
    }

    public class ListTest
    {
        [Fact]
        public void ClassCloneTest()
        {

            List<string> lli = new List<string>();
            lli.Add("123");
            lli.Add("456");
            var tt1 = FastCloneOperator.Clone(lli.ToArray());
            Assert.NotSame(lli, tt1);
            for (int i = 0; i < lli.Count; i++)
            {
                Assert.Equal(tt1[i], tt1[i]);
            }

            //List<TestList> kk = new List<TestList>();
            //TestList t1 = new TestList();
            //t1.B = "13";
            //TestList2 t2 = new TestList2();
            //t2.A = "234";
            //t1.List2 = t2;
            //kk.Add(t1);
            ////var ttt1 = CloneOperator.Clone(kk);

            //var testList2 = CloneOperator.Clone(t1); // 实体拷贝时出错

            //var tt = CloneOperator.Clone(lli);
            //Console.WriteLine(tt == null);
        }

        [Fact]
        public void ListCloneWithChildClassInstanceTest()
        {
            var list = new List<Parent>(){
                new Parent(){A="1",B="1"},
                new Child(){A="1",B=1}
            };
            var cloneList = FastCloneOperator.Clone(list);
            Assert.IsType<Child>(list[1]);
            Assert.IsType<Parent>(cloneList[1]);
            Assert.NotEqual(list[1].GetType(), cloneList[1].GetType());
            Assert.Equal(((Parent)list[1]).B, ((Parent)cloneList[1]).B);
            Assert.Equal(default, cloneList[1].B);
        }

        [Fact]
        public void CloneWithChildClassInstanceTest()
        {
            Parent p = new Child() { A = "1", B = 1 };
            Parent cloneP = FastCloneOperator.Clone(p);
            Assert.IsType<Child>(p);
            Assert.Equal(default, p.B);
            Assert.Equal(1, ((Child)p).B);
            Assert.IsType<Parent>(cloneP);
            Assert.NotSame(p, cloneP);
        }
    }
}
