﻿using DesafioSMN.Dominio.Enums;
using System;
using System.Collections.Generic;

namespace DesafioSMN.Dominio.Model
{
    public  class FuncionarioModel
    {
        public FuncionarioModel()
        {

        }
        public FuncionarioModel(FuncionarioModel funcionario)
        {
            Id = funcionario.Id;
            Nome = funcionario.Nome;
            Email = funcionario.Email;
            Perfil = funcionario.Perfil;
            Senha = funcionario.Senha;
            Celular = funcionario.Celular;
            DataNascimento = funcionario.DataNascimento;
            Rua = funcionario.Rua;
            Numero = funcionario.Numero;
            Bairro = funcionario.Bairro;
            Cidade = funcionario.Cidade;
            Cep = funcionario.Cep;
            Gestor_FuncionarioId = funcionario.Gestor_FuncionarioId;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public PerfilEmun Perfil { get; set; }
        public string Senha { get; set; }
        public string Celular { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public int? Gestor_FuncionarioId { get; set; }
        public virtual List<FuncionarioModel>Funcionarios { get; set; }// relacioamento com a tarefa

        public bool SenhaValida( string senha) {

            return Senha == senha;
        }
    }
}
