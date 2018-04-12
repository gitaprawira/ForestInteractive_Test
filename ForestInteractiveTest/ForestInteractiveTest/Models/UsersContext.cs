using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ForestInteractiveTest.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext() : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Contact> Contact { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
    }
}