import React, { useState, useEffect } from 'react';
import {
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Paper,
    Typography,
    CircularProgress,
    Alert,
    Box,
    Chip,
    IconButton,
    Tooltip
} from '@mui/material';
import {
    CheckCircle as CheckCircleIcon,
    Warning as WarningIcon,
    Error as ErrorIcon,
    LowPriority as LowPriorityIcon,
    PriorityHigh as PriorityHighIcon,
    MoreVert as MoreVertIcon
} from '@mui/icons-material';

function Listar() {
    const [tarefas, setTarefas] = useState([]);
    const [carregando, setCarregando] = useState(true);
    const [erro, setErro] = useState(null);

    useEffect(() => {
        async function fetchTarefas() {
            try {
                const response = await fetch('http://localhost:53011/Tarefa'); // Ajuste a URL conforme necessário
                if (!response.ok) {
                    throw new Error('Erro ao carregar tarefas');
                }
                const data = await response.json();
                setTarefas(data);
            } catch (error) {
                setErro(error.message);
            } finally {
                setCarregando(false);
            }
        }

        fetchTarefas();
    }, []);

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

    const formatarTempo = (tempo) => {
        if (!tempo) return '00:00';
        const horas = Math.floor(tempo / 3600).toString().padStart(2, '0');
        const minutos = Math.floor((tempo % 3600) / 60).toString().padStart(2, '0');
        return `${horas}:${minutos}`;
    };

    if (carregando) {
        return (
            <Box display="flex" justifyContent="center" mt={4}>
                <CircularProgress />
            </Box>
        );
    }

    if (erro) {
        return (
            <Alert severity="error" sx={{ mt: 2 }}>
                {erro}
            </Alert>
        );
    }

    return (
        <Box sx={{ padding: 3 }}>
            <Typography variant="h4" component="h1" gutterBottom>
                Lista de Tarefas
            </Typography>

            {tarefas.length === 0 ? (
                <Typography variant="body1">Nenhuma tarefa encontrada.</Typography>
            ) : (
                <TableContainer component={Paper}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Status</TableCell>
                                <TableCell>Título</TableCell>
                                <TableCell>Prioridade</TableCell>
                                <TableCell>Responsável</TableCell>
                                <TableCell>Prazo</TableCell>
                                <TableCell>Tempo Estimado</TableCell>
                                <TableCell>Ações</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {tarefas.map((tarefa) => (
                                <TableRow key={tarefa.id}>
                                    <TableCell>
                                        <Tooltip title={['Concluído', 'Em Andamento', 'Pendente'][tarefa.status]}>
                                            {getStatusIcon(tarefa.status)}
                                        </Tooltip>
                                    </TableCell>
                                    <TableCell>
                                        <Typography fontWeight="medium">{tarefa.titulo}</Typography>
                                        <Typography variant="body2" color="text.secondary">
                                            {tarefa.descricao.substring(0, 50)}...
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
                                        {formatarTempo(tarefa.tempoTotal)}
                                    </TableCell>
                                    <TableCell>
                                        <IconButton>
                                            <MoreVertIcon />
                                        </IconButton>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            )}
        </Box>
    );
}

export default Listar;