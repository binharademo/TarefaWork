import * as React from 'react';
import { styled, useTheme, alpha } from '@mui/material/styles';
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
import { Link, useLocation } from 'react-router-dom';
import DomainAddIcon from '@mui/icons-material/DomainAdd';
import BusinessIcon from '@mui/icons-material/Business';
import PeopleAltRoundedIcon from '@mui/icons-material/PeopleAltRounded';
import GroupAddRoundedIcon from '@mui/icons-material/GroupAddRounded';
import Avatar from '@mui/material/Avatar';
import Badge from '@mui/material/Badge';
import NotificationsIcon from '@mui/icons-material/Notifications';
import SearchIcon from '@mui/icons-material/Search';
import InputBase from '@mui/material/InputBase';
import Tooltip from '@mui/material/Tooltip';
import ListSubheader from '@mui/material/ListSubheader';
import Collapse from '@mui/material/Collapse';
import ExpandLess from '@mui/icons-material/ExpandLess';
import ExpandMore from '@mui/icons-material/ExpandMore';
import Menu from '@mui/material/Menu';
import MenuItem from '@mui/material/MenuItem';
import CloseIcon from '@mui/icons-material/Close';
import AccessTimeIcon from '@mui/icons-material/AccessTime';
import AssignmentIcon from '@mui/icons-material/Assignment';
import PersonIcon from '@mui/icons-material/Person';
import WarningIcon from '@mui/icons-material/Warning';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import Button from '@mui/material/Button';

const drawerWidth = 260;

const Main = styled('main', { shouldForwardProp: (prop) => prop !== 'open' })(
    ({ theme, open }) => ({
        flexGrow: 1,
        padding: theme.spacing(3),
        transition: theme.transitions.create('margin', {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.leavingScreen,
        }),
        marginLeft: `-${drawerWidth}px`,
        ...(open && {
            transition: theme.transitions.create('margin', {
                easing: theme.transitions.easing.easeOut,
                duration: theme.transitions.duration.enteringScreen,
            }),
            marginLeft: 0,
        }),
    })
);

const AppBar = styled(MuiAppBar, {
    shouldForwardProp: (prop) => prop !== 'open',
})(({ theme, open }) => ({
    transition: theme.transitions.create(['margin', 'width'], {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen,
    }),
    boxShadow: '0 3px 5px 2px rgba(0, 0, 0, .1)',
    background: 'linear-gradient(90deg, #3949ab 0%, #5c6bc0 100%)',
    ...(open && {
        width: `calc(100% - ${drawerWidth}px)`,
        marginLeft: `${drawerWidth}px`,
        transition: theme.transitions.create(['margin', 'width'], {
            easing: theme.transitions.easing.easeOut,
            duration: theme.transitions.duration.enteringScreen,
        }),
    }),
}));

const Search = styled('div')(({ theme }) => ({
    position: 'relative',
    borderRadius: theme.shape.borderRadius,
    backgroundColor: alpha(theme.palette.common.white, 0.15),
    '&:hover': {
        backgroundColor: alpha(theme.palette.common.white, 0.25),
    },
    marginRight: theme.spacing(2),
    marginLeft: 0,
    width: '100%',
    [theme.breakpoints.up('sm')]: {
        marginLeft: theme.spacing(3),
        width: 'auto',
    },
}));

const SearchIconWrapper = styled('div')(({ theme }) => ({
    padding: theme.spacing(0, 2),
    height: '100%',
    position: 'absolute',
    pointerEvents: 'none',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
}));

const StyledInputBase = styled(InputBase)(({ theme }) => ({
    color: 'inherit',
    '& .MuiInputBase-input': {
        padding: theme.spacing(1, 1, 1, 0),
        paddingLeft: `calc(1em + ${theme.spacing(4)})`,
        transition: theme.transitions.create('width'),
        width: '100%',
        [theme.breakpoints.up('md')]: {
            width: '20ch',
        },
    },
}));

const DrawerHeader = styled('div')(({ theme }) => ({
    display: 'flex',
    alignItems: 'center',
    padding: theme.spacing(0, 1),
    ...theme.mixins.toolbar,
    justifyContent: 'space-between',
    background: 'linear-gradient(90deg, #3949ab 0%, #5c6bc0 100%)',
    color: 'white',
}));

const MainDrawerHeader = styled('div')(({ theme }) => ({
    display: 'flex',
    alignItems: 'center',
    padding: theme.spacing(0, 1),
    ...theme.mixins.toolbar,
    justifyContent: 'flex-end',
    visibility: 'hidden', // Tornar invisível
    height: 0, // Remover espaço vertical
    padding: 0, // Remover padding
}));

const StyledListSubheader = styled(ListSubheader)(({ theme }) => ({
    backgroundColor: alpha(theme.palette.primary.light, 0.08),
    lineHeight: '48px',
    color: theme.palette.primary.dark,
    fontWeight: 600,
    fontSize: '0.875rem',
}));

const StyledListItemButton = styled(ListItemButton)(({ theme, active }) => ({
    borderRadius: '8px',
    margin: '6px 8px',
    '&:hover': {
        backgroundColor: alpha(theme.palette.primary.main, 0.08),
    },
    ...(active && {
        backgroundColor: alpha(theme.palette.primary.main, 0.12),
        '&:hover': {
            backgroundColor: alpha(theme.palette.primary.main, 0.16),
        },
    }),
}));

const StyledListItemIcon = styled(ListItemIcon)(({ theme, active }) => ({
    minWidth: 40,
    color: active ? theme.palette.primary.main : theme.palette.text.secondary,
}));

const StyledListItemText = styled(ListItemText)(({ theme, active }) => ({
    '& .MuiTypography-root': {
        fontWeight: active ? 600 : 400,
        color: active ? theme.palette.primary.main : theme.palette.text.primary,
    },
}));

export default function PersistentDrawerLeft({ children }) {
    const theme = useTheme();
    const [open, setOpen] = React.useState(false);
    const location = useLocation();
    const activeColor = '#4E71FF';
    const [openUsers, setOpenUsers] = React.useState(true);
    const [openTasks, setOpenTasks] = React.useState(true);
    const [openCompanies, setOpenCompanies] = React.useState(true);
    const [openSectors, setOpenSectors] = React.useState(true);

    // Estados para o menu de notificações
    const [notificationsAnchorEl, setNotificationsAnchorEl] = React.useState(null);
    const openNotifications = Boolean(notificationsAnchorEl);

    // Mock de dados para notificações
    const notificationsMock = [
        {
            id: 1,
            type: 'task',
            title: 'Nova tarefa atribuída',
            message: 'Você recebeu uma nova tarefa: "Revisar relatório mensal"',
            time: 'Há 5 minutos',
            read: false,
            priority: 'high',
            icon: <AssignmentIcon color="primary" />
        },
        {
            id: 2,
            type: 'alert',
            title: 'Prazo próximo',
            message: 'A tarefa "Reunião com cliente" vence em 2 horas',
            time: 'Há 30 minutos',
            read: false,
            priority: 'urgent',
            icon: <WarningIcon sx={{ color: '#ff9800' }} />
        },
        {
            id: 3,
            type: 'user',
            title: 'Novo comentário',
            message: 'xxmarceloo comentou na sua tarefa "Implementar login"',
            time: 'Há 2 horas',
            read: true,
            priority: 'normal',
            icon: <PersonIcon color="secondary" />
        },
        {
            id: 4,
            type: 'success',
            title: 'Tarefa concluída',
            message: 'A tarefa "Atualizar documentação" foi marcada como concluída',
            time: 'Há 1 dia',
            read: true,
            priority: 'low',
            icon: <CheckCircleIcon sx={{ color: 'green' }} />
        }
    ];

    // Contador de notificações não lidas
    const unreadCount = notificationsMock.filter(notification => !notification.read).length;

    const handleDrawerOpen = () => {
        setOpen(true);
    };

    const handleDrawerClose = () => {
        setOpen(false);
    };

    const handleToggleUsers = () => {
        setOpenUsers(!openUsers);
    };

    const handleToggleTasks = () => {
        setOpenTasks(!openTasks);
    };

    const handleToggleCompanies = () => {
        setOpenCompanies(!openCompanies);
    };

    const handleToggleSectors = () => {
        setOpenSectors(!openSectors);
    };

    const handleNotificationsClick = (event) => {
        setNotificationsAnchorEl(event.currentTarget);
    };

    const handleNotificationsClose = () => {
        setNotificationsAnchorEl(null);
    };

    const isActive = (path) => {
        return location.pathname === path;
    };

    return (
        <Box sx={{ display: 'flex' }}>
            <CssBaseline />
            <AppBar position="fixed" open={open} sx={{ backgroundColor: '#4E71FF', borderBottom: '1px solid #1F509A' }}>
                <Toolbar>
                    <IconButton
                        color="inherit"
                        aria-label="open drawer"
                        onClick={handleDrawerOpen}
                        edge="start"
                        sx={{
                            mr: 2,
                            ...(open && { display: 'none' }),
                        }}
                    >
                        <MenuIcon />
                    </IconButton>
                    <Typography variant="h6" noWrap component="div" sx={{ display: { xs: 'none', sm: 'block' } }}>
                        Tarefas React
                    </Typography>
                    <Search>
                        <SearchIconWrapper>
                            <SearchIcon />
                        </SearchIconWrapper>
                        <StyledInputBase
                            placeholder="Pesquisar..."
                            inputProps={{ 'aria-label': 'search' }}
                        />
                    </Search>
                    <Box sx={{ flexGrow: 1 }} />
                    <Box sx={{ display: { xs: 'none', md: 'flex' } }}>
                        <Tooltip title="Notificações">
                            <IconButton
                                size="large"
                                color="inherit"
                                onClick={handleNotificationsClick}
                                aria-controls={openNotifications ? 'notifications-menu' : undefined}
                                aria-haspopup="true"
                                aria-expanded={openNotifications ? 'true' : undefined}
                            >
                                <Badge badgeContent={unreadCount} color="error">
                                    <NotificationsIcon />
                                </Badge>
                            </IconButton>
                        </Tooltip>
                        <Menu
                            id="notifications-menu"
                            anchorEl={notificationsAnchorEl}
                            open={openNotifications}
                            onClose={handleNotificationsClose}
                            PaperProps={{
                                elevation: 3,
                                sx: {
                                    width: '350px',
                                    maxHeight: '450px',
                                    overflow: 'auto',
                                    border: '1px solid',
                                    borderColor: 'divider',
                                    borderRadius: 2,
                                },
                            }}
                            transformOrigin={{ horizontal: 'right', vertical: 'top' }}
                            anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
                        >
                            <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', p: 2, borderBottom: '1px solid', borderColor: 'divider' }}>
                                <Typography variant="subtitle1" fontWeight="bold">
                                    Notificações ({unreadCount} não lidas)
                                </Typography>
                                <IconButton size="small" onClick={handleNotificationsClose}>
                                    <CloseIcon fontSize="small" />
                                </IconButton>
                            </Box>

                            {notificationsMock.length > 0 ? (
                                notificationsMock.map((notification) => (
                                    <MenuItem
                                        key={notification.id}
                                        onClick={handleNotificationsClose}
                                        sx={{
                                            py: 1,
                                            px: 2,
                                            borderLeft: notification.read ? 'none' : '4px solid',
                                            borderColor: notification.read ? 'transparent' : 'primary.main',
                                            backgroundColor: notification.read ? 'transparent' : alpha(theme.palette.primary.light, 0.08),
                                            '&:hover': {
                                                backgroundColor: notification.read ? alpha(theme.palette.primary.light, 0.04) : alpha(theme.palette.primary.light, 0.12)
                                            }
                                        }}
                                    >
                                        <Box sx={{ display: 'flex', width: '100%' }}>
                                            <Box sx={{ mr: 2, mt: 0.5 }}>
                                                {notification.icon}
                                            </Box>
                                            <Box sx={{ flex: 1 }}>
                                                <Typography variant="subtitle2" sx={{ fontWeight: notification.read ? 400 : 600 }}>
                                                    {notification.title}
                                                </Typography>
                                                <Typography variant="body2" color="text.secondary" sx={{ mt: 0.5 }}>
                                                    {notification.message}
                                                </Typography>
                                                <Box sx={{ display: 'flex', alignItems: 'center', mt: 1 }}>
                                                    <AccessTimeIcon sx={{ fontSize: 14, mr: 0.5, color: 'text.disabled' }} />
                                                    <Typography variant="caption" color="text.disabled">
                                                        {notification.time}
                                                    </Typography>
                                                </Box>
                                            </Box>
                                        </Box>
                                    </MenuItem>
                                ))
                            ) : (
                                <Box sx={{ py: 4, textAlign: 'center' }}>
                                    <Typography variant="body2" color="text.secondary">
                                        Nenhuma notificação disponível
                                    </Typography>
                                </Box>
                            )}

                            <Box sx={{ p: 1.5, borderTop: '1px solid', borderColor: 'divider', textAlign: 'center' }}>
                                <Button size="small" color="primary">
                                    Ver todas as notificações
                                </Button>
                            </Box>
                        </Menu>
                        <Tooltip title="Perfil">
                            <IconButton
                                size="large"
                                edge="end"
                                color="inherit"
                                sx={{ ml: 1 }}
                            >
                                <Avatar sx={{ width: 32, height: 32, bgcolor: alpha(theme.palette.common.white, 0.8), color: theme.palette.primary.main }}>
                                    U
                                </Avatar>
                            </IconButton>
                        </Tooltip>
                    </Box>
                </Toolbar>
            </AppBar>
            <Drawer
                sx={{
                    width: drawerWidth,
                    backgroundColor: '#DDDDDD',
                    flexShrink: 0,
                    '& .MuiDrawer-paper': {
                        width: drawerWidth,
                        boxSizing: 'border-box',
                        boxShadow: '0 10px 30px rgba(0, 0, 0, 0.1)',
                        borderRight: 'none',
                    },
                }}
                variant="persistent"
                anchor="left"
                open={open}
            >
                <DrawerHeader>
                    <Box sx={{ display: 'flex', alignItems: 'center', p: 1 }}>
                        <Avatar sx={{ bgcolor: 'white', color: theme.palette.primary.main, mr: 1, width: 32, height: 32 }}>TR</Avatar>
                        <Typography variant="h6" noWrap component="div">
                            Tarefas React
                        </Typography>
                    </Box>
                    <IconButton onClick={handleDrawerClose} sx={{ color: 'white' }}>
                        {theme.direction === 'ltr' ? <ChevronLeftIcon /> : <ChevronRightIcon />}
                    </IconButton>
                </DrawerHeader>
                <Divider />
                <List sx={{ textAlign: 'center' }}>
                    <h4>Dashboard</h4>
                    <ListItem key="BoardTarefas" disablePadding>
                        <ListItemButton
                            component={Link}
                            to="/"
                            sx={{
                                '&.Mui-focusVisible': { backgroundColor: 'rgba(255, 127, 62, 0.12)' },
                                '&:hover': { backgroundColor: 'rgba(78, 113, 255, 0.1)' },
                                '& .MuiTouchRipple-root span': { backgroundColor: activeColor + ' !important' },
                            }}
                        >
                            <ListItemIcon sx={location.pathname === '/' ? { color: activeColor } : {}}>
                                <DashboardIcon />
                            </ListItemIcon>
                            <ListItemText primary="Board" sx={location.pathname === '/' ? { color: activeColor } : {}} />
                        </ListItemButton>
                    </ListItem>
                    <h4>Usuarios</h4>
                    <ListItem key="CadastroUsuario" disablePadding>
                        <ListItemButton
                            component={Link}
                            to="/usuario/cadastro"
                            sx={{
                                '&.Mui-focusVisible': { backgroundColor: 'rgba(255, 127, 62, 0.12)' },
                                '&:hover': { backgroundColor: 'rgba(78, 113, 255, 0.1)' },
                                '& .MuiTouchRipple-root span': { backgroundColor: activeColor + ' !important' },
                            }}
                        >
                            <ListItemIcon sx={location.pathname === '/usuario/cadastro' ? { color: activeColor } : {}}>
                                <PersonAddIcon />
                            </ListItemIcon>
                            <ListItemText primary="Cadastro" sx={location.pathname === '/usuario/cadastro' ? { color: activeColor } : {}} />
                        </ListItemButton>
                    </ListItem>
                    <ListItem key="ListarUsuario" disablePadding>
                        <ListItemButton
                            component={Link}
                            to="/usuario/listar"
                            sx={{
                                '&.Mui-focusVisible': { backgroundColor: 'rgba(255, 127, 62, 0.12)' },
                                '&:hover': { backgroundColor: 'rgba(78, 113, 255, 0.1)' },
                                '& .MuiTouchRipple-root span': { backgroundColor: activeColor + ' !important' },
                            }}
                        >
                            <ListItemIcon sx={location.pathname === '/usuario/listar' ? { color: activeColor } : {}}>
                                <Diversity3Icon />
                            </ListItemIcon>
                            <ListItemText primary="Listar" sx={location.pathname === '/usuario/listar' ? { color: activeColor } : {}} />
                        </ListItemButton>
                    </ListItem>
                </List>
                <Divider />
                <List sx={{ textAlign: 'center' }}>
                    <h4>Tarefas</h4>
                    <ListItem key="CadastroTarefa" disablePadding>
                        <ListItemButton
                            component={Link}
                            to="/tarefa/cadastro"
                            sx={{
                                '&.Mui-focusVisible': { backgroundColor: 'rgba(255, 127, 62, 0.12)' },
                                '&:hover': { backgroundColor: 'rgba(78, 113, 255, 0.1)' },
                                '& .MuiTouchRipple-root span': { backgroundColor: activeColor + ' !important' },
                            }}
                        >
                            <ListItemIcon sx={location.pathname === '/tarefa/cadastro' ? { color: activeColor } : {}}>
                                <AddTaskIcon />
                            </ListItemIcon>
                            <ListItemText primary="Cadastro" sx={location.pathname === '/tarefa/cadastro' ? { color: activeColor } : {}} />
                        </ListItemButton>
                    </ListItem>
                    <ListItem key="ListarTarefa" disablePadding>
                        <ListItemButton
                            component={Link}
                            to="/tarefa/listar"
                            sx={{
                                '&.Mui-focusVisible': { backgroundColor: 'rgba(255, 127, 62, 0.12)' },
                                '&:hover': { backgroundColor: 'rgba(78, 113, 255, 0.1)' },
                                '& .MuiTouchRipple-root span': { backgroundColor: activeColor + ' !important' },
                            }}
                        >
                            <ListItemIcon sx={location.pathname === '/tarefa/listar' ? { color: activeColor } : {}}>
                                <FormatListBulletedIcon />
                            </ListItemIcon>
                            <ListItemText primary="Listar" sx={location.pathname === '/tarefa/listar' ? { color: activeColor } : {}} />
                        </ListItemButton>
                    </ListItem>
                </List>
                <List sx={{ textAlign: 'center' }}>
                    <h4>Empresas</h4>
                    <ListItem key="CadastroEmpresa" disablePadding>
                        <ListItemButton
                            component={Link}
                            to="/empresa/cadastro"
                            sx={{
                                '&.Mui-focusVisible': { backgroundColor: 'rgba(255, 127, 62, 0.12)' },
                                '&:hover': { backgroundColor: 'rgba(78, 113, 255, 0.1)' },
                                '& .MuiTouchRipple-root span': { backgroundColor: activeColor + ' !important' },
                            }}
                        >
                            <ListItemIcon sx={location.pathname === '/empresa/cadastro' ? { color: activeColor } : {}}>
                                <DomainAddIcon />
                            </ListItemIcon>
                            <ListItemText primary="Cadastro" sx={location.pathname === '/empresa/cadastro' ? { color: activeColor } : {}} />
                        </ListItemButton>
                    </ListItem>
                    <ListItem key="ListarEmpresa" disablePadding>
                        <ListItemButton
                            component={Link}
                            to="/empresa/listar"
                            sx={{
                                '&.Mui-focusVisible': { backgroundColor: 'rgba(255, 127, 62, 0.12)' },
                                '&:hover': { backgroundColor: 'rgba(78, 113, 255, 0.1)' },
                                '& .MuiTouchRipple-root span': { backgroundColor: activeColor + ' !important' },
                            }}
                        >
                            <ListItemIcon sx={location.pathname === '/empresa/listar' ? { color: activeColor } : {}}>
                                <BusinessIcon />
                            </ListItemIcon>
                            <ListItemText primary="Listar" sx={location.pathname === '/empresa/listar' ? { color: activeColor } : {}} />
                        </ListItemButton>
                    </ListItem>
                </List>
            </Drawer>
            <Main open={open}>
                <MainDrawerHeader />
                {children}
            </Main>
        </Box>
    );
}