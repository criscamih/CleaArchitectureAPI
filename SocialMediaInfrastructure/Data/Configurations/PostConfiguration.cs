using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaCore.Entities;

namespace SocialMediaInfrastructure.Data.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
                builder.ToTable("Publicacion");
                builder.HasKey(e => e.Id)
                    .HasName("PK__Publicac__24F1B7D3232C42B7");

                builder.Property(c => c.Id)
                    .HasColumnName("IdPublicacion");

                builder.Property(c => c.IdUser)
                    .HasColumnName("IdUsuario");

                builder.Property(e => e.Description)
                    .HasColumnName("Descripcion")
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                builder.Property(e => e.Date)
                    .HasColumnName("Fecha")
                    .HasColumnType("datetime");

                builder.Property(e => e.Image)
                    .HasColumnName("Imagen")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                builder.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioPublicacion");
        }
    }
}