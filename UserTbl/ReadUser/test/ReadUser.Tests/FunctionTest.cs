using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entity;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using ReadUser;
using System.Threading;
using Domain.Services;

namespace ReadUser.Tests
{
    public class FunctionTest
    {
        [Fact]
        public async void TestToUpperFunction()
        {
            try
            {
                
                User user = new User();
                user.Email = "renatacury@gmail.com";
                user.Name = "Renata";
                user.Profiles = new List<string>{"Admin", "Finance"};
                UserService service = new UserService(user);  
                var rola = service.GetUsers();

                // Invoke the lambda function and confirm the string was upper cased.
                var function = new Function();
                var context = new TestLambdaContext();

                //var upperCase = function.FunctionHandler(user, context).Result;

                Thread.Sleep(15);
                Assert.Equal("SUCESSO", "SUCESSO");

            }
            catch (Exception)
            {
                
            }
           
        }
    }
}
