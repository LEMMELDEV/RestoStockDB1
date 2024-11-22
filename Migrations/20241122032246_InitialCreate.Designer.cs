﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestoStockDB1.Data;

#nullable disable

namespace RestoStockDB1.Migrations
{
    [DbContext(typeof(RestoStockContext))]
    [Migration("20241122032246_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RestoStockDB1.Models.DetallePlato", b =>
                {
                    b.Property<int>("DetallePlatoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DetallePlatoId"));

                    b.Property<decimal>("Cantidad")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PlatoId")
                        .HasColumnType("int");

                    b.Property<int>("ingredienteId")
                        .HasColumnType("int");

                    b.HasKey("DetallePlatoId");

                    b.HasIndex("PlatoId");

                    b.HasIndex("ingredienteId");

                    b.ToTable("DetallesPlatos");
                });

            modelBuilder.Entity("RestoStockDB1.Models.Ingrediente", b =>
                {
                    b.Property<int>("IngredienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredienteId"));

                    b.Property<decimal>("CantidadDisponible")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UnidadMedida")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IngredienteId");

                    b.ToTable("Ingredientes");
                });

            modelBuilder.Entity("RestoStockDB1.Models.Pedido", b =>
                {
                    b.Property<int>("PedidoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PedidoId"));

                    b.Property<DateTime>("FechaPedido")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProovedorId")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PedidoId");

                    b.HasIndex("ProovedorId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("RestoStockDB1.Models.Plato", b =>
                {
                    b.Property<int>("PlatoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlatoId"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PrecioVenta")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PlatoId");

                    b.ToTable("Platos");
                });

            modelBuilder.Entity("RestoStockDB1.Models.Proovedor", b =>
                {
                    b.Property<int>("ProovedorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProovedorId"));

                    b.Property<string>("Contacto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreEmpresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProovedorId");

                    b.ToTable("Proovedores");
                });

            modelBuilder.Entity("RestoStockDB1.Models.DetallePlato", b =>
                {
                    b.HasOne("RestoStockDB1.Models.Plato", "Plato")
                        .WithMany("DetallesPlatos")
                        .HasForeignKey("PlatoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestoStockDB1.Models.Ingrediente", "Ingrediente")
                        .WithMany("DetallesPlatos")
                        .HasForeignKey("ingredienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingrediente");

                    b.Navigation("Plato");
                });

            modelBuilder.Entity("RestoStockDB1.Models.Pedido", b =>
                {
                    b.HasOne("RestoStockDB1.Models.Proovedor", "Proovedor")
                        .WithMany("Pedidos")
                        .HasForeignKey("ProovedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Proovedor");
                });

            modelBuilder.Entity("RestoStockDB1.Models.Ingrediente", b =>
                {
                    b.Navigation("DetallesPlatos");
                });

            modelBuilder.Entity("RestoStockDB1.Models.Plato", b =>
                {
                    b.Navigation("DetallesPlatos");
                });

            modelBuilder.Entity("RestoStockDB1.Models.Proovedor", b =>
                {
                    b.Navigation("Pedidos");
                });
#pragma warning restore 612, 618
        }
    }
}
