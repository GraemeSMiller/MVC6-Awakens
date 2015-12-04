using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using MVC6Awakens.Models;

namespace MVC6Awakens.Migrations.Domain
{
    [DbContext(typeof(DomainContext))]
    [Migration("20151204184727_Added WeaponOfChoice For Character")]
    partial class AddedWeaponOfChoiceForCharacter
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MVC6Awakens.Models.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<Guid>("PlanetId");

                    b.Property<string>("WeaponOfChoice");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("MVC6Awakens.Models.Planet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("MVC6Awakens.Models.Character", b =>
                {
                    b.HasOne("MVC6Awakens.Models.Planet")
                        .WithMany()
                        .HasForeignKey("PlanetId");
                });
        }
    }
}
