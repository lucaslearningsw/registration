function SetModal() {

    $(document).ready(function () {
        $(function () {
            $.ajaxSetup({ cache: false });

            $("a[data-modal]").on("click",
                function (e) {
                    $('#myModalContent').load(this.href,
                        function () {
                            $('#myModal').modal({
                                keyboard: true
                            },
                                'show');
                            bindForm(this);
                        });
                    return false;
                });
        });
    });
}

function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    $('#AddressTarget').load(result.url); // load result in HTML to the div with id AddressTrget
                } else {
                    $('#myModalContent').html(result);
                    bindForm(dialog);
                }
            }
        });

        SetModal();
        return false;
    });
}

function FindZipCode() {
    $(document).ready(function () {

        function clean_form_zipcode() {
            // Clean values from form zipcode
            $("#Address_District").val("");
            $("#Address_City").val("");
            $("#Address_State").val("");
        }

        //when field cep lost focus
        $("#Address_Cep").blur(function () {

            //new variable with just digits
            var cep = $(this).val().replace(/\D/g, '');

            //check if field has some value
            if (cep != "") {

                //Regular expression to validate cep
                var cepvalidate = /^[0-9]{8}$/;

                //Validate cep formt
                if (cepvalidate.test(cep)) {

                    //Fill in the fields with "..." while search in webservice.
                    $("#Address_District").val("...");
                    $("#Address_City").val("...");
                    $("#Address_State").val("...");

                    //Consulta o webservice viacep.com.br/
                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?",
                        function (dados) {

                            if (!("erro" in dados)) {
                                //update fields with values that come from search
                                $("#Address_District").val(dados.bairro);
                                $("#Address_City").val(dados.localidade);
                                $("#Address_State").val(dados.uf);
                            } //end if.
                            else {
                                //zipcdoe not found
                                clean_form_zipcode();
                                alert("CEP não encontrado.");
                            }
                        });
                } //end if.
                else {
                    //zipcode invalid
                    clean_form_zipcode();
                    alert("Formato de CEP inválido.");
                }
            } //end if.
            else {
                //zipcode without value 
                clean_form_zipcode();
            }
        });
    });
}

$(document).ready(function () {
    $("#msg_box").fadeOut(2500);
});