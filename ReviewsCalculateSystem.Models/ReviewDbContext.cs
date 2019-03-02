using ReviewsCalculateSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewsCalculateSystem.Models
{
    public class ReviewDbContext : DbContext
    {
        public ReviewDbContext() : base("name=Con")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ReviewDbContext, ReviewsCalculateSystem.Models.Migrations.Configuration>("Con"));
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        private void FixEfProviderServicesProblem()
        {

            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Product>Products { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ReviewerTaskAsign> ReviewerTaskAsigns { get; set; }
        public DbSet<Registration> Registrations { get; set; }
    }
}
