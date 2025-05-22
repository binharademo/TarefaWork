import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import {
    Button,
    TextField,
    FormControl,
    InputLabel,
    Select,
    MenuItem,
    Typography,
    Container,
    Paper,
    Box,
    Grid,
    Alert,
    IconButton,
    InputAdornment
} from '@mui/material';
import {
    Save as SaveIcon,
    Cancel as CancelIcon,
    Visibility as VisibilityIcon,
    VisibilityOff as VisibilityOffIcon,
    Person as PersonIcon,
    Work as WorkIcon,
    BusinessCenter as BusinessCenterIcon
} from '@mui/icons-material';

function CadastroUsuario() {
    const navigate = useNavigate();
    const [usuario, setUsuario] = useState({
        nome: '',
        senha: '',
        funcaoUsuario: '',
        setorUsuario: ''
    });
    const [error, setError] = useState(null);
    const [showPassword, setShowPassword] = useState(false);

    const handleChange = e => {
        const { name, value } = e.target;
        setUsuario(prev => ({ ...prev, [name]: value }));
    };

    const handleClickShowPassword = () => {
        setShowPassword(!showPassword);
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
            navigate('/usuario/listar');
        } catch (err) {
            console.error('Erro ao salvar usuário:', err);
            setError('Falha ao salvar usuário. Tente novamente.');
        }
    };

    const cancelar = () => {
        navigate('/usuario/listar');
    };

    return (
        <Container maxWidth="xl" sx={{ mt: 4, mb: 4 }}>
            <Paper
                elevation={12}
                sx={{
                    p: 4,
                    borderRadius: 5,
                    background: 'linear-gradient(to right bottom, #ffffff, #f8f9fa)',
                    border: '1px solid #4E71FF'
                }}
            >
                <Box sx={{ mb: 3, display: 'flex', alignItems: 'center' }}>
                    <PersonIcon sx={{ fontSize: 32, mr: 2, color: '#4E71FF ' }} />
                    <Typography variant="h4" component="h1" fontWeight="500" color="#4E71FF">
                        Novo Usuario
                    </Typography>
                </Box>

                {error && (
                    <Alert severity="error" sx={{ mb: 3 }}>
                        {error}
                    </Alert>
                )}

                <form name="novoUsuarioForm" onSubmit={salvarUsuario}>
                    <Grid container spacing={3} sx={{ display: 'flex', flexDirection: 'column'} }>
                        <Grid sx={{
                            display: 'flex',
                            flexDirection: 'row',
                            justifyContent: 'flex-start',
                            gap: 2
                        }}>
                            <Grid item xs={12}>
                                <TextField
                                    id="nome"
                                    label="Nome do Usuario"
                                    fullWidth
                                    variant="outlined"
                                    name="nome"
                                    value={usuario.nome}
                                    onChange={handleChange}
                                    required
                                    InputProps={{
                                        endAdornment: (
                                            <InputAdornment position="start">
                                                <PersonIcon color="action" />
                                            </InputAdornment>
                                        ),
                                    }}
                                />
                            </Grid>

                            <Grid item xs={12}>
                                <TextField
                                    id="senha"
                                    label="Senha"
                                    fullWidth
                                    variant="outlined"
                                    name="senha"
                                    type={showPassword ? 'text' : 'password'}
                                    value={usuario.senha}
                                    onChange={handleChange}
                                    required
                                    InputProps={{
                                        endAdornment: (
                                            <InputAdornment position="end">
                                                <IconButton
                                                    aria-label="toggle password visibility"
                                                    onClick={handleClickShowPassword}
                                                    edge="end"
                                                >
                                                    {showPassword ? <VisibilityOffIcon /> : <VisibilityIcon />}
                                                </IconButton>
                                            </InputAdornment>
                                        ),
                                    }}
                                />
                            </Grid>

                            <Grid item xs={12}>
                                <FormControl fullWidth variant="outlined">
                                    <InputLabel id="funcao-label">Funcao</InputLabel>
                                    <Select
                                        labelId="funcao-label"
                                        id="funcaoUsuario"
                                        name="funcaoUsuario"
                                        value={usuario.funcaoUsuario}
                                        onChange={handleChange}
                                        label="Funcao"
                                        endAdornment={
                                            <InputAdornment position="start">
                                                <WorkIcon color="action" />
                                            </InputAdornment>
                                        }
                                        sx={{minWidth: '150px;'} }
                                    >
                                        <MenuItem value="">Selecione uma funcao</MenuItem>
                                        <MenuItem value={0}>Dev</MenuItem>
                                        <MenuItem value={1}>Analista</MenuItem>
                                        <MenuItem value={2}>Marketing</MenuItem>
                                    </Select>
                                </FormControl>
                            </Grid>

                            <Grid item xs={12}>
                                <FormControl fullWidth variant="outlined">
                                    <InputLabel id="setor-label">Setor</InputLabel>
                                    <Select
                                        labelId="setor-label"
                                        id="setorUsuario"
                                        name="setorUsuario"
                                        value={usuario.setorUsuario}
                                        onChange={handleChange}
                                        label="Setor"
                                        endAdornment={
                                            <InputAdornment position="start">
                                                <BusinessCenterIcon color="action" />
                                            </InputAdornment>
                                        }
                                        sx={{ minWidth: '150px;' }}
                                    >
                                        <MenuItem value="">Selecione um setor</MenuItem>
                                        <MenuItem value={0}>TI</MenuItem>
                                        <MenuItem value={1}>Marketing</MenuItem>
                                    </Select>
                                </FormControl>
                            </Grid>

                        </Grid>

                        <Grid item xs={12}>
                            <Box
                                sx={{
                                    display: 'flex',
                                    justifyContent: 'center',
                                    gap: 2,
                                    mt: 2
                                }}
                            >
                                <Button
                                    variant="outlined"
                                    onClick={cancelar}
                                    startIcon={<CancelIcon />}
                                    sx={{ px: 3 }}
                                    color="error"
                                >
                                    Cancelar
                                </Button>
                                <Button
                                    type="submit"
                                    variant="contained"
                                    color="success"
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

export default CadastroUsuario;