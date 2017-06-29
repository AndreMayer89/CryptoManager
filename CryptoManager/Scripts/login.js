
function Logar() {
    var uploadForm = $('#login-form');
    uploadForm.ajaxForm({
        dataType: 'json',
        beforeSend: function () {
        },
        uploadProgress: function (event, position, total, percentComplete) {
            var percentVal = percentComplete + '%';
            $('.progress-bar').width(percentVal);
        },
        complete: function (resultado) {
            mostrarCarteira(resultado.responseJSON);
        }
    });
    uploadForm.submit();
}