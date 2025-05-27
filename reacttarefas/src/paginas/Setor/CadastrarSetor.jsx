import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
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
    FormControl,
    InputLabel,
    Select,
    MenuItem,
    FormControlLabel,
    Switch,
    CircularProgress
} from '@mui/material';
import {
    Save as SaveIcon,
    Cancel as CancelIcon,
    Apartment as ApartmentIcon,
    Business as BusinessIcon
} from '@mui/icons-material';

function CadastrarSetor() {
    const navigate = useNavigate();
    const [setor, setSetor] = useState({
        nome: '',
        status: true,
        empresaId: ''
    });
    const [empresas, setEmpresas] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const carregarEmpresas = async () => {
            try {
                const response = await fetch(`${import.meta.env.VITE_API_URL}/Empresa`);
                if (!response.ok) {
                    throw new Error('Erro ao carregar empresas');
                }
                const data = await response.json();
                setEmpresas(data);
            } catch (err) {
                console.error('Erro ao carregar empresas:', err);
                setError('Não foi possível carregar a lista de empresas.');
            } finally {
                setLoading(false);
            }
        };

        carregarEmpresas();
    }, []);

    const handleChange = e => {
        const { name, value } = e.target;
        setSetor(prev => ({ ...prev, [name]: value }));
    };

    const handleSwitchChange = e => {
        setSetor(prev => ({ ...prev, status: e.target.checked }));
    };

    const salvarSetor = async e => {
        e.preventDefault();
        setError(null);

        if (!setor.empresaId) {
            setError('Selecione uma empresa válida.');
            return;
        }

        try {
            const response = await fetch(`${import.meta.env.VITE_API_URL}/Setor`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    id: 0,
                    nome: setor.nome,
                    status: setor.status,
                    empresaId: parseInt(setor.empresaId)
                })
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(errorText || 'Erro ao criar setor');
            }

            navigate('/setor/listar');
        } catch (err) {
            console.error('Erro ao salvar setor:', err);
            setError('Falha ao salvar setor. Tente novamente.');
        }
    };

    const cancelar = () => {
        navigate('/setor/listar');
    };

    if (loading) {
        return (
            <Container maxWidth="md" sx={{ mt: 4, mb: 4, display: 'flex', justifyContent: 'center' }}>
                <CircularProgress />
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
                        Novo Setor
                    </Typography>
                </Box>

                {error && (
                    <Alert severity="error" sx={{ mb: 3 }}>
                        {error}
                    </Alert>
                )}

                <form name="novoSetorForm" onSubmit={salvarSetor}>
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
                            <FormControl fullWidth variant="outlined" required>
                                <InputLabel id="empresa-label">Empresa</InputLabel>
                                <Select
                                    labelId="empresa-label"
                                    id="empresaId"
                                    name="empresaId"
                                    value={setor.empresaId}
                                    onChange={handleChange}
                                    label="Empresa"
                                    startAdornment={
                                        <InputAdornment position="start">
                                            <BusinessIcon color="action" />
                                        </InputAdornment>
                                    }
                                >
                                    <MenuItem value="" disabled>
                                        <em>Selecione uma empresa</em>
                                    </MenuItem>
                                    {empresas.map((empresa) => (
                                        <MenuItem key={empresa.id} value={empresa.id}>
                                            {empresa.nome} ({empresa.cnpj})
                                        </MenuItem>
                                    ))}
                                </Select>
                            </FormControl>
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

export default CadastrarSetor;