function loadWidget() {
    // Remove any existing widget if present
    removeWidget();

    var script = document.createElement('script');
    script.id = 'widget-script';
    script.src = "https://app.calculojuridico.com.br/script/contribution_time_widget.js";
    script.async = true;
    document.body.appendChild(script);

    var div = document.createElement('div');
    div.id = 'cj-widget';
    div.setAttribute('data-src', 'https://app.calculojuridico.com.br/embedded/contribution_time_calculation_widgets/start?rel=lp');
    document.getElementById('widget-container').appendChild(div);
}

function removeWidget() {
    var script = document.getElementById('widget-script');
    if (script) {
        document.body.removeChild(script);
    }

    var div = document.getElementById('cj-widget');
    if (div) {
        document.getElementById('widget-container').removeChild(div);
    }
}
