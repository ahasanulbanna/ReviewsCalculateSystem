using ReviewsCalculateSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewsCalculateSystem.Models
{
    public class ReviewDbContext : DbContext
    {
        public ReviewDbContext() : base("name=Con")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ReviewDbContext, ReviewsCalculateSystem.Models.Migrations.Configuration>("Con"));

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {         

        }
        private void FixEfProviderServicesProblem()
        {

            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Product>Products { get; set; }
        public DbSet<ReviewProduct> ReviewProducts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ReviewerTaskAsign> ReviewerTaskAsigns { get; set; }
    }
}
