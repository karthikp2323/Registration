using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DataContext
{
    public class DatabaseContext : DbContext
    {
        public class OptionsBuild
        {
            public OptionsBuild()
            {
                settings = new AppConfiguration();
                opsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
                opsBuilder.UseSqlServer(settings.ConnectionString);
                dbOption = opsBuilder.Options;
            }

            public DbContextOptionsBuilder<DatabaseContext> opsBuilder { get; set; }

            public DbContextOptions<DatabaseContext> dbOption { get; set; }

            private AppConfiguration settings { get; set; }
        }

        public static OptionsBuild ops = new OptionsBuild();

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        //DBSETS

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Address { get; set; }

    }
}
