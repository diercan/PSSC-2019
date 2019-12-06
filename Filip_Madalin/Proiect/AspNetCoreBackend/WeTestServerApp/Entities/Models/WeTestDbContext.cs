using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entities.Models
{
    public partial class WeTestDbContext : DbContext
    {
        public WeTestDbContext()
        {
        }

        public WeTestDbContext(DbContextOptions<WeTestDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Functionality> Functionality { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<Testers> Testers { get; set; }
        public virtual DbSet<Tests> Tests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;database=mydb;user=root;password=Manelesarbesti21", x => x.ServerVersion("8.0.18-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Functionality>(entity =>
            {
                entity.ToTable("functionality");

                entity.Property(e => e.FunctionalityId)
                    .HasColumnName("functionality_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FunctionalityName)
                    .HasColumnName("functionality_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Projects>(entity =>
            {
                entity.HasKey(e => e.ProjectId)
                    .HasName("PRIMARY");

                entity.ToTable("projects");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("project_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasColumnName("project_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Testers>(entity =>
            {
                entity.HasKey(e => e.TesterId)
                    .HasName("PRIMARY");

                entity.ToTable("testers");

                entity.HasIndex(e => e.ProjectsProjectId)
                    .HasName("fk_Testers_Projects1_idx");

                entity.Property(e => e.TesterId)
                    .HasColumnName("tester_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProjectRole)
                    .HasColumnName("project_role")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ProjectsProjectId)
                    .HasColumnName("Projects_project_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TesterName)
                    .HasColumnName("tester_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.ProjectsProject)
                    .WithMany(p => p.Testers)
                    .HasForeignKey(d => d.ProjectsProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Testers_Projects1");
            });

            modelBuilder.Entity<Tests>(entity =>
            {
                entity.HasKey(e => e.TestId)
                    .HasName("PRIMARY");

                entity.ToTable("tests");

                entity.HasIndex(e => e.FunctionalityFunctionalityId)
                    .HasName("fk_Tests_Functionality1_idx");

                entity.HasIndex(e => e.ProjectsProjectId)
                    .HasName("fk_Tests_Projects_idx");

                entity.HasIndex(e => e.TestersTesterId)
                    .HasName("fk_Tests_Testers1_idx");

                entity.Property(e => e.TestId)
                    .HasColumnName("test_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasColumnType("date");

                entity.Property(e => e.Functionality)
                    .HasColumnName("functionality")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FunctionalityFunctionalityId)
                    .HasColumnName("Functionality_functionality_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProjectsProjectId)
                    .HasColumnName("Projects_project_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RevisionHistory)
                    .HasColumnName("revision_history")
                    .HasColumnType("varchar(24)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TestDescription)
                    .HasColumnName("test_description")
                    .HasColumnType("varchar(1024)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TestTitle)
                    .IsRequired()
                    .HasColumnName("test_title")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TesterId)
                    .HasColumnName("tester_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TestersTesterId)
                    .HasColumnName("Testers_tester_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.FunctionalityFunctionality)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.FunctionalityFunctionalityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Tests_Functionality1");

                entity.HasOne(d => d.ProjectsProject)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.ProjectsProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Tests_Projects");

                entity.HasOne(d => d.TestersTester)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.TestersTesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Tests_Testers1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
