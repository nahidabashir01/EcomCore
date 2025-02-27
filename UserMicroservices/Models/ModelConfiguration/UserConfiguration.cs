using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserMicroservice.Models.ModelConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.UserId);
            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(p => p.Email)
                  .IsRequired()
                  .HasMaxLength(255);
            builder.Property(p => p.Password)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(p => p.PhoneNumber)
               .IsRequired()
               .HasMaxLength(15);
        }

    }
}
