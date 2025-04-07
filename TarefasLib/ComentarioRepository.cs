﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarefas_Library;

namespace TarefasLibrary
{
    public class ComentarioRepository
    {

        public void SalvarComentario(Comentario comentario)
        {
            comentario.Id = ++ListaComentarios.contadorIDs;
            ListaComentarios.ListaComentario.Add(comentario);
        }

        public Comentario BuscarComentario(int id)
        {
            return ListaComentarios.ListaComentario.FirstOrDefault(c => c.Id == id);

        }

        public List<Comentario> ListarComentarios()
        {
            return ListaComentarios.ListaComentario;
        }

    }
}
