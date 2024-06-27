function loadWidget() {
    var script = document.createElement('script');
    script.src = "https://app.calculojuridico.com.br/script/contribution_time_widget.js";
    script.async = true;
    document.body.appendChild(script);

    var div = document.createElement('div');
    div.id = 'cj-widget';
    div.setAttribute('data-src', 'https://app.calculojuridico.com.br/embedded/contribution_time_calculation_widgets/start?rel=lp');
    document.body.appendChild(div);
}