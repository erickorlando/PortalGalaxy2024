Chart.register(ChartDataLabels);

window.setup = (id, config) => {

    // el siguiente codigo se encarga de borrar el canvas
    // para redibujar el chart

    var ctx = document.getElementById(id).getContext('2d');

    var myChart = Chart.getChart(id);
    if (myChart !== undefined) {
        myChart.destroy();
    }

    myChart = new Chart(ctx, config);
}