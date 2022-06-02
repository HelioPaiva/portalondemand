$(document).ready( function() {
   /* Executa a requisição quando o campo CEP perder o foco */
   $('#idCEP').blur(function(){
           /* Configura a requisição AJAX */
           $.ajax({
                url : 'consultar_cep.php', /* URL que será chamada */ 
                type : 'POST', /* Tipo da requisição */ 
                data: 'cep=' + $('#idCEP').val(), /* dado que será enviado via POST */
                dataType: 'json', /* Tipo de transmissão */
                success: function(data){
                    if(data.sucesso == 1){
                        $('#idEndereco').val(data.rua);
                        $('#idBairro').val(data.bairro);
                        $('#idCidade').val(data.cidade);
                        $('#idUF').val(data.estado);
                        
                        $('#idNumero').val("");
                        $('#idNumero').focus();
                    }
                }
           });   
   return false;    
   })
});

