using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeepingApp.Models;

namespace TimeKeepingApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TimeKeepingApp.Models.Shift> Shift { get; set; }
        public DbSet<TimeKeepingApp.Models.Employee> Employee { get; set; }
    }
}
