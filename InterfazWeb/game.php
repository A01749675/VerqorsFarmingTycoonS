
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Verqor Farming Tycoon</title>
    <style>
         body, html {
            height: 100%;
            margin: 0;
            padding: 0;
            overflow: hidden; 
        }
        #unity-container {
            width: 100%;
            height: 100%;
            display: flex;
            justify-content: center;
            align-items: center;
            background-color: #000; 
        }
        #unity-canvas {
            width: 100%;
            height: 100%;
        }

        #unity-loading-bar {
            position: absolute;
            top: 50%;
            width: 100%;
            transform: translateY(-50%);
            text-align: center;
            font-family: sans-serif;
            color: white;
        }
    </style>
</head>
<body>
    <div id="unity-container">
        <canvas id="unity-canvas"></canvas>
        <div id="unity-loading-bar">Cargando...</div>
    </div>

    <!-- PENDIENTE -->

    <!-- Incluir el script de Unity Loader -->
    <script src="webTest/UnityLoader.js"></script>
    <script>
        var unityInstance = UnityLoader.instantiate("unity-container", "webTest/Build/webTest.loader.js", {
            onProgress: function (unityInstance, progress) {
                // Actualizar la barra de carga (opcional)
                var loadingBar = document.getElementById('unity-loading-bar');
                if (loadingBar) {
                    loadingBar.innerHTML = 'Cargando... ' + (progress * 100) + '%';
                    if (progress === 1) {
                        // Juego cargado (ocultar barra de carga de unity fea)
                        loadingBar.style.display = 'none';
                    }
                }
            }
        });
    </script>
</body>
</html>
