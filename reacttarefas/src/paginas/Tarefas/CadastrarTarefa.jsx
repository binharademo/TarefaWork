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
    CircularProgress,
    Alert
} from '@mui/material';
import {
    Save as SaveIcon,
    Cancel as CancelIcon,
    CheckCircle as CheckCircleIcon,
    Pending as PendingIcon,
    KeyboardArrowDown as KeyboardArrowDownIcon,
    KeyboardArrowUp as KeyboardArrowUpIcon,
    KeyboardDoubleArrowUp as KeyboardDoubleArrowUpIcon,
    PlayCircle as PlayCircleIcon
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
                            <Grid item size={12}>
                                <TextField
                                    fullWidth
                                    label="Título"
                                    name="titulo"
                                    value={formData.titulo}
                                    onChange={handleChange}
                                    required
                                />
                            </Grid>

                            <Grid item size={12}>
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

                            <Grid item size={{ xs: 12, sm: 8 }}>
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

                            <Grid item size={{ xs: 12, sm: 4 }}>
                                <FormControl fullWidth required>
                                    <DateTimePicker
                                        label="Prazo"
                                        value={formData.prazo}
                                        onChange={handleDateChange}
                                        renderInput={(params) => <TextField {...params} fullWidth required />}
                                        minDate={new Date()}
                                        />
                                </FormControl>
                            </Grid>

                            <Grid item size={{ xs: 12, sm: 4 }}>
                                <FormControl fullWidth variant="standard">
                                    <InputLabel>Prioridade</InputLabel>
                                    <Select
                                        name="prioridadeTarefa"
                                        value={formData.prioridadeTarefa}
                                        onChange={handleChange}
                                        label="Prioridade"
                                    >
                                        <MenuItem value={2}><KeyboardDoubleArrowUpIcon color="error" /> Alta
                                        </MenuItem>
                                        <MenuItem value={1}><KeyboardArrowUpIcon color="primary" /> Média
                                        </MenuItem>
                                        <MenuItem value={0}><KeyboardArrowDownIcon color="success"/> Baixa
                                        </MenuItem>
                                    </Select>
                                </FormControl>
                            </Grid>

                            <Grid item size={{ xs: 12, sm: 4 }}>
                                <FormControl fullWidth variant="standard">
                                    <InputLabel>Status</InputLabel>
                                    <Select
                                        defaultValue={30}
                                        inputProps={{
                                            name: 'age',
                                            id: 'uncontrolled-native',
                                        }}
                                    >
                                        <MenuItem value={0}><CheckCircleIcon color="success"/>Concluído</MenuItem>
                                        <MenuItem value={1}><PlayCircleIcon color="primary" />Em Andamento</MenuItem>
                                        <MenuItem value={2}><PendingIcon color="action" />Pendente</MenuItem>
                                    </Select>
                                </FormControl>
                            </Grid>

                            <Grid item size={12} sx={{ display: 'flex', justifyContent: 'flex-end', gap: 2 }}>
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