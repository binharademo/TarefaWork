import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import {
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Paper,
    Typography,
    Button,
    CircularProgress,
    Alert,
    Box,
    Container,
    Chip,
    Tooltip,
    IconButton,
    Divider,
    Menu,
    MenuItem
} from '@mui/material';
import {
    Add as AddIcon,
    Refresh as RefreshIcon,
    Person as PersonIcon,
    Error as ErrorIcon,
    MoreVert as Icon,
    Edit as EditIcon,
    Delete as DeleteIcon,
    Visibility as VisibilityIcon
} from '@mui/icons-material';

function ListarUsuario() {
    const [usuarios, setUsuarios] = useState([]);
    const [carregando, setCarregando] = useState(true);
    const [erro, setErro] = useState(null);
    const navigate = useNavigate();

    const [anchorEl, setAnchorEl] = useState(null);
    const [usuarioSelecionado, setUsuarioSelecionado] = useState(null);

    const fetchUsuarios = async () => {
        setCarregando(true);
        setErro(null);

        try {
            const response = await fetch(`${import.meta.env.VITE_API_URL}/Usuario`);
            if (!response.ok) {
                throw new Error('Erro ao carregar usuarios');
            }
            const data = await response.json();
            console.log("Dados recebidos:", data);
            setUsuarios(Array.isArray(data) ? data : [data]);
        } catch (error) {
            setErro(error.message);
        } finally {
            setCarregando(false);
            console.log(usuarios);
        }
    };

    useEffect(() => {
        fetchUsuarios();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    useEffect(() => {
        console.log("Estado usuarios atualizado:", usuarios);
    }, [usuarios]);

    const handleNovoUsuario = () => {
        navigate('/usuario/cadastro');
    };

    const handleAbrirMenu = (event, usuario) => {
        setAnchorEl(event.currentTarget);
        setUsuarioSelecionado(usuario);
    };

    const handleFecharMenu = () => {
        setAnchorEl(null);
    };

    const handleEditarUsuario = () => {
        if (usuarioSelecionado) {
            navigate(`/usuario/editar/${usuarioSelecionado.id}`);
        }
        handleFecharMenu();
    };

    const renderFuncao = (funcao) => {
        switch (funcao) {
            case 0:
                return <Chip label="Dev" size="small" color="primary" />;
            case 1:
                return <Chip label="Analista" size="small" color="secondary" />;
            case 2:
                return <Chip label="Marketing" size="small" color="info" />;
            default:
                return <Chip label={funcao} size="small" />;
        }
    };

    const renderSetor = (setor) => {
        switch (setor) {
            case 1:
                return <Chip label="TI" size="small" color="success" />;
            case 2:
                return <Chip label="Marketing" size="small" color="warning" />;
            default:
                return <Chip label={`ERRO (${setor})`} size="small" />;
        }
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
                        Carregando usuarios...
                    </Typography>
                </Box>
            </Container>
        );
    }

    return (
        <Container maxWidth="lg" sx={{ mt: 4, mb: 8 }}>
            <Paper
                elevation={3}
                sx={{
                    p: 3,
                    borderRadius: 2,
                    background: 'linear-gradient(to right bottom, #ffffff, #f8f9fa)',
                    border: '1px solid #4E71FF'
                }}
            >
                <Box display="flex" justifyContent="space-between" alignItems="center" mb={3}>
                    <Box display="flex" alignItems="center">
                        <PersonIcon sx={{ fontSize: 32, mr: 2, color: '#3f51b5' }} />
                        <Typography variant="h4" component="h1" fontWeight="500" color="primary">
                            Lista de Usuarios
                        </Typography>
                    </Box>

                    <Box>
                        <Tooltip title="Atualizar lista">
                            <IconButton
                                color="primary"
                                onClick={fetchUsuarios}
                                sx={{ mr: 1 }}
                            >
                                <RefreshIcon />
                            </IconButton>
                        </Tooltip>

                        <Button
                            variant="contained"
                            color="primary"
                            onClick={handleNovoUsuario}
                            startIcon={<AddIcon />}
                            sx={{
                                boxShadow: '0 4px 6px rgba(50, 50, 93, 0.11), 0 1px 3px rgba(0, 0, 0, 0.08)',
                                borderRadius: 2,
                                px: 2
                            }}
                        >
                            Novo Usuario
                        </Button>
                    </Box>
                </Box>

                <Divider sx={{ mb: 3 }} />

                {erro && (
                    <Alert
                        severity="error"
                        sx={{ mb: 3 }}
                        icon={<ErrorIcon fontSize="inherit" />}
                        action={
                            <Button color="inherit" size="small" onClick={fetchUsuarios}>
                                Tentar novamente
                            </Button>
                        }
                    >
                        {erro}
                    </Alert>
                )}

                {usuarios.length === 0 ? (
                    <Box
                        display="flex"
                        flexDirection="column"
                        alignItems="center"
                        justifyContent="center"
                        py={6}
                        bgcolor="#f5f5f5"
                        borderRadius={2}
                    >
                        <PersonIcon sx={{ fontSize: 64, color: '#bdbdbd', mb: 2 }} />
                        <Typography variant="h6" color="text.secondary" gutterBottom>
                            Nenhum usuario cadastrado
                        </Typography>
                        <Typography variant="body2" color="text.secondary" mb={2}>
                            Clique no botao acima para adicionar um novo usuario
                        </Typography>
                        <Button
                            variant="outlined"
                            color="primary"
                            onClick={handleNovoUsuario}
                            startIcon={<AddIcon />}
                        >
                            Adicionar Usuario
                        </Button>
                    </Box>
                ) : (
                    <TableContainer
                        component={Paper}
                        elevation={0}
                        sx={{
                            borderRadius: 2,
                            overflow: 'hidden',
                            border: '1px solid #e0e0e0'
                        }}
                    >
                        <Table>
                            <TableHead sx={{ bgcolor: '#f5f5f5' }}>
                                <TableRow>
                                    <TableCell sx={{ fontWeight: 'bold' }}>ID</TableCell>
                                    <TableCell sx={{ fontWeight: 'bold' }}>Nome</TableCell>
                                    <TableCell sx={{ fontWeight: 'bold' }}>Funcao</TableCell>
                                    <TableCell sx={{ fontWeight: 'bold' }}>Setor</TableCell>
                                    <TableCell sx={{ fontWeight: 'bold' }}>Ações</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {usuarios.map((usuario) => (
                                    <TableRow
                                        key={usuario.id}
                                        hover
                                        sx={{
                                            '&:nth-of-type(odd)': { backgroundColor: '#fafafa' },
                                            '&:last-child td, &:last-child th': { border: 0 },
                                            transition: 'all 0.2s',
                                            cursor: 'pointer',
                                            '&:hover': {
                                                backgroundColor: '#f0f4ff'
                                            }
                                        }}
                                    >
                                        <TableCell>{usuario.id}</TableCell>
                                        <TableCell sx={{ fontWeight: 500 }}>{usuario.nome}</TableCell>
                                        <TableCell>{renderFuncao(usuario.funcaoUsuario)}</TableCell>
                                        <TableCell>{renderSetor(usuario.setorUsuarioId)}</TableCell>
                                        <TableCell>
                                            <IconButton
                                                size="small"
                                                onClick={(e) => handleAbrirMenu(e, usuario)}
                                                aria-label="Opções da tarefa"
                                            >
                                                <Icon />
                                            </IconButton>
                                        </TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                )}

                <Box display="flex" justifyContent="flex-end" mt={3}>
                    <Typography variant="body2" color="text.secondary">
                        Total: {usuarios.length} usuario{usuarios.length !== 1 ? 's' : ''}
                    </Typography>
                </Box>
            </Paper>
            <Menu
                anchorEl={anchorEl}
                open={Boolean(anchorEl)}
                onClose={handleFecharMenu}
                keepMounted
                anchorOrigin={{
                    vertical: 'bottom',
                    horizontal: 'right',
                }}
                transformOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                }}
            >
                <MenuItem onClick={handleEditarUsuario}>
                    <EditIcon fontSize="small" sx={{ mr: 1 }} color="primary" />
                    Editar
                </MenuItem>
            </Menu>
        </Container>
    );
}

export default ListarUsuario;