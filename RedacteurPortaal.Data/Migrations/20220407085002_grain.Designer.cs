// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RedacteurPortaal.Data.Context;

#nullable disable

namespace RedacteurPortaal.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220407085002_grain")]
    partial class grain
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RedacteurPortaal.Data.Models.GrainReference", b =>
                {
                    b.Property<string>("GrainId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("GrainId");

                    b.ToTable("GrainReferences");
                });

            modelBuilder.Entity("RedacteurPortaal.Data.Models.PluginSettings", b =>
                {
                    b.Property<string>("PluginId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<string>("ApiKey")
                        .HasColumnType("text");

                    b.HasKey("PluginId");

                    b.ToTable("PluginSettings");
                });
#pragma warning restore 612, 618
        }
    }
}
