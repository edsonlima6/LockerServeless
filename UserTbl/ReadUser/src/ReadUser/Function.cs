using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda.Core;
using Domain.Entity;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ReadUser
{
    public class Function
    {
        private static AmazonDynamoDBClient client;
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> FunctionHandler(User user, ILambdaContext context)
        {
            try
            {
                await GetUser(user);
                string nome = "sucesso";
                return nome.ToUpper();
            }
            catch (System.Exception ex)
            {
                context.Logger.Log($"erro na busca do usuario {ex.Message}");
                return ex.Message;
            } 
        }

        private async Task GetUser(User user)
        {
            Document tes;
            try
            {
                 if (user != null && (!string.IsNullOrEmpty(user.Email) && !string.IsNullOrEmpty(user.Name)))
                {
                    GetItemOperationConfig config = new GetItemOperationConfig
                    {
                        //AttributesToGet = new List<string> { "Id", "ISBN", "Title", "Authors", "Price" },
                        ConsistentRead = true
                    };
                    using(client = new AmazonDynamoDBClient())
                    {
                        Table tbl = Table.LoadTable(client, "User", DynamoDBEntryConversion.V2);
                        DateTime twoWeeksAgoDate = DateTime.UtcNow - TimeSpan.FromDays(15);
                        tes = await tbl.GetItemAsync(user.Email, config);
                    }
                }
                

            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}
