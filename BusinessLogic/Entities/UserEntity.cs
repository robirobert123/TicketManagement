﻿using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities
{
    public class UserEntity
    {
        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }
}
