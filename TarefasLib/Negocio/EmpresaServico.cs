﻿using System.Collections;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;
using TarefasLibrary.Repositorio;

namespace TarefasLibrary.Negocio
{
    public class EmpresaServico
    {
        private readonly IRepositorio<Empresa> _repository;
        public EmpresaServico(IRepositorio<Empresa> repository)
        {
            _repository = repository;
        }

        public bool Cadastrar(Empresa empresa)
        {
            return _repository.Cadastrar(empresa);
        }

        public bool Editar(int id, string nome, string cnpj)
        {
            Empresa e = _repository.BuscarPorId(id);
            e.Nome = nome; 
            e.Cnpj = cnpj;

            return _repository.Editar(e);
        }

        public List<Empresa> Listar()
        {
            return _repository.Listar();
        }

        public bool Remover(Empresa empresa)
        {
            return _repository.Remover(empresa);
        }
    }
}
