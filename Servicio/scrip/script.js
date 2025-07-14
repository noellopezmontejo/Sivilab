
const animatedEls = document.querySelectorAll('.slide-in-right, .slide-in-left, .slide-in-up');

const observer = new IntersectionObserver(entries => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.classList.add('visible');
            observer.unobserve(entry.target);
        }
    });
}, {
    threshold: 0.1
});

animatedEls.forEach(el => observer.observe(el));

// Carrusel mejorado - Versión final
document.addEventListener('DOMContentLoaded', function() {
    const carousel = document.querySelector('.carousel-container');
    const items = document.querySelectorAll('.carousel-item');
    const indicators = document.querySelectorAll('.carousel-indicators span');
    const prevBtn = document.querySelector('.carousel-control.prev');
    const nextBtn = document.querySelector('.carousel-control.next');
            
    let currentIndex = 0;
    let interval = null;
    const slideInterval = 5000; // 5 segundos
            
    // Función para mostrar el slide actual
    function showSlide(index) {
        // Oculta todos los slides
        items.forEach(item => item.classList.remove('active'));
        indicators.forEach(ind => ind.classList.remove('active'));
                
        // Muestra el slide actual
        items[index].classList.add('active');
        indicators[index].classList.add('active');
        currentIndex = index;
    }
            
    // Función para avanzar al siguiente slide
    function nextSlide() {
        const newIndex = (currentIndex + 1) % items.length;
        showSlide(newIndex);
    }
            
    // Función para retroceder al slide anterior
    function prevSlide() {
        const newIndex = (currentIndex - 1 + items.length) % items.length;
        showSlide(newIndex);
    }
            
    // Inicia el intervalo automático
    function startInterval() {
        interval = setInterval(nextSlide, slideInterval);
    }
            
    // Reinicia el intervalo
    function resetInterval() {
        clearInterval(interval);
        startInterval();
    }
            
    // Event listeners para los controles
    prevBtn.addEventListener('click', function() {
        prevSlide();
        resetInterval();
    });
            
    nextBtn.addEventListener('click', function() {
        nextSlide();
        resetInterval();
    });
            
    // Event listeners para los indicadores
    indicators.forEach((indicator, index) => {
        indicator.addEventListener('click', function() {
            showSlide(index);
            resetInterval();
        });
    });
            
    // Pausar el carrusel cuando el mouse está sobre él
    carousel.addEventListener('mouseenter', function() {
        clearInterval(interval);
    });
            
    carousel.addEventListener('mouseleave', function() {
        startInterval();
    });
            
    // Inicializar el carrusel
    showSlide(0);
    startInterval();
});

document.getElementById("job-search-form").addEventListener("submit", function (e) {
    e.preventDefault();

    const que = document.getElementById("input-que").value.trim();
    const donde = document.getElementById("input-donde").value.trim();
    const captchaChecked = document.getElementById("captcha").checked;

    if (!captchaChecked) {
        const messageElement = document.createElement('div');
        messageElement.style.cssText = `
        position: fixed;
        top: 62.2%;
        left: 50%;
        transform: translate(-50%, -50%);
        background-color: #f8c6d1;
        color: #000;
        border: 2px solid #e64a19;
        padding: 10px 20px;
        font-weight: bold;
        font-size: 16px;
        border-radius: 5px;
        z-index: 9999;
        text-align: center;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
    `;
        messageElement.textContent = "Por favor, marca la casilla de 'Soy humano'.";

        document.body.appendChild(messageElement);

        setTimeout(() => {
            messageElement.remove();
        }, 3000);
        return;
    }

    if (que && donde) {
        const queryString = `?que=${encodeURIComponent(que)}&donde=${encodeURIComponent(donde)}`;
        window.location.href = `empleos.html${queryString}`;
    } else {
        const messageElement = document.createElement('div');
        messageElement.style.cssText = `
        position: fixed;
        top: 62.2%;
        left: 50%;
        transform: translate(-50%, -50%);
        background-color: #d4f8d4;
        color: #000;
        border: 2px solid #388e3c;
        padding: 10px 20px;
        font-weight: bold;
        font-size: 16px;
        border-radius: 5px;
        z-index: 9999;
        text-align: center;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
    `;
        messageElement.textContent = "Por favor, completa ambos campos.";

        document.body.appendChild(messageElement);

        setTimeout(() => {
            messageElement.remove();
        }, 3000);
    }
});

const captchaCheckbox = document.getElementById("captcha");
const modal = document.getElementById("captcha-modal");
const questionEl = document.getElementById("captcha-question");
const answerInput = document.getElementById("captcha-answer");
const submitBtn = document.getElementById("captcha-submit");
const cancelBtn = document.getElementById("captcha-cancel");

let correctAnswer = 0;

captchaCheckbox.addEventListener("click", function (e) {
    e.preventDefault();

    const num1 = Math.floor(Math.random() * 10) + 1;
    const num2 = Math.floor(Math.random() * 10) + 1;
    correctAnswer = num1 + num2;

    questionEl.textContent = `¿Cuánto es ${num1} + ${num2}?`;
    answerInput.value = "";
    modal.style.display = "flex";
});

submitBtn.addEventListener("click", function () {
    const userAnswer = parseInt(answerInput.value, 10);
    if (userAnswer === correctAnswer) {
        captchaCheckbox.checked = true;
        modal.style.display = "none";
    } else {
        alert("Respuesta incorrecta. Intenta de nuevo.");
    }
});

cancelBtn.addEventListener("click", function () {
    modal.style.display = "none";
});

const loginLink = document.getElementById('open-login');
const loginModal = document.getElementById('login-modal');
const closeLoginBtn = document.getElementById('close-login-modal');

loginLink.addEventListener('click', function (e) {
    e.preventDefault();
    loginModal.style.display = 'flex';
});

closeLoginBtn.addEventListener('click', function () {
    loginModal.style.display = 'none';
});

document.getElementById('login-form').addEventListener('submit', function (e) {
    e.preventDefault();
    const email = this.querySelector('input[type="email"]').value;
    const password = this.querySelector('input[type="password"]').value;

    alert(`Correo: ${email}\nContraseña: ${password}`);
    loginModal.style.display = 'none';
});

 
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


// Inicializar en paso 1
mostrarPaso(1);
mostrarPasoEmp(1);
