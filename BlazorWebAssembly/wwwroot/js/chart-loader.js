// Carregando Chart.js diretamente no arquivo index.html

// Função para criar o gráfico de pizza
window.createPieChart = function(elementId, labels, data, backgroundColor) {
    console.log('Criando gráfico de pizza:', elementId, labels, data);
    
    // Verificar se o elemento existe
    const canvas = document.getElementById(elementId);
    if (!canvas) {
        console.error('Elemento não encontrado:', elementId);
        return false;
    }
    
    // Verificar se o Chart.js está carregado
    if (!window.Chart) {
        console.error('Chart.js não está carregado');
        return false;
    }
    
    // Destruir o gráfico existente se houver
    if (window.taskChart) {
        window.taskChart.destroy();
    }
    
    try {
        const ctx = canvas.getContext('2d');
        
        window.taskChart = new Chart(ctx, {
            type: 'pie',
            data: {
                labels: labels,
                datasets: [{
                    data: data,
                    backgroundColor: backgroundColor,
                    borderColor: ['#fff', '#fff'],
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        position: 'bottom',
                        labels: {
                            color: '#333',
                            font: {
                                size: 14
                            }
                        }
                    },
                    tooltip: {
                        callbacks: {
                            label: function(context) {
                                const label = context.label || '';
                                const value = context.raw || 0;
                                const total = context.dataset.data.reduce((a, b) => a + b, 0);
                                const percentage = Math.round((value / total) * 100);
                                return `${label}: ${value} (${percentage}%)`;
                            }
                        }
                    }
                }
            }
        });
        
        console.log('Gráfico criado com sucesso');
        return true;
    } catch (error) {
        console.error('Erro ao criar gráfico:', error);
        return false;
    }
};
