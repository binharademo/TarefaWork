import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import {
    Box,
    Button,
    TextField,
    Typography,
    Paper,
    Grid,
    FormControl,
    InputLabel,
    Select,
    MenuItem,
    Chip,
    CircularProgress,
    Alert
} from '@mui/material';
import {
    Save as SaveIcon,
    Cancel as CancelIcon,
    LowPriority as LowPriorityIcon,
    PriorityHigh as PriorityHighIcon,
    CheckCircle as CheckCircleIcon,
    Pending as PendingIcon
} from '@mui/icons-material';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { TimePicker } from '@mui/x-date-pickers/TimePicker';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import ptBR from 'date-fns/locale/pt-BR';

function CadastroTarefa() {
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const [success, setSuccess] = useState(false);
    const [usuarios, setUsuarios] = useState([]);

    // Estados do formulário
    const [formData, setFormData] = useState({
        titulo: '',
        descricao: '',
        criadorId: 1, // ID do usuário logado (ajustar conforme sua autenticação)
        responsavelId: '',
        prazo: null,
        tempoTotal: null,
        status: 2, // 2 = Pendente (valor padrão)
        prioridadeTarefa: 0 // 0 = Baixa
    });
    useEffect(() => {
        async function fetchUsuarios() {
            try {
                const response = await fetch('http://localhost:53011/Usuario');
                if (!response.ok) {
                    throw new Error('Erro ao carregar usuários');
                }
                const data = await response.json();
                setUsuarios(Array.isArray(data) ? data : [data]);
            } catch (err) {
                console.error('Erro ao buscar usuários:', err);
            }
        }

        fetchUsuarios();
    }, []);

        


    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: value
        }));
    };

    const handleDateChange = (date) => {
        setFormData(prev => ({
            ...prev,
            prazo: date
        }));
    };

    const handleTimeChange = (time) => {
        setFormData(prev => ({
            ...prev,
            tempoTotal: time ? time.getHours() * 3600 + time.getMinutes() * 60 : 0
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        setError(null);

        try {
            const response = await fetch('http://localhost:53011/Tarefa', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    Titulo: formData.titulo,
                    Descricao: formData.descricao,
                    CriadorId: formData.criadorId,
                    ResponsavelId: formData.responsavelId,
                    Prazo: formData.prazo,
                    Status: formData.status,
                    PrioridadeTarefa: formData.prioridadeTarefa
                })
            });

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || 'Erro ao criar tarefa');
            }

            setSuccess(true);
            setTimeout(() => navigate('/tarefa/listar'), 2000);
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    const handleCancel = () => {
        navigate('/tarefa/listar');
    };

    return (
        <LocalizationProvider dateAdapter={AdapterDateFns} adapterLocale={ptBR}>
            <Box sx={{ p: 3, maxWidth: 800, margin: 'auto' }}>
                <Paper elevation={3} sx={{ p: 3 }}>
                    <Typography variant="h4" component="h1" gutterBottom>
                        Cadastrar Nova Tarefa
                    </Typography>

                    {error && (
                        <Alert severity="error" sx={{ mb: 2 }}>
                            {error}
                        </Alert>
                    )}

                    {success && (
                        <Alert severity="success" sx={{ mb: 2 }}>
                            Tarefa criada com sucesso! Redirecionando...
                        </Alert>
                    )}

                    <form onSubmit={handleSubmit}>
                        <Grid container spacing={2}>
                            <Grid item xs={12}>
                                <TextField
                                    fullWidth
                                    label="Título"
                                    name="titulo"
                                    value={formData.titulo}
                                    onChange={handleChange}
                                    required
                                />
                            </Grid>

                            <Grid item xs={12}>
                                <TextField
                                    fullWidth
                                    label="Descrição"
                                    name="descricao"
                                    value={formData.descricao}
                                    onChange={handleChange}
                                    multiline
                                    rows={4}
                                    required
                                />
                            </Grid>

                            <Grid item xs={12} sm={6}>
                                <FormControl fullWidth required>
                                    <InputLabel>Responsável</InputLabel>
                                    <Select
                                        name="responsavelId"
                                        value={formData.responsavelId}
                                        onChange={handleChange}
                                        label="Responsável"
                                    >
                                        {usuarios.map((usuario) => (
                                            <MenuItem key={usuario.id} value={usuario.id}>
                                                {usuario.nome} — {usuario.funcaoUsuario === 0 ? 'Dev' :
                                                    usuario.funcaoUsuario === 1 ? 'Analista' :
                                                        usuario.funcaoUsuario === 2 ? 'Marketing' : 'Outro'}
                                            </MenuItem>
                                        ))}
                                    </Select>
                                </FormControl>
                            </Grid>


                            <Grid item xs={12} sm={6}>
                                <FormControl fullWidth>
                                    <InputLabel>Prioridade</InputLabel>
                                    <Select
                                        name="prioridadeTarefa"
                                        value={formData.prioridadeTarefa}
                                        onChange={handleChange}
                                        label="Prioridade"
                                    >
                                        <MenuItem value={0}>
                                            <Chip icon={<LowPriorityIcon />} label="Baixa" />
                                        </MenuItem>
                                        <MenuItem value={1}>
                                            <Chip icon={<PriorityHighIcon />} label="Média" color="warning" />
                                        </MenuItem>
                                        <MenuItem value={2}>
                                            <Chip icon={<PriorityHighIcon />} label="Alta" color="error" />
                                        </MenuItem>
                                    </Select>
                                </FormControl>
                            </Grid>

                            <Grid item xs={12} sm={6}>
                                <DatePicker
                                    label="Prazo"
                                    value={formData.prazo}
                                    onChange={handleDateChange}
                                    renderInput={(params) => <TextField {...params} fullWidth required />}
                                    minDate={new Date()}
                                />
                            </Grid>

                            <Grid item xs={12} sm={6}>
                                <TimePicker
                                    label="Tempo Estimado"
                                    value={formData.tempoTotal ?
                                        new Date().setHours(0, 0, 0, 0) + formData.tempoTotal * 1000 : null}
                                    onChange={handleTimeChange}
                                    renderInput={(params) => <TextField {...params} fullWidth />}
                                />
                            </Grid>

                            <Grid item xs={12}>
                                <FormControl fullWidth>
                                    <InputLabel>Status</InputLabel>
                                    <Select
                                        name="status"
                                        value={formData.status}
                                        onChange={handleChange}
                                        label="Status"
                                    >
                                        <MenuItem value={0}>
                                            <Chip icon={<CheckCircleIcon color="success" />} label="Concluído" />
                                        </MenuItem>
                                        <MenuItem value={1}>
                                            <Chip icon={<PendingIcon color="warning" />} label="Em Andamento" />
                                        </MenuItem>
                                        <MenuItem value={2}>
                                            <Chip icon={<PendingIcon color="error" />} label="Pendente" />
                                        </MenuItem>
                                    </Select>
                                </FormControl>
                            </Grid>

                            <Grid item xs={12} sx={{ display: 'flex', justifyContent: 'flex-end', gap: 2 }}>
                                <Button
                                    variant="outlined"
                                    color="error"
                                    startIcon={<CancelIcon />}
                                    onClick={handleCancel}
                                >
                                    Cancelar
                                </Button>
                                <Button
                                    type="submit"
                                    variant="contained"
                                    color="primary"
                                    startIcon={<SaveIcon />}
                                    disabled={loading}
                                >
                                    {loading ? <CircularProgress size={24} /> : 'Salvar Tarefa'}
                                </Button>
                            </Grid>
                        </Grid>
                    </form>
                </Paper>
            </Box>
        </LocalizationProvider>
    );
}

export default CadastroTarefa;