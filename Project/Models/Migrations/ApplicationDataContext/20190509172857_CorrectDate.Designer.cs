﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.Data;

namespace Project.Migrations
{
    [DbContext(typeof(ApplicationDataContext))]
    [Migration("20190509172857_CorrectDate")]
    partial class CorrectDate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Project.Models.CommentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("DatePosted");

                    b.Property<string>("Username");

                    b.Property<int?>("VideoModelId");

                    b.HasKey("Id");

                    b.HasIndex("VideoModelId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Project.Models.VideoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DatePosted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("05/09/2019 00:00:00");

                    b.Property<string>("Description");

                    b.Property<string>("Owner");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("VideoLink")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("Project.Models.CommentModel", b =>
                {
                    b.HasOne("Project.Models.VideoModel")
                        .WithMany("Comments")
                        .HasForeignKey("VideoModelId");
                });
#pragma warning restore 612, 618
        }
    }
}
