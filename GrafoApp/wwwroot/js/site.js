// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

Chart.helpers.merge(Chart.defaults.global, {
    tooltips: false,
    elements: {
        line: {
            fill: false
        }
    },
    plugins: {
        legend: false,
        title: false
    }
});
