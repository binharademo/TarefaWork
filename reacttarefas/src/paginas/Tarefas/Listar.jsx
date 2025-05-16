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
    Divider
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
    Assignment as AssignmentIcon
} from '@mui/icons-material';

function Listar() {
    const [tarefas, setTarefas] = useState([]);
    const [carregando, setCarregando] = useState(true);
    const [erro, setErro] = useState(null);
    const navigate = useNavigate();

    const fetchTarefas = async () => {
        setCarregando(true);
        setErro(null);

        try {
            const response = await fetch('http://localhost:53011/Tarefa');
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
            console.log(tarefas);
        }
    };

    useEffect(() => {
        fetchTarefas();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    useEffect(() => {
        console.log("Estado tarefas atualizado:", tarefas);
    }, [tarefas]);

    const handleNovaTarefa = () => {
        navigate('/tarefa/cadastro');
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
                    background: 'linear-gradient(to right bottom, #ffffff, #f8f9fa)'
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
                                onClick={fetchTarefas}
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
                            Clique no botao acima para adicionar uma nova tarefa
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
                                    <TableCell sx={{ fontWeight: 'bold' }}>Titulo</TableCell>
                                    <TableCell sx={{ fontWeight: 'bold' }}>Prioridade</TableCell>
                                    <TableCell sx={{ fontWeight: 'bold' }}>Responsavel</TableCell>
                                    <TableCell sx={{ fontWeight: 'bold' }}>Prazo</TableCell>
                                    <TableCell sx={{ fontWeight: 'bold' }}>Tempo total</TableCell>
                                    <TableCell sx={{ fontWeight: 'bold' }}>Acoes</TableCell>
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
                                            <Tooltip title={['Concluído', 'Em Andamento', 'Pendente'][tarefa.status]}>
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
                                            <Tooltip title={['Baixa', 'Média', 'Alta'][tarefa.prioridadeTarefa]}>
                                                {getPrioridadeIcon(tarefa.prioridadeTarefa)}
                                            </Tooltip>
                                        </TableCell>
                                        <TableCell>
                                            <Chip
                                                label={`ID: ${tarefa.responsavelId}`}
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
                                            <IconButton size="small">
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
        </Container>
    );
}

export default Listar;