using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;


namespace TarefasLibrary.Repositorio
{
    public class UsuarioRepositorioSQLITE : IRepositorio<Usuario>
    {
        private static readonly string _connectionString = "Data Source=tarefas.db;";

        private List<Usuario> ListaUsuarios = new List<Usuario>();

        public static void CriarTabela()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    CREATE TABLE IF NOT EXISTS Usuarios (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nome TEXT NOT NULL,
                        Senha TEXT NOT NULL,
                        Funcao TEXT NOT NULL,
                        Setor TEXT NOT NULL
                    )
                ";
                command.ExecuteNonQuery();
            }
        }

        public Usuario? BuscarPorId(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                string query = "SELECT * FROM Usuarios WHERE Id = @Id";
                command.CommandText = query;
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Usuario(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetString(4)
                        );
                    }
                    return null;
                }
            }
        }

        public bool Cadastrar(Usuario obj)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                string query = "INSERT INTO Usuarios (Nome, Senha, Funcao, Setor) VALUES (@Nome, @Senha, @Funcao, @Setor)";
                command.CommandText = query;
                command.Parameters.AddWithValue("@Nome", obj.Nome);
                command.Parameters.AddWithValue("@Senha", obj.Senha);
                command.Parameters.AddWithValue("@Funcao", obj.Funcao);
                command.Parameters.AddWithValue("@Setor", obj.Setor);
                command.ExecuteNonQuery();
                return true;
            }
            
        }

        public bool Editar(Usuario obj)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                string query = "UPDATE Usuarios SET Nome = @Nome, Senha = @Senha, Funcao = @Funcao, Setor = @Setor WHERE Id = @Id";
                command.CommandText = query;
                command.Parameters.AddWithValue("@Nome", obj.Nome);
                command.Parameters.AddWithValue("@Senha", obj.Senha);
                command.Parameters.AddWithValue("@Funcao", obj.Funcao);
                command.Parameters.AddWithValue("@Setor", obj.Setor);
                command.Parameters.AddWithValue("@Id", obj.Id);
                command.ExecuteNonQuery();
                return true;

            }
        }

        public List<Usuario> Listar()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand(); ;
                string query = "SELECT * FROM Usuarios";
                command.CommandText = query;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var usuario = new Usuario(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetString(4)
                        );
                        ListaUsuarios.Add(usuario);
                    }
                    return ListaUsuarios;
                }

            }
        }

        public bool Remover(Usuario obj)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand(); ;
                string query = "DELETE FROM Usuarios WHERE Id = @Id";
                command.CommandText = query;
                command.Parameters.AddWithValue("@Id", obj.Id);
                command.ExecuteNonQuery();
                return true;

            }
        }
    }
}
