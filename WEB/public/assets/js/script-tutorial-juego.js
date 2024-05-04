const stepTexts = [
    "1. Conoce tu La granja y tus parcelas",
    "2. Arbol de mejoras",
    "2. Arbol de mejoras",
    "2. Arbol de mejoras",
    "3. Mercado agrícola",
    "3. Mercado agrícola",
    "3. Mercado agrícola",
    "4. Inventario",
    "4. Inventario",
    "4. Inventario",
    "5. Sembrando esperanza",
    "5. Sembrando esperanza",
    "5. Sembrando esperanza",
    "6. Manten tus cultivos hidratados",
    "6. Manten tus cultivos hidratados",
    "7. Paga tu deuda",
    "8. Desafíos y metas",
    "9. Financiamiento"
];
const animationTexts = [
    "¡Bienvenido a la granja Lopez!  Las parcelas iniciales se encuentran en la parte inferior, mientras que las de las parte superior tendrás que irlas desbloqueando conforme vayas obteniendo ganancias.",
    "Ahora conoceremos el árbol de mejoras,  que será de gran utilidad para mejorar la productividad de la granja. Da click en el icono de arbol de mejoras y compra las mejoras básicas.",
    "Esta mejora permitirá dar seguimiento a la hidratación de los cultivos",
    "ESTA MEJORA permitirá cultIvar nuestras prImeras semIllaS",
    "Lo primero por hacer es comprar semillas para empezar a sembrar. Para ello acudamos al mercado agrícola que se encuentra en el menú inferior derecha, donde podrás comprar o vender insumos.",
    "La cantidad de semillas y precios  cambian cada dos semanas, así que estate atento",
    "Selecciona la cantIdad de Insumos a vender y da click en vender o bien vender todo de ser el caso. ",
    "En el Ícono del costal, podrás consultar la cantIDAD de semIllas e INSUMOS QUE POSEES",
    "Los ICONOS De la parte superIOR representan las semILLAS",
    "MIEntras que los de la parte InferIOR a los Insumos cosechados",
    "eL MOMENTO DE CULTIVAR HA LLEGADO, DA CLICK AL ICONO DE LA ASADA Y ELIGE EL INSUMO QUE HAYAS COMPRADO ",
    "IDENTIFICA LA PARCELA CON EL LETRERO DEL INSUMO A SEMBRAR Y DA cLICK PARA SEMBRARLO",
    "¡Muy bIEN!, Ya hemos cultIvado nuestros prIMEROS cultIvos",
    "Da clICK en el ícono del agua y dIrIge el cursor a la parcela que te Interesa regar",
    "Usaremos los medidores de agua para saber cuando debemos regar nuestros cultIvos",
    "Es de suma importancia que pagues tu deuda y así evitar el fIn del legado",
    "En este viaje, enfrentaremos desafíos como cambios en los precios de los cultivos y eventos inesperados. Pero con perseverancia y determinación, estoy seguro de que lograremos convertir nuestra pequeña granja en un imperio agrícola próspero.",
    "Ahora, enfrentamos una decisión crucial: ¿Cómo financiaremos la renovación de nuestra granja familiar? Tenemos tres opciones: el Coyote, el Banco y Verqor. Cada uno tiene sus pros y sus contras. ¿Qué camino elegirás?"
];
const firstImagePaths = [
    "../assets/img-tutorial/icono1.png",
    "../assets/img-tutorial/icono2.png",
    "../assets/img-tutorial/icono2.png",
    "../assets/img-tutorial/icono2.png",
    "../assets/img-tutorial/icono3.png",
    "../assets/img-tutorial/icono3.png",
    "../assets/img-tutorial/icono3.png",
    "../assets/img-tutorial/icono4.png",
    "../assets/img-tutorial/icono4.png",
    "../assets/img-tutorial/icono4.png",
    "../assets/img-tutorial/icono5.png",
    "../assets/img-tutorial/icono5.png",
    "../assets/img-tutorial/icono5.png",
    "../assets/img-tutorial/icono6.png",
    "../assets/img-tutorial/icono6.png",
    "../assets/img-tutorial/icono7.png",
    "../assets/img-tutorial/icono8.png",
    "../assets/img-tutorial/icono1.png"
];
const secondImagePaths = [
    "../assets/img-tutorial/Img1.png",
    "../assets/img-tutorial/Img2.png",
    "../assets/img-tutorial/Img3.png",
    "../assets/img-tutorial/Img4.png",
    "../assets/img-tutorial/Img5.png",
    "../assets/img-tutorial/Img6.png",
    "../assets/img-tutorial/Img7.png",
    "../assets/img-tutorial/Img8.png",
    "../assets/img-tutorial/Img9.png",
    "../assets/img-tutorial/Img10.png",
    "../assets/img-tutorial/Img11.png",
    "../assets/img-tutorial/Img12.png",
    "../assets/img-tutorial/Img13.png",
    "../assets/img-tutorial/Img14.png",
    "../assets/img-tutorial/Img15.png",
    "../assets/img-tutorial/Img16.png",
    "../assets/img-tutorial/Img17.png",
    "../assets/img-tutorial/Img18.png"
];
let currentTextIndex = 0;
const stepElement = document.getElementById('step-text');
const typingElement = document.getElementById('text-animation');
const firstImageElement = document.getElementById('first-image');
const secondImageElement = document.getElementById('second-image');
const prevButton = document.querySelector('.prev-button');
const nextButton = document.querySelector('.next-button');

// Función typeWriter para escribir el texto animado
function typeWriter() {
    const currentText = animationTexts[currentTextIndex];
    let i = 0;
    typingElement.innerHTML = ''; // Limpiar el contenido antes de escribir el nuevo texto
    function type() {
        if (i < currentText.length) {
            typingElement.innerHTML += currentText.charAt(i);
            i++;
            setTimeout(type, 1); // VELOCIDAD DE ESCRITURA
        } else {
            nextButton.style.display = 'block';
            prevButton.style.display = 'block';
        }
    }
    type();
}

// Funciones para navegar entre los textos
function nextText() {
    currentTextIndex = (currentTextIndex + 1) % animationTexts.length;
    nextButton.style.display = 'none'; 
    prevButton.style.display = 'none'; 
    typeWriter();
    stepElement.textContent = stepTexts[currentTextIndex];
    firstImageElement.src = firstImagePaths[currentTextIndex];
    secondImageElement.src = secondImagePaths[currentTextIndex];
}

// Función para retroceder entre los textos
function prevText() {
    currentTextIndex = (currentTextIndex - 1 + animationTexts.length) % animationTexts.length;
    nextButton.style.display = 'block'; // Mostrar el botón de "Siguiente" después de retroceder
    if (currentTextIndex === 0) {
        prevButton.style.display = 'none'; // Ocultar el botón de "Anterior" si volvemos al primer paso
    }
    typeWriter();
    stepElement.textContent = stepTexts[currentTextIndex];
    firstImageElement.src = firstImagePaths[currentTextIndex];
    secondImageElement.src = secondImagePaths[currentTextIndex];
}

// Llamar a la función typeWriter al cargar la página
typeWriter();
