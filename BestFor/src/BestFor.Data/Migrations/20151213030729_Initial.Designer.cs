using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using BestFor.Data;

namespace BestFor.Data.Migrations
{
    [DbContext(typeof(BestDataContext))]
    [Migration("20151213030729_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BestFor.Domain.Entities.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<string>("Key");

                    b.Property<string>("LeftWord");

                    b.Property<string>("Phrase");

                    b.Property<string>("RightWord");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("BestFor.Domain.Entities.Suggestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Phrase");

                    b.HasKey("Id");
                });
        }
    }
}
