﻿// <auto-generated />
using System;
using KarmaMarketplace.Infrastructure.EventSourcing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KarmaMarketplace.Migrations.EventStore
{
    [DbContext(typeof(EventStoreContext))]
    partial class EventStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("KarmaMarketplace.Infrastructure.EventSourcing.StoredEvent", b =>
                {
                    b.Property<Guid>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ByUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<string>("EventType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("EventId");

                    b.HasIndex("EventId")
                        .HasDatabaseName("IX_EventId");

                    b.HasIndex("EventType")
                        .HasDatabaseName("IX_EventType");

                    b.ToTable("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
