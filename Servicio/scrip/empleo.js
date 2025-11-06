
        let empleosSimulados = [];

        fetch('vacantes.json')
            .then(res => res.json())
            .then(data => {
                empleosSimulados = data;

                const resultadosIniciales = empleosSimulados.filter(coincideConBusqueda);
                mostrarResultados(resultadosIniciales);

                // Mover aquí el listener:
                ordenarSelect.addEventListener('change', () => {
                    let listaOrdenada = [...empleosSimulados].filter(coincideConBusqueda);
                    const criterio = ordenarSelect.value;

                    switch (criterio) {
                        case 'salarios':
                            listaOrdenada.sort((a, b) => b.salario - a.salario);
                            break;
                        case 'fecha':
                            listaOrdenada.sort((a, b) => new Date(b.fechaPublicacion) - new Date(a.fechaPublicacion));
                            break;
                        case 'experiencia':
                            listaOrdenada.sort((a, b) => {
                                const expA = parseInt(a.experiencia) || 0;
                                const expB = parseInt(b.experiencia) || 0;
                                return expB - expA;
                            });
                            break;
                    }

                    mostrarResultados(listaOrdenada);
                });
            })
            .catch(err => console.error("Error cargando vacantes.json", err));

        function crearCard(empleo) {
            const card = document.createElement('div');
            card.className = 'job-card';

            const isFavorito = JSON.parse(localStorage.getItem('favoritos') || '[]')
                .some(fav => fav.titulo === empleo.titulo && fav.empresa === empleo.empresa);

            card.innerHTML = `
            <div class="job-header">
                <div>
                    <h3>
                        ${empleo.titulo} 
                        <a href="#guardar" title="Guardar en favoritos" style="text-decoration: none; margin-left: 200px;">
                            <i class="fa-bookmark fa ${isFavorito ? 'fas' : 'far'} favorito-icon" 
                            style="cursor: pointer; color: #9d2449; font-size: 16px;"></i>
                        </a>
                        <a href="#ubicacion" title="Ubicación" style="text-decoration: none; margin-left: 50px;">
                            <i class="fas fa-map-marker-alt" 
                            style="cursor: pointer; color: #9d2449; font-size: 16px;"></i>
                        </a>
                    </h3>
                    <p><strong>${empleo.empresa}</strong></p>
                    <p class="location">${empleo.ubicacion}</p>
                </div>
                <div class="job-age">
                    <p><strong>Edad</strong><br>${empleo.edad} años</p>
                </div>
            </div>

            <p>${empleo.descripcion}</p>

            <div class="job-details">
                <p><strong>Escolaridad</strong><br>${empleo.escolaridad}</p>
                <p><strong>Experiencia</strong><br>${empleo.experiencia}</p>
                <p><strong>Discapacidad</strong><br>${empleo.discapacidad}</p>
            </div>

            <div class="job-salary">
                <p><strong>Salario</strong><br>$${empleo.salario} MXN</p>
            </div>
        `;

            const starIcon = card.querySelector('.favorito-icon');
            starIcon.addEventListener('click', () => {
                toggleFavorito(empleo, starIcon);
            });

            document.getElementById('job-results').appendChild(card);
        }

        document.getElementById('filter-form').addEventListener('submit', (e) => {
            e.preventDefault();

            const tipo = document.getElementById('tipo').value;
            const escolaridad = document.getElementById('escolaridad').value;
            const discapacidad = document.getElementById('discapacidad').value;
            const edad = parseInt(document.getElementById('edad').value) || 0;
            const salario = parseInt(document.getElementById('salario').value) || 0;

            const filtrados = empleosSimulados.filter(empleo => {
                const titulo = empleo.titulo.toLowerCase();
                const ubicacion = empleo.ubicacion.toLowerCase();

                const coincideBusqueda = titulo.includes(paramQue) && ubicacion.includes(paramDonde);

                return (
                    coincideBusqueda &&
                    (tipo === "" || empleo.tipo === tipo) &&
                    (escolaridad === "" || empleo.escolaridad === escolaridad) &&
                    (discapacidad === "" || empleo.discapacidad === discapacidad) &&
                    (edad === 0 || empleo.edad >= edad) &&
                    (salario === 0 || empleo.salario >= salario)
                );
            });

            mostrarResultados(filtrados);
        });

        function mostrarResultados(lista) {
            const resultados = document.getElementById('job-results');
            resultados.innerHTML = "";
            if (lista.length === 0) {
                resultados.innerHTML = "<p>No se encontraron resultados.</p>";
                return;
            }
            lista.forEach(crearCard);
        }

        // Extraer parámetros de la URL
        const urlParams = new URLSearchParams(window.location.search);
        const paramQue = urlParams.get("que")?.toLowerCase() || "";
        const paramDonde = urlParams.get("donde")?.toLowerCase() || "";

        function coincideConBusqueda(empleo) {
            const titulo = empleo.titulo.toLowerCase();
            const ubicacion = empleo.ubicacion.toLowerCase();
            return titulo.includes(paramQue) && ubicacion.includes(paramDonde);
        }

        const resultadosIniciales = empleosSimulados.filter(coincideConBusqueda);

        mostrarResultados(resultadosIniciales);


        const ordenarSelect = document.getElementById('ordenar');

        ordenarSelect.addEventListener('change', () => {
            let listaOrdenada = [...empleosSimulados].filter(coincideConBusqueda);
            const criterio = ordenarSelect.value;

            switch (criterio) {
                case 'salarios':
                    listaOrdenada.sort((a, b) => b.salario - a.salario);
                    break;
                case 'fecha':
                    listaOrdenada.sort((a, b) => new Date(b.fechaPublicacion) - new Date(a.fechaPublicacion));
                    break;
                case 'experiencia':
                    listaOrdenada.sort((a, b) => {
                        const expA = parseInt(a.experiencia) || 0;
                        const expB = parseInt(b.experiencia) || 0;
                        return expB - expA;
                    });
                    break;
            }

            mostrarResultados(listaOrdenada);
        });

