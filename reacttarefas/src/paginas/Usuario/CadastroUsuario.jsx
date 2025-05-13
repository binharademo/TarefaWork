import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Button from '@mui/material/Button';
import { FormControl, Select, MenuItem, InputLabel, TextField, Grid } from '@mui/material';
function CadastroUsuario() {
    const navigate = useNavigate();
    const [usuario, setUsuario] = useState({
        nome: '',
        senha: '',
        funcaoUsuario: '',
        setorUsuario: ''
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
            <h1>Novo Usuario</h1>
            {error && <div className="alert alert-danger" role="alert">{error}</div>}
            <form name="novoUsuarioForm" onSubmit={salvarUsuario}>

                <Grid container spacing={2}>
                    <TextField id="nome" label="Nome" fullWidth variant="outlined" 
                        name="nome"
                        className="form-control"
                        value={usuario.nome}
                        onChange={handleChange}
                        required />

                    <TextField id="senha" label="Senha" fullWidth variant="outlined"
                        name="senha"
                        className="form-control"
                        value={usuario.senha}
                        onChange={handleChange}
                        required />

                    <FormControl fullWidth>
                        <InputLabel id="funcao">Funcao</InputLabel>
                        <Select
                            id="funcaoUsuario"
                            name="funcaoUsuario"
                            className="form-control"
                            value={usuario.funcaoUsuario}
                            onChange={handleChange}
                        >
                            <MenuItem value={0}>Dev</MenuItem>
                            <MenuItem value={1}>Analista</MenuItem>
                            <MenuItem value={2}>Marketing</MenuItem>
                        </Select>
                    </FormControl>

                    <FormControl variant="outlined" fullWidth>
                        <InputLabel id="setor">Setor</InputLabel>
                        <Select
                            id="setorUsuario"
                            name="setorUsuario"
                            className="form-control"
                            value={usuario.setorUsuario}
                            onChange={handleChange}
                        >
                            <MenuItem value={0}>TI</MenuItem>
                            <MenuItem value={1}>Marketing</MenuItem>
                        </Select>
                    </FormControl>

                    <Grid
                        container
                        direction="row"
                        size="grow"
                        sx={{
                            justifyContent: "center",
                            alignItems: "center",
                        }}
                    >
                        <Button type="button" variant="outlined" onClick={cancelar}>
                            Cancelar
                        </Button>


                        <Button type="submit" variant="contained">
                            Salvar
                        </Button>
                    </Grid>
                </Grid>
            </form>
        </div>
    );
}

export default CadastroUsuario;
