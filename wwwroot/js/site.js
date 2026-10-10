// La Tóxica - scripts del sitio (sin librerías extra para que cargue rápido en el celular)
(function () {
    'use strict';

    var reducirMovimiento = window.matchMedia &&
        window.matchMedia('(prefers-reduced-motion: reduce)').matches;

    // 1. Animación de aparición al hacer scroll
    var elementos = document.querySelectorAll('[data-reveal]');
    if ('IntersectionObserver' in window && !reducirMovimiento) {
        var observador = new IntersectionObserver(function (entradas) {
            entradas.forEach(function (entrada) {
                if (entrada.isIntersecting) {
                    entrada.target.classList.add('visible');
                    observador.unobserve(entrada.target);
                }
            });
        }, { threshold: 0.1 });
        elementos.forEach(function (el) { observador.observe(el); });
    } else {
        elementos.forEach(function (el) { el.classList.add('visible'); });
    }

    // 2. Contadores animados (data-count="16")
    document.querySelectorAll('[data-count]').forEach(function (el) {
        var meta = parseInt(el.getAttribute('data-count'), 10);
        if (isNaN(meta) || reducirMovimiento) { return; }
        var inicio = null;
        var duracion = 900;
        function paso(t) {
            if (inicio === null) { inicio = t; }
            var avance = Math.min((t - inicio) / duracion, 1);
            el.textContent = Math.round(meta * avance);
            if (avance < 1) { window.requestAnimationFrame(paso); }
        }
        el.textContent = '0';
        window.requestAnimationFrame(paso);
    });

    // 3. Sombra en la barra de navegación al bajar
    var barra = document.querySelector('.navbar-toxica');
    if (barra) {
        var alBajar = function () { barra.classList.toggle('con-sombra', window.scrollY > 10); };
        window.addEventListener('scroll', alBajar, { passive: true });
        alBajar();
    }

    // 4. Filtro de la carta por categoría
    var botones = document.querySelectorAll('[data-filtro]');
    var grupos = document.querySelectorAll('.grupo-carta');
    botones.forEach(function (boton) {
        boton.addEventListener('click', function () {
            var filtro = boton.getAttribute('data-filtro');
            botones.forEach(function (b) { b.classList.remove('activo'); });
            boton.classList.add('activo');
            grupos.forEach(function (g) {
                var mostrar = filtro === 'todas' || g.getAttribute('data-cat') === filtro;
                g.classList.toggle('d-none', !mostrar);
                if (mostrar) {
                    g.querySelectorAll('[data-reveal]').forEach(function (el) { el.classList.add('visible'); });
                }
            });
        });
    });

    // 5. Mostrar u ocultar la contraseña
    document.querySelectorAll('[data-mostrar-clave]').forEach(function (boton) {
        boton.addEventListener('click', function () {
            var campo = document.getElementById(boton.getAttribute('data-mostrar-clave'));
            if (!campo) { return; }
            var oculto = campo.type === 'password';
            campo.type = oculto ? 'text' : 'password';
            var icono = boton.querySelector('i');
            if (icono) { icono.className = oculto ? 'bi bi-eye-slash-fill' : 'bi bi-eye-fill'; }
        });
    });

    // 6. Vista previa de la imagen en el formulario de platos
    var campoImagen = document.getElementById('ImagenUrl');
    var vistaPrevia = document.getElementById('vistaPrevia');
    if (campoImagen && vistaPrevia) {
        campoImagen.addEventListener('change', function () {
            var url = campoImagen.value.trim();
            if (url) { vistaPrevia.src = url; }
        });
    }
})();
