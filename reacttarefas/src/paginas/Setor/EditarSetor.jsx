import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import {
    Button,
    TextField,
    Typography,
    Container,
    Paper,
    Box,
    Grid,
    Alert,
    InputAdornment,
    FormControlLabel,
    Switch,
    CircularProgress
} from '@mui/material';
import {
    Save as SaveIcon,
    Cancel as CancelIcon,
    Apartment as ApartmentIcon
} from '@mui/icons-material';

function EditarSetor() {
    const { id } = useParams();
    const navigate = useNavigate();

    const [setor, setSetor] = useState({
        nome: '',
        status: true
    });
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [success, setSuccess] = useState(false);

    useEffect(() => {
        const carregarDados = async () => {
            try {
                const setorResponse = await fetch(`${import.meta.env.VITE_API_URL}/Setor/${id}`);
                if (!setorResponse.ok) {
                    throw new Error('Erro ao carregar dados do setor');
                }
                const setorData = await setorResponse.json();
                const statusBoolean = setorData.status === true || setorData.status === "true";
                setSetor({
                    nome: setorData.nome,
                    status: statusBoolean
                });
                console.log("Status carregado:", statusBoolean);
            } catch (err) {
                console.error('Erro ao carregar dados:', err);
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        carregarDados();
    }, [id]);

    const handleChange = e => {
        const { name, value } = e.target;
        setSetor(prev => ({ ...prev, [name]: value }));
    };

    const handleSwitchChange = e => {
        const isChecked = e.target.checked;
        console.log("Switch alterado para:", isChecked);
        setSetor(prev => ({ ...prev, status: isChecked }));
    };

    const atualizarSetor = async e => {
        e.preventDefault();
        setError(null);
        setSuccess(false);

        try {
            const getSetorResponse = await fetch(`${import.meta.env.VITE_API_URL}/Setor/${id}`);
            if (!getSetorResponse.ok) {
                throw new Error('Erro ao obter dados atuais do setor');
            }
            const setorAtual = await getSetorResponse.json();

            console.log("Enviando status:", setor.status);
            console.log("Dados completos para envio:", {
                id: parseInt(id),
                nome: setor.nome,
                status: setor.status,
                empresaId: setorAtual.empresa.id
            });

            const response = await fetch(`${import.meta.env.VITE_API_URL}/Setor/${id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    id: parseInt(id),
                    nome: setor.nome,
                    status: setor.status,
                    empresaId: setorAtual.empresa.id
                })
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(errorText || 'Erro ao atualizar setor');
            }

            setSuccess(true);
            setTimeout(() => navigate('/setor/listar'), 1000);
        } catch (err) {
            console.error('Erro ao atualizar setor:', err);
            setError('Falha ao atualizar setor. Tente novamente.');
        }
    };

    const cancelar = () => {
        navigate('/setor/listar');
    };

    if (loading) {
        return (
            <Container maxWidth="md" sx={{ mt: 4, mb: 4, display: 'flex', justifyContent: 'center' }}>
                <Box display="flex" flexDirection="column" alignItems="center">
                    <CircularProgress />
                    <Typography mt={2}>Carregando setor...</Typography>
                </Box>
            </Container>
        );
    }

    return (
        <Container maxWidth="md" sx={{ mt: 4, mb: 4 }}>
            <Paper
                elevation={3}
                sx={{
                    p: 4,
                    borderRadius: 2,
                    background: 'linear-gradient(to right bottom, #ffffff, #f8f9fa)'
                }}
            >
                <Box sx={{ mb: 3, display: 'flex', alignItems: 'center' }}>
                    <ApartmentIcon sx={{ fontSize: 32, mr: 2, color: '#3f51b5' }} />
                    <Typography variant="h4" component="h1" fontWeight="500" color="primary">
                        Editar Setor
                    </Typography>
                </Box>

                {error && (
                    <Alert severity="error" sx={{ mb: 3 }}>
                        {error}
                    </Alert>
                )}

                {success && (
                    <Alert severity="success" sx={{ mb: 3 }}>
                        Setor atualizado com sucesso!
                    </Alert>
                )}

                <form name="editarSetorForm" onSubmit={atualizarSetor}>
                    <Grid container spacing={3}>
                        <Grid item xs={12}>
                            <TextField
                                id="nome"
                                label="Nome do Setor"
                                fullWidth
                                variant="outlined"
                                name="nome"
                                value={setor.nome}
                                onChange={handleChange}
                                required
                                InputProps={{
                                    startAdornment: (
                                        <InputAdornment position="start">
                                            <ApartmentIcon color="action" />
                                        </InputAdornment>
                                    ),
                                }}
                            />
                        </Grid>

                        <Grid item xs={12}>
                            <FormControlLabel
                                control={
                                    <Switch
                                        checked={setor.status}
                                        onChange={handleSwitchChange}
                                        name="status"
                                        color="primary"
                                    />
                                }
                                label="Setor Ativo"
                            />
                        </Grid>

                        <Grid item xs={12}>
                            <Box
                                sx={{
                                    display: 'flex',
                                    justifyContent: 'flex-end',
                                    gap: 2,
                                    mt: 2
                                }}
                            >
                                <Button
                                    variant="outlined"
                                    onClick={cancelar}
                                    startIcon={<CancelIcon />}
                                    sx={{ px: 3 }}
                                >
                                    Cancelar
                                </Button>
                                <Button
                                    type="submit"
                                    variant="contained"
                                    color="primary"
                                    startIcon={<SaveIcon />}
                                    sx={{ px: 3 }}
                                >
                                    Salvar
                                </Button>
                            </Box>
                        </Grid>
                    </Grid>
                </form>
            </Paper>
        </Container>
    );
}

export default EditarSetor;