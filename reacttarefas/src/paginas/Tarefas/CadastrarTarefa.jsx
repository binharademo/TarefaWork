import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import {
    Button,
    TextField,
    FormControl,
    InputLabel,
    Select,
    MenuItem,
    Typography,
    Container,
    Paper,
    Box,
    Grid,
    Alert,
    InputAdornment,
    CircularProgress
} from '@mui/material';
import {
    Save as SaveIcon,
    Cancel as CancelIcon,
    CheckCircle as CheckCircleIcon,
    Pending as PendingIcon,
    KeyboardArrowDown as KeyboardArrowDownIcon,
    KeyboardArrowUp as KeyboardArrowUpIcon,
    KeyboardDoubleArrowUp as KeyboardDoubleArrowUpIcon,
    PlayCircle as PlayCircleIcon,
    Title as TitleIcon,
    Description as DescriptionIcon,
    Person as PersonIcon,
    Schedule as ScheduleIcon,
    Flag as FlagIcon,
    AssignmentTurnedIn as AssignmentTurnedInIcon
} from '@mui/icons-material';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';
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
        status: 0, // 0 = Pendente (valor padrão)
        prioridadeTarefa: 0 // 0 = Baixa
    });

    useEffect(() => {
        async function fetchUsuarios() {
            try {
                const response = await fetch(`${import.meta.env.VITE_API_URL}/Usuario`);
                if (!response.ok) {
                    throw new Error('Erro ao carregar usuarios');
                }
                const data = await response.json();
                setUsuarios(Array.isArray(data) ? data : [data]);
            } catch (err) {
                console.error('Erro ao buscar usuarios:', err);
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

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        setError(null);

        try {
            const response = await fetch(`${import.meta.env.VITE_API_URL}/Tarefa`, {
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
            <Container maxWidth="md" sx={{ mt: 4, mb: 4 }}>
                <Paper
                    elevation={3}
                    sx={{
                        p: 4,
                        borderRadius: 2,
                        background: 'linear-gradient(to right bottom, #ffffff, #f8f9fa)',
                        border: '1px solid #4E71FF'
                    }}
                >
                    <Box sx={{ mb: 3, display: 'flex', alignItems: 'center' }}>
                        <AssignmentTurnedInIcon sx={{ fontSize: 32, mr: 2, color: '#3f51b5' }} />
                        <Typography variant="h4" component="h1" fontWeight="500" color="primary">
                            Nova Tarefa
                        </Typography>
                    </Box>

                    {error && (
                        <Alert severity="error" sx={{ mb: 3 }}>
                            {error}
                        </Alert>
                    )}

                    {success && (
                        <Alert severity="success" sx={{ mb: 3 }}>
                            Tarefa criada com sucesso! Redirecionando...
                        </Alert>
                    )}

                    <form onSubmit={handleSubmit}>
                        <Grid container spacing={3}>
                            <Grid item xs={12}>
                                <TextField
                                    fullWidth
                                    label="Título"
                                    name="titulo"
                                    value={formData.titulo}
                                    onChange={handleChange}
                                    required
                                    variant="outlined"
                                    InputProps={{
                                        startAdornment: (
                                            <InputAdornment position="start">
                                                <TitleIcon color="action" />
                                            </InputAdornment>
                                        ),
                                    }}
                                />
                            </Grid>

                            <Grid item xs={12} md={6}>
                                <FormControl fullWidth variant="outlined">
                                    <InputLabel id="responsavel-label">Responsável</InputLabel>
                                    <Select
                                        labelId="responsavel-label"
                                        name="responsavelId"
                                        value={formData.responsavelId}
                                        onChange={handleChange}
                                        label="Responsável"
                                        required
                                        startAdornment={
                                            <InputAdornment position="start">
                                                <PersonIcon color="action" />
                                            </InputAdornment>
                                        }
                                    >
                                        <MenuItem value="">Selecione um responsável</MenuItem>
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

                            <Grid item xs={12} md={6}>
                                <FormControl fullWidth variant="outlined">
                                    <DateTimePicker
                                        label="Prazo"
                                        value={formData.prazo}
                                        onChange={handleDateChange}
                                        renderInput={(params) => (
                                            <TextField
                                                {...params}
                                                fullWidth
                                                required
                                                InputProps={{
                                                    ...params.InputProps,
                                                    startAdornment: (
                                                        <InputAdornment position="start">
                                                            <ScheduleIcon color="action" />
                                                            {params.InputProps?.startAdornment}
                                                        </InputAdornment>
                                                    ),
                                                }}
                                            />
                                        )}
                                        minDate={new Date()}
                                    />
                                </FormControl>
                            </Grid>

                            <Grid item xs={12} md={6}>
                                <FormControl fullWidth variant="outlined">
                                    <InputLabel id="prioridade-label">Prioridade</InputLabel>
                                    <Select
                                        labelId="prioridade-label"
                                        name="prioridadeTarefa"
                                        value={formData.prioridadeTarefa}
                                        onChange={handleChange}
                                        label="Prioridade"
                                        startAdornment={
                                            <InputAdornment position="start">
                                                <FlagIcon color="action" />
                                            </InputAdornment>
                                        }
                                    >
                                        <MenuItem value={2}>
                                            <Box sx={{ display: 'flex', alignItems: 'center' }}>
                                                <KeyboardDoubleArrowUpIcon color="error" sx={{ mr: 1 }} /> Alta
                                            </Box>
                                        </MenuItem>
                                        <MenuItem value={1}>
                                            <Box sx={{ display: 'flex', alignItems: 'center' }}>
                                                <KeyboardArrowUpIcon color="primary" sx={{ mr: 1 }} /> Normal
                                            </Box>
                                        </MenuItem>
                                        <MenuItem value={0}>
                                            <Box sx={{ display: 'flex', alignItems: 'center' }}>
                                                <KeyboardArrowDownIcon color="success" sx={{ mr: 1 }} /> Baixa
                                            </Box>
                                        </MenuItem>
                                    </Select>
                                </FormControl>
                            </Grid>

                            <Grid item xs={12} md={6}>
                                <FormControl fullWidth variant="outlined">
                                    <InputLabel id="status-label">Status</InputLabel>
                                    <Select
                                        labelId="status-label"
                                        id="status"
                                        name="status"
                                        value={formData.status}
                                        onChange={handleChange}
                                        label="Status"
                                        startAdornment={
                                            <InputAdornment position="start">
                                                <AssignmentTurnedInIcon color="action" />
                                            </InputAdornment>
                                        }
                                    >
                                        <MenuItem value={2}>
                                            <Box sx={{ display: 'flex', alignItems: 'center' }}>
                                                <CheckCircleIcon color="success" sx={{ mr: 1 }} /> Concluído
                                            </Box>
                                        </MenuItem>
                                        <MenuItem value={1}>
                                            <Box sx={{ display: 'flex', alignItems: 'center' }}>
                                                <PlayCircleIcon color="primary" sx={{ mr: 1 }} /> Em Andamento
                                            </Box>
                                        </MenuItem>
                                        <MenuItem value={0}>
                                            <Box sx={{ display: 'flex', alignItems: 'center' }}>
                                                <PendingIcon color="action" sx={{ mr: 1 }} /> Pendente
                                            </Box>
                                        </MenuItem>
                                    </Select>
                                </FormControl>
                            </Grid>
                            </Grid>

                            <Grid mt={3}>
                                <Grid item xs={12}>
                                    <TextField
                                        fullWidth
                                        label="Descrição"
                                        name="descricao"
                                        value={formData.descricao}
                                        onChange={handleChange}
                                        multiline
                                        rows={3}
                                        required
                                        variant="outlined"
                                        InputProps={{
                                            endAdornment: (
                                                <InputAdornment position="start">
                                                    <DescriptionIcon color="action" />
                                                </InputAdornment>
                                            ),
                                        }}
                                    />
                                </Grid>

                            <Grid item xs={12}>
                                <Box
                                    sx={{
                                        display: 'flex',
                                        justifyContent: 'flex-end',
                                        gap: 2,
                                        mt: 3
                                    }}
                                >
                                    <Button
                                        variant="outlined"
                                        onClick={handleCancel}
                                        startIcon={<CancelIcon />}
                                        sx={{ px: 3 }}
                                    >
                                        Cancelar
                                    </Button>
                                    <Button
                                        type="submit"
                                        variant="contained"
                                        color="primary"
                                        startIcon={loading ? null : <SaveIcon />}
                                        disabled={loading}
                                        sx={{ px: 3 }}
                                    >
                                        {loading ? <CircularProgress size={24} /> : 'Salvar Tarefa'}
                                    </Button>
                                </Box>
                            </Grid>
                        </Grid>
                    </form>
                </Paper>
            </Container>
        </LocalizationProvider>
    );
}

export default CadastroTarefa;