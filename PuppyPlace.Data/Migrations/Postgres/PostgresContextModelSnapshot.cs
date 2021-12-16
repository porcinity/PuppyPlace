﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PuppyPlace.Data;

#nullable disable

namespace PuppyPlace.Data.Migrations.Postgres
{
    [DbContext(typeof(PuppyPlaceContext.PostgresContext))]
    partial class PostgresContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PuppyPlace.Domain.Dog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Dogs");
                });

            modelBuilder.Entity("PuppyPlace.Domain.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("PuppyPlace.Domain.Dog", b =>
                {
                    b.HasOne("PuppyPlace.Domain.Person", "Owner")
                        .WithMany("Dogs")
                        .HasForeignKey("OwnerId");

                    b.OwnsOne("PuppyPlace.Domain.Value_Objects.DogValueObjects.DogAge", "_age", b1 =>
                        {
                            b1.Property<Guid>("DogId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("Age");

                            b1.HasKey("DogId");

                            b1.ToTable("Dogs");

                            b1.WithOwner()
                                .HasForeignKey("DogId");
                        });

                    b.OwnsOne("PuppyPlace.Domain.Value_Objects.DogValueObjects.DogBreed", "_breed", b1 =>
                        {
                            b1.Property<Guid>("DogId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Breed");

                            b1.HasKey("DogId");

                            b1.ToTable("Dogs");

                            b1.WithOwner()
                                .HasForeignKey("DogId");
                        });

                    b.OwnsOne("PuppyPlace.Domain.Value_Objects.DogValueObjects.DogName", "_name", b1 =>
                        {
                            b1.Property<Guid>("DogId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Name");

                            b1.HasKey("DogId");

                            b1.ToTable("Dogs");

                            b1.WithOwner()
                                .HasForeignKey("DogId");
                        });

                    b.Navigation("Owner");

                    b.Navigation("_age");

                    b.Navigation("_breed");

                    b.Navigation("_name");
                });

            modelBuilder.Entity("PuppyPlace.Domain.Person", b =>
                {
                    b.OwnsOne("PuppyPlace.Domain.Value_Objects.PersonValueObjects.PersonAge", "_age", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("Age");

                            b1.HasKey("PersonId");

                            b1.ToTable("Persons");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.OwnsOne("PuppyPlace.Domain.Value_Objects.PersonValueObjects.PersonName", "_name", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Name");

                            b1.HasKey("PersonId");

                            b1.ToTable("Persons");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.Navigation("_age");

                    b.Navigation("_name");
                });

            modelBuilder.Entity("PuppyPlace.Domain.Person", b =>
                {
                    b.Navigation("Dogs");
                });
#pragma warning restore 612, 618
        }
    }
}
