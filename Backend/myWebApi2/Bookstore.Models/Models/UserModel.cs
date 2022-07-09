﻿using Bookstore.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int Roleid { get; set; }
    }
    public class RoleModel
    {
        public RoleModel() { }
        public RoleModel(Role role)
        {
            Id = role.Id;
            Name = role.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
       
    }
}