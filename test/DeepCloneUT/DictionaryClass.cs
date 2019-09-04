using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DeepCloneUT
{
    public class DictionaryClass
    {
        [Fact]
        public void DictionaryCloneTest()
        {
            var dict = new Dictionary<string, List<int>>() { { "1", new List<int> { 1} } };
            var dictNew = DeepClone.CloneOperator.Clone(dict);
            Assert.NotNull(dictNew);
            Assert.NotSame(dict, dictNew);
            Assert.True(dict.Count == dictNew.Count);
            Assert.NotNull(dictNew["1"]);
            Assert.NotSame(dict["1"], dictNew["1"]);

            var dictModel = new Dictionary<TestModel, int> { { new TestModel(),1 } };
            var dictModelNew = DeepClone.CloneOperator.Clone(dictModel);
            Assert.NotNull(dictModelNew); 
            Assert.NotSame(dictModel, dictModelNew);
            Assert.True(dictModel.Count == dictModelNew.Count);
            Assert.NotNull(dictModelNew.Keys);
            Assert.NotSame(dictModel.Keys.FirstOrDefault(), dictModelNew.Keys.FirstOrDefault());
        }
    }
}
