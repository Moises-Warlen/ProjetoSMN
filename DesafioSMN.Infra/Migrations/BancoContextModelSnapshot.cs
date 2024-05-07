﻿// <auto-generated />
using System;
using DesafioSMN.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DesafioSMN.Infra.Migrations
{
    [DbContext(typeof(BancoContext))]
    partial class BancoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DesafioSMN.Dominio.Model.FuncionarioModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Celular")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cep")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FuncionarioModelId")
                        .HasColumnType("int");

                    b.Property<int?>("Gestor_FuncionarioId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<int>("Perfil")
                        .HasColumnType("int");

                    b.Property<string>("Rua")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TarefaModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FuncionarioModelId");

                    b.HasIndex("TarefaModelId");

                    b.ToTable("Funcionarios");
                });

            modelBuilder.Entity("DesafioSMN.MVC.Models.TarefaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CriadorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataAtribuicao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataConclusao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FuncionarioId")
                        .HasColumnType("int");

                    b.Property<string>("Responsavel")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FuncionarioId");

                    b.ToTable("Tarefas");
                });

            modelBuilder.Entity("DesafioSMN.Dominio.Model.FuncionarioModel", b =>
                {
                    b.HasOne("DesafioSMN.Dominio.Model.FuncionarioModel", null)
                        .WithMany("Funcionarios")
                        .HasForeignKey("FuncionarioModelId");

                    b.HasOne("DesafioSMN.MVC.Models.TarefaModel", null)
                        .WithMany("Funcionarios")
                        .HasForeignKey("TarefaModelId");
                });

            modelBuilder.Entity("DesafioSMN.MVC.Models.TarefaModel", b =>
                {
                    b.HasOne("DesafioSMN.Dominio.Model.FuncionarioModel", "Funcionario")
                        .WithMany()
                        .HasForeignKey("FuncionarioId");

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("DesafioSMN.Dominio.Model.FuncionarioModel", b =>
                {
                    b.Navigation("Funcionarios");
                });

            modelBuilder.Entity("DesafioSMN.MVC.Models.TarefaModel", b =>
                {
                    b.Navigation("Funcionarios");
                });
#pragma warning restore 612, 618
        }
    }
}
