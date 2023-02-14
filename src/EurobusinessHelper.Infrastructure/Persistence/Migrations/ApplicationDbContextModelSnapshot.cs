﻿// <auto-generated />
using System;
using EurobusinessHelper.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EurobusinessHelper.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EurobusinessHelper.Domain.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Balance")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("EurobusinessHelper.Domain.Entities.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPasswordProtected")
                        .HasColumnType("bit");

                    b.Property<int>("MinimalBankTransferApprovals")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StartingAccountBalance")
                        .HasColumnType("int");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("EurobusinessHelper.Domain.Entities.Identity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(260)
                        .HasColumnType("nvarchar(260)")
                        .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                    b.Property<string>("FirstName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                    b.Property<string>("LastName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Identities");
                });

            modelBuilder.Entity("EurobusinessHelper.Domain.Entities.TransferRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("ApprovalCount")
                        .HasColumnType("int");

                    b.Property<bool>("Approved")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("GameId");

                    b.ToTable("TransferRequest");
                });

            modelBuilder.Entity("EurobusinessHelper.Domain.Entities.Account", b =>
                {
                    b.HasOne("EurobusinessHelper.Domain.Entities.Game", "Game")
                        .WithMany("Accounts")
                        .HasForeignKey("GameId");

                    b.HasOne("EurobusinessHelper.Domain.Entities.Identity", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.Navigation("Game");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("EurobusinessHelper.Domain.Entities.Game", b =>
                {
                    b.HasOne("EurobusinessHelper.Domain.Entities.Identity", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("EurobusinessHelper.Domain.Entities.TransferRequest", b =>
                {
                    b.HasOne("EurobusinessHelper.Domain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("EurobusinessHelper.Domain.Entities.Game", null)
                        .WithMany("TransferRequests")
                        .HasForeignKey("GameId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("EurobusinessHelper.Domain.Entities.Game", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("TransferRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
