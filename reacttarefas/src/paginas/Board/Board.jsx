import React, { useState, useEffect } from 'react';
// Importamos versão estritamente compatível com ES6 para evitar problemas
import { DragDropContext, Droppable, Draggable } from '@hello-pangea/dnd';
import {
    Paper,
    Typography,
    Card,
    CardContent,
    Box,
    Grid,
    CircularProgress,
    Container,
    Snackbar,
    Alert,
    IconButton
} from '@mui/material';
import {
    CheckCircle as CheckCircleIcon,
    Warning as WarningIcon,
    Error as ErrorIcon,
    LowPriority as LowPriorityIcon,
    PriorityHigh as PriorityHighIcon,
    MoreVert as MoreVertIcon,
    DragIndicator as DragIndicatorIcon
} from '@mui/icons-material';

// Estilos para as colunas por status
const columnStyles = {
    0: { borderTop: '4px solid #4caf50' },   // Concluído
    1: { borderTop: '4px solid #ff9800' },   // Em Andamento
    2: { borderTop: '4px solid #f44336' }    // Pendente
};

const borderStyles = {
    0: { border: '1px solid #4caf50' },   // Concluído
    1: { border: '1px solid #ff9800' },   // Em Andamento
    2: { border: '1px solid #f44336' }    // Pendente
};

const statusLabels = {
    0: 'Concluído',
    1: 'Em Andamento',
    2: 'Pendente'
};

export default function BoardTarefas() {
    const [columns, setColumns] = useState({ 0: [], 1: [], 2: [] });
    const [loading, setLoading] = useState(true);
    const [notification, setNotification] = useState({ open: false, message: '', severity: 'info' });

    // Carrega tarefas e agrupa por status
    useEffect(() => {
        fetchTarefas();
    }, []);

    const fetchTarefas = () => {
        setLoading(true);
        fetch('http://localhost:53011/Tarefa')
            .then((res) => {
                if (!res.ok) throw new Error('Falha ao carregar tarefas');
                return res.json();
            })
            .then((data) => {
                const grouped = { 0: [], 1: [], 2: [] };
                data.forEach((tarefa) => {
                    // Certifique-se de que o status é um número
                    const status = Number(tarefa.status);
                    if (grouped[status] !== undefined) {
                        grouped[status].push(tarefa);
                    }
                });
                setColumns(grouped);
            })
            .catch((error) => {
                console.error('Erro ao carregar tarefas:', error);
                showNotification('Erro ao carregar tarefas', 'error');
            })
            .finally(() => setLoading(false));
    };

    // Função para mostrar notificações
    const showNotification = (message, severity = 'info') => {
        setNotification({ open: true, message, severity });
    };

    const closeNotification = () => {
        setNotification({ ...notification, open: false });
    };

    // Atualiza tarefa no backend
    const updateTarefa = async (tarefa) => {
        try {
            const response = await fetch(`http://localhost:53011/Tarefa/${tarefa.id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(tarefa)
            });

            if (!response.ok) {
                throw new Error('Falha ao atualizar tarefa');
            }

            showNotification(`Tarefa "${tarefa.titulo}" movida para ${statusLabels[tarefa.status]}`, 'success');
            return true;
        } catch (error) {
            console.error('Erro ao atualizar tarefa:', error);
            showNotification('Erro ao atualizar status da tarefa', 'error');

            // Recarregar tarefas em caso de erro para garantir sincronização
            fetchTarefas();
            return false;
        }
    };

    // Ícones de status e prioridade
    const getStatusIcon = (status) => {
        if (status === 0) return <CheckCircleIcon color="success" fontSize="small" />;
        if (status === 1) return <WarningIcon color="warning" fontSize="small" />;
        return <ErrorIcon color="error" fontSize="small" />;
    };

    const getPrioridadeIcon = (p) => {
        if (p === 0) return <PriorityHighIcon fontSize="small" color="success" />;
        if (p === 1) return <PriorityHighIcon fontSize="small" color="warning" />;
        if (p === 2) return <PriorityHighIcon fontSize="small" color="danger" />;
        return <PriorityHighIcon fontSize="small" color="error" />;
    };

    // Formatação de datas e tempos
    const formatarData = (d) => {
        try {
            return new Date(d).toLocaleDateString('pt-BR');
        } catch (e) {
            return 'Data inválida';
        }
    };

    const converterParaSegundos = (str) => {
        if (!str) return 0;
        try {
            const [h = 0, m = 0, ss = 0] = str.split(':').map(Number);
            return h * 3600 + m * 60 + ss;
        } catch (e) {
            return 0;
        }
    };

    const formatarTempo = (s) => {
        const h = Math.floor(s / 3600);
        const m = Math.floor((s % 3600) / 60);
        const ss = s % 60;
        return [h, m, ss]
            .map((n) => n.toString().padStart(2, '0'))
            .join(':');
    };

    // Handle drag & drop
    const handleDragEnd = async (result) => {
        const { source, destination } = result;

        // Se não houver destino (tarefa solta fora de coluna) ou
        // se a tarefa for solta na mesma posição, não faz nada
        if (!destination ||
            (source.droppableId === destination.droppableId &&
                source.index === destination.index)) {
            return;
        }

        try {
            const srcId = source.droppableId;
            const dstId = destination.droppableId;

            // Cria cópias das listas para manipulação
            const srcList = [...columns[srcId]];
            const dstList = srcId === dstId ? srcList : [...columns[dstId]];

            // Remove da origem
            const [movedTask] = srcList.splice(source.index, 1);

            // Cria uma cópia da tarefa com o status atualizado
            const updatedTask = {
                ...movedTask,
                status: parseInt(dstId, 10)
            };

            // Insere no destino
            dstList.splice(destination.index, 0, updatedTask);

            // Atualiza estado de forma otimista
            const newColumns = {
                ...columns,
                [srcId]: srcList,
                [dstId]: dstList
            };

            setColumns(newColumns);

            // Feedback visual imediato
            showNotification(`Movendo "${updatedTask.titulo}" para ${statusLabels[updatedTask.status]}...`, 'info');

            // Tenta atualizar no backend
            const response = await fetch(`http://localhost:53011/Tarefa/${updatedTask.id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(updatedTask)
            });

            if (!response.ok) {
                throw new Error('Falha ao atualizar tarefa');
            }

            showNotification(`Tarefa "${updatedTask.titulo}" movida para ${statusLabels[updatedTask.status]}`, 'success');
        } catch (error) {
            console.error('Erro ao atualizar tarefa:', error);
            showNotification('Erro ao atualizar status da tarefa. Recarregando...', 'error');

            // Recarregar tarefas em caso de erro para garantir sincronização
            setTimeout(fetchTarefas, 1000); // Pequeno atraso para melhor UX
        }
    };

    if (loading) {
        return (
            <Container>
                <Box display="flex" justifyContent="center" alignItems="center" height="80vh">
                    <CircularProgress size={60} />
                </Box>
            </Container>
        );
    }

    return (
        <Container maxWidth="xl" sx={{ mt: 4, mb: 4, backgroundColor: 'white', padding: '20px 50px 20px 50px', borderRadius: 5, border: '1px solid #4E71FF' }}>
            <Box sx={{ mb: 3, display: 'flex', alignItems: 'center' }}>
                <Typography variant="h4" component="h1" fontWeight="500" color="#4E71FF">
                    Board Tarefas
                </Typography>
            </Box>
            <DragDropContext onDragEnd={handleDragEnd}>
                <Grid container spacing={5} wrap="nowrap" sx={{ overflowX: 'auto' }}>
                    {[2, 1, 0].map((status) => (
                        <Grid item key={status} sx={{ minWidth: 280 }}>
                            <Droppable droppableId={String(status)}>
                                {(provided, snapshot) => (
                                    <Paper
                                        ref={provided.innerRef}
                                        {...provided.droppableProps}
                                        sx={{
                                            p: 2,
                                            mb: '10px',
                                            ...borderStyles[status],
                                            backgroundColor: snapshot.isDraggingOver ? '#f0f0f0' : 'white',
                                            transition: 'background-color 0.2s ease',
                                            ...columnStyles[status]
                                        }}
                                    >
                                        <Typography variant="h6" textAlign="center" gutterBottom>
                                            {statusLabels[status]} ({columns[status].length})
                                        </Typography>

                                        {columns[status].map((tarefa, index) => (
                                            <Draggable
                                                key={tarefa.id.toString()}
                                                draggableId={tarefa.id.toString()}
                                                index={index}
                                            >
                                                {(provided, snapshot) => (
                                                    <div
                                                        ref={provided.innerRef}
                                                        {...provided.draggableProps}
                                                        style={{
                                                            ...provided.draggableProps.style,
                                                            marginBottom: 16,
                                                            opacity: snapshot.isDragging ? 0.8 : 1,
                                                            boxShadow: snapshot.isDragging ? '0 0 10px rgba(0,0,0,0.3)' : 'none'
                                                        }}
                                                    >
                                                        <Card sx={{
                                                            cursor: 'grab',
                                                            backgroundColor: snapshot.isDragging ? '#f5f5f5' : 'white',
                                                            transition: 'all 0.2s ease'
                                                        }}>
                                                            <CardContent>
                                                                <Box display="flex" justifyContent="space-between" alignItems="center">
                                                                    <Box display="flex" alignItems="center" width="100%">
                                                                        <Box {...provided.dragHandleProps} sx={{ mr: 1, cursor: 'grab' }}>
                                                                            <DragIndicatorIcon fontSize="small" color="action" />
                                                                        </Box>
                                                                        <Typography fontWeight={500} sx={{ flex: 1 }}>
                                                                            {tarefa.titulo}
                                                                        </Typography>
                                                                        <IconButton size="small">
                                                                            <MoreVertIcon fontSize="small" />
                                                                        </IconButton>
                                                                    </Box>
                                                                </Box>
                                                                <Typography variant="body2" mt={1} mb={2}>
                                                                    {tarefa.descricao?.slice(0, 80)}
                                                                    {tarefa.descricao?.length > 80 && '...'}
                                                                </Typography>
                                                                <Box display="flex" gap={1} mb={1}>
                                                                    {getStatusIcon(tarefa.status)}
                                                                    {getPrioridadeIcon(tarefa.prioridadeTarefa)}
                                                                </Box>
                                                                <Box display="flex" justifyContent="space-between">
                                                                    <Typography variant="caption">
                                                                        Prazo: {formatarData(tarefa.prazo)}
                                                                    </Typography>
                                                                    <Typography variant="caption">
                                                                        {formatarTempo(converterParaSegundos(tarefa.tempoTotal))}
                                                                    </Typography>
                                                                </Box>
                                                            </CardContent>
                                                        </Card>
                                                    </div>
                                                )}
                                            </Draggable>
                                        ))}

                                        {provided.placeholder}
                                        {columns[status].length === 0 && (
                                            <Typography variant="body2" color="text.secondary" textAlign="center" sx={{ mt: 2 }}>
                                                Nenhuma tarefa
                                            </Typography>
                                        )}
                                    </Paper>
                                )}
                            </Droppable>
                        </Grid>
                    ))}
                </Grid>
            </DragDropContext>

            {/* Notificação */}
            <Snackbar
                open={notification.open}
                autoHideDuration={4000}
                onClose={closeNotification}
                anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
            >
                <Alert
                    onClose={closeNotification}
                    severity={notification.severity}
                    sx={{ width: '100%' }}
                >
                    {notification.message}
                </Alert>
            </Snackbar>
        </Container>
    );
}