$(document).ready( function() {
   /* Executa a requisição quando o campo CEP perder o foco */
   $('#idFase').change(function(){
           var tmp = '{"local": '+ '"' + $('#idLocal').val() + '",' +
                     '"modalidade":"' + $('#idModalidade').val() + '",' +
                     '"fase": '+ '"' + $('#idFase').val() + '",' + 
                     '"dia":"' + $('#idDia').val() + '",' +
                     '"inicio":"' + $('#idInicio').val() + '"}';
           $.ajax({
                url : 'consultar_professor.php', /* URL que será chamada */ 
                type : 'POST', /* Tipo da requisição */ 
                //data: 'local=' + $('#idLocal').val() + 'f=' + $('#idFrequencia').val(),
                data: {'rel':tmp},
                dataType: 'json', /* Tipo de transmissão */
                success: function(data){   
                    if(data.sucesso == 1){
                        $('#idProfessor').val(data.professor);
                        //$('#idBairro').val(data.bairro);
                        //$('#idCidade').val(data.cidade);
                        //$('#idUF').val(data.estado);
                        
                        //$('#idNumero').val("");
                        //$('#idNumero').focus();
                    }
                }
           });   
   return false;    
   })
   
   /* Executa a requisição quando o campo CEP perder o foco */
   $('#idDia').change(function(){
           var tmp = '{"local": '+ '"' + $('#idLocal').val() + '",' +
                     '"modalidade":"' + $('#idModalidade').val() + '",' +
                     '"fase": '+ '"' + $('#idFase').val() + '",' + 
                     '"dia":"' + $('#idDia').val() + '",' +
                     '"inicio":"' + $('#idInicio').val() + '"}';
           $.ajax({
                url : 'consultar_professor.php', /* URL que será chamada */ 
                type : 'POST', /* Tipo da requisição */ 
                //data: 'local=' + $('#idLocal').val() + 'f=' + $('#idFrequencia').val(),
                data: {'rel':tmp},
                dataType: 'json', /* Tipo de transmissão */
                success: function(data){   
                    if(data.sucesso == 1){
                        $('#idProfessor').val(data.professor);
                        //$('#idBairro').val(data.bairro);
                        //$('#idCidade').val(data.cidade);
                        //$('#idUF').val(data.estado);
                        
                        //$('#idNumero').val("");
                        //$('#idNumero').focus();
                    }
                }
           });   
   return false;    
   })
   
    /* Executa a requisição quando o campo CEP perder o foco */
   $('#idInicio').change(function(){
           var tmp = '{"local": '+ '"' + $('#idLocal').val() + '",' +
                     '"modalidade":"' + $('#idModalidade').val() + '",' +
                     '"fase": '+ '"' + $('#idFase').val() + '",' + 
                     '"dia":"' + $('#idDia').val() + '",' +
                     '"inicio":"' + $('#idInicio').val() + '"}';
           $.ajax({
                url : 'consultar_professor.php', /* URL que será chamada */ 
                type : 'POST', /* Tipo da requisição */ 
                //data: 'local=' + $('#idLocal').val() + 'f=' + $('#idFrequencia').val(),
                data: {'rel':tmp},
                dataType: 'json', /* Tipo de transmissão */
                success: function(data){   
                    if(data.sucesso == 1){
                        $('#idProfessor').val(data.professor);
                        //$('#idBairro').val(data.bairro);
                        //$('#idCidade').val(data.cidade);
                        //$('#idUF').val(data.estado);
                        
                        //$('#idNumero').val("");
                        //$('#idNumero').focus();
                    }
                }
           });   
   return false;    
   })
   
    
   /* Executa a requisição quando o campo CEP perder o foco */
   $('#idDia2x').change(function(){
           var tmp = '{"local": '+ '"' + $('#idLocal').val() + '",' +
                     '"modalidade":"' + $('#idModalidade').val() + '",' +
                     '"fase": '+ '"' + $('#idFase').val() + '",' + 
                     '"dia":"' + $('#idDia2x').val() + '",' +
                     '"inicio":"' + $('#idInicio2x').val() + '"}';
           $.ajax({
                url : 'consultar_professor.php', /* URL que será chamada */ 
                type : 'POST', /* Tipo da requisição */ 
                //data: 'local=' + $('#idLocal').val() + 'f=' + $('#idFrequencia').val(),
                data: {'rel':tmp},
                dataType: 'json', /* Tipo de transmissão */
                success: function(data){   
                    if(data.sucesso == 1){
                        $('#idProfessor2x').val(data.professor);
                        //$('#idBairro').val(data.bairro);
                        //$('#idCidade').val(data.cidade);
                        //$('#idUF').val(data.estado);
                        
                        //$('#idNumero').val("");
                        //$('#idNumero').focus();
                    }
                }
           });   
   return false;    
   })
   
    /* Executa a requisição quando o campo CEP perder o foco */
   $('#idInicio2x').change(function(){
           var tmp = '{"local": '+ '"' + $('#idLocal').val() + '",' +
                     '"modalidade":"' + $('#idModalidade').val() + '",' +
                     '"fase": '+ '"' + $('#idFase').val() + '",' + 
                     '"dia":"' + $('#idDia2x').val() + '",' +
                     '"inicio":"' + $('#idInicio2x').val() + '"}';
           $.ajax({
                url : 'consultar_professor.php', /* URL que será chamada */ 
                type : 'POST', /* Tipo da requisição */ 
                //data: 'local=' + $('#idLocal').val() + 'f=' + $('#idFrequencia').val(),
                data: {'rel':tmp},
                dataType: 'json', /* Tipo de transmissão */
                success: function(data){   
                    if(data.sucesso == 1){
                        $('#idProfessor2x').val(data.professor);
                        //$('#idBairro').val(data.bairro);
                        //$('#idCidade').val(data.cidade);
                        //$('#idUF').val(data.estado);
                        
                        //$('#idNumero').val("");
                        //$('#idNumero').focus();
                    }
                }
           });   
   return false;    
   })
   
   /* Executa a requisição quando o campo CEP perder o foco */
   $('#idDia3x').change(function(){
           var tmp = '{"local": '+ '"' + $('#idLocal').val() + '",' +
                     '"modalidade":"' + $('#idModalidade').val() + '",' +
                     '"fase": '+ '"' + $('#idFase').val() + '",' + 
                     '"dia":"' + $('#idDia3x').val() + '",' +
                     '"inicio":"' + $('#idInicio3x').val() + '"}';
           $.ajax({
                url : 'consultar_professor.php', /* URL que será chamada */ 
                type : 'POST', /* Tipo da requisição */ 
                //data: 'local=' + $('#idLocal').val() + 'f=' + $('#idFrequencia').val(),
                data: {'rel':tmp},
                dataType: 'json', /* Tipo de transmissão */
                success: function(data){   
                    if(data.sucesso == 1){
                        $('#idProfessor3x').val(data.professor);
                        //$('#idBairro').val(data.bairro);
                        //$('#idCidade').val(data.cidade);
                        //$('#idUF').val(data.estado);
                        
                        //$('#idNumero').val("");
                        //$('#idNumero').focus();
                    }
                }
           });   
   return false;    
   })
   
    /* Executa a requisição quando o campo CEP perder o foco */
   $('#idInicio3x').change(function(){
           var tmp = '{"local": '+ '"' + $('#idLocal').val() + '",' +
                     '"modalidade":"' + $('#idModalidade').val() + '",' +
                     '"fase": '+ '"' + $('#idFase').val() + '",' + 
                     '"dia":"' + $('#idDia3x').val() + '",' +
                     '"inicio":"' + $('#idInicio3x').val() + '"}';
           $.ajax({
                url : 'consultar_professor.php', /* URL que será chamada */ 
                type : 'POST', /* Tipo da requisição */ 
                //data: 'local=' + $('#idLocal').val() + 'f=' + $('#idFrequencia').val(),
                data: {'rel':tmp},
                dataType: 'json', /* Tipo de transmissão */
                success: function(data){   
                    if(data.sucesso == 1){
                        $('#idProfessor3x').val(data.professor);
                        //$('#idBairro').val(data.bairro);
                        //$('#idCidade').val(data.cidade);
                        //$('#idUF').val(data.estado);
                        
                        //$('#idNumero').val("");
                        //$('#idNumero').focus();
                    }
                }
           });   
   return false;    
   })
   
   /* Executa a requisição quando o campo CEP perder o foco */
   $('#idDia4x').change(function(){
           var tmp = '{"local": '+ '"' + $('#idLocal').val() + '",' +
                     '"modalidade":"' + $('#idModalidade').val() + '",' +
                     '"fase": '+ '"' + $('#idFase').val() + '",' + 
                     '"dia":"' + $('#idDia4x').val() + '",' +
                     '"inicio":"' + $('#idInicio4x').val() + '"}';
           $.ajax({
                url : 'consultar_professor.php', /* URL que será chamada */ 
                type : 'POST', /* Tipo da requisição */ 
                //data: 'local=' + $('#idLocal').val() + 'f=' + $('#idFrequencia').val(),
                data: {'rel':tmp},
                dataType: 'json', /* Tipo de transmissão */
                success: function(data){   
                    if(data.sucesso == 1){
                        $('#idProfessor4x').val(data.professor);
                        //$('#idBairro').val(data.bairro);
                        //$('#idCidade').val(data.cidade);
                        //$('#idUF').val(data.estado);
                        
                        //$('#idNumero').val("");
                        //$('#idNumero').focus();
                    }
                }
           });   
   return false;    
   })
   
    /* Executa a requisição quando o campo CEP perder o foco */
   $('#idInicio4x').change(function(){
           var tmp = '{"local": '+ '"' + $('#idLocal').val() + '",' +
                     '"modalidade":"' + $('#idModalidade').val() + '",' +
                     '"fase": '+ '"' + $('#idFase').val() + '",' + 
                     '"dia":"' + $('#idDia4x').val() + '",' +
                     '"inicio":"' + $('#idInicio4x').val() + '"}';
           $.ajax({
                url : 'consultar_professor.php', /* URL que será chamada */ 
                type : 'POST', /* Tipo da requisição */ 
                //data: 'local=' + $('#idLocal').val() + 'f=' + $('#idFrequencia').val(),
                data: {'rel':tmp},
                dataType: 'json', /* Tipo de transmissão */
                success: function(data){   
                    if(data.sucesso == 1){
                        $('#idProfessor4x').val(data.professor);
                        //$('#idBairro').val(data.bairro);
                        //$('#idCidade').val(data.cidade);
                        //$('#idUF').val(data.estado);
                        
                        //$('#idNumero').val("");
                        //$('#idNumero').focus();
                    }
                }
           });   
   return false;    
   })
   
   /* Executa a requisição quando o campo CEP perder o foco */
   $('#idDia5x').change(function(){
           var tmp = '{"local": '+ '"' + $('#idLocal').val() + '",' +
                     '"modalidade":"' + $('#idModalidade').val() + '",' +
                     '"fase": '+ '"' + $('#idFase').val() + '",' + 
                     '"dia":"' + $('#idDia5x').val() + '",' +
                     '"inicio":"' + $('#idInicio5x').val() + '"}';
           $.ajax({
                url : 'consultar_professor.php', /* URL que será chamada */ 
                type : 'POST', /* Tipo da requisição */ 
                //data: 'local=' + $('#idLocal').val() + 'f=' + $('#idFrequencia').val(),
                data: {'rel':tmp},
                dataType: 'json', /* Tipo de transmissão */
                success: function(data){   
                    if(data.sucesso == 1){
                        $('#idProfessor5x').val(data.professor);
                        //$('#idBairro').val(data.bairro);
                        //$('#idCidade').val(data.cidade);
                        //$('#idUF').val(data.estado);
                        
                        //$('#idNumero').val("");
                        //$('#idNumero').focus();
                    }
                }
           });   
   return false;    
   })
   
    /* Executa a requisição quando o campo CEP perder o foco */
   $('#idInicio5x').change(function(){
           var tmp = '{"local": '+ '"' + $('#idLocal').val() + '",' +
                     '"modalidade":"' + $('#idModalidade').val() + '",' +
                     '"fase": '+ '"' + $('#idFase').val() + '",' + 
                     '"dia":"' + $('#idDia5x').val() + '",' +
                     '"inicio":"' + $('#idInicio5x').val() + '"}';
           $.ajax({
                url : 'consultar_professor.php', /* URL que será chamada */ 
                type : 'POST', /* Tipo da requisição */ 
                //data: 'local=' + $('#idLocal').val() + 'f=' + $('#idFrequencia').val(),
                data: {'rel':tmp},
                dataType: 'json', /* Tipo de transmissão */
                success: function(data){   
                    if(data.sucesso == 1){
                        $('#idProfessor5x').val(data.professor);
                        //$('#idBairro').val(data.bairro);
                        //$('#idCidade').val(data.cidade);
                        //$('#idUF').val(data.estado);
                        
                        //$('#idNumero').val("");
                        //$('#idNumero').focus();
                    }
                }
           });   
   return false;    
   })
   
});
