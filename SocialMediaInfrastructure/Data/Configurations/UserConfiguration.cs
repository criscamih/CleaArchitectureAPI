using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaCore.Entities;

namespace SocialMediaInfrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
                builder.ToTable("Usuario");
                builder.HasKey(e => e.IdUser)
                    .HasName("PK__Usuario__5B65BF973DBDAE3F");

                builder.Property(c => c.IdUser)
                    .HasColumnName("IdUsuario")
                    .ValueGeneratedNever();     

                builder.Property(e => e.Surnames)
                    .HasColumnName("Apellidos")
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                builder.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                builder.Property(e => e.BirthDay)
                    .HasColumnName("FechaNacimiento")
                    .HasColumnType("datetime");

                builder.Property(e => e.Names)
                    .HasColumnName("Nombres")
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                builder.Property(e => e.Phone)
                    .HasColumnName("Telefono")
                    .HasMaxLength(10)
                    .IsUnicode(false);
        }
    }
}