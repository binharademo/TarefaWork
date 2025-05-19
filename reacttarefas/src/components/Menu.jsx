import * as React from 'react';
import { styled, useTheme } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Drawer from '@mui/material/Drawer';
import CssBaseline from '@mui/material/CssBaseline';
import MuiAppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import List from '@mui/material/List';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import ChevronRightIcon from '@mui/icons-material/ChevronRight';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import PersonAddIcon from '@mui/icons-material/PersonAdd';
import Diversity3Icon from '@mui/icons-material/Diversity3';
import AddTaskIcon from '@mui/icons-material/AddTask';
import FormatListBulletedIcon from '@mui/icons-material/FormatListBulleted';
import DashboardIcon from '@mui/icons-material/Dashboard';
import { Link } from 'react-router-dom'; // Adicione esta importação

const drawerWidth = 240;

const Main = styled('main', { shouldForwardProp: (prop) => prop !== 'open' })(
    ({ theme }) => ({
        flexGrow: 1,
        padding: theme.spacing(3),
        transition: theme.transitions.create('margin', {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.leavingScreen,
        }),
        marginLeft: `-${drawerWidth}px`,
        variants: [
            {
                props: ({ open }) => open,
                style: {
                    transition: theme.transitions.create('margin', {
                        easing: theme.transitions.easing.easeOut,
                        duration: theme.transitions.duration.enteringScreen,
                    }),
                    marginLeft: 0,
                },
            },
        ],
    }),
);

const AppBar = styled(MuiAppBar, {
    shouldForwardProp: (prop) => prop !== 'open',
})(({ theme }) => ({
    transition: theme.transitions.create(['margin', 'width'], {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen,
    }),
    variants: [
        {
            props: ({ open }) => open,
            style: {
                width: `calc(100% - ${drawerWidth}px)`,
                marginLeft: `${drawerWidth}px`,
                transition: theme.transitions.create(['margin', 'width'], {
                    easing: theme.transitions.easing.easeOut,
                    duration: theme.transitions.duration.enteringScreen,
                }),
            },
        },
    ],
}));

const DrawerHeader = styled('div')(({ theme }) => ({
    display: 'flex',
    alignItems: 'center',
    padding: theme.spacing(0, 1),
    // necessary for content to be below app bar
    ...theme.mixins.toolbar,
    justifyContent: 'flex-end',
}));

export default function PersistentDrawerLeft({ children }) {
    const theme = useTheme();
    const [open, setOpen] = React.useState(false);

    const handleDrawerOpen = () => {
        setOpen(true);
    };

    const handleDrawerClose = () => {
        setOpen(false);
    };

    return (
        <Box sx={{ display: 'flex' }}>
            <CssBaseline />
            <AppBar position="fixed" open={open}>
                <Toolbar>
                    <IconButton
                        color="inherit"
                        aria-label="open drawer"
                        onClick={handleDrawerOpen}
                        edge="start"
                        sx={[
                            {
                                mr: 2,
                            },
                            open && { display: 'none' },
                        ]}
                    >
                        <MenuIcon />
                    </IconButton>
                    <Typography variant="h6" noWrap component="div">
                        Tarefas React
                    </Typography>
                </Toolbar>
            </AppBar>
            <Drawer
                sx={{
                    width: drawerWidth,
                    flexShrink: 0,
                    '& .MuiDrawer-paper': {
                        width: drawerWidth,
                        boxSizing: 'border-box',
                    },
                }}
                variant="persistent"
                anchor="left"
                open={open}
            >
                <DrawerHeader>
                    <IconButton onClick={handleDrawerClose}>
                        {theme.direction === 'ltr' ? <ChevronLeftIcon /> : <ChevronRightIcon />}
                    </IconButton>
                </DrawerHeader>
                <Divider />
                <List>
                    <h4>Dashboard</h4>
                    <ListItem key="BoardTarefas" disablePadding>
                        <ListItemButton component={Link} to="/">
                            <ListItemIcon>
                                <DashboardIcon />
                            </ListItemIcon>
                            <ListItemText primary="Board" />
                        </ListItemButton>
                    </ListItem>
                    <h4>Usuarios</h4>
                    <ListItem key="CadastroUsuario" disablePadding>
                        <ListItemButton component={Link} to="/usuario/cadastro">
                            <ListItemIcon>
                                <PersonAddIcon />
                            </ListItemIcon>
                            <ListItemText primary="Cadastro" />
                        </ListItemButton>
                    </ListItem>
                    <ListItem key="ListarUsuario" disablePadding>
                        <ListItemButton component={Link} to="/usuario/listar">
                            <ListItemIcon>
                                <Diversity3Icon />
                            </ListItemIcon>
                            <ListItemText primary="Listar" />
                        </ListItemButton>
                    </ListItem>
                </List>
                <Divider />
                <List>
                    <h4>Tarefas</h4>
                    <ListItem key="CadastroTarefa" disablePadding>
                        <ListItemButton component={Link} to="/tarefa/cadastro">
                            <ListItemIcon>
                                <AddTaskIcon></AddTaskIcon>
                            </ListItemIcon>
                            <ListItemText primary="Cadastro"></ListItemText>
                        </ListItemButton>
                    </ListItem>
                    <ListItem key="ListarTarefa" disablePadding>
                        <ListItemButton component={Link} to="/tarefa/listar">
                            <ListItemIcon>
                                <FormatListBulletedIcon></FormatListBulletedIcon>
                            </ListItemIcon>
                            <ListItemText primary="Listar"></ListItemText>
                        </ListItemButton>
                    </ListItem>
                </List>
                <List>
                    <h4>Empresas</h4>
                    <ListItem key="CadastroEmpresa" disablePadding>
                        <ListItemButton component={Link} to="/empresa/cadastro">
                            <ListItemIcon>
                                <PersonAddIcon />
                            </ListItemIcon>
                            <ListItemText primary="Cadastro" />
                        </ListItemButton>
                    </ListItem>
                    <ListItem key="ListarEmpresa" disablePadding>
                        <ListItemButton component={Link} to="/empresa/listar">
                            <ListItemIcon>
                                <Diversity3Icon />
                            </ListItemIcon>
                            <ListItemText primary="Listar" />
                        </ListItemButton>
                    </ListItem>
                </List>
            </Drawer>
            <Main open={open}>
                <DrawerHeader />
                {children}
            </Main>
        </Box>
    );
}
