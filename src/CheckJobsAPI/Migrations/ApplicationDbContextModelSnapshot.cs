using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CheckJobsAPI.Data;

namespace CheckJobsAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CheckJobsAPI.Data.Place", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreationDate");

                    b.Property<bool>("IsActive");

                    b.Property<double?>("Latitude");

                    b.Property<double?>("Longitude");

                    b.Property<string>("Title");

                    b.Property<Guid?>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("CheckJobsAPI.Data.Report", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<Guid?>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("CheckJobsAPI.Data.Spot", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreationDate");

                    b.Property<int?>("Days");

                    b.Property<string>("Description");

                    b.Property<string>("Gender");

                    b.Property<double?>("HighSal");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDone");

                    b.Property<bool>("IsPM");

                    b.Property<double?>("LowSal");

                    b.Property<int?>("Minutes");

                    b.Property<string>("Phone");

                    b.Property<Guid>("PlaceID");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.HasIndex("PlaceID");

                    b.ToTable("Spots");
                });

            modelBuilder.Entity("CheckJobsAPI.Data.Stared", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("SpotID");

                    b.Property<Guid?>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("SpotID");

                    b.HasIndex("UserID");

                    b.ToTable("Stareds");
                });

            modelBuilder.Entity("CheckJobsAPI.Data.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Details");

                    b.Property<string>("Email");

                    b.Property<string>("FaceBookID");

                    b.Property<string>("ImagePath");

                    b.Property<bool>("IsPro");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CheckJobsAPI.Data.Place", b =>
                {
                    b.HasOne("CheckJobsAPI.Data.User", "User")
                        .WithMany("Places")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("CheckJobsAPI.Data.Report", b =>
                {
                    b.HasOne("CheckJobsAPI.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("CheckJobsAPI.Data.Spot", b =>
                {
                    b.HasOne("CheckJobsAPI.Data.Place", "Place")
                        .WithMany("Spots")
                        .HasForeignKey("PlaceID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CheckJobsAPI.Data.Stared", b =>
                {
                    b.HasOne("CheckJobsAPI.Data.Spot", "Spot")
                        .WithMany()
                        .HasForeignKey("SpotID");

                    b.HasOne("CheckJobsAPI.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });
        }
    }
}
