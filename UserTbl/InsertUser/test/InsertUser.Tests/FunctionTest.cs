using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using InsertUser;
using Domain.Entity;

namespace InsertUser.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestToUpperFunction()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            
            User _user = new User();
            _user.Id = Guid.NewGuid().ToString();
            _user.Name = "Edson"; 
            _user.Email = "edsonlima6@gmail.com";
            _user.Password = "22025843";

            var upperCase = function.FunctionHandler(_user, context);

            Assert.Equal("Sucesso", upperCase);
        }
    }
}
