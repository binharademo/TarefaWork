import React, { useState, useEffect } from 'react';
import { DragDropContext, Droppable, Draggable } from 'react-beautiful-dnd';
import {
  Paper,
  Typography,
  Card,
  CardContent,
  Box,
  Grid,
  CircularProgress,
  Container
} from '@mui/material';
import {
  CheckCircle as CheckCircleIcon,
  Warning as WarningIcon,
  Error as ErrorIcon,
  LowPriority as LowPriorityIcon,
  PriorityHigh as PriorityHighIcon,
  MoreVert as MoreVertIcon
} from '@mui/icons-material';

// Estilos para as colunas por status
const columnStyles = {
  0: { borderTop: '4px solid #4caf50' },   // Concluído
  1: { borderTop: '4px solid #ff9800' },   // Em Andamento
  2: { borderTop: '4px solid #f44336' }    // Pendente
};

const statusLabels = {
  0: 'Concluído',
  1: 'Em Andamento',
  2: 'Pendente'
};

export default function BoardTarefas() {
  const [columns, setColumns] = useState({ 0: [], 1: [], 2: [] });
  const [loading, setLoading] = useState(true);

  // Carrega tarefas e agrupa por status
  useEffect(() => {
    fetch('http://localhost:53011/Tarefa')
      .then((res) => res.json())
      .then((data) => {
        const grup = { 0: [], 1: [], 2: [] };
        data.forEach((t) => grup[t.status]?.push(t));
        setColumns(grup);
      })
      .catch(console.error)
      .finally(() => setLoading(false));
  }, []);

  // Ícones de status e prioridade
  const getStatusIcon = (status) => {
    if (status === 0) return <CheckCircleIcon color="success" fontSize="small" />;
    if (status === 1) return <WarningIcon color="warning" fontSize="small" />;
    return <ErrorIcon color="error" fontSize="small" />;
  };

  const getPrioridadeIcon = (p) => {
    if (p === 1) return <LowPriorityIcon fontSize="small" />;
    if (p === 2) return <PriorityHighIcon fontSize="small" color="warning" />;
    return <PriorityHighIcon fontSize="small" color="error" />;
  };

  // Formatação de datas e tempos
  const formatarData = (d) => new Date(d).toLocaleDateString('pt-BR');
  const converterParaSegundos = (str) => {
    const [h = 0, m = 0, ss = 0] = str.split(':').map(Number);
    return h * 3600 + m * 60 + ss;
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
  const handleDragEnd = (result) => {
    const { source, destination } = result;
    if (!destination) return;
    if (
      source.droppableId === destination.droppableId &&
      source.index === destination.index
    ) {
      return;
    }

    const srcId = source.droppableId;
    const dstId = destination.droppableId;
    const srcList = Array.from(columns[srcId]);
    const dstList = Array.from(columns[dstId]);

    // Remove da origem
    const [moved] = srcList.splice(source.index, 1);
    // Atualiza status
    moved.status = parseInt(dstId, 10);
    // Insere no destino
    dstList.splice(destination.index, 0, moved);

    // Atualiza estado
    setColumns({
      ...columns,
      [srcId]: srcList,
      [dstId]: dstList
    });

    // Salva backend (opcional)
    fetch(`http://localhost:53011/Tarefa/${moved.id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(moved)
    }).catch(console.error);
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
    <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
      <DragDropContext onDragEnd={handleDragEnd}>
        <Grid container spacing={2} wrap="nowrap" sx={{ overflowX: 'auto' }}>
          {[2, 1, 0].map((status) => (
            <Grid item key={status} sx={{ flex: '0 0 30%', minWidth: 280 }}>
              <Droppable droppableId={String(status)}>
                {(provided, snapshot) => (
                  <Paper
                    ref={provided.innerRef}
                    {...provided.droppableProps}
                    sx={{
                      p: 2,
                      minHeight: 500,
                      backgroundColor: snapshot.isDraggingOver ? '#f0f0f0' : 'white',
                      ...columnStyles[status]
                    }}
                  >
                    <Typography variant="h6" textAlign="center" gutterBottom>
                      {statusLabels[status]} ({columns[status].length})
                    </Typography>

                    {columns[status].map((tarefa, index) => (
                      <Draggable
                        key={tarefa.id}
                        draggableId={String(tarefa.id)}
                        index={index}
                      >
                        {(prov) => (
                          // Wrapper div para aplicar estilo de drag
                          <div
                            ref={prov.innerRef}
                            {...prov.draggableProps}
                            {...prov.dragHandleProps}
                            style={{
                              ...prov.draggableProps.style,
                              marginBottom: 16,
                              zIndex: 1000
                            }}
                          >
                            <Card>
                              <CardContent>
                                <Box display="flex" justifyContent="space-between">
                                  <Typography fontWeight={500}>
                                    {tarefa.titulo}
                                  </Typography>
                                  <MoreVertIcon fontSize="small" />
                                </Box>
                                <Typography variant="body2" mt={1} mb={2}>
                                  {tarefa.descricao?.slice(0, 80)}{tarefa.descricao?.length > 80 && '...'}
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
                      <Typography variant="body2" color="text.secondary" textAlign="center">
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
    </Container>
  );
}
