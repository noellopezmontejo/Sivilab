
$(document).ready(function () {
    $('#competencias').select2({
        placeholder: "Seleccione competencias",
        allowClear: true
    });
});

// Función para registrar (simulación)
function registrar() {
    // Aquí puedes agregar la lógica para enviar los datos a tu servidor
    alert('Datos registrados exitosamente.');
    regresarInicio();
}

// Función para registrar empleador (simulación)
function registrarEmpleador() {
    // Aquí puedes agregar la lógica para enviar los datos a tu servidor
    alert('Oferta de empleo registrada exitosamente.');
    regresarInicio();
}

// Función para regresar a las opciones de empleo
function regresarOpcionesEmpleo() {
    // Oculta todo lo relacionado al formulario
    document.getElementById('formularioDatos').style.display = 'none';
    document.getElementById('formularioEmpleador').style.display = 'none';
    document.getElementById('stepper').style.display = 'none';
    document.getElementById('stepperEmpleador').style.display = 'none';

    // Muestra de nuevo las opciones de empleo
    document.getElementById('opcionesEmpleo').style.display = 'block';

    // Reinicia los pasos actuales
    pasoActual = 1;
    pasoActualEmp = 1;
}

// Función para regresar al inicio del formulario
function regresarInicio() {
    // Oculta todo lo relacionado al formulario
    document.getElementById('formularioDatos').style.display = 'none';
    document.getElementById('formularioEmpleador').style.display = 'none';
    document.getElementById('stepper').style.display = 'none';
    document.getElementById('stepperEmpleador').style.display = 'none';

    // Muestra de nuevo las opciones de empleo
    document.getElementById('opcionesEmpleo').style.display = 'block';

    // Limpia todos los inputs del formulario (por si se escribieron cosas)
    const inputs = document.querySelectorAll('#formularioDatos input, #formularioEmpleador input, #formularioEmpleador textarea, #formularioEmpleador select');
    inputs.forEach(input => input.value = '');

    // Reinicia pasos visibles (muestra solo paso 1)
    const steps = document.querySelectorAll('.form-step');
    steps.forEach(step => step.classList.remove('active'));
    document.getElementById('paso1').classList.add('active');
    document.getElementById('pasoEmp1').classList.add('active');
}
  
let pasoActual = 1;
let pasoActualEmp = 1;

function validateCURP(curp) {
    const regex = /^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/;
    return regex.test(curp);
}

function validarCURP() {
    const curpInput = document.getElementById('curpInput');
    const curpValue = curpInput.value.trim();

    // Verificar si el CURP es válido
    if (!curpValue || !validateCURP(curpValue)) {
        alert('Por favor ingresa un CURP válido.');
        return;
    }

    // Si es válido, mostrar las opciones de empleo
    document.getElementById('opcionesEmpleo').style.display = 'block';
}

function mostrarFormulario() {
    // Asignar el valor al campo oculto
    const curpValue = document.getElementById('curpInput').value.trim();
    document.getElementById('curp').value = curpValue;

    // Ocultar opciones de empleo
    document.getElementById('opcionesEmpleo').style.display = 'none';
            
    // Mostrar el formulario de buscador de empleo
    document.getElementById('formularioDatos').style.display = 'block';
    document.getElementById('stepper').style.display = 'flex';

    // Ocultar formulario de empleador si está visible
    document.getElementById('formularioEmpleador').style.display = 'none';
    document.getElementById('stepperEmpleador').style.display = 'none';

    mostrarPaso(1);
}

function mostrarFormularioEmpleador() {
    // Ocultar opciones de empleo
    document.getElementById('opcionesEmpleo').style.display = 'none';
            
    // Mostrar el formulario de empleador
    document.getElementById('formularioEmpleador').style.display = 'block';
    document.getElementById('stepperEmpleador').style.display = 'flex';

    // Ocultar formulario de buscador de empleo si está visible
    document.getElementById('formularioDatos').style.display = 'none';
    document.getElementById('stepper').style.display = 'none';

    mostrarPasoEmp(1);
}

function mostrarPaso(numero) {
    const pasos = document.querySelectorAll('#formularioDatos .form-step');
    const anterior = document.getElementById(`paso${pasoActual}`);
    const nuevo = document.getElementById(`paso${numero}`);

    pasos.forEach(paso => {
        paso.classList.remove('active', 'slide-out-left', 'slide-out-right');
        paso.style.display = 'none';
    });

    if (pasoActual < numero) {
        anterior.classList.add('slide-out-left');
    } else if (pasoActual > numero) {
        anterior.classList.add('slide-out-right');
    }

    nuevo.style.display = 'grid';
    // Pequeño retraso para activar la transición
    setTimeout(() => {
        nuevo.classList.add('active');
    }, 10);

    pasoActual = numero;

    // Mostrar el botón registrar solo en el último paso
    document.getElementById("registrarBtn").style.display = numero === 8 ? "inline-block" : "none";
            
    // Actualizar stepper visual
    actualizarStepper(numero);
}

function mostrarPasoEmp(numero) {
    const pasos = document.querySelectorAll('#formularioEmpleador .form-step');
    const anterior = document.getElementById(`pasoEmp${pasoActualEmp}`);
    const nuevo = document.getElementById(`pasoEmp${numero}`);

    pasos.forEach(paso => {
        paso.classList.remove('active', 'slide-out-left', 'slide-out-right');
        paso.style.display = 'none';
    });

    if (pasoActualEmp < numero) {
        anterior.classList.add('slide-out-left');
    } else if (pasoActualEmp > numero) {
        anterior.classList.add('slide-out-right');
    }

    nuevo.style.display = 'grid';
    setTimeout(() => {
        nuevo.classList.add('active');
    }, 10);

    pasoActualEmp = numero;

    // Mostrar el botón registrar solo en el último paso
    document.getElementById("registrarEmpBtn").style.display = numero === 4 ? "inline-block" : "none";
            
    // Actualizar stepper visual
    actualizarStepperEmp(numero);
}

function actualizarStepper(step) {
    const steps = document.querySelectorAll('#stepper .step');
    const arrows = document.querySelectorAll('#stepper .arrow');

    steps.forEach((stepEl, idx) => {
        stepEl.classList.remove('active', 'completed');
        if (idx < step - 1) {
            stepEl.classList.add('completed');
        } else if (idx === step - 1) {
            stepEl.classList.add('active');
        }
    });

    arrows.forEach((arrowEl, idx) => {
        if (idx < step - 1) {
            arrowEl.classList.add('active');
        } else {
            arrowEl.classList.remove('active');
        }
    });
}

function actualizarStepperEmp(step) {
    const steps = document.querySelectorAll('#stepperEmpleador .step');
    const arrows = document.querySelectorAll('#stepperEmpleador .arrow');

    steps.forEach((stepEl, idx) => {
    stepEl.classList.remove('active', 'completed');
        if (idx < step - 1) {
            stepEl.classList.add('completed');
        } else if (idx === step - 1) {
            stepEl.classList.add('active');
        }
    });

    arrows.forEach((arrowEl, idx) => {
        if (idx < step - 1) {
            arrowEl.classList.add('active');
        } else {
            arrowEl.classList.remove('active');
        }
    });
}

// Event listeners para los botones de opciones de empleo
document.getElementById('buscoEmpleoBtn').addEventListener('click', function(e) {
    e.preventDefault();
    mostrarFormulario();
});
        
document.getElementById('ofrezcoEmpleoBtn').addEventListener('click', function(e) {
    e.preventDefault();
    mostrarFormularioEmpleador();
});

// Control del modal de inicio de sesión
const loginLink = document.getElementById('open-login');
const loginModal = document.getElementById('login-modal');
const closeLoginBtn = document.getElementById('close-login-modal');

// Abrir modal al hacer clic en "Iniciar sesión"
loginLink.addEventListener('click', function (e) {
    e.preventDefault();
    loginModal.style.display = 'flex';
});

// Cerrar modal al hacer clic en "Regresar"
closeLoginBtn.addEventListener('click', function () {
    loginModal.style.display = 'none';
});

// Manejar envío del formulario
document.getElementById('login-form').addEventListener('submit', function (e) {
    e.preventDefault();
    const email = this.querySelector('input[type="email"]').value;
    const password = this.querySelector('input[type="password"]').value;

    // Simulación de inicio de sesión (puedes reemplazar con lógica real)
    alert(`Correo: ${email}\nContraseña: ${password}`);
    loginModal.style.display = 'none';
});

// Inicializar en paso 1
mostrarPaso(1);
mostrarPasoEmp(1);