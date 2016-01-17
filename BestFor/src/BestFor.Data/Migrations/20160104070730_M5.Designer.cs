using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using BestFor.Data;

namespace BestFor.Data.Migrations
{
    [DbContext(typeof(BestDataContext))]
    [Migration("20160104070730_M5")]
    partial class M5
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

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("LeftWord")
                        .IsRequired();

                    b.Property<string>("Phrase")
                        .IsRequired();

                    b.Property<string>("RightWord")
                        .IsRequired();

                    b.HasKey("Id");
                });

            modelBuilder.Entity("BestFor.Domain.Entities.AnswerDescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.HasKey("Id");
                });

            modelBuilder.Entity("BestFor.Domain.Entities.BadWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Phrase")
                        .IsRequired();

                    b.HasKey("Id");
                });

            modelBuilder.Entity("BestFor.Domain.Entities.Suggestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Phrase")
                        .IsRequired();

                    b.HasKey("Id");
                });
        }
    }
}
