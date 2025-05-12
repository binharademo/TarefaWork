import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import ReactComponent from './components/ReactComponent'
import CadastroUsuario from './paginas/CadastroUsuario'
import ListarUsuario from './paginas/ListarUsuario'
import { BrowserRouter, Routes, Route } from 'react-router-dom'

function App() {
  const [count, setCount] = useState(0)

  return (
      <BrowserRouter>
        <Routes>
            <Route path="/" element={<CadastroUsuario />} />
            <Route path="/listar" element={<ListarUsuario />} />
        </Routes>
      </BrowserRouter>
  )
}

export default App
