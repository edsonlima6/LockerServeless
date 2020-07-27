using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Domain.Entity;

namespace Domain.Services
{
    public class UserService
    {
         private AmazonDynamoDBClient client {get;set;}
         public List<ScanCondition> listCondition {get;set;}
         private User user {get;set;}
         public UserService(User _user)
         {
             user = _user;
         }
        
         public async Task<User> AddEntity()
         {
             try
             {
                 if (Validauser(user))
                 {
                    //user.Id = Guid.NewGuid().ToString();
                    //user.dateInsert = DateTime.Now;
                    using (client = new AmazonDynamoDBClient())
                    {
                        DynamoDBContext context = new DynamoDBContext(client);
                        //Table tbl = Table.LoadTable(client, "User", DynamoDBEntryConversion.V2);
                        await context.SaveAsync<User>(user);
                        return user;
                    }
                 }
                 
                 return null;
             }
             catch (System.Exception ex)
             {
                 throw ex;
             }
         }

         public async  Task<User> GetUserByEmail()
         {
             try
             {
                 if (!string.IsNullOrEmpty(user?.Email))
                 {
                     using (client = new AmazonDynamoDBClient())
                    {
                        DynamoDBContext context = new DynamoDBContext(client);
                        User _user = await context.LoadAsync<User>(user?.Email);
                        return _user;
                    }
                 }
                 return null;
             }
             catch (System.Exception ex)
             {                 
                 throw ex;
             }
         } 

         public async Task<bool> DeleteUserByEmail(string email)
         {
             try
             {
                 if (!string.IsNullOrEmpty(email))
                 {
                      using (client = new AmazonDynamoDBClient())
                    {
                        DynamoDBContext context = new DynamoDBContext(client);
                        //Table tbl = Table.LoadTable(client, "User", DynamoDBEntryConversion.V2);
                        await context.DeleteAsync<User>(email);
                        return true;
                    }
                 }
                    return false;
             }
             catch (System.Exception ex) 
             {
                 throw ex;
             }
         }

        public IEnumerable<User> GetUsersByProfile(string profile)
        {
            IEnumerable<User> listUsers = new List<User>();
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


                        listUsers = context.ScanAsync<User>(new List<ScanCondition>{ new ScanCondition("Name", ScanOperator.Equal, "Renata") })
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
         private bool Validauser(User _user)
         {
             try
             {
                 return ((_user != null) && !string.IsNullOrEmpty(_user.Email) && !string.IsNullOrEmpty(_user.Name) );
             }
             catch (System.Exception )
             {
                 return false;
             }
         }

    }
}