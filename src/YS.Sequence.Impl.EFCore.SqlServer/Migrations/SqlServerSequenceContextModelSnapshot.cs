﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YS.Sequence.Impl.EFCore.SqlServer;

namespace YS.Sequence.Impl.EFCore.SqlServer.Migrations
{
    [DbContext(typeof(SqlServerSequenceContext))]
    partial class SqlServerSequenceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("YS.Sequence.Impl.EFCore.SequenceInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("CurrentValue")
                        .HasColumnType("bigint");

                    b.Property<long?>("EndValue")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(128)")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.Property<long>("StartValue")
                        .HasColumnType("bigint");

                    b.Property<int>("Step")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Sequences");
                });
#pragma warning restore 612, 618
        }
    }
}
