import React, { useState } from 'react';
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
    InputAdornment
} from '@mui/material';
import {
    Save as SaveIcon,
    Cancel as CancelIcon,
    Business as BusinessIcon,
    Badge as BadgeIcon
} from '@mui/icons-material';

function CadastroEmpresa() {
    const navigate = useNavigate();
    const [empresa, setEmpresa] = useState({
        nome: '',
        cnpj: ''
    });
    const [error, setError] = useState(null);

    const handleChange = e => {
        const { name, value } = e.target;
        setEmpresa(prev => ({ ...prev, [name]: value }));
    };

    const formatCNPJ = (value) => {
        // Remove non-digit characters
        const digits = value.replace(/\D/g, '');

        // Apply CNPJ mask: xx.xxx.xxx/xxxx-xx
        if (digits.length <= 2) {
            return digits;
        } else if (digits.length <= 5) {
            return `${digits.slice(0, 2)}.${digits.slice(2)}`;
        } else if (digits.length <= 8) {
            return `${digits.slice(0, 2)}.${digits.slice(2, 5)}.${digits.slice(5)}`;
        } else if (digits.length <= 12) {
            return `${digits.slice(0, 2)}.${digits.slice(2, 5)}.${digits.slice(5, 8)}/${digits.slice(8)}`;
        } else {
            return `${digits.slice(0, 2)}.${digits.slice(2, 5)}.${digits.slice(5, 8)}/${digits.slice(8, 12)}-${digits.slice(12, 14)}`;
        }
    };

    const handleCNPJChange = e => {
        const formattedCNPJ = formatCNPJ(e.target.value);
        setEmpresa(prev => ({ ...prev, cnpj: formattedCNPJ }));
    };

    const salvarEmpresa = async e => {
        e.preventDefault();
        setError(null);
        try {
            const response = await fetch(`${import.meta.env.VITE_API_URL}/Empresa`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    "Nome": empresa.nome,
                    "CNPJ": empresa.cnpj
                })
            });
            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(errorText || 'Erro ao criar empresa');
            }
            navigate('/empresa/listar');
        } catch (err) {
            console.error('Erro ao salvar empresa:', err);
            setError('Falha ao salvar empresa. Tente novamente.');
        }
    };

    const cancelar = () => {
        navigate('/empresa/listar');
    };

    return (
        <Container maxWidth="md" sx={{ mt: 4, mb: 4 }}>
            <Paper
                elevation={3}
                sx={{
                    p: 4,
                    borderRadius: 2,
                    background: 'linear-gradient(to right bottom, #ffffff, #f8f9fa)',
                    border: '1px solid #4E71FF'
                }}
            >
                <Box sx={{ mb: 3, display: 'flex', alignItems: 'center' }}>
                    <BusinessIcon sx={{ fontSize: 32, mr: 2, color: '#3f51b5' }} />
                    <Typography variant="h4" component="h1" fontWeight="500" color="primary">
                        Nova Empresa
                    </Typography>
                </Box>

                {error && (
                    <Alert severity="error" sx={{ mb: 3 }}>
                        {error}
                    </Alert>
                )}

                <form name="novaEmpresaForm" onSubmit={salvarEmpresa}>
                    <Grid container spacing={3}>
                        <Grid item xs={12}>
                            <TextField
                                id="nome"
                                label="Nome da Empresa"
                                fullWidth
                                variant="outlined"
                                name="nome"
                                value={empresa.nome}
                                onChange={handleChange}
                                required
                                InputProps={{
                                    startAdornment: (
                                        <InputAdornment position="start">
                                            <BusinessIcon color="action" />
                                        </InputAdornment>
                                    ),
                                }}
                            />
                        </Grid>

                        <Grid item xs={12}>
                            <TextField
                                id="cnpj"
                                label="CNPJ"
                                fullWidth
                                variant="outlined"
                                name="cnpj"
                                value={empresa.cnpj}
                                onChange={handleCNPJChange}
                                required
                                placeholder="00.000.000/0000-00"
                                inputProps={{ maxLength: 18 }}
                                InputProps={{
                                    startAdornment: (
                                        <InputAdornment position="start">
                                            <BadgeIcon color="action" />
                                        </InputAdornment>
                                    ),
                                }}
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

export default CadastroEmpresa;