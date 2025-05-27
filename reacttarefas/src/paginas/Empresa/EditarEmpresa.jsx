import React, { useEffect, useState } from 'react';
import {
    TextField,
    Button,
    Typography,
    CircularProgress,
    Alert,
    Paper,
    Box,
    Container
} from '@mui/material';
import { useParams, useNavigate } from 'react-router-dom';

function EditarEmpresa() {
    const { id } = useParams();
    const navigate = useNavigate();

    const [empresa, setEmpresa] = useState({ nome: '', cnpj: '' });
    const [carregando, setCarregando] = useState(true);
    const [erro, setErro] = useState(null);
    const [sucesso, setSucesso] = useState(false);

    // Buscar dados da empresa
    useEffect(() => {
        const fetchEmpresa = async () => {
            try {
                const response = await fetch(`${import.meta.env.VITE_API_URL}/Empresa/${id}`);
                if (!response.ok) {
                    throw new Error('Erro ao carregar dados da empresa');
                }
                const data = await response.json();
                setEmpresa(data);
            } catch (err) {
                setErro(err.message);
            } finally {
                setCarregando(false);
            }
        };

        fetchEmpresa();
    }, [id]);

    const handleChange = (e) => {
        setEmpresa({ ...empresa, [e.target.name]: e.target.value });
    };

    const handleSalvar = async () => {
        setErro(null);
        setSucesso(false);

        if (!empresa.nome || !empresa.cnpj) {
            setErro('Preencha todos os campos obrigatórios.');
            return;
        }

        try {
            const response = await fetch(`${import.meta.env.VITE_API_URL}/Empresa/${id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(empresa)
            });

            if (!response.ok) {
                throw new Error('Erro ao atualizar empresa');
            }

            setSucesso(true);
            setTimeout(() => navigate('/empresa/listar'), 500);
        } catch (err) {
            setErro(err.message);
        }
    };

    const handleCancelar = () => {
        navigate('/empresa/listar');
    };

    if (carregando) {
        return (
            <Container maxWidth="sm">
                <Box display="flex" flexDirection="column" alignItems="center" mt={8}>
                    <CircularProgress />
                    <Typography mt={2}>Carregando empresa...</Typography>
                </Box>
            </Container>
        );
    }

    return (
        <Container maxWidth="sm" sx={{ mt: 4 }}>
            <Paper elevation={3} sx={{
                p: 4, borderRadius: 2, border: '1px solid #4E71FF' }}>
                <Typography variant="h5" gutterBottom>
                    Editar Empresa
                </Typography>

                {erro && <Alert severity="error" sx={{ mb: 2 }}>{erro}</Alert>}
                {sucesso && <Alert severity="success" sx={{ mb: 2 }}>Empresa atualizada com sucesso!</Alert>}

                <TextField
                    fullWidth
                    label="Nome da Empresa"
                    name="nome"
                    value={empresa.nome}
                    onChange={handleChange}
                    margin="normal"
                    required
                />

                <TextField
                    fullWidth
                    label="CNPJ"
                    name="cnpj"
                    value={empresa.cnpj}
                    onChange={handleChange}
                    margin="normal"
                    disabled
                />

                <Box mt={3} display="flex" justifyContent="space-between">
                    <Button variant="outlined" color="secondary" onClick={handleCancelar}>
                        Cancelar
                    </Button>
                    <Button variant="contained" color="primary" onClick={handleSalvar}>
                        Salvar
                    </Button>
                </Box>
            </Paper>
        </Container>
    );
}

export default EditarEmpresa;
