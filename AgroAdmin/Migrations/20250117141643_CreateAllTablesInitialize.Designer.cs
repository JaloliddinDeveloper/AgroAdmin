﻿// <auto-generated />
using System;
using AgroAdmin.Brokers.Storages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AgroAdmin.Migrations
{
    [DbContext(typeof(StorageBroker))]
    [Migration("20250117141643_CreateAllTablesInitialize")]
    partial class CreateAllTablesInitialize
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AgroAdmin.Models.Foundations.Contacts.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("AgroAdmin.Models.Foundations.Katalogs.Katalog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePicture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Katalogs");
                });

            modelBuilder.Entity("AgroAdmin.Models.Foundations.News.New", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("DesRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DesUz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescribtionRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescribtionUz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NewPicture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleUz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("News");
                });

            modelBuilder.Entity("AgroAdmin.Models.Foundations.Photos.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NameRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameUz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PictureUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("AgroAdmin.Models.Foundations.ProductOnes.ProductOne", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AdditionRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdditionUz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DesRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DesUz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescriptionRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescriptionUz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IconUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KimyoviySinfiRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KimyoviySinfiUz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreparatShakliRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreparatShakliUz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductPicture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductType")
                        .HasColumnType("int");

                    b.Property<string>("QadogiRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QadogiUz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TasirModdaRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TasirModdaUz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleUz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProductOnes");
                });

            modelBuilder.Entity("AgroAdmin.Models.Foundations.ProductOnes.TableOne", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BegonaQarshiRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BegonaQarshiUz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BirgaSarfRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BirgaSarfUz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EkinTuriRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EkinTuriUz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Onlsum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductOneId")
                        .HasColumnType("int");

                    b.Property<string>("SarfMeyoriRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SarfMeyoriUz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductOneId");

                    b.ToTable("TableOnes");
                });

            modelBuilder.Entity("AgroAdmin.Models.Foundations.ProductTwos.ProductTwo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DesRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DesUz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescriptionRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescriptionUZ")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameUz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductIcon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductPicture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductTwoType")
                        .HasColumnType("int");

                    b.Property<string>("SarfRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SarfUz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleUz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProductTwos");
                });

            modelBuilder.Entity("AgroAdmin.Models.Foundations.ProductTwos.TableTwo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Foiz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameUz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductTwoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductTwoId");

                    b.ToTable("TableTwos");
                });

            modelBuilder.Entity("AgroAdmin.Models.Foundations.ProductOnes.TableOne", b =>
                {
                    b.HasOne("AgroAdmin.Models.Foundations.ProductOnes.ProductOne", null)
                        .WithMany("TableOnes")
                        .HasForeignKey("ProductOneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AgroAdmin.Models.Foundations.ProductTwos.TableTwo", b =>
                {
                    b.HasOne("AgroAdmin.Models.Foundations.ProductTwos.ProductTwo", null)
                        .WithMany("TableTwos")
                        .HasForeignKey("ProductTwoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AgroAdmin.Models.Foundations.ProductOnes.ProductOne", b =>
                {
                    b.Navigation("TableOnes");
                });

            modelBuilder.Entity("AgroAdmin.Models.Foundations.ProductTwos.ProductTwo", b =>
                {
                    b.Navigation("TableTwos");
                });
#pragma warning restore 612, 618
        }
    }
}
