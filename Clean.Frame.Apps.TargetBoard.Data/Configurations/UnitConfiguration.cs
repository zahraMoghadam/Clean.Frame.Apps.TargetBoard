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
    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.HasKey(x => x.Id); 
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.ToTable("Units");
            builder.HasMany(p => p.MainBoards).WithOne(x=>x.Unit).HasForeignKey(c => c.UnitId)
                .OnDelete(DeleteBehavior.Restrict);




        }
    }
}
