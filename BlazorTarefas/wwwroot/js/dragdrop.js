// Funções para manipular arrastar e soltar
window.dragDropManager = {
    // Inicia o arrastar
    dragStart: function (event, id) {
        event.dataTransfer.setData("text/plain", id);
        event.target.classList.add("dragging");
    },
    
    // Durante o arrastar sobre uma área
    dragOver: function (event) {
        event.preventDefault();
        if (event.target.classList.contains("task-list") || 
            event.target.closest(".task-list")) {
            
            const taskList = event.target.classList.contains("task-list") 
                ? event.target 
                : event.target.closest(".task-list");
                
            taskList.classList.add("drag-over");
        }
    },
    
    // Quando sai da área
    dragLeave: function (event) {
        if (event.target.classList.contains("task-list") || 
            event.target.closest(".task-list")) {
            
            const taskList = event.target.classList.contains("task-list") 
                ? event.target 
                : event.target.closest(".task-list");
                
            taskList.classList.remove("drag-over");
        }
    },
    
    // Quando solta o elemento
    drop: function (event, column, dotNetHelper) {
        event.preventDefault();
        const taskId = event.dataTransfer.getData("text/plain");
        
        // Remover classes de estilo
        const draggingElement = document.querySelector(".dragging");
        if (draggingElement) {
            draggingElement.classList.remove("dragging");
        }
        
        if (event.target.classList.contains("task-list") || 
            event.target.closest(".task-list")) {
            
            const taskList = event.target.classList.contains("task-list") 
                ? event.target 
                : event.target.closest(".task-list");
                
            taskList.classList.remove("drag-over");
            
            // Chamar método .NET para atualizar o status
            dotNetHelper.invokeMethodAsync('UpdateTaskStatus', taskId, column);
            
            console.log(`Tarefa ${taskId} movida para ${column}`);
        }
    },

    // Inicializar as referências do .NET
    initializeDragDrop: function (dotNetHelper) {
        window.dotNetHelper = dotNetHelper;
        console.log('Drag and drop inicializado com sucesso');
    }
};
