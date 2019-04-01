using System;
using Project.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Project.Data.Migrations
{
    [DbContext(typeof(ApplicationDataContext))]
    partial class ApplicationDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618

            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-preview1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", 
                    SqlServerValueGenerationStrategy.IdentityColumn);

            // Entity(String, Action<EntityTypeBuilder>) allows additional configuration
            // at the model level to be chained after configuration for the entity type.
            modelBuilder.Entity("Project.Models.Comment", b =>
            {
                b.Property<int>("Id")
                      .ValueGeneratedOnAdd()
                      .HasAnnotation("SqlServer:ValueGenerationStrategy",
                      SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("CommentID");

                b.Property<string>("Username");

                b.Property<string>("Content");
            });

            modelBuilder.Entity("Project.Models.Video", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                    SqlServerValueGenerationStrategy.IdentityColumn);

                // Each video can have many comments.
                b.HasMany("Project.Models.Comment")
                    .WithOne("Project.Models.Video")
                    .HasForeignKey("CommentID")
                    // If video is deleted, all child comments are too.
                    .OnDelete(DeleteBehavior.Cascade);

                b.Property<string>("VideoLink");

                b.Property<string>("Title");

                b.Property<string>("Description");
            });

#pragma warning restore 612, 618
        }
    }
}
