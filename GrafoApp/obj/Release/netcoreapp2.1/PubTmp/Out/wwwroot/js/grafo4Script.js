var x = new Chart(document.getElementById("grafo4"), {
    type: 'scatter',
    data: {
        datasets: dadosGrafo4
    },
    options: {
        plugins: {
            datalabels: {
                backgroundColor: function (context) {
                    return context.dataset.backgroundColor;
                },
                borderRadius: 4,
                color: 'white',
                font: {
                    weight: 'bold'
                },
                formatter: function (value, context) {
                    return context.dataset.data[context.dataIndex].label;
                }
            }
        },
        showLines: true,
        responsive: true,
        lineTension: 0,
        scales: {
            xAxes: [{
                ticks: {
                    max: grafo4xAxe,
                    min: -grafo4xAxe
                },
                scaleLabel: {
                    display: true,
                    labelString: 'X'
                }
            }],
            yAxes: [{
                ticks: {
                    max: grafo4yAxe,
                    min: -grafo4yAxe
                },
                scaleLabel: {
                    display: true,
                    labelString: 'Y'
                }
            }]
        }
    }
});