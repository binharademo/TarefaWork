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
    IconButton,
    Tooltip,
    Divider,
    Menu,
    MenuItem,
    Dialog,
    DialogActions,
    DialogContent,
    DialogContentText,
    DialogTitle
} from '@mui/material';
import {
    CheckCircle as CheckCircleIcon,
    Warning as WarningIcon,
    Error as ErrorIcon,
    LowPriority as LowPriorityIcon,
    PriorityHigh as PriorityHighIcon,
    MoreVert as MoreVertIcon,
    Add as AddIcon,
    Refresh as RefreshIcon,
    Assignment as AssignmentIcon,
    Edit as EditIcon,
    Delete as DeleteIcon,
    Visibility as VisibilityIcon
} from '@mui/icons-material';

function Listar() {
    const [tarefas, setTarefas] = useState([]);
    const [usuarios, setUsuarios] = useState({});
    const [carregando, setCarregando] = useState(true);
    const [erro, setErro] = useState(null);
    const navigate = useNavigate();

    // Estados para gerenciar o menu de ações
    const [anchorEl, setAnchorEl] = useState(null);
    const [tarefaSelecionada, setTarefaSelecionada] = useState(null);

    // Estados para o diálogo de confirmação
    const [dialogoAberto, setDialogoAberto] = useState(false);
    const [carregandoExclusao, setCarregandoExclusao] = useState(false);

    const fetchUsuarios = async () => {
        try {
            const response = await fetch(`${import.meta.env.VITE_API_URL}/Usuario`);
            if (!response.ok) {
                throw new Error('Erro ao carregar usuários');
            }
            const data = await response.json();

            // Transformar o array em um objeto de lookup por ID para acesso rápido
            const usuariosMap = {};
            if (Array.isArray(data)) {
                data.forEach(usuario => {
                    usuariosMap[usuario.id] = usuario.nome;
                });
            } else if (data && data.id) {
                usuariosMap[data.id] = data.nome;
            }

            setUsuarios(usuariosMap);
            console.log("Usuários carregados:", usuariosMap);
        } catch (error) {
            console.error("Erro ao carregar usuários:", error);
        }
    };

    const fetchTarefas = async () => {
        setCarregando(true);
        setErro(null);

        try {
            const response = await fetch(`${import.meta.env.VITE_API_URL}/Tarefa`);
            if (!response.ok) {
                throw new Error('Erro ao carregar tarefas');
            }
            const data = await response.json();
            console.log("Dados recebidos:", data);
            setTarefas(Array.isArray(data) ? data : [data]);
        } catch (error) {
            setErro(error.message);
        } finally {
            setCarregando(false);
        }
    };

    useEffect(() => {
        // Primeiro carregamos os usuários e depois as tarefas
        const carregarDados = async () => {
            await fetchUsuarios();
            await fetchTarefas();
        };

        carregarDados();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const handleNovaTarefa = () => {
        navigate('/tarefa/cadastro');
    };

    const getNomeUsuario = (id) => {
        return usuarios[id] || `Usuário ${id}`;
    };

    const getStatusIcon = (status) => {
        switch (status) {
            case 2: // Concluído
                return <CheckCircleIcon color="success" />;
            case 1: // EmAndamento
                return <WarningIcon color="warning" />;
            case 0: // Pendente
                return <ErrorIcon color="error" />;
            default:
                return <ErrorIcon />;
        }
    };

    const getPrioridadeIcon = (prioridade) => {
        switch (prioridade) {
            case 0: // Baixa
                return <LowPriorityIcon color="info" />;
            case 1: // Média
                return <PriorityHighIcon />;
            case 2: // Alta
                return <PriorityHighIcon color="error" />;
            default:
                return <LowPriorityIcon />;
        }
    };

    const formatarData = (data) => {
        return new Date(data).toLocaleDateString('pt-BR');
    };

    const converterParaSegundos = (tempoStr) => {
        if (!tempoStr || typeof tempoStr !== 'string') return 0;
        const [hh, mm] = tempoStr.split(':').map(Number);
        if (isNaN(hh) || isNaN(mm)) return 0;
        return hh * 3600 + mm * 60;
    };

    const formatarTempo = (tempo) => {
        if (!tempo) return '00:00';
        const horas = Math.floor(tempo / 3600).toString().padStart(2, '0');
        const minutos = Math.floor((tempo % 3600) / 60).toString().padStart(2, '0');
        return `${horas}:${minutos}`;
    };

    // Handlers para o menu de ações
    const handleAbrirMenu = (event, tarefa) => {
        setAnchorEl(event.currentTarget);
        setTarefaSelecionada(tarefa);
    };

    const handleFecharMenu = () => {
        setAnchorEl(null);
    };

    const handleEditarTarefa = () => {
        if (tarefaSelecionada) {
            navigate(`/tarefa/editar/${tarefaSelecionada.id}`);
        }
        handleFecharMenu();
    };

    const handleVisualizarTarefa = () => {
        if (tarefaSelecionada) {
            navigate(`/tarefa/visualizar/${tarefaSelecionada.id}`);
        }
        handleFecharMenu();
        handleFecharMenu();
    };

    const handleConfirmarExclusao = () => {
        setDialogoAberto(true);
        handleFecharMenu();
    };

    const handleFecharDialogo = () => {
        setDialogoAberto(false);
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
                        Carregando tarefas...
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
                        <AssignmentIcon sx={{ fontSize: 32, mr: 2, color: '#3f51b5' }} />
                        <Typography variant="h4" component="h1" fontWeight="500" color="primary">
                            Lista de Tarefas
                        </Typography>
                    </Box>

                    <Box>
                        <Tooltip title="Atualizar lista">
                            <IconButton
                                color="primary"
                                onClick={async () => {
                                    await fetchUsuarios();
                                    await fetchTarefas();
                                }}
                                sx={{ mr: 1 }}
                            >
                                <RefreshIcon />
                            </IconButton>
                        </Tooltip>

                        <Button
                            variant="contained"
                            color="primary"
                            onClick={handleNovaTarefa}
                            startIcon={<AddIcon />}
                            sx={{
                                boxShadow: '0 4px 6px rgba(50, 50, 93, 0.11), 0 1px 3px rgba(0, 0, 0, 0.08)',
                                borderRadius: 2,
                                px: 2
                            }}
                        >
                            Nova Tarefa
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
                            <Button color="inherit" size="small" onClick={fetchTarefas}>
                                Tentar novamente
                            </Button>
                        }
                    >
                        {erro}
                    </Alert>
                )}

                {tarefas.length === 0 ? (
                    <Box
                        display="flex"
                        flexDirection="column"
                        alignItems="center"
                        justifyContent="center"
                        py={6}
                        bgcolor="#f5f5f5"
                        borderRadius={2}
                    >
                        <AssignmentIcon sx={{ fontSize: 64, color: '#bdbdbd', mb: 2 }} />
                        <Typography variant="h6" color="text.secondary" gutterBottom>
                            Nenhuma tarefa cadastrada
                        </Typography>
                        <Typography variant="body2" color="text.secondary" mb={2}>
                            Clique no botão acima para adicionar uma nova tarefa
                        </Typography>
                        <Button
                            variant="outlined"
                            color="primary"
                            onClick={handleNovaTarefa}
                            startIcon={<AddIcon />}
                        >
                            Adicionar Tarefa
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
                                    <TableCell sx={{ fontWeight: 'bold' }}>Status</TableCell>
                                    <TableCell sx={{ fontWeight: 'bold' }}>Título</TableCell>
                                    <TableCell sx={{ fontWeight: 'bold' }}>Prioridade</TableCell>
                                    <TableCell sx={{ fontWeight: 'bold' }}>Criador</TableCell>
                                    <TableCell sx={{ fontWeight: 'bold' }}>Responsável</TableCell>
                                    <TableCell sx={{ fontWeight: 'bold' }}>Prazo</TableCell>
                                    <TableCell sx={{ fontWeight: 'bold' }}>Tempo total</TableCell>
                                    <TableCell sx={{ fontWeight: 'bold' }}>Ações</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {tarefas.map((tarefa) => (
                                    <TableRow
                                        key={tarefa.id}
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
                                        <TableCell>
                                            <Tooltip title={['Pendente', 'Em Andamento', 'Concluído'][tarefa.status]}>
                                                {getStatusIcon(tarefa.status)}
                                            </Tooltip>
                                        </TableCell>
                                        <TableCell>
                                            <Typography fontWeight="medium">{tarefa.titulo}</Typography>
                                            <Typography variant="body2" color="text.secondary">
                                                {tarefa.descricao && tarefa.descricao.substring(0, 50)}
                                                {tarefa.descricao && tarefa.descricao.length > 50 ? '...' : ''}
                                            </Typography>
                                        </TableCell>
                                        <TableCell>
                                            <Tooltip title={['Baixa', 'Normal', 'Alta'][tarefa.prioridadeTarefa]}>
                                                {getPrioridadeIcon(tarefa.prioridadeTarefa)}
                                            </Tooltip>
                                        </TableCell>
                                        <TableCell>
                                            <Chip
                                                label={getNomeUsuario(tarefa.criadorId)}
                                                size="small"
                                                sx={{ fontWeight: 500 }}
                                            />
                                        </TableCell>
                                        <TableCell>
                                            <Chip
                                                label={getNomeUsuario(tarefa.responsavelId)}
                                                size="small"
                                                sx={{ fontWeight: 500 }}
                                            />
                                        </TableCell>
                                        <TableCell>
                                            <Chip
                                                label={formatarData(tarefa.prazo)}
                                                color={new Date(tarefa.prazo) < new Date() ? 'error' : 'default'}
                                                size="small"
                                            />
                                        </TableCell>
                                        <TableCell>
                                            {formatarTempo(converterParaSegundos(tarefa.tempoTotal))}
                                        </TableCell>
                                        <TableCell>
                                            <IconButton
                                                size="small"
                                                onClick={(e) => handleAbrirMenu(e, tarefa)}
                                                aria-label="Opções da tarefa"
                                            >
                                                <MoreVertIcon />
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
                        Total: {tarefas.length} tarefa{tarefas.length !== 1 ? 's' : ''}
                    </Typography>
                </Box>
            </Paper>

            {/* Menu de ações da tarefa */}
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
                <MenuItem onClick={handleEditarTarefa}>
                    <EditIcon fontSize="small" sx={{ mr: 1 }} color="primary" />
                    Editar
                </MenuItem>
                <MenuItem onClick={handleVisualizarTarefa}>
                    <VisibilityIcon fontSize="small" sx={{ mr: 1 }} color="action" />
                    Visualizar
                </MenuItem>
            </Menu>
        </Container>
    );
}

export default Listar;