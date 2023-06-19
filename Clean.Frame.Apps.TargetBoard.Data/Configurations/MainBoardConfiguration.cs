using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Frame.Apps.TargetBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Frame.Apps.TargetBoard.Data.Configurations
{
    public class MainBoardConfiguration : IEntityTypeConfiguration<MainBoard>
    {
        public void Configure(EntityTypeBuilder<MainBoard> builder)
        {
            builder.HasKey(x => x.Id); 
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Year).IsRequired();
            builder.Property(x => x.Month).IsRequired();
            builder.HasOne(p => p.Unit)
                .WithMany(b => b.MainBoards)
                .HasForeignKey(x=>x.UnitId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Aspects).WithOne(x => x.MainBoard).HasForeignKey(c => c.MainBoardId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.ToTable("MainBoards");
        }
    }
}
