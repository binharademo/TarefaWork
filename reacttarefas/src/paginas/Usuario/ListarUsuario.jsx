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
    Grid
} from '@mui/material';

function ListarUsuario() {
    const [usuarios, setUsuarios] = useState([]);
    const [carregando, setCarregando] = useState(true);
    const [erro, setErro] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        async function fetchUsuarios() {
            try {
                const response = await fetch('http://localhost:53011/Usuario'); // Substitua pela URL correta
                if (!response.ok) {
                    throw new Error('Erro ao carregar usuários');
                }
                const data = await response.json();
                console.log("Dados recebidos:", data); // Verifique se é um array
                setUsuarios(Array.isArray(data) ? data : [data]);
            } catch (error) {
                setErro(error.message);
            } finally {
                setCarregando(false);
                console.log(usuarios);
            }
        }

        fetchUsuarios();
    }, []);

    useEffect(() => {
        console.log("Estado usuarios atualizado:", usuarios);
    }, [usuarios]);

    const handleNovoUsuario = () => {
        navigate('/');
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
        <div style={{ padding: 16 }}>
            <Grid container justifyContent="space-between" alignItems="center" mb={4}>
                <Typography variant="h4" component="h1">
                    Lista de Usuarios
                </Typography>
                <Button
                    variant="contained"
                    color="primary"
                    onClick={handleNovoUsuario}
                >
                    Novo Usuario
                </Button>
            </Grid>

            {usuarios.length === 0 ? (
                <Typography variant="body1">Nenhum usuario cadastrado.</Typography>
            ) : (
                <TableContainer component={Paper}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>ID</TableCell>
                                <TableCell>Nome</TableCell>
                                <TableCell>Funcao</TableCell>
                                <TableCell>Setor</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {usuarios.map((usuario) => (
                                <TableRow key={usuario.id}>
                                    <TableCell>{usuario.id}</TableCell>
                                    <TableCell>{usuario.nome}</TableCell>
                                    <TableCell>
                                        {usuario.funcaoUsuario === 0 ? 'Dev' :
                                            usuario.funcaoUsuario === 1 ? 'Analista' :
                                                usuario.funcaoUsuario === 2 ? 'Marketing' : usuario.funcaoUsuario}
                                    </TableCell>
                                    <TableCell>
                                        {usuario.setorUsuario === 0 ? 'TI' :
                                            usuario.setorUsuario === 1 ? 'Marketing' : usuario.setorUsuario}
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            )}
        </div>
    );
}

export default ListarUsuario;