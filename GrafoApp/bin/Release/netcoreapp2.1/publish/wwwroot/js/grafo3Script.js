var x = new Chart(document.getElementById("grafo3"), {
    type: 'scatter',
    data: {
        datasets: dadosGrafo3
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
                    max: grafo3xAxe,
                    min: -grafo3xAxe
                },
                scaleLabel: {
                    display: true,
                    labelString: 'X'
                }
            }],
            yAxes: [{
                ticks: {
                    max: grafo3yAxe,
                    min: -grafo3yAxe
                },
                scaleLabel: {
                    display: true,
                    labelString: 'Y'
                }
            }]
        }
    }
});