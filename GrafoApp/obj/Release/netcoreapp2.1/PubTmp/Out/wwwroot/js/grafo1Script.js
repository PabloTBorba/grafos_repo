﻿var x = new Chart(document.getElementById("grafo1"), {
    type: 'scatter',
    data: {
        datasets: dadosGrafo1
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
                    max: grafo1xAxe,
                    min: -grafo1xAxe
                },
                scaleLabel: {
                    display: true,
                    labelString: 'X'
                }
            }],
            yAxes: [{
                ticks: {
                    max: grafo1yAxe,
                    min: -grafo1yAxe
                },
                scaleLabel: {
                    display: true,
                    labelString: 'Y'
                }
            }]
        }
    }
});