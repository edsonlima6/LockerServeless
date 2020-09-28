using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Domain.Entity;

namespace Domain.Services
{
    public class DynamoDbService
    {
         private AmazonDynamoDBClient client {get;set;}
         public List<ScanCondition> listCondition {get;set;}
         public Supplier supplier { get; set; }
         public DynamoDbService()
         {

         }

        public async Task<Response> AddEntity<T>(T user)
        {
            try
            {
                if (user != null)
                {
                    using (client = new AmazonDynamoDBClient())
                    {
                        DynamoDBContext context = new DynamoDBContext(client);
                        await context.SaveAsync<T>(user);
                    }
                    return new Response{
                        statusCode = 200, 
                        Message = "Data has been sucess created"
                    };
                }

                 return new Response{
                        statusCode = 404, 
                        Message = "User is empty"
                    };
            }
            catch (System.Exception ex)
            {
                 return new Response{
                        statusCode = 500, 
                        Message = ex.Message
                    };
            }
        }

         public async  Task<T> GetUserByEmail<T>(string email)
         {
             try
             {
                using (client = new AmazonDynamoDBClient())
                {
                    DynamoDBContext context = new DynamoDBContext(client);
                    T user = await context.LoadAsync<T>(email);
                    return user;
                }
             }
             catch (System.Exception ex)
             {              
                throw ex;
             }
         } 

         public async Task<Response> DeleteUserByEmail<T>(string email)
         {
             try
             {
                 if (!string.IsNullOrEmpty(email))
                 {
                      using (client = new AmazonDynamoDBClient())
                    {
                        DynamoDBContext context = new DynamoDBContext(client);
                        await context.DeleteAsync<T>(email);
                        return new Response{
                            statusCode = 200, 
                            Message = "Data has been deleted"
                        };
                    }
                 }
                    return new Response{
                        statusCode = 404, 
                        Message = "User is empty"
                    };
             }
             catch (System.Exception ex) 
             {
                 return new Response{
                        statusCode = 500, 
                        Message = ex.Message
                    };
             }
         }

        public IEnumerable<T> GetUsersByProfile<T>(string profile)
        {
            IEnumerable<T> listUsers = new List<T>();
            try
            {
                //"Profiles", QueryOperator.Equal, new List<AttributeValue> { new AttributeValue { S = "Renata" } }
                using (client = new AmazonDynamoDBClient())
                {
                    
                    using (DynamoDBContext context = new DynamoDBContext(client))
                    {
                        QueryFilter filter = new QueryFilter();
                        filter.AddCondition("Profiles", QueryOperator.Equal, profile);

                        QueryOperationConfig config = new QueryOperationConfig()
                        {
                            Limit = 2,
                            Select = SelectValues.SpecificAttributes,
                            AttributesToGet = new List<string> { "Name", "Email" },
                            ConsistentRead = true,
                            Filter = filter
                        };


                        listUsers = context.ScanAsync<T>(new List<ScanCondition>{ new ScanCondition("Name", ScanOperator.Equal, "Renata") })
                                       .GetRemainingAsync()
                                       .Result;
                    }
                    
                }


                
                return listUsers;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
    }
}