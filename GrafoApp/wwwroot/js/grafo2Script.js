var x = new Chart(document.getElementById("grafo2"), {
    type: 'scatter',
    data: {
        datasets: dadosGrafo2
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
                    max: grafo2xAxe,
                    min: -grafo2xAxe
                },
                scaleLabel: {
                    display: true,
                    labelString: 'X'
                }
            }],
            yAxes: [{
                ticks: {
                    max: grafo2yAxe,
                    min: -grafo2yAxe
                },
                scaleLabel: {
                    display: true,
                    labelString: 'Y'
                }
            }]
        }
    }
});