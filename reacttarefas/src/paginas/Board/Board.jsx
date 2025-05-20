import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
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
    IconButton,
    Menu,
    MenuItem,
    ListItemIcon,
    useTheme,
    styled,
    alpha
} from '@mui/material';
import {
    CheckCircle as CheckCircleIcon,
    Warning as WarningIcon,
    Error as ErrorIcon,
    PriorityHigh as PriorityHighIcon,
    MoreVert as MoreVertIcon,
    DragIndicator as DragIndicatorIcon,
    Visibility as VisibilityIcon,
    Edit as EditIcon
} from '@mui/icons-material';

const STATUS_COLORS = {
    0: 'success', // Concluído
    1: 'warning', // Em Andamento
    2: 'error'    // Pendente
};

const StyledColumn = styled(Paper)(({ theme, status }) => ({
    padding: theme.spacing(2),
    minHeight: 500,
    borderRadius: theme.shape.borderRadius * 2,
    backgroundColor: theme.palette.background.paper,
    borderTop: `4px solid ${theme.palette[STATUS_COLORS[status]].main}`,
    transition: 'background-color 0.3s ease',
    '&:hover': {
        backgroundColor: theme.palette.action.hover
    }
}));

const StyledCard = styled(Card)(({ theme, isdragging, status }) => ({
    cursor: 'grab',
    borderRadius: theme.shape.borderRadius * 2,
    boxShadow: isdragging ? theme.shadows[6] : theme.shadows[1],
    transform: isdragging ? 'scale(1.02)' : 'scale(1)',
    transition: 'all 0.2s ease-in-out',
    backgroundColor: alpha(theme.palette[STATUS_COLORS[status]].main, 0.1),
    borderLeft: `4px solid ${theme.palette[STATUS_COLORS[status]].main}`,
    '&:hover': {
        boxShadow: theme.shadows[4]
    }
}));

export default function BoardTarefas() {
    const theme = useTheme();
    const navigate = useNavigate();
    const [columns, setColumns] = useState({ 0: [], 1: [], 2: [] });
    const [loading, setLoading] = useState(true);
    const [notification, setNotification] = useState({ open: false, message: '', severity: 'info' });
    const [anchorEl, setAnchorEl] = useState(null);
    const [selectedTaskId, setSelectedTaskId] = useState(null);
    const menuOpen = Boolean(anchorEl);

    useEffect(() => { fetchTarefas(); }, []);

    const fetchTarefas = () => {
        setLoading(true);
        fetch('http://localhost:53011/Tarefa')
            .then(res => { if (!res.ok) throw new Error(); return res.json(); })
            .then(data => {
                const grouped = { 0: [], 1: [], 2: [] };
                data.forEach(t => grouped[Number(t.status)]?.push(t));
                setColumns(grouped);
            })
            .catch(() => showNotification('Erro ao carregar tarefas', 'error'))
            .finally(() => setLoading(false));
    };

    const showNotification = (message, severity = 'info') => setNotification({ open: true, message, severity });
    const closeNotification = () => setNotification(prev => ({ ...prev, open: false }));

    const statusLabels = { 0: 'Concluído', 1: 'Em Andamento', 2: 'Pendente' };


    const converterParaSegundos = str => {
        if (!str) return 0;
        const [h = 0, m = 0, s = 0] = str.split(':').map(Number);
        return h * 3600 + m * 60 + s;
    };

    const formatarData = d => new Date(d).toLocaleDateString('pt-BR');
    const formatarTempo = s => {
        const h = Math.floor(s / 3600);
        const m = Math.floor((s % 3600) / 60);
        const ss = s % 60;
        return [h, m, ss].map(n => n.toString().padStart(2, '0')).join(':');
    };

    // Handle menu open
    const handleMenuOpen = (event, taskId) => {
        event.stopPropagation();
        setSelectedTaskId(taskId);
        setAnchorEl(event.currentTarget);
    };
    const handleMenuClose = () => {
        setAnchorEl(null);
        setSelectedTaskId(null);
    };

    // Navigation
    const handleVisualizar = () => {
        navigate(`/tarefa/visualizar/${selectedTaskId}`);
        handleMenuClose();
    };
    const handleEditar = () => {
        navigate(`/tarefa/editar/${selectedTaskId}`);
        handleMenuClose();
    };

    const handleDragEnd = async result => {
        const { source, destination } = result;
        if (!destination || (source.droppableId === destination.droppableId && source.index === destination.index)) return;

        const srcId = source.droppableId;
        const dstId = destination.droppableId;
        const srcList = Array.from(columns[srcId]);
        const dstList = Array.from(columns[dstId]);
        const [moved] = srcList.splice(source.index, 1);
        const updated = { ...moved, status: Number(dstId) };
        dstList.splice(destination.index, 0, updated);
        setColumns({ ...columns, [srcId]: srcList, [dstId]: dstList });

        showNotification(`Movendo "${updated.titulo}" para ${statusLabels[updated.status]}...`, 'info');
        try {
            const res = await fetch(`http://localhost:53011/Tarefa/${updated.id}`, {
                method: 'PUT', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(updated)
            });
            if (!res.ok) throw new Error();
            showNotification(`Tarefa "${updated.titulo}" movida!`, 'success');
        } catch {
            showNotification('Erro ao atualizar. Sincronizando...', 'error');
            setTimeout(fetchTarefas, 1000);
        }
    };

    if (loading) return (
        <Container>
            <Box display="flex" justifyContent="center" alignItems="center" height="80vh">
                <CircularProgress size={60} />
            </Box>
        </Container>
    );

    return (
        <Container maxWidth="xl" sx={{ mt: 4, mb: 4 }}>
            <DragDropContext onDragEnd={handleDragEnd}>
                <Grid container spacing={3} wrap="nowrap" sx={{ overflowX: 'auto' }}>
                    {[2, 1, 0].map(status => (
                        <Grid item key={status} sx={{ flex: '0 0 30%', minWidth: 300 }}>
                            <Droppable droppableId={String(status)}>
                                {(provided, snapshot) => (
                                    <StyledColumn
                                        elevation={snapshot.isDraggingOver ? 4 : 1}
                                        status={status}
                                        ref={provided.innerRef}
                                        {...provided.droppableProps}
                                    >
                                        <Typography variant="h6" align="center" gutterBottom sx={{ color: theme.palette[STATUS_COLORS[status]].dark }}>
                                            {statusLabels[status]} ({columns[status].length})
                                        </Typography>
                                        {columns[status].map((t, idx) => {
                                            const expired = new Date(t.prazo) < new Date();
                                            return (
                                                <Draggable key={t.id} draggableId={String(t.id)} index={idx}>
                                                    {(prov, snap) => (
                                                        <Box
                                                            ref={prov.innerRef}
                                                            {...prov.draggableProps}
                                                            {...prov.dragHandleProps}
                                                            sx={{ mb: 2 }}
                                                        >
                                                            <StyledCard isdragging={snap.isDragging ? 1 : 0} status={t.status}>
                                                                <CardContent>
                                                                    <Box display="flex" justifyContent="space-between" alignItems="center">
                                                                        <Typography fontWeight={600}>{t.titulo}</Typography>
                                                                        <IconButton size="small" onClick={e => handleMenuOpen(e, t.id)}>
                                                                            <MoreVertIcon />
                                                                        </IconButton>
                                                                    </Box>
                                                                    <Typography variant="body2" mt={1} mb={1} sx={{ color: theme.palette.text.secondary }}>
                                                                        {t.descricao?.slice(0, 80)}{t.descricao?.length > 80 && '...'}
                                                                    </Typography>
                                                                    <Box display="flex" alignItems="center" gap={1} mb={1}>
                                                                        {t.status === 0
                                                                            ? <CheckCircleIcon color="success" />
                                                                            : t.status === 1
                                                                                ? <WarningIcon color="warning" />
                                                                                : <ErrorIcon color="error" />}
                                                                        <PriorityHighIcon color={
                                                                            t.prioridadeTarefa === 0 ? "success" :
                                                                                t.prioridadeTarefa === 1 ? "warning" : "error"
                                                                        } />
                                                                    </Box>
                                                                    <Box display="flex" justifyContent="space-between" alignItems="center">
                                                                        <Box
                                                                            sx={{
                                                                                px: 1,
                                                                                py: 0.25,
                                                                                borderRadius: 1,
                                                                                backgroundColor: expired
                                                                                    ? alpha(theme.palette.error.main, 0.2)
                                                                                    : 'transparent'
                                                                            }}
                                                                        >
                                                                            <Typography variant="caption" sx={{ color: expired ? theme.palette.error.dark : 'inherit' }}>
                                                                                Prazo: {formatarData(t.prazo)}
                                                                            </Typography>
                                                                        </Box>
                                                                        <Typography variant="caption">
                                                                            {formatarTempo(converterParaSegundos(t.tempoTotal))}
                                                                        </Typography>
                                                                    </Box>
                                                                </CardContent>
                                                            </StyledCard>
                                                        </Box>
                                                    )}
                                                </Draggable>
                                            );
                                        })}
                                        {provided.placeholder}
                                        {columns[status].length === 0 && (
                                            <Typography variant="body2" align="center" sx={{ mt: 2, color: theme.palette.text.secondary }}>
                                                Nenhuma tarefa
                                            </Typography>
                                        )}
                                    </StyledColumn>
                                )}
                            </Droppable>
                        </Grid>
                    ))}
                </Grid>
            </DragDropContext>

            {/* Menu de opções */}
            <Menu
                anchorEl={anchorEl}
                open={menuOpen}
                onClose={handleMenuClose}
                anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
                transformOrigin={{ vertical: 'top', horizontal: 'right' }}
            >
                <MenuItem onClick={handleVisualizar}>
                    <ListItemIcon>
                        <VisibilityIcon fontSize="small" />
                    </ListItemIcon>
                    Visualizar
                </MenuItem>
                <MenuItem onClick={handleEditar}>
                    <ListItemIcon>
                        <EditIcon fontSize="small" />
                    </ListItemIcon>
                    Editar
                </MenuItem>
            </Menu>

            <Snackbar
                open={notification.open}
                autoHideDuration={4000}
                onClose={closeNotification}
                anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
            >
                <Alert onClose={closeNotification} severity={notification.severity} elevation={6} variant="filled">
                    {notification.message}
                </Alert>
            </Snackbar>
        </Container>
    );
}