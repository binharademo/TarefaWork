import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import {
    Paper,
    Typography,
    Button,
    CircularProgress,
    Alert,
    Box,
    Container,
    TextField,
    FormControl,
    InputLabel,
    Select,
    MenuItem,
    Grid,
    Divider,
    IconButton,
    Tooltip
} from '@mui/material';
import {
    Save as SaveIcon,
    ArrowBack as ArrowBackIcon,
    Person as PersonIcon,
    Error as ErrorIcon,
    Edit as EditIcon
} from '@mui/icons-material';

function EditarUsuario() {
    const { id } = useParams();
    const navigate = useNavigate();
    const [carregando, setCarregando] = useState(true);
    const [salvando, setSalvando] = useState(false);
    const [erro, setErro] = useState(null);
    const [usuario, setUsuario] = useState({
        id: '',
        nome: '',
        senha: '',
        funcaoUsuario: '',
        setorUsuario: ''
    });

    useEffect(() => {
        const fetchUsuario = async () => {
            setCarregando(true);
            setErro(null);

            try {
                const response = await fetch(`http://localhost:53011/Usuario/${id}`);
                if (!response.ok) {
                    throw new Error('Erro ao carregar dados do usuário');
                }
                const data = await response.json();
                setUsuario(data);
            } catch (error) {
                setErro(error.message);
            } finally {
                setCarregando(false);
            }
        };

        fetchUsuario();
    }, [id]);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setUsuario({
            ...usuario,
            [name]: value
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setSalvando(true);
        setErro(null);

        try {
            // Criar o objeto que corresponde à estrutura AtualizarUsuarioDTO do backend
            const usuarioDTO = {
                nome: usuario.nome,
                senha: usuario.senha || '', // Inclui senha no DTO, mesmo se vazia
                funcaoUsuario: parseInt(usuario.funcaoUsuario),
                setorUsuario: parseInt(usuario.setorUsuario)
            };

            console.log('Enviando dados para atualização:', usuarioDTO);

            const response = await fetch(`http://localhost:53011/Usuario/${id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(usuarioDTO)
            });

            if (!response.ok) {
                const errorData = await response.text();
                console.error('Erro na resposta:', errorData);
                throw new Error(`Erro ao atualizar usuário: ${response.status}`);
            }

            // Redirecionar para a lista após salvar com sucesso
            navigate('/usuario/listar');
        } catch (error) {
            console.error('Exceção ao atualizar:', error);
            setErro(error.message);
            setSalvando(false);
        }
    };

    const handleVoltar = () => {
        navigate('/usuario/listar');
    };

    if (carregando) {
        return (
            <Container maxWidth="lg">
                <Box
                    display="flex"
                    flexDirection="column"
                    alignItems="center"
                    justifyContent="center"
                    height="50vh"
                >
                    <CircularProgress size={60} thickness={4} />
                    <Typography variant="h6" mt={2} color="text.secondary">
                        Carregando dados do usuário...
                    </Typography>
                </Box>
            </Container>
        );
    }

    return (
        <Container maxWidth="md" sx={{ mt: 4, mb: 8 }}>
            <Paper
                elevation={3}
                sx={{
                    p: 3,
                    borderRadius: 2,
                    background: 'linear-gradient(to right bottom, #ffffff, #f8f9fa)'
                }}
            >
                <Box display="flex" justifyContent="space-between" alignItems="center" mb={3}>
                    <Box display="flex" alignItems="center">
                        <EditIcon sx={{ fontSize: 32, mr: 2, color: '#3f51b5' }} />
                        <Typography variant="h4" component="h1" fontWeight="500" color="primary">
                            Editar Usuário
                        </Typography>
                    </Box>

                    <Tooltip title="Voltar para lista">
                        <IconButton
                            color="primary"
                            onClick={handleVoltar}
                        >
                            <ArrowBackIcon />
                        </IconButton>
                    </Tooltip>
                </Box>

                <Divider sx={{ mb: 3 }} />

                {erro && (
                    <Alert
                        severity="error"
                        sx={{ mb: 3 }}
                        icon={<ErrorIcon fontSize="inherit" />}
                    >
                        {erro}
                    </Alert>
                )}

                <form onSubmit={handleSubmit}>
                    <Grid container spacing={3}>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                label="ID"
                                value={usuario.id}
                                fullWidth
                                disabled
                                variant="outlined"
                                margin="normal"
                            />
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                required
                                label="Nome"
                                name="nome"
                                value={usuario.nome}
                                onChange={handleInputChange}
                                fullWidth
                                variant="outlined"
                                margin="normal"
                            />
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                label="Senha"
                                name="senha"
                                type="password"
                                value={usuario.senha || ''}
                                onChange={handleInputChange}
                                fullWidth
                                variant="outlined"
                                margin="normal"
                                placeholder="Digite para alterar a senha"
                                disabled
                            />
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <FormControl fullWidth variant="outlined" margin="normal">
                                <InputLabel>Função</InputLabel>
                                <Select
                                    required
                                    label="Função"
                                    name="funcaoUsuario"
                                    value={usuario.funcaoUsuario}
                                    onChange={handleInputChange}
                                >
                                    <MenuItem value={0}>Dev</MenuItem>
                                    <MenuItem value={1}>Analista</MenuItem>
                                    <MenuItem value={2}>Marketing</MenuItem>
                                </Select>
                            </FormControl>
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <FormControl fullWidth variant="outlined" margin="normal">
                                <InputLabel>Setor</InputLabel>
                                <Select
                                    required
                                    label="Setor"
                                    name="setorUsuario"
                                    value={usuario.setorUsuario}
                                    onChange={handleInputChange}
                                >
                                    <MenuItem value={0}>TI</MenuItem>
                                    <MenuItem value={1}>Marketing</MenuItem>
                                </Select>
                            </FormControl>
                        </Grid>
                    </Grid>

                    <Box display="flex" justifyContent="flex-end" mt={4} gap={2}>
                        <Button
                            variant="outlined"
                            onClick={handleVoltar}
                            disabled={salvando}
                        >
                            Cancelar
                        </Button>
                        <Button
                            type="submit"
                            variant="contained"
                            color="primary"
                            startIcon={salvando ? <CircularProgress size={20} color="inherit" /> : <SaveIcon />}
                            disabled={salvando}
                            sx={{
                                boxShadow: '0 4px 6px rgba(50, 50, 93, 0.11), 0 1px 3px rgba(0, 0, 0, 0.08)',
                                borderRadius: 2,
                                px: 3
                            }}
                        >
                            {salvando ? 'Salvando...' : 'Salvar'}
                        </Button>
                    </Box>
                </form>
            </Paper>
        </Container>
    );
}

export default EditarUsuario;