using ContacPro_Serveur.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContacPro_Serveur.DAL
{
    public class ContacProDBContext : DbContext
    {
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Professionnel> Professionnels { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Expertise> Expertises { get; set; }
        public DbSet<Prestation> Prestations { get; set; }
        public DbSet<Entente> Ententes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Tag_Prestation> Tags_Prestations { get; set; }
        public DbSet<ProExp> ProExps { get; set; }

        public ContacProDBContext(DbContextOptions<ContacProDBContext> options) : base(options) 
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=EFCore-SchoolDB;Trusted_Connection=True");
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ContacPro;Trusted_Connection=True");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.HasIndex(e => e.Courriel)
                    .HasName("UQ__Utilisat__049FB202EAE654B1")
                    .IsUnique();

                entity.Property(e => e.UtilisateurID).ValueGeneratedOnAdd();

                entity.Property(e => e.AddrPhoto)
                    .HasColumnName("addr_photo")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Courriel)
                    .IsRequired()
                    .HasColumnName("courriel")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mdp)
                    .IsRequired()
                    .HasColumnName("mdp")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasColumnName("nom")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Prenom)
                    .IsRequired()
                    .HasColumnName("prenom")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Client>(entity =>
            {

                entity.Property(e => e.Institution)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<Entente>(entity =>
            {
                entity.HasKey(e => e.EntenteID)
                    .HasName("PK__Entente__1CC5D80D0E446E07");

                entity.Property(e => e.EntenteID)
                    .HasColumnName("Id_entente")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ApprouveClient).HasColumnName("Approuve_client");

                entity.Property(e => e.ApprouvePro).HasColumnName("Approuve_pro");

                
                entity.HasOne(e => e.Client)
                    .WithMany(e => e.Ententes)
                    .HasForeignKey(e => e.Client_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_entente_client");

                entity.HasOne(e => e.Pro)
                    .WithMany(e => e.Ententes)
                    .HasForeignKey(e => e.Pro_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_entente_pro");
            });

            modelBuilder.Entity<Expertise>(entity =>
            {
                entity.HasKey(e => e.ExpertiseID)
                    .HasName("PK__Expertis__9F8DB893847A1F2A");

                entity.Property(e => e.ExpertiseID)
                    .HasColumnName("Id_expertise")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Valeur)
                    .IsRequired()
                    .HasColumnName("valeur")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(m => m.MessageID)
                    .HasName("PK__Messages__0A1524C05E7B23AE");

                entity.Property(m => m.MessageID)
                    .HasColumnName("MessageID")
                    .ValueGeneratedOnAdd();

                entity.Property(m => m.Contenu)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Titre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(m => m.Auteur)
                    .WithMany(u => u.Messages_Envoyes)
                    .HasForeignKey(m => m.Auteur_Id)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(m => m.Destinataire)
                    .WithMany(u => u.Messages_Recus)
                    .HasForeignKey(m => m.Destinataire_Id)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(m => m.Entente_Associee)
                    .WithMany(e => e.Messages)
                    .HasForeignKey(d => d.Entente_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_message_entente");
            });

           
            modelBuilder.Entity<Prestation>(entity =>
            {
                entity.HasKey(p => p.PrestationID)
                    .HasName("PK__Prestati__52B08CDD15B0DEDA");

                entity.Property(p => p.PrestationID)
                    .HasColumnName("PrestationID")
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.Date).HasColumnType("smalldatetime");

                entity.Property(p => p.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(p => p.Lieu)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(p => p.Retribution).HasColumnType("decimal(6, 2)");

                entity.Property(p => p.Titre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(p => p.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(p => p.Beneficiaire)
                    .WithMany(c => c.Prestations)
                    .HasForeignKey(p => p.Client_Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_prest_id_client");

                entity.HasOne(p => p.Prestataire)
                    .WithMany(p => p.Prestations)
                    .HasForeignKey(p => p.Pro_Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_prest_id_pro");
            });

            modelBuilder.Entity<ProExp>(entity =>
            {
                entity.HasKey(pe => new { pe.ExpertiseID, pe.UtilisateurID });

                entity.HasOne(pe => pe.Expertise)
                    .WithMany(e => e.Professionnels)
                    .HasForeignKey(pe => pe.ExpertiseID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_pro_exp_Id_exp");

                entity.HasOne(pe => pe.Professionnel)
                    .WithMany(p => p.Expertises)
                    .HasForeignKey(pe => pe.UtilisateurID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_pro_exp_Id_pro");
            });

            modelBuilder.Entity<Professionnel>(entity =>
            {
                entity.Property(e => e.AddrCv)
                    .HasColumnName("addr_cv")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(t => t.TagID)
                    .HasName("PK__Tags__2D1436F3C199AA3D");

                entity.Property(t => t.TagID)
                    .HasColumnName("Id_tag")
                    .ValueGeneratedOnAdd();

                entity.Property(t => t.Valeur)
                    .IsRequired()
                    .HasColumnName("valeur")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tag_Prestation>(entity =>
            {
                entity.HasKey(tp => new { tp.PrestationID, tp.TagID });

                entity.HasOne(tp => tp.Prestation)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(tp => tp.PrestationID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tags_prest");

                entity.HasOne(tp => tp.Tag)
                    .WithMany(t => t.Prestations)
                    .HasForeignKey(tp => tp.TagID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tags");
            });

        }

    }
}
