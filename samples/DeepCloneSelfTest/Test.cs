using Natasha;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepCloneSelfTest
{
    public class Test
    {
        public string Name;
        public string Age
        {
            get;
        }

        public void Clone()
        {

            var action = NewMethod.Create<Func<Test, Test>>(builder=>
            builder
            .MethodBody(@"
                   
        
            "));


            



        }
    }
}
