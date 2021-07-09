﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PersonalPatientAccount.Models;

namespace PersonalPatientAccount.Migrations
{
    [DbContext(typeof(PatientContext))]
    [Migration("20210709113810_FullDb4")]
    partial class FullDb4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("DoctorShedule", b =>
                {
                    b.Property<int>("Doctorid")
                        .HasColumnType("integer");

                    b.Property<int>("Sheduleid")
                        .HasColumnType("integer");

                    b.HasKey("Doctorid", "Sheduleid");

                    b.HasIndex("Sheduleid");

                    b.ToTable("DoctorShedule");
                });

            modelBuilder.Entity("PersonalPatientAccount.Models.Appointment", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("date")
                        .HasColumnType("text");

                    b.Property<int>("docotorid")
                        .HasColumnType("integer");

                    b.Property<int>("patientid")
                        .HasColumnType("integer");

                    b.Property<string>("time")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("docotorid");

                    b.HasIndex("patientid");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("PersonalPatientAccount.Models.Doctor", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("Positionid")
                        .HasColumnType("integer");

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.Property<string>("office")
                        .HasColumnType("text");

                    b.Property<string>("patrynomic")
                        .HasColumnType("text");

                    b.Property<List<int>>("sheduleid")
                        .HasColumnType("integer[]");

                    b.Property<string>("surname")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("Positionid");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("PersonalPatientAccount.Models.Outpatient_card", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("date")
                        .HasColumnType("text");

                    b.Property<int>("docotorid")
                        .HasColumnType("integer");

                    b.Property<string>("formulation")
                        .HasColumnType("text");

                    b.Property<int>("patientid")
                        .HasColumnType("integer");

                    b.Property<string>("type")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("docotorid");

                    b.HasIndex("patientid");

                    b.ToTable("Outpatient_cards");
                });

            modelBuilder.Entity("PersonalPatientAccount.Models.Patient", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("dateofbirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("email")
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.Property<string>("numberpassport")
                        .HasColumnType("text");

                    b.Property<string>("numberpolicy")
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .HasColumnType("text");

                    b.Property<string>("patronymic")
                        .HasColumnType("text");

                    b.Property<string>("phone")
                        .HasColumnType("text");

                    b.Property<string>("photourl")
                        .HasColumnType("text");

                    b.Property<string>("surname")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("PersonalPatientAccount.Models.Position", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("PersonalPatientAccount.Models.Shedule", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("dateofweek")
                        .HasColumnType("integer");

                    b.Property<List<int>>("doctorid")
                        .HasColumnType("integer[]");

                    b.Property<string>("time")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Shedules");
                });

            modelBuilder.Entity("DoctorShedule", b =>
                {
                    b.HasOne("PersonalPatientAccount.Models.Doctor", null)
                        .WithMany()
                        .HasForeignKey("Doctorid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonalPatientAccount.Models.Shedule", null)
                        .WithMany()
                        .HasForeignKey("Sheduleid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PersonalPatientAccount.Models.Appointment", b =>
                {
                    b.HasOne("PersonalPatientAccount.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("docotorid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonalPatientAccount.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("patientid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("PersonalPatientAccount.Models.Doctor", b =>
                {
                    b.HasOne("PersonalPatientAccount.Models.Position", null)
                        .WithMany("Doctor")
                        .HasForeignKey("Positionid");
                });

            modelBuilder.Entity("PersonalPatientAccount.Models.Outpatient_card", b =>
                {
                    b.HasOne("PersonalPatientAccount.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("docotorid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonalPatientAccount.Models.Doctor", "Patient")
                        .WithMany()
                        .HasForeignKey("patientid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("PersonalPatientAccount.Models.Position", b =>
                {
                    b.Navigation("Doctor");
                });
#pragma warning restore 612, 618
        }
    }
}
