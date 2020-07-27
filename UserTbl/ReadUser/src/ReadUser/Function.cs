using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda.Core;
using Domain.Entity;
using Domain.Services;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ReadUser
{
    public class Function
    {
        private UserService userService;

        public async Task<User> FunctionHandler(User user, ILambdaContext context)
        {
           
            try
            {
                if (user != null && !string.IsNullOrEmpty(user.Email))
                {
                    userService = new UserService(user);
                    return await userService.GetUserByEmail();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                context.Logger.Log($"erro na busca do usuario { ex.Message }");
                return null;
            } 
        }
    }
}
