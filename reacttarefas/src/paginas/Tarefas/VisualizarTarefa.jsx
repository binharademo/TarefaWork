import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import {
    Container,
    Paper,
    Typography,
    Box,
    Grid,
    Divider,
    Chip,
    Button,
    CircularProgress,
    Alert,
    IconButton,
    Tooltip,
    List,
    ListItem,
    ListItemText,
    Card,
    CardContent
} from '@mui/material';
import {
    CheckCircle as CheckCircleIcon,
    Warning as WarningIcon,
    Error as ErrorIcon,
    LowPriority as LowPriorityIcon,
    PriorityHigh as PriorityHighIcon,
    ArrowBack as ArrowBackIcon,
    Assignment as AssignmentIcon,
    Person as PersonIcon,
    Today as TodayIcon,
    AccessTime as AccessTimeIcon,
    Edit as EditIcon,
    CalendarToday as CalendarTodayIcon
} from '@mui/icons-material';

function VisualizarTarefa() {
    const { id } = useParams();
    const navigate = useNavigate();
    const [tarefa, setTarefa] = useState(null);
    const [usuarios, setUsuarios] = useState({});
    const [carregando, setCarregando] = useState(true);
    const [erro, setErro] = useState(null);

    useEffect(() => {
        const fetchUsuarios = async () => {
            try {
                const response = await fetch('http://localhost:53011/Usuario');
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
            } catch (error) {
                console.error("Erro ao carregar usuários:", error);
            }
        };

        const fetchTarefa = async () => {
            setCarregando(true);
            setErro(null);

            try {
                const response = await fetch(`http://localhost:53011/Tarefa/${id}`);
                if (!response.ok) {
                    throw new Error('Erro ao carregar detalhes da tarefa');
                }
                const data = await response.json();
                setTarefa(data);
            } catch (error) {
                setErro(error.message);
            } finally {
                setCarregando(false);
            }
        };

        fetchUsuarios();
        fetchTarefa();
    }, [id]);

    const getNomeUsuario = (id) => {
        return usuarios[id] || `Usuário ${id}`;
    };

    const getStatusLabel = (status) => {
        const statusLabels = ['Concluído', 'Em Andamento', 'Pendente'];
        return statusLabels[status] || 'Desconhecido';
    };

    const getStatusIcon = (status) => {
        switch (status) {
            case 0: // Concluído
                return <CheckCircleIcon color="success" />;
            case 1: // EmAndamento
                return <WarningIcon color="warning" />;
            case 2: // Pendente
                return <ErrorIcon color="error" />;
            default:
                return <ErrorIcon />;
        }
    };

    const getStatusColor = (status) => {
        switch (status) {
            case 0: // Concluído
                return 'success';
            case 1: // EmAndamento
                return 'warning';
            case 2: // Pendente
                return 'error';
            default:
                return 'default';
        }
    };

    const getPrioridadeLabel = (prioridade) => {
        const prioridadeLabels = ['Baixa', 'Média', 'Alta'];
        return prioridadeLabels[prioridade] || 'Desconhecida';
    };

    const getPrioridadeIcon = (prioridade) => {
        switch (prioridade) {
            case 0: // Baixa
                return <LowPriorityIcon color="info" />;
            case 1: // Média
                return <PriorityHighIcon color="warning" />;
            case 2: // Alta
                return <PriorityHighIcon color="error" />;
            default:
                return <LowPriorityIcon />;
        }
    };

    const formatarData = (data) => {
        return new Date(data).toLocaleDateString('pt-BR');
    };

    const formatarDataCompleta = (data) => {
        return new Date(data).toLocaleString('pt-BR');
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

    const handleVoltar = () => {
        navigate('/tarefa/listar');
    };

    const handleEditar = () => {
        navigate(`/tarefa/editar/${id}`);
    };

    if (carregando) {
        return (
            <Container maxWidth="md">
                <Box
                    display="flex"
                    flexDirection="column"
                    alignItems="center"
                    justifyContent="center"
                    height="50vh"
                >
                    <CircularProgress size={60} thickness={4} />
                    <Typography variant="h6" mt={2} color="text.secondary">
                        Carregando detalhes da tarefa...
                    </Typography>
                </Box>
            </Container>
        );
    }

    if (erro) {
        return (
            <Container maxWidth="md" sx={{ mt: 4 }}>
                <Alert
                    severity="error"
                    action={
                        <Button color="inherit" size="small" onClick={handleVoltar}>
                            Voltar
                        </Button>
                    }
                >
                    {erro}
                </Alert>
            </Container>
        );
    }

    if (!tarefa) {
        return (
            <Container maxWidth="md" sx={{ mt: 4 }}>
                <Alert
                    severity="warning"
                    action={
                        <Button color="inherit" size="small" onClick={handleVoltar}>
                            Voltar
                        </Button>
                    }
                >
                    Tarefa não encontrada
                </Alert>
            </Container>
        );
    }

    const isPrazoVencido = new Date(tarefa.prazo) < new Date();

    return (
        <Container maxWidth="md" sx={{ mt: 4, mb: 8 }}>
            <Box mb={2} display="flex" alignItems="center">
                <Tooltip title="Voltar para a lista">
                    <IconButton onClick={handleVoltar} sx={{ mr: 1 }}>
                        <ArrowBackIcon />
                    </IconButton>
                </Tooltip>
                <Typography variant="h5" component="h1">
                    Detalhes da Tarefa
                </Typography>
            </Box>

            <Paper
                elevation={3}
                sx={{
                    p: 3,
                    borderRadius: 2,
                    background: 'linear-gradient(to right bottom, #ffffff, #f8f9fa)'
                }}
            >
                <Box display="flex" justifyContent="space-between" alignItems="flex-start" mb={3}>
                    <Box display="flex" alignItems="center">
                        <AssignmentIcon sx={{ fontSize: 40, mr: 2, color: '#3f51b5' }} />
                        <Box>
                            <Typography variant="h4" fontWeight="500">
                                {tarefa.titulo}
                            </Typography>
                            <Box display="flex" alignItems="center" mt={1}>
                                <Chip
                                    icon={getStatusIcon(tarefa.status)}
                                    label={getStatusLabel(tarefa.status)}
                                    color={getStatusColor(tarefa.status)}
                                    sx={{ mr: 1 }}
                                />
                                <Chip
                                    icon={getPrioridadeIcon(tarefa.prioridadeTarefa)}
                                    label={getPrioridadeLabel(tarefa.prioridadeTarefa)}
                                    color={tarefa.prioridadeTarefa === 2 ? 'error' : tarefa.prioridadeTarefa === 1 ? 'warning' : 'info'}
                                />
                            </Box>
                        </Box>
                    </Box>

                    <Button
                        variant="outlined"
                        color="primary"
                        startIcon={<EditIcon />}
                        onClick={handleEditar}
                    >
                        Editar Tarefa
                    </Button>
                </Box>

                <Divider sx={{ mb: 3 }} />

                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        <Card variant="outlined" sx={{ mb: 3, bgcolor: '#fafafa' }}>
                            <CardContent>
                                <Typography variant="h6" gutterBottom>
                                    Descrição
                                </Typography>
                                <Typography variant="body1" sx={{ whiteSpace: 'pre-line' }}>
                                    {tarefa.descricao || "Sem descrição."}
                                </Typography>
                            </CardContent>
                        </Card>
                    </Grid>

                    <Grid item xs={12} md={6}>
                        <Box mb={3}>
                            <Typography variant="h6" gutterBottom>
                                Informações da Tarefa
                            </Typography>
                            <List disablePadding>
                                <ListItem divider>
                                    <ListItemText
                                        primary="ID da Tarefa"
                                        secondary={tarefa.id}
                                    />
                                </ListItem>
                                <ListItem divider>
                                    <PersonIcon sx={{ mr: 2, color: '#3f51b5' }} />
                                    <ListItemText
                                        primary="Criado por"
                                        secondary={getNomeUsuario(tarefa.criadorId)}
                                    />
                                </ListItem>
                                <ListItem divider>
                                    <PersonIcon sx={{ mr: 2, color: '#f50057' }} />
                                    <ListItemText
                                        primary="Responsável"
                                        secondary={getNomeUsuario(tarefa.responsavelId)}
                                    />
                                </ListItem>
                                <ListItem divider>
                                    <CalendarTodayIcon sx={{ mr: 2, color: '#4caf50' }} />
                                    <ListItemText
                                        primary="Data de Criação"
                                        secondary={formatarDataCompleta(tarefa.dataCriacao)}
                                    />
                                </ListItem>
                                <ListItem divider>
                                    <TodayIcon sx={{ mr: 2, color: isPrazoVencido ? '#f44336' : '#2196f3' }} />
                                    <ListItemText
                                        primary="Prazo"
                                        secondary={
                                            <>
                                                {formatarDataCompleta(tarefa.prazo)}
                                                {isPrazoVencido && (
                                                    <Typography variant="caption" color="error" component="div">
                                                        Prazo vencido
                                                    </Typography>
                                                )}
                                            </>
                                        }
                                    />
                                </ListItem>
                                <ListItem>
                                    <AccessTimeIcon sx={{ mr: 2, color: '#ff9800' }} />
                                    <ListItemText
                                        primary="Tempo Total"
                                        secondary={formatarTempo(converterParaSegundos(tarefa.tempoTotal))}
                                    />
                                </ListItem>
                            </List>
                        </Box>
                    </Grid>
                </Grid>

                <Divider sx={{ my: 3 }} />

                <Box display="flex" justifyContent="space-between">
                    <Button
                        variant="outlined"
                        startIcon={<ArrowBackIcon />}
                        onClick={handleVoltar}
                    >
                        Voltar para a Lista
                    </Button>
                </Box>
            </Paper>
        </Container>
    );
}

export default VisualizarTarefa;