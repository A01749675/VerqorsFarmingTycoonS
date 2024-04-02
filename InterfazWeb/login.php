<?php
session_start();

// Conexión a la base de datos
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "Verqor";

// Crear conexión
$conn = new mysqli($servername, $username, $password, $dbname);

// Verificar la conexión
if ($conn->connect_error) {
    die("La conexión falló: " . $conn->connect_error);
}

// Obtener los datos del formulario
$correo = $_POST['correo'];
$contraseña = $_POST['contraseña']; // La contraseña introducida por el usuario

// Preparar consulta SQL para obtener el hash de la contraseña basado en el correo
$sql = "SELECT * FROM usuarios WHERE correo = ?";
$stmt = $conn->prepare($sql);
$stmt->bind_param("s", $correo);
$stmt->execute();
$result = $stmt->get_result();

if ($result->num_rows > 0) {
    $userData = $result->fetch_assoc();

    // Verificar la contraseña con el hash almacenado
    if (password_verify($contraseña, $userData['contraseña'])) {
        // Inicio de sesión exitoso, establecer variables de sesión
        $_SESSION['usuario_id'] = $userData['id'];
        $_SESSION['usuario_nombre'] = $userData['usuario'];

        // Verificar el tipo de usuario
        if ($userData['tipo_usuario'] === "administrador") {
            // Redirigir al administrador a la página de información
            header("Location: info.php");
            exit();
        }

        // Comprobar si el usuario tiene algún progreso guardado
        $stmt = $conn->prepare("SELECT * FROM Progreso WHERE id_usuario = ?");
        $stmt->bind_param("i", $_SESSION['usuario_id']);
        $stmt->execute();
        $progressResult = $stmt->get_result();

        if ($progressResult->num_rows > 0) {
            $puerto = 60308; // Puerto de UNITY
            header("Location: http://localhost:$puerto/?user_id=" . $_SESSION['usuario_id']);

            exit();
        } else {
            // Si no hay progreso, redirigir a credito.html para elegir financiamiento
            header("Location: credito.html");
            exit();
        }
    } else {
        // Credenciales incorrectas
        echo "Correo electrónico o contraseña incorrectos.";
    }
} else {
    // Usuario no encontrado
    echo "Correo electrónico o contraseña incorrectos.";
}

// Cerrar la conexión y la declaración preparada
$stmt->close();
$conn->close();
