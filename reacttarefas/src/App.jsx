import { useState } from 'react'
import './App.css'
import CadastroUsuario from './paginas/Usuario/CadastroUsuario'
import ListarUsuario from './paginas/Usuario/ListarUsuario'
import CadastroTarefa from './paginas/Tarefas/CadastrarTarefa';
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import PersistentDrawerLeft from './components/Menu'
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import Listar from './paginas/Tarefas/Listar'


function App() {
  const [count, setCount] = useState(0)

  return (
      <BrowserRouter>
        <PersistentDrawerLeft>
            <Routes>
                <Route path="/" element={<CadastroUsuario />} />
                <Route path="/listar" element={<ListarUsuario />} />
                  <Route path="/tarefa/listar" element={<Listar />} />
                  <Route path="/tarefa/cadastro" element={<CadastroTarefa />} />
              </Routes>
        </PersistentDrawerLeft>
      </BrowserRouter>
  )
}

export default App
