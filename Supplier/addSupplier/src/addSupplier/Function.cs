using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Domain.Entity;
using Domain.Services;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace addSupplier
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task<Response> FunctionHandler(Supplier sup, ILambdaContext context)
        {
            Response res = new Response();
            try
            {
                DynamoDbService dynDB = new DynamoDbService();
                return dynDB.AddEntity<Supplier>(sup);                 
            }
            catch (System.Exception ex)
            {
                res.Message = ex.Message;
                return Task.FromResult(res);
            }
        }
    }
}
