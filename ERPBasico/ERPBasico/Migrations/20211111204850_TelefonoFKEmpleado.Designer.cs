﻿// <auto-generated />
using System;
using ERPBasico.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ERPBasico.Migrations
{
    [DbContext(typeof(ModelContext))]
    [Migration("20211111204850_TelefonoFKEmpleado")]
    partial class TelefonoFKEmpleado
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("ERPBasico.Models.CentroDeCosto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("TEXT");

                    b.Property<long>("GerenciaId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("MontoMaximo")
                        .HasColumnType("REAL");

                    b.Property<string>("Nombre")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GerenciaId");

                    b.ToTable("CentrosDeCosto");
                });

            modelBuilder.Entity("ERPBasico.Models.Empleado", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<int>("Dni")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmpleadoActivo")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("TEXT");

                    b.Property<long?>("FotoId")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("Legajo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("ObraSocial")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Password")
                        .HasColumnType("BLOB");

                    b.Property<int>("Rol")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FotoId");

                    b.ToTable("Empleados");
                });

            modelBuilder.Entity("ERPBasico.Models.Empresa", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailContacto")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("TEXT");

                    b.Property<long?>("LogoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long?>("TelefonoContactoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("rubro")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("LogoId");

                    b.HasIndex("TelefonoContactoId");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("ERPBasico.Models.Gasto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("CentroDeCostoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .HasColumnType("TEXT");

                    b.Property<long>("EmpleadoId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("TEXT");

                    b.Property<double>("Monto")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("CentroDeCostoId");

                    b.HasIndex("EmpleadoId");

                    b.ToTable("Gastos");
                });

            modelBuilder.Entity("ERPBasico.Models.Gerencia", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long?>("DireccionId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("EmpresaId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("EsGerenciaGeneral")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DireccionId");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Gerencias");
                });

            modelBuilder.Entity("ERPBasico.Models.Imagen", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("imagen")
                        .HasColumnType("BLOB");

                    b.Property<string>("nombreImagen")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Imagenes");
                });

            modelBuilder.Entity("ERPBasico.Models.Posicion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("EmpleadoId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("TEXT");

                    b.Property<long>("GerenciaId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("JefeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("descripcion")
                        .HasColumnType("TEXT");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("sueldo")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("EmpleadoId");

                    b.HasIndex("GerenciaId")
                        .IsUnique();

                    b.HasIndex("JefeId");

                    b.ToTable("Posiciones");
                });

            modelBuilder.Entity("ERPBasico.Models.Telefono", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("EmpleadoId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("TEXT");

                    b.Property<int>("Numero")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Tipo")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EmpleadoId");

                    b.ToTable("Telefonos");
                });

            modelBuilder.Entity("ERPBasico.Models.CentroDeCosto", b =>
                {
                    b.HasOne("ERPBasico.Models.Gerencia", "Gerencia")
                        .WithMany()
                        .HasForeignKey("GerenciaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gerencia");
                });

            modelBuilder.Entity("ERPBasico.Models.Empleado", b =>
                {
                    b.HasOne("ERPBasico.Models.Imagen", "Foto")
                        .WithMany()
                        .HasForeignKey("FotoId");

                    b.Navigation("Foto");
                });

            modelBuilder.Entity("ERPBasico.Models.Empresa", b =>
                {
                    b.HasOne("ERPBasico.Models.Imagen", "Logo")
                        .WithMany()
                        .HasForeignKey("LogoId");

                    b.HasOne("ERPBasico.Models.Telefono", "TelefonoContacto")
                        .WithMany()
                        .HasForeignKey("TelefonoContactoId");

                    b.Navigation("Logo");

                    b.Navigation("TelefonoContacto");
                });

            modelBuilder.Entity("ERPBasico.Models.Gasto", b =>
                {
                    b.HasOne("ERPBasico.Models.CentroDeCosto", "CentroDeCosto")
                        .WithMany("Gastos")
                        .HasForeignKey("CentroDeCostoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ERPBasico.Models.Empleado", "Empleado")
                        .WithMany()
                        .HasForeignKey("EmpleadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CentroDeCosto");

                    b.Navigation("Empleado");
                });

            modelBuilder.Entity("ERPBasico.Models.Gerencia", b =>
                {
                    b.HasOne("ERPBasico.Models.Gerencia", "Direccion")
                        .WithMany()
                        .HasForeignKey("DireccionId");

                    b.HasOne("ERPBasico.Models.Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Direccion");

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("ERPBasico.Models.Posicion", b =>
                {
                    b.HasOne("ERPBasico.Models.Empleado", "empleado")
                        .WithMany()
                        .HasForeignKey("EmpleadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ERPBasico.Models.Gerencia", "gerencia")
                        .WithOne("Responsable")
                        .HasForeignKey("ERPBasico.Models.Posicion", "GerenciaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ERPBasico.Models.Posicion", "Jefe")
                        .WithMany()
                        .HasForeignKey("JefeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("empleado");

                    b.Navigation("gerencia");

                    b.Navigation("Jefe");
                });

            modelBuilder.Entity("ERPBasico.Models.Telefono", b =>
                {
                    b.HasOne("ERPBasico.Models.Empleado", "Empleado")
                        .WithMany("Telefonos")
                        .HasForeignKey("EmpleadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empleado");
                });

            modelBuilder.Entity("ERPBasico.Models.CentroDeCosto", b =>
                {
                    b.Navigation("Gastos");
                });

            modelBuilder.Entity("ERPBasico.Models.Empleado", b =>
                {
                    b.Navigation("Telefonos");
                });

            modelBuilder.Entity("ERPBasico.Models.Gerencia", b =>
                {
                    b.Navigation("Responsable");
                });
#pragma warning restore 612, 618
        }
    }
}
