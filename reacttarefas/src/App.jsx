import { useState } from 'react'
import './App.css'
import CadastroUsuario from './paginas/Usuario/CadastroUsuario'
import ListarUsuario from './paginas/Usuario/ListarUsuario'
import CadastroTarefa from './paginas/Tarefas/CadastrarTarefa';
import EditarTarefa from './paginas/Tarefas/EditarTarefa';
import VisualizarTarefa from './paginas/Tarefas/VisualizarTarefa';
import CadastroEmpresa from './paginas/Empresa/CadastrarEmpresa';
import ListarEmpresa from './paginas/Empresa/ListarEmpresa';
import EditarEmpresa from './paginas/Empresa/EditarEmpresa'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import PersistentDrawerLeft from './components/Menu'
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import Listar from './paginas/Tarefas/Listar'
import BoardTarefas from './paginas/Board/Board';


function App() {
    const [count, setCount] = useState(0)
    return (
        <BrowserRouter>
            <PersistentDrawerLeft>
                <Routes>
                    <Route path="/" element={<BoardTarefas />} />
                    <Route path="/usuario/cadastro" element={<CadastroUsuario />} />
                    <Route path="/usuario/listar" element={<ListarUsuario />} />
                    <Route path="/tarefa/listar" element={<Listar />} />
                    <Route path="/tarefa/cadastro" element={<CadastroTarefa />} />
                    <Route path="/empresa/cadastro" element={<CadastroEmpresa />} />
                    <Route path="/empresa/listar" element={<ListarEmpresa />} />
                    <Route path="/empresa/editar/:id" element={<EditarEmpresa />} />
                    <Route path="/tarefa/editar/:id" element={<EditarTarefa />} />
                    <Route path="/tarefa/visualizar/:id" element={<VisualizarTarefa />} />
                </Routes>
            </PersistentDrawerLeft>
        </BrowserRouter>
    )
}

export default App