using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Amazon.DynamoDBv2.DataModel;

namespace Domain.Entity
{
    [DynamoDBTable("User")] 
    public class User
    {
        //   UserID?: number;
        //   UserName: string;
        //   Email: string;
        //   Password: string;
        [Required]
        [DynamoDBProperty("GuidId")]    
        public string Id { get; set; }

        [DynamoDBProperty("Name")]    
        public string Name { get; set; }

        [DynamoDBProperty("Email")]    
        [DynamoDBHashKey] 
        public string Email { get; set; }

        [DynamoDBProperty("Password")]    
        public string Password { get; set; }
        
        [DynamoDBProperty("dateCreated")] 
        public DateTime dateInsert { get; set; }

        [DynamoDBProperty("Profiles")] 
        public List<string> Profiles { get; set; }
    }
}