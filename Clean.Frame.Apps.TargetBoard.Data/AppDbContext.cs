using Microsoft.EntityFrameworkCore;
using Clean.Frame.Apps.TargetBoard.Core.Entities;
using Clean.Frame.Apps.TargetBoard.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Clean.Frame.Apps.TargetBoard.Data
{

    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Target> Targets { get; set; }
        public DbSet<MainBoard> MainBoards { get; set; }
        public DbSet<Aspect> Aspects { get; set; }
        public DbSet<Unit> Units { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TargetConfiguration());
            //modelBuilder.ApplyConfiguration(new AspectTargetConfiguration());
            modelBuilder.ApplyConfiguration(new AspectConfiguration());
            modelBuilder.ApplyConfiguration(new MainBoardConfiguration());
            modelBuilder.ApplyConfiguration(new UnitConfiguration());
        }
    }
}
