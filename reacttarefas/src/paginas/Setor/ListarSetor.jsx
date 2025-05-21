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
    MenuItem,
    Chip
} from '@mui/material';
import {
    Add as AddIcon,
    Refresh as RefreshIcon,
    Apartment as ApartmentIcon,
    Error as ErrorIcon,
    MoreVert as Icon,
    Edit as EditIcon,
    Delete as DeleteIcon,
    Visibility as VisibilityIcon,
    Business as BusinessIcon
} from '@mui/icons-material';

function ListarSetor() {
    const [setores, setSetores] = useState([]);
    const [carregando, setCarregando] = useState(true);
    const [erro, setErro] = useState(null);
    const navigate = useNavigate();

    // Estados para gerenciar o menu de ações
    const [anchorEl, setAnchorEl] = useState(null);
    const [setorSelecionado, setSetorSelecionado] = useState(null);

    const fetchSetores = async () => {
        setCarregando(true);
        setErro(null);

        try {
            const response = await fetch('http://localhost:53011/Setor');
            if (!response.ok) {
                throw new Error('Erro ao carregar setores');
            }
            const data = await response.json();
            console.log("Dados recebidos:", data);
            setSetores(Array.isArray(data) ? data : [data]);
        } catch (error) {
            setErro(error.message);
        } finally {
            setCarregando(false);
            console.log(setores);
        }
    };

    useEffect(() => {
        fetchSetores();
    }, []);

    useEffect(() => {
        console.log("Estado setores atualizado:", setores);
    }, [setores]);

    const handleNovoSetor = () => {
        navigate('/setor/cadastro');
    };

    // Handlers para o menu de ações
    const handleAbrirMenu = (event, setor) => {
        setAnchorEl(event.currentTarget);
        setSetorSelecionado(setor);
    };

    const handleFecharMenu = () => {
        setAnchorEl(null);
    };

    const handleEditarSetor = () => {
        if (setorSelecionado) {
            navigate(`/setor/editar/${setorSelecionado.id}`);
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
                        Carregando setores...
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
                        <ApartmentIcon sx={{ fontSize: 32, mr: 2, color: '#3f51b5' }} />
                        <Typography variant="h4" component="h1" fontWeight="500" color="primary">
                            Lista de Setores
                        </Typography>
                    </Box>

                    <Box>
                        <Tooltip title="Atualizar lista">
                            <IconButton
                                color="primary"
                                onClick={fetchSetores}
                                sx={{ mr: 1 }}
                            >
                                <RefreshIcon />
                            </IconButton>
                        </Tooltip>

                        <Button
                            variant="contained"
                            color="primary"
                            onClick={handleNovoSetor}
                            startIcon={<AddIcon />}
                            sx={{
                                boxShadow: '0 4px 6px rgba(50, 50, 93, 0.11), 0 1px 3px rgba(0, 0, 0, 0.08)',
                                borderRadius: 2,
                                px: 2
                            }}
                        >
                            Novo Setor
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
                            <Button color="inherit" size="small" onClick={fetchSetores}>
                                Tentar novamente
                            </Button>
                        }
                    >
                        {erro}
                    </Alert>
                )}

                {setores.length === 0 ? (
                    <Box
                        display="flex"
                        flexDirection="column"
                        alignItems="center"
                        justifyContent="center"
                        py={6}
                        bgcolor="#f5f5f5"
                        borderRadius={2}
                    >
                        <ApartmentIcon sx={{ fontSize: 64, color: '#bdbdbd', mb: 2 }} />
                        <Typography variant="h6" color="text.secondary" gutterBottom>
                            Nenhum setor cadastrado
                        </Typography>
                        <Typography variant="body2" color="text.secondary" mb={2}>
                            Clique no botão acima para adicionar um novo setor
                        </Typography>
                        <Button
                            variant="outlined"
                            color="primary"
                            onClick={handleNovoSetor}
                            startIcon={<AddIcon />}
                        >
                            Adicionar Setor
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
                                    <TableCell sx={{ fontWeight: 'bold' }}>Status</TableCell>
                                    <TableCell sx={{ fontWeight: 'bold' }}>Empresa</TableCell>
                                    <TableCell sx={{ fontWeight: 'bold' }}></TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {setores.map((setor) => (
                                    <TableRow
                                        key={setor.id}
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
                                        <TableCell>{setor.id}</TableCell>
                                        <TableCell sx={{ fontWeight: 500 }}>{setor.nome}</TableCell>
                                        <TableCell>
                                            <Chip
                                                label={setor.status ? "Ativo" : "Inativo"}
                                                color={setor.status ? "success" : "default"}
                                                size="small"
                                                variant="outlined"
                                            />
                                        </TableCell>
                                        <TableCell>
                                            <Box display="flex" alignItems="center">
                                                <BusinessIcon fontSize="small" sx={{ mr: 1, color: '#757575' }} />
                                                {setor.empresa?.nome || "N/A"}
                                            </Box>
                                        </TableCell>
                                        <TableCell>
                                            <IconButton
                                                size="small"
                                                onClick={(e) => handleAbrirMenu(e, setor)}
                                                aria-label="Opções do setor"
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
                        Total: {setores.length} setor{setores.length !== 1 ? 'es' : ''}
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
                <MenuItem onClick={handleEditarSetor}>
                    <EditIcon fontSize="small" sx={{ mr: 1 }} color="primary" />
                    Editar
                </MenuItem>
            </Menu>
        </Container>
    );
}

export default ListarSetor;