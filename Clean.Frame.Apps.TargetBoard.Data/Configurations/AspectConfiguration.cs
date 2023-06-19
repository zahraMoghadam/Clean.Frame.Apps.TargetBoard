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
    public class AspectConfiguration : IEntityTypeConfiguration<Aspect>
    {
        public void Configure(EntityTypeBuilder<Aspect> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn().IsRequired();
            builder.Property(x => x.Weight).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Score).IsRequired().HasMaxLength(200); 
            builder.Property(x => x.Progress).IsRequired().HasMaxLength(200);
            builder.HasMany(p => p.Targets).WithOne(x => x.Aspect).HasForeignKey(c => c.AspectId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.ToTable("Aspects");
        }
    }
}
