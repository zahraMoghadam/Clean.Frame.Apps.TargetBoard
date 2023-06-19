using Clean.Frame.Apps.TargetBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Frame.Apps.TargetBoard.Data.Configurations
{
    public class TargetConfiguration : IEntityTypeConfiguration<Target>
    {
        public void Configure(EntityTypeBuilder<Target> builder)
        {
            builder.HasKey(x => x.Id); 
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Score).IsRequired();
            builder.Property(x => x.Progress).IsRequired();
          
            builder.ToTable("Targets");
        }
    }
}
