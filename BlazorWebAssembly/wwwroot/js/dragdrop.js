// Gerenciador de arrastar e soltar aprimorado com animações e feedback visual
window.dragDropManager = {
    // Inicia o arrastar com efeito de elevação
    dragStart: function (event, id) {
        event.dataTransfer.setData("text/plain", id);
        const card = event.target.closest(".task-card");

        if (card) {
            card.classList.add("dragging");

            // Efeito de escala e sombra
            card.style.transform = "scale(1.02)";
            card.style.opacity = "0.8";

            // Criar uma imagem de preview personalizada (opcional)
            const preview = card.cloneNode(true);
            preview.style.width = card.offsetWidth + "px";
            preview.style.height = "80px";
            preview.style.opacity = "0.7";
            document.body.appendChild(preview);
            event.dataTransfer.setDragImage(preview, 10, 10);

            // Remover o elemento de preview após um curto período
            setTimeout(() => {
                if (preview && preview.parentNode) {
                    document.body.removeChild(preview);
                }
            }, 100);
        }
    },

    // Durante o arrastar sobre uma área
    dragOver: function (event) {
        event.preventDefault();

        // Encontra a lista de tarefas mais próxima
        const taskList = event.target.closest(".task-list");
        if (taskList) {
            // Adiciona classe para highlight
            taskList.classList.add("drag-over");

            // Adiciona efeito de pulsação
            if (!taskList.classList.contains("pulse-animation")) {
                taskList.classList.add("pulse-animation");
            }

            // Mostra um indicador de posição (opcional)
            this.showDropIndicator(event, taskList);
        }
    },

    // Quando sai da área
    dragLeave: function (event) {
        const taskList = event.target.closest(".task-list");
        const relatedTarget = event.relatedTarget?.closest(".task-list");

        // Somente remove o efeito se realmente saiu da área da lista
        if (taskList && taskList !== relatedTarget) {
            taskList.classList.remove("drag-over");
            taskList.classList.remove("pulse-animation");
            this.removeDropIndicator();
        }
    },

    // Quando solta o elemento
    drop: function (event, column, dotNetHelper) {
        event.preventDefault();
        const taskId = event.dataTransfer.getData("text/plain");

        // Remover indicador de posição
        this.removeDropIndicator();

        // Encontrar e restaurar estilo do elemento arrastado
        const draggingElement = document.querySelector(".dragging");
        if (draggingElement) {
            draggingElement.classList.remove("dragging");
            draggingElement.style.transform = "";
            draggingElement.style.opacity = "";

            // Adicionar efeito de conclusão
            draggingElement.classList.add("drop-complete");
            setTimeout(() => {
                draggingElement.classList.remove("drop-complete");
            }, 500);
        }

        // Encontra a lista de tarefas e remove estilos
        const taskList = event.target.closest(".task-list");
        if (taskList) {
            taskList.classList.remove("drag-over");
            taskList.classList.remove("pulse-animation");

            // Animação de confirmação na coluna de destino
            taskList.classList.add("drop-highlight");
            setTimeout(() => {
                taskList.classList.remove("drop-highlight");
            }, 300);

            // Chamar método .NET para atualizar o status
            dotNetHelper.invokeMethodAsync('AtualizaStatus', taskId, column)
                .then(() => {
                    console.log(`Tarefa ${taskId} movida para coluna ${column}`);
                })
                .catch(err => {
                    console.error("Erro ao atualizar status:", err);
                    this.showNotification("Erro ao mover tarefa", "error");
                });
        }
    },

    // Inicializar as referências do .NET e configurações
    initializeDragDrop: function (dotNetHelper) {
        window.dotNetHelper = dotNetHelper;
        console.log('Sistema de arrastar e soltar inicializado');

        // Adicionar estilos dinâmicos para os efeitos
        this.addDragDropStyles();
    },

    // Mostrar indicador de posição de soltar
    showDropIndicator: function (event, container) {
        // Remove qualquer indicador existente
        this.removeDropIndicator();

        // Cria um novo indicador
        const indicator = document.createElement('div');
        indicator.className = 'drop-indicator';
        indicator.style.width = container.offsetWidth - 20 + 'px';
        indicator.style.top = (event.clientY - container.getBoundingClientRect().top) + 'px';

        container.appendChild(indicator);
    },

    // Remover indicador de posição
    removeDropIndicator: function () {
        const indicators = document.querySelectorAll('.drop-indicator');
        indicators.forEach(indicator => indicator.remove());
    },

    // Mostrar notificação
    showNotification: function (message, severity) {
        // Se o MudBlazor estiver configurado com Snackbar
        if (window.mudBlazorSnackbar) {
            window.mudBlazorSnackbar.show(message, severity);
        } else {
            // Fallback para alert em caso de erro
            if (severity === 'error') {
                alert(message);
            } else {
                console.log(message);
            }
        }
    },

    // Adicionar estilos CSS dinâmicos para os efeitos
    addDragDropStyles: function () {
        if (!document.getElementById('drag-drop-styles')) {
            const styleSheet = document.createElement('style');
            styleSheet.id = 'drag-drop-styles';
            styleSheet.textContent = `
                .task-card {
                    transition: transform 0.2s, opacity 0.2s, box-shadow 0.2s;
                }
                .dragging {
                    box-shadow: 0 8px 16px rgba(0,0,0,0.2);
                    z-index: 1000;
                }
                .drag-over {
                    background-color: rgba(var(--mud-palette-primary-rgb), 0.05);
                    border: 2px dashed var(--mud-palette-primary);
                    border-radius: 4px;
                }
                .drop-indicator {
                    position: absolute;
                    height: 2px;
                    background-color: var(--mud-palette-primary);
                    left: 10px;
                    pointer-events: none;
                    z-index: 1000;
                    box-shadow: 0 0 6px var(--mud-palette-primary);
                }
                .drop-highlight {
                    animation: highlight-pulse 0.3s ease-in-out;
                }
                .drop-complete {
                    animation: drop-bounce 0.5s ease-out;
                }
                .pulse-animation {
                    animation: subtle-pulse 1.5s infinite;
                }
                @keyframes highlight-pulse {
                    0% { background-color: transparent; }
                    50% { background-color: rgba(var(--mud-palette-primary-rgb), 0.1); }
                    100% { background-color: transparent; }
                }
                @keyframes drop-bounce {
                    0% { transform: scale(1); }
                    50% { transform: scale(1.05); }
                    100% { transform: scale(1); }
                }
                @keyframes subtle-pulse {
                    0% { background-color: rgba(var(--mud-palette-primary-rgb), 0.02); }
                    50% { background-color: rgba(var(--mud-palette-primary-rgb), 0.08); }
                    100% { background-color: rgba(var(--mud-palette-primary-rgb), 0.02); }
                }
            `;
            document.head.appendChild(styleSheet);
        }
    }
};
