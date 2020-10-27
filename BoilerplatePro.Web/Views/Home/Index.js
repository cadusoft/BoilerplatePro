$(function () {
    //Widgets count
    $('.count-to').countTo();

    //Sales count to
    $('.sales-count-to').countTo({
        formatter: function (value, options) {
            return '$' + value.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, ' ').replace('.', ',');
        }
    });

    initRealTimeChart();
    initDonutChart();
    initSparkline();
});

var realtime = 'on';
function initRealTimeChart() {
    var ArrayData = $.map($('#TenHourTransfers').val().split(','), function (value) {
        return parseInt(value, 10);
        // or return +value; which handles float values as well
    });

    var datarays = $('#TenHourTransfers').val();
    var dataray = datarays.split(',').map(function (item) {
        return parseInt(item, 10);
    });
    var maxnum = 2;
    var dt = new Date();
    var s = new Array(dataray.length);
    for (var i = 1; i < dataray.length + 1; i++) {
        s[i - 1] = [new Date().setHours(dt.getHours()-8+i,0,0,0), dataray[i - 1]];
        if (dataray[i - 1] > maxnum) {
            maxnum = dataray[i - 1];
        }
    }

    console.log(s);

    //Real time ==========================================================================================
    var plot = $.plot('#real_time_chart', [s], {
        series: {
            shadowSize: 0,
            color: 'rgb(0, 188, 212)'
        },
        grid: {
            borderColor: '#f3f3f3',
            borderWidth: 1,
            tickColor: '#f3f3f3'
        },
        lines: {
            fill: true
        },
        yaxis: {
            min: 0,
            max: maxnum,
			minorTickSize:100,
            tickSize: 1000,
            tickDecimals: 0
        },
        xaxis: {
            min: new Date().setHours(dt.getHours() -7,0,0,0),
            max: new Date().setHours(dt.getHours() +2,0,0,0),
            minTickSize: [1, "hour"],
            mode: "time",
            timeformat: "%h:%M"
        }
    });

    var timeout;

    //function updateRealTime() {
    //    var ArrayData = $.map($('#TenHourTransfers').val().split(','), function (value) {
    //        return parseInt(value, 10);
    //        // or return +value; which handles float values as well
    //    });

    //    plot.setData(ArrayData);
    //    plot.draw();
    //}

    //updateRealTime();

    function refreshPage() {
        window.location.reload();
    }

    function pad(n, width, z) {
        z = z || '0';
        n = n + '';
        return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
    }

    $('#realtime').on('change', function () {
        console.log("Changed refresh to " + this.checked);
        realtime = this.checked ? 'on' : 'off';

        if (realtime === 'on') {
            timeout = setTimeout(refreshPage, 300000);
        } else {
            clearTimeout(timeout);
        }
    });
    //====================================================================================================
}

function initSparkline() {
    $(".sparkline").each(function () {
        var $this = $(this);
        $this.sparkline('html', $this.data());
    });
}

function initDonutChart() {
    Morris.Donut({
        element: 'donut_chart',
        data: [{
            label: 'Success',
            value: $("#TransfersByStatusses").val().split(",")[0]
            }, {
                label: 'Waiting',
                value: $("#TransfersByStatusses").val().split(",")[1]
            }, {
                label: 'Failed',
                value: $("#TransfersByStatusses").val().split(",")[2]
            }],
        colors: ['rgb(0, 255, 0)', 'rgb(0, 150, 150)', 'rgb(255, 0, 0)'],
        formatter: function (y) {
            return y
        }
    });
}

var data = [], totalPoints = 110;
function getRandomData() {
    if (data.length > 0) data = data.slice(1);

    while (data.length < totalPoints) {
        var prev = data.length > 0 ? data[data.length - 1] : 50, y = prev + Math.random() * 10 - 5;
        if (y < 0) { y = 0; } else if (y > 100) { y = 100; }

        data.push(y);
    }

    var res = [];
    for (var i = 0; i < data.length; ++i) {
        res.push([i, data[i]]);
    }

    return res;
}