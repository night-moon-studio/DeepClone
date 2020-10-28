using DeepClone;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DeepCloneUT
{
    public class FluentSheetMetadata : SheetMetadata<FluentFieldMetadata>, IFluentMetadata
    {
        public FluentSheetMetadata()
        {

        }

    }

    internal interface IFluentMetadata
    {
    }

    public class FluentFieldMetadata : FieldMetadata, IFluentMetadata
    {
        public FluentFieldMetadata()
        {

        }
    }

    public class FieldMetadata
    {
        public string Name { get; set; }
        public string[] Header { get; set; }
        public object Value { get; set; }
        public bool Required { get; set; } = false;
    }

    public class SheetMetadata<TFieldMetadata> where TFieldMetadata : FieldMetadata
    {
        public string Name { get; set; }
        public int HeaderRow { get; set; } = 1;
        public int DataStartRow { get; set; } = 2;
        public bool Required { get; set; } = false;
        public Dictionary<string, TFieldMetadata> FieldMetadatas { get; set; } = new Dictionary<string, TFieldMetadata>();
    }

    public interface ITest
    {

    }


    public class TestFromMoney : Prepare
    {

        [Fact]
        public void FluentSheetMetadataCloneTest()
        {
            var metadata = new FluentSheetMetadata()
            {
                Name = "test",
                FieldMetadatas = new Dictionary<string, FluentFieldMetadata>()
                {
                    ["ApplicationName"] = new FluentFieldMetadata()
                    { Name = "ApplicationName", Header = new[] { "申请单位名称" }, Required = true },
                }
            };
            var cloneMetadata = CloneOperator.Clone(metadata);
            Assert.NotSame(metadata, cloneMetadata);

        }
    }
}
