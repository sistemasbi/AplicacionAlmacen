function deshabilitarCbo() {
    var sector = document.getElementById('cboSector'); sector.disabled = true;
    var tipo = document.getElementById('cboTipo'); tipo.disabled = true;
    var valor = document.getElementById('cboValor'); valor.disabled = true;
}

function habilitarCbo() {
    var sector = document.getElementById('cboSector'); sector.disabled = false;
    var tipo = document.getElementById('cboTipo'); tipo.disabled = false;
    var valor = document.getElementById('cboValor'); valor.disabled = false;
}

function deshabilitarCboValor() {
    var valor = document.getElementById('cboValor'); valor.disabled = true;
}

function habilitarCboValor() {
    var valor = document.getElementById('cboValor'); valor.disabled = false;
}

function smsRegistrado() {
    alert('Politica registrada...');
}

function smsErrorPeso() {
    alert('Revise el Peso Total');
}

function editarPolitica() {
    var nmpolitica = document.getElementById('txtNombrePolitica'); nmpolitica.disabled = true;
    var fachaa = document.getElementById('txtVigenciaDesde'); fachaa.disabled = true;
    var btnsave = document.getElementById('btnGuardar'); btnsave.disabled = true;
    var fechab = document.getElementById('txtVigenciaHasta'); fechab.disabled = false;
}

function nuevaPolitica() {
    var nmpolitica = document.getElementById('txtNombrePolitica'); nmpolitica.disabled = false;
    var fachaa = document.getElementById('txtVigenciaDesde'); fachaa.disabled = false;
    var btnsave = document.getElementById('btnGuardar'); btnsave.disabled = false;
    var fechab = document.getElementById('txtVigenciaHasta'); fechab.disabled = true;
}

function smsEditado() {
    alert('Politica modificada');
}