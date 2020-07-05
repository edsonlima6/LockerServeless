using System;
using System.IO;
using System.Text;

using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using System.Collections.Generic;
using System.Threading.Tasks;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace CreateuserFunction
{
    public class Function
    {
        private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        private static string tableName = "Music";
        // The sample uses the following id PK value to add book item.
        private static int sampleBookId = 3;

        public void FunctionHandler(DynamoDBEvent dynamoEvent, ILambdaContext context)
        {
            // context.Logger.LogLine($"Beginning to process {dynamoEvent.Records.Count} records...");

            // foreach (var record in dynamoEvent.Records)
            // {
            //     context.Logger.LogLine($"Event ID: {record.EventID}");
            //     context.Logger.LogLine($"Event Name: {record.EventName}");				
            // 	// TODO: Add business logic processing the record.Dynamodb object.
            // }   
            // context.Logger.LogLine("Stream processing complete.");


            try
            {
                CreateBookItem();
            }
            catch (System.Exception) 
            {
                throw;
            }
        }

        public static async void CreateBookItem()
        {
            try
            {
                using (client = new AmazonDynamoDBClient())
                {                    
                    Table user = Table.LoadTable(client, "User", DynamoDBEntryConversion.V2);
                    for (int i = 0; i < 2; i++)
                    {
                        Document _user = new Document();
                        _user["Id"] = i;
                        // _user["SongTitle"] = "SE É LOCO ";
                        // _user["AlbumTitle"] = "TOME";
                        // _user["Awards"] = "1";
                        await user.PutItemAsync(_user);
                    }

                }
                

                //var benga = TbUser.GetItemAsync("Felipe");
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            

        }
    }
}