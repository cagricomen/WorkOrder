using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WorkOrder.Core.Entities;

namespace WorkOrder.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<WorkPLaceType> WorkPLaceTypes { get; set; }
        public DbSet<WorkPlace> WorkPlaces { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<CaseType> CaseTypes { get; set; }
        public DbSet<WorkOrders> WorkOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
