var x = new Chart(document.getElementById("grafo5"), {
    type: 'scatter',
    data: {
        datasets: dadosGrafo5
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
                    max: grafo5xAxe,
                    min: -grafo5xAxe
                },
                scaleLabel: {
                    display: true,
                    labelString: 'X'
                }
            }],
            yAxes: [{
                ticks: {
                    max: grafo5yAxe,
                    min: -grafo5yAxe
                },
                scaleLabel: {
                    display: true,
                    labelString: 'Y'
                }
            }]
        }
    }
});