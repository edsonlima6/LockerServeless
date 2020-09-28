using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using addSupplier;
using Domain.Entity;

namespace addSupplier.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestToUpperFunction()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            
            Supplier sup = new Supplier(){
                Name="SABESP", 
                registerNumber = "2000011"
            };
            var upperCase = function.FunctionHandler(sup, context);

            Assert.Equal("HELLO WORLD", "HELLO WORLD");
        }
    }
}
