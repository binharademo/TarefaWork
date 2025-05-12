import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

function CadastroUsuario() {
    const navigate = useNavigate();
    const [usuario, setUsuario] = useState({
        nome: '',
        senha: '',
        funcaoUsuario: '0',
        setorUsuario: '0'
    });
    const [error, setError] = useState(null);

    const handleChange = e => {
        const { name, value } = e.target;
        setUsuario(prev => ({ ...prev, [name]: value }));
    };

    const salvarUsuario = async e => {
        e.preventDefault();
        setError(null);
        try {
            const response = await fetch('http://localhost:53011/Usuario', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    "Nome": usuario.nome,
                    "Senha": usuario.senha,
                    "FuncaoUsuario": parseInt(usuario.funcaoUsuario, 10),
                    "SetorUsuario": parseInt(usuario.setorUsuario, 10)
                    
                })
            });
            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(errorText || 'Erro ao criar usuário');
            }
           navigate('/listar');
        } catch (err) {
            console.error('Erro ao salvar usuário:', err);
            setError('Falha ao salvar usuário. Tente novamente.');
        }
    };

    const cancelar = () => {
        navigate('/listar');
    };

    return (
        <div className="container mt-4">
            <h1>Novo Usuário</h1>
            {error && <div className="alert alert-danger" role="alert">{error}</div>}
            <form name="novoUsuarioForm" onSubmit={salvarUsuario}>
                <div className="form-group mb-3">
                    <label htmlFor="nome">Nome</label>a
                    <input
                        type="text"
                        id="nome"
                        name="nome"
                        className="form-control"
                        value={usuario.nome}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div className="form-group mb-3">
                    <label htmlFor="senha">Senha</label>
                    <input
                        type="password"
                        id="senha"
                        name="senha"
                        className="form-control"
                        value={usuario.senha}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div className="form-group mb-3">
                    <label htmlFor="funcaoUsuario">Função</label>
                    <select
                        id="funcaoUsuario"
                        name="funcaoUsuario"
                        className="form-control"
                        value={usuario.funcaoUsuario}
                        onChange={handleChange}
                    >
                        <option value="0">Dev</option>
                        <option value="1">Analista</option>
                        <option value="2">Marketing</option>
                    </select>
                </div>

                <div className="form-group mb-3">
                    <label htmlFor="setorUsuario">Setor</label>
                    <select
                        id="setorUsuario"
                        name="setorUsuario"
                        className="form-control"
                        value={usuario.setorUsuario}
                        onChange={handleChange}
                    >
                        <option value="0">TI</option>
                        <option value="1">Marketing</option>
                    </select>
                </div>

                <button type="submit" className="btn btn-success me-2">
                    Salvar
                </button>
                <button type="button" className="btn btn-secondary" onClick={cancelar}>
                    Cancelar
                </button>
            </form>
        </div>
    );
}

export default CadastroUsuario;
