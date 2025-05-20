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
    Tooltip,
    IconButton,
    Divider,
    Menu,
    MenuItem
} from '@mui/material';
import {
    Add as AddIcon,
    Refresh as RefreshIcon,
    Business as BusinessIcon,
    Error as ErrorIcon,
    MoreVert as Icon,
    Edit as EditIcon,
    Delete as DeleteIcon,
    Visibility as VisibilityIcon
} from '@mui/icons-material';

function ListarEmpresa() {
    const [empresas, setEmpresas] = useState([]);
    const [carregando, setCarregando] = useState(true);
    const [erro, setErro] = useState(null);
    const navigate = useNavigate();

    // Estados para gerenciar o menu de ações
    const [anchorEl, setAnchorEl] = useState(null);
    const [empresaSelecionada, setEmpresaSelecionada] = useState(null);

    const fetchEmpresas = async () => {
        setCarregando(true);
        setErro(null);

        try {
            const response = await fetch('http://localhost:53011/Empresa');
            if (!response.ok) {
                throw new Error('Erro ao carregar empresas');
            }
            const data = await response.json();
            console.log("Dados recebidos:", data);
            setEmpresas(Array.isArray(data) ? data : [data]);
        } catch (error) {
            setErro(error.message);
        } finally {
            setCarregando(false);
            console.log(empresas);
        }
    };

    useEffect(() => {
        fetchEmpresas();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    useEffect(() => {
        console.log("Estado empresas atualizado:", empresas);
    }, [empresas]);

    const handleNovaEmpresa = () => {
        navigate('/empresa/cadastro');
    };

    // Handlers para o menu de ações
    const handleAbrirMenu = (event, empresa) => {
        setAnchorEl(event.currentTarget);
        setEmpresaSelecionada(empresa);
    };

    const handleFecharMenu = () => {
        setAnchorEl(null);
    };

    const handleEditarEmpresa = () => {
        if (empresaSelecionada) {
            navigate(`/empresa/editar/${empresaSelecionada.id}`);
        }
        handleFecharMenu();
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
                        Carregando empresas...
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
                    background: 'linear-gradient(to right bottom, #ffffff, #f8f9fa)'
                }}
            >
                <Box display="flex" justifyContent="space-between" alignItems="center" mb={3}>
                    <Box display="flex" alignItems="center">
                        <BusinessIcon sx={{ fontSize: 32, mr: 2, color: '#3f51b5' }} />
                        <Typography variant="h4" component="h1" fontWeight="500" color="primary">
                            Lista de Empresas
                        </Typography>
                    </Box>

                    <Box>
                        <Tooltip title="Atualizar lista">
                            <IconButton
                                color="primary"
                                onClick={fetchEmpresas}
                                sx={{ mr: 1 }}
                            >
                                <RefreshIcon />
                            </IconButton>
                        </Tooltip>

                        <Button
                            variant="contained"
                            color="primary"
                            onClick={handleNovaEmpresa}
                            startIcon={<AddIcon />}
                            sx={{
                                boxShadow: '0 4px 6px rgba(50, 50, 93, 0.11), 0 1px 3px rgba(0, 0, 0, 0.08)',
                                borderRadius: 2,
                                px: 2
                            }}
                        >
                            Nova Empresa
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
                            <Button color="inherit" size="small" onClick={fetchEmpresas}>
                                Tentar novamente
                            </Button>
                        }
                    >
                        {erro}
                    </Alert>
                )}

                {empresas.length === 0 ? (
                    <Box
                        display="flex"
                        flexDirection="column"
                        alignItems="center"
                        justifyContent="center"
                        py={6}
                        bgcolor="#f5f5f5"
                        borderRadius={2}
                    >
                        <BusinessIcon sx={{ fontSize: 64, color: '#bdbdbd', mb: 2 }} />
                        <Typography variant="h6" color="text.secondary" gutterBottom>
                            Nenhuma empresa cadastrada
                        </Typography>
                        <Typography variant="body2" color="text.secondary" mb={2}>
                            Clique no botao acima para adicionar uma nova empresa
                        </Typography>
                        <Button
                            variant="outlined"
                            color="primary"
                            onClick={handleNovaEmpresa}
                            startIcon={<AddIcon />}
                        >
                            Adicionar Empresa
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
                                        <TableCell sx={{ fontWeight: 'bold' }}>CNPJ</TableCell>
                                        <TableCell sx={{ fontWeight: 'bold' }}></TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {empresas.map((empresa) => (
                                    <TableRow
                                        key={empresa.id}
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
                                        <TableCell>{empresa.id}</TableCell>
                                        <TableCell sx={{ fontWeight: 500 }}>{empresa.nome}</TableCell>
                                        <TableCell>{empresa.cnpj}</TableCell>
                                        <TableCell>
                                            <IconButton
                                                size="small"
                                                onClick={(e) => handleAbrirMenu(e, empresa)}
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
                        Total: {empresas.length} empresa{empresas.length !== 1 ? 's' : ''}
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
                <MenuItem onClick={handleEditarEmpresa}>
                    <EditIcon fontSize="small" sx={{ mr: 1 }} color="primary" />
                    Editar
                </MenuItem>
            </Menu>
        </Container>
    );
}

export default ListarEmpresa;