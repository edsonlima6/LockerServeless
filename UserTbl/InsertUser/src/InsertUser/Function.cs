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

namespace InsertUser
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
        public string FunctionHandler(User user, ILambdaContext context)
        {
            try
            {
                return SaveUser(user).Result;
            }
            catch (System.Exception ex)
            {
                context.Logger.LogLine($"Erro na chamada - {ex.Message}");
                return ex.Message;
            }
            
        }

        private async Task<string> SaveUser(User user1)
        {
            try
            {
                 if (user1 != null)
                {
                    bool validProperties = (string.IsNullOrEmpty(user1.Email) || string.IsNullOrEmpty(user1.Name) || string.IsNullOrEmpty(user1.Password));
                    if (validProperties)
                    {
                        return "Email|Name|Password fields are required";
                    }

                    Document doc = new Document();
                    doc["GuidId"] = Guid.NewGuid().ToString();
                    doc["Name"] = user1.Name;
                    doc["Email"] = user1.Email;
                    doc["Password"] = user1.Password;

                    using (client = new AmazonDynamoDBClient())
                    {
                        Table tbluser = Table.LoadTable(client,"User", DynamoDBEntryConversion.V2);
                        Document ret = await tbluser.PutItemAsync(doc);
                    }
                    return "Sucesso";
                }
                return "Usuario não definido";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
