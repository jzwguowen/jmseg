using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jmseg.Models.Context
{
    public class MySQLContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public MySQLContext()
        {

        }

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
        {
            
        }
    }
}
