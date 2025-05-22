using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;
using TarefasLibrary.Repositorio.Entity;
using Microsoft.VisualBasic;

namespace BlazorTarefas.Servicos
{
    public static class InitSeed
    {
        public static async Task InicializarBancoDeDados(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                
                // Garante que o banco de dados está criado
                await dbContext.Database.EnsureCreatedAsync();
                
                // Verifica se o banco de dados está vazio
                if (!await dbContext.Tarefas.AnyAsync() && !await dbContext.Usuarios.AnyAsync())
                {
                    await SeedDadosAsync(dbContext);
                }
            }
        }

        private static async Task SeedDadosAsync(AppDbContext dbContext)
        {
            // Seed de Empresas
            var empresa = new Empresa("Empresa Padrão", "00.000.000/0001-00");
            dbContext.Empresas.Add(empresa);
            await dbContext.SaveChangesAsync();

            // Seed de Setores
            var setorTI = new Setor("Tecnologia da Informação", empresa);
            var setorRH = new Setor("Recursos Humanos", empresa);
            
            dbContext.Setores.AddRange(setorTI, setorRH);
            await dbContext.SaveChangesAsync();

            // Seed de Usuários
            var admin = new Usuario
            {
                Nome = "Administrador",
                Senha = "admin123", // Em produção, use hash de senha
                FuncaoUsuario = Usuario.Funcao.Dev,
                SetorUsuario = setorTI
            };
            
            var usuario1 = new Usuario
            {
                Nome = "Usuário Padrão",
                Senha = "user123", // Em produção, use hash de senha
                FuncaoUsuario = Usuario.Funcao.Analista,
                SetorUsuario = setorRH
            };
            
            dbContext.Usuarios.AddRange(admin, usuario1);
            await dbContext.SaveChangesAsync();

            // Seed de Tarefas baseado no arquivo seed-tarefas.md
            var tarefas = new List<Tarefa>
            {
                new Tarefa
                {
                    Titulo = "Permitir que o dono da tarefa atribua pessoas a ela",
                    Descricao = "Implementar funcionalidade para que o dono da tarefa possa atribuir pessoas a ela",
                    StatusTarefa = Tarefa.Status.ToDo,
                    PrioridadeTarefa = Tarefa.Prioridade.Baixa,
                    DataCriacao = DateAndTime.Now,
                    Prazo = DateAndTime.Now.AddDays(7),
                    Criador = admin,
                    Responsavel = admin
                },
                new Tarefa
                {
                    Titulo = "Criar um usuário no sistema a partir de outubro",
                    Descricao = "Implementar funcionalidade para criar usuários no sistema a partir de outubro",
                    StatusTarefa = Tarefa.Status.ToDo,
                    PrioridadeTarefa = Tarefa.Prioridade.Baixa,
                    DataCriacao = DateAndTime.Now,
                    Prazo = DateAndTime.Now.AddDays(14),
                    Criador = admin,
                    Responsavel = usuario1
                },
                new Tarefa
                {
                    Titulo = "Sugerir login seguro na versão web",
                    Descricao = "Implementar sugestões de login seguro na versão web do sistema",
                    StatusTarefa = Tarefa.Status.ToDo,
                    PrioridadeTarefa = Tarefa.Prioridade.Alta,
                    DataCriacao = DateAndTime.Now,
                    Prazo = DateAndTime.Now.AddDays(5),
                    Criador = admin,
                    Responsavel = admin
                },
                new Tarefa
                {
                    Titulo = "Acessar tarefas entre diferentes colunas no sistema",
                    Descricao = "Implementar funcionalidade para acessar tarefas entre diferentes colunas no sistema",
                    StatusTarefa = Tarefa.Status.ToDo,
                    PrioridadeTarefa = Tarefa.Prioridade.Baixa,
                    DataCriacao = DateAndTime.Now,
                    Prazo = DateAndTime.Now.AddDays(10),
                    Criador = admin,
                    Responsavel = usuario1
                },
                new Tarefa
                {
                    Titulo = "Incluir arquivos anexados a uma tarefa na versão web",
                    Descricao = "Implementar funcionalidade para incluir arquivos anexados a uma tarefa na versão web",
                    StatusTarefa = Tarefa.Status.ToDo,
                    PrioridadeTarefa = Tarefa.Prioridade.Baixa,
                    DataCriacao = DateAndTime.Now,
                    Prazo = DateAndTime.Now.AddDays(12),
                    Criador = admin,
                    Responsavel = admin
                },
                new Tarefa
                {
                    Titulo = "Gerar relatório com as condições das tarefas na versão web",
                    Descricao = "Implementar funcionalidade para gerar relatório com as condições das tarefas na versão web",
                    StatusTarefa = Tarefa.Status.ToDo,
                    PrioridadeTarefa = Tarefa.Prioridade.Baixa,
                    DataCriacao = DateAndTime.Now,
                    Prazo = DateAndTime.Now.AddDays(15),
                    Criador = admin,
                    Responsavel = usuario1
                },
                new Tarefa
                {
                    Titulo = "Corrigir o erro de quebra na pré-visualização ao excluir (testes)",
                    Descricao = "Corrigir o erro de quebra na pré-visualização ao excluir (testes)",
                    StatusTarefa = Tarefa.Status.ToDo,
                    PrioridadeTarefa = Tarefa.Prioridade.Alta,
                    DataCriacao = DateAndTime.Now,
                    Prazo = DateAndTime.Now.AddDays(3),
                    Criador = admin,
                    Responsavel = admin
                },
                new Tarefa
                {
                    Titulo = "Cadastrar usuários no sistema",
                    Descricao = "Implementar funcionalidade para cadastrar usuários no sistema",
                    StatusTarefa = Tarefa.Status.ToDo,
                    PrioridadeTarefa = Tarefa.Prioridade.Baixa,
                    DataCriacao = DateAndTime.Now,
                    Prazo = DateAndTime.Now.AddDays(8),
                    Criador = admin,
                    Responsavel = usuario1
                },
                new Tarefa
                {
                    Titulo = "Adicionar um contador de tempo às tarefas",
                    Descricao = "Implementar funcionalidade para adicionar um contador de tempo às tarefas",
                    StatusTarefa = Tarefa.Status.ToDo,
                    PrioridadeTarefa = Tarefa.Prioridade.Baixa,
                    DataCriacao = DateAndTime.Now,
                    Prazo = DateAndTime.Now.AddDays(9),
                    Criador = admin,
                    Responsavel = admin
                },
                new Tarefa
                {
                    Titulo = "Cadastrar departamentos no sistema",
                    Descricao = "Implementar funcionalidade para cadastrar departamentos no sistema",
                    StatusTarefa = Tarefa.Status.ToDo,
                    PrioridadeTarefa = Tarefa.Prioridade.Baixa,
                    DataCriacao = DateAndTime.Now,
                    Prazo = DateAndTime.Now.AddDays(11),
                    Criador = admin,
                    Responsavel = usuario1
                }
                // As outras tarefas podem ser adicionadas de forma semelhante
            };
            
            dbContext.Tarefas.AddRange(tarefas);
            await dbContext.SaveChangesAsync();
        }
    }
}
