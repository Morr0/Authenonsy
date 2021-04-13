﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Auth.Data.Repositories.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Auth.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210413222934_Initial2")]
    partial class Initial2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Auth.Core.Models.Application", b =>
                {
                    b.Property<string>("ClientId")
                        .HasColumnType("text");

                    b.Property<string>("ClientSecret")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatorId")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("FirstParty")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("RedirectUrl")
                        .HasColumnType("text");

                    b.Property<List<string>>("Scopes")
                        .HasColumnType("text[]");

                    b.Property<string>("WebsiteUrl")
                        .HasColumnType("text");

                    b.HasKey("ClientId");

                    b.ToTable("Application");
                });

            modelBuilder.Entity("Auth.Core.Models.Auth.UserApplicationAccess", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("ApplicationClientId")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<List<string>>("Scopes")
                        .HasColumnType("text[]");

                    b.HasKey("UserId", "ApplicationClientId");

                    b.ToTable("UserApplicationAccess");
                });

            modelBuilder.Entity("Auth.Core.Models.Auth.UserApplicationCodeRequest", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("ApplicationAccessApplicationClientId")
                        .HasColumnType("text");

                    b.Property<string>("ApplicationAccessUserId")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Code");

                    b.HasIndex("ApplicationAccessUserId", "ApplicationAccessApplicationClientId");

                    b.ToTable("UserApplicationCodeRequest");
                });

            modelBuilder.Entity("Auth.Core.Models.Auth.UserApplicationSession", b =>
                {
                    b.Property<string>("AccessToken")
                        .HasColumnType("text");

                    b.Property<string>("ApplicationAccessApplicationClientId")
                        .HasColumnType("text");

                    b.Property<string>("ApplicationAccessUserId")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("AccessToken");

                    b.HasIndex("ApplicationAccessUserId", "ApplicationAccessApplicationClientId");

                    b.ToTable("UserApplicationSession");
                });

            modelBuilder.Entity("Auth.Core.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Auth.Core.Models.Auth.UserApplicationCodeRequest", b =>
                {
                    b.HasOne("Auth.Core.Models.Auth.UserApplicationAccess", "ApplicationAccess")
                        .WithMany("UserApplicationCodeRequests")
                        .HasForeignKey("ApplicationAccessUserId", "ApplicationAccessApplicationClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ApplicationAccess");
                });

            modelBuilder.Entity("Auth.Core.Models.Auth.UserApplicationSession", b =>
                {
                    b.HasOne("Auth.Core.Models.Auth.UserApplicationAccess", "ApplicationAccess")
                        .WithMany("UserApplicationSessions")
                        .HasForeignKey("ApplicationAccessUserId", "ApplicationAccessApplicationClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ApplicationAccess");
                });

            modelBuilder.Entity("Auth.Core.Models.Auth.UserApplicationAccess", b =>
                {
                    b.Navigation("UserApplicationCodeRequests");

                    b.Navigation("UserApplicationSessions");
                });
#pragma warning restore 612, 618
        }
    }
}
