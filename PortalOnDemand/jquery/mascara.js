 function maskIt(w,e,m,r,a){
    // Cancela se o evento for Backspace
    if (!e) var e = window.event
    if (e.keyCode) code = e.keyCode;
    else if (e.which) code = e.which;
    // Variáveis da função
    var txt  = (!r) ? w.value.replace(/[^\d]+/gi,'') : w.value.replace(/[^\d]+/gi,'').reverse();
    var mask = (!r) ? m : m.reverse();
    var pre  = (a ) ? a.pre : "";
    var pos  = (a ) ? a.pos : "";
    var ret  = "";
    if(code == 9 || code == 8 || txt.length == mask.replace(/[^#]+/g,'').length) return false;
    // Loop na máscara para aplicar os caracteres
    for(var x=0,y=0, z=mask.length;x<z && y<txt.length;){
    if(mask.charAt(x)!='#'){
    ret += mask.charAt(x); x++; } 
    else {
    ret += txt.charAt(y); y++; x++; } }
    // Retorno da função
    ret = (!r) ? ret : ret.reverse()	
    w.value = pre+ret+pos; 
}
// Novo método para o objeto 'String'
String.prototype.reverse = function(){
return this.split('').reverse().join(''); };

 function maskCPF(CPF) {
        //var CPF = document.getElementById("idCPF");
        //alert(CPF.value);
 	var evt = window.event;
 	kcode=evt.keyCode;
 	if (kcode == 8) return;
 	if (CPF.value.length == 3) { CPF.value = CPF.value + '.'; }
 	if (CPF.value.length == 7) { CPF.value = CPF.value + '.'; }
 	if (CPF.value.length == 11) { CPF.value = CPF.value + '-'; }
 }
 
    function maskCEP(CEP) {
        //var CEP = document.getElementById("idCEP");
        //alert(CPF.value);
 	var evt = window.event;
 	kcode=evt.keyCode;
 	if (kcode == 8) return;
 	if (CEP.value.length == 5) { CEP.value = CEP.value + '-'; }
 }
 function maskTelefone(TEL) {
        //var TEL = document.getElementById("idTelefone");
        //alert(CPF.value);
 	var evt = window.event;
 	kcode=evt.keyCode;
 	if (kcode == 8) return;
 	if (TEL.value.length == 1) { TEL.value = '(' + TEL.value; }
        if (TEL.value.length == 3) { TEL.value = TEL.value + ')'; }
        if (TEL.value.length == 4) { TEL.value = TEL.value + ' '; }
        if (TEL.value.length == 9) { TEL.value = TEL.value + '-'; }
        
 }
 function maskCelular(CEL) {
        //var TEL = document.getElementById("idTelefone");
        //alert(CPF.value);
 	var evt = window.event;
 	kcode=evt.keyCode;
 	if (kcode == 8) return;
 	if (CEL.value.length == 1) { CEL.value = '(' + CEL.value; }
        if (CEL.value.length == 3) { CEL.value = CEL.value + ')'; }
        if (CEL.value.length == 4) { CEL.value = CEL.value + ' '; }
        if (CEL.value.length == 10) { CEL.value = CEL.value + '-'; }
        
 }
 function maskHora(HORA) {
        //var CPF = document.getElementById("idCPF");
        //alert(CPF.value);
 	var evt = window.event;
 	kcode=evt.keyCode;
 	if (kcode == 8) return;
 	if (HORA.value.length == 2) { HORA.value = HORA.value + ':'; }
 	
 }
 function maskHora2(HORA) {
        //var CPF = document.getElementById("idCPF");
        //alert(CPF.value);
    var evt = window.event;
    kcode=evt.keyCode;
    if (kcode == 8) return;
    if (HORA.value.length == 2) { HORA.value = HORA.value + ':'; }
    if (HORA.value.length == 5) { HORA.value = HORA.value + ':'; }
    
 }



