﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using desafio_microondas.Infrastructure.Data;

#nullable disable

namespace desafio_microondas.Migrations
{
    [DbContext(typeof(MicrowaveDbContext))]
    [Migration("20241201224607_HeatingProgram")]
    partial class HeatingProgram
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.35");

            modelBuilder.Entity("desafio_microondas.Domain.Entities.HeatingProgram", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long?>("MicrowaveId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("Power")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Time")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MicrowaveId");

                    b.ToTable("HeatingProgram");
                });

            modelBuilder.Entity("desafio_microondas.Domain.Entities.Microwave", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("Power")
                        .HasColumnType("INTEGER");

                    b.Property<int>("State")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Time")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Microwave");
                });

            modelBuilder.Entity("desafio_microondas.Domain.Entities.HeatingProgram", b =>
                {
                    b.HasOne("desafio_microondas.Domain.Entities.Microwave", "Microwave")
                        .WithMany("HeatingPrograms")
                        .HasForeignKey("MicrowaveId");

                    b.Navigation("Microwave");
                });

            modelBuilder.Entity("desafio_microondas.Domain.Entities.Microwave", b =>
                {
                    b.Navigation("HeatingPrograms");
                });
#pragma warning restore 612, 618
        }
    }
}
