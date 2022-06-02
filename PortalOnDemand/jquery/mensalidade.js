$(document).ready( function() {
   /* Executa a requisição quando o campo CEP perder o foco */
   $('#idPlano').change(function(){
       alert("teste");
           var tmp = '{"local": '+ '"' + $('#idLocal').val() + '",' + 
                     '"f":"' + $('#idFrequencia').val() + '",' +
                     '"m":"' + $('#idModalidade').val() + '",' +
                     '"p":"' + $('#idPlano').val() + '"}';
           $.ajax({
                url : 'consultar_plano.php', /* URL que será chamada */ 
                type : 'POST', /* Tipo da requisição */ 
                //data: 'local=' + $('#idLocal').val() + 'f=' + $('#idFrequencia').val(),
                data: {'rel':tmp},
                dataType: 'json', /* Tipo de transmissão */
                success: function(data){
                    if(data.sucesso == 1){
                        $('#idMensalidade').val(data.valor);
                        $('#idTaxa').val(data.taxa);
                    }
                }
           });   
   return false;    
   })
   
   /* Executa a requisição quando o campo CEP perder o foco */
   $('#idLocal').change(function(){
           var tmp = '{"local": '+ '"' + $('#idLocal').val() + '",' + 
                     '"f":"' + $('#idFrequencia').val() + '",' +
                     '"m":"' + $('#idModalidade').val() + '",' +
                     '"p":"' + $('#idPlano').val() + '"}';
           $.ajax({
                url : 'consultar_mensalidade.php', /* URL que será chamada */ 
                type : 'POST', /* Tipo da requisição */ 
                //data: 'local=' + $('#idLocal').val() + 'f=' + $('#idFrequencia').val(),
                data: {'rel':tmp},
                dataType: 'json', /* Tipo de transmissão */
                success: function(data){
                    if(data.sucesso == 1){
                        $('#idMensalidade').val(data.valor);
                        $('#idTaxa').val(data.taxa);
                    }
                }
           });   
   return false;    
   })   
   
   /* Executa a requisição quando o campo CEP perder o foco */
   $('#idFrequencia').change(function(){
           var tmp = '{"local": '+ '"' + $('#idLocal').val() + '",' + 
                     '"f":"' + $('#idFrequencia').val() + '",' +
                     '"m":"' + $('#idModalidade').val() + '",' +
                     '"p":"' + $('#idPlano').val() + '"}';
           $.ajax({
                url : 'consultar_mensalidade.php', /* URL que será chamada */ 
                type : 'POST', /* Tipo da requisição */ 
                //data: 'local=' + $('#idLocal').val() + 'f=' + $('#idFrequencia').val(),
                data: {'rel':tmp},
                dataType: 'json', /* Tipo de transmissão */
                success: function(data){
                    if(data.sucesso == 1){
                        $('#idMensalidade').val(data.valor);
                        $('#idTaxa').val(data.taxa);
                    }
                }
           });   
   return false;    
   })
   
   /* Executa a requisição quando o campo CEP perder o foco */
   $('#idModalidade').change(function(){
           var tmp = '{"local": '+ '"' + $('#idLocal').val() + '",' + 
                     '"f":"' + $('#idFrequencia').val() + '",' +
                     '"m":"' + $('#idModalidade').val() + '",' +
                     '"p":"' + $('#idPlano').val() + '"}';
           $.ajax({
                url : 'consultar_mensalidade.php', /* URL que será chamada */ 
                type : 'POST', /* Tipo da requisição */ 
                //data: 'local=' + $('#idLocal').val() + 'f=' + $('#idFrequencia').val(),
                data: {'rel':tmp},
                dataType: 'json', /* Tipo de transmissão */
                success: function(data){
                    if(data.sucesso == 1){
                        $('#idMensalidade').val(data.valor);
                        $('#idTaxa').val(data.taxa);
                    }
                }
           });   
   return false;    
   })
   
   
});

