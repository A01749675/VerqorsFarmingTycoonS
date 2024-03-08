<?php
session_start(); // Inicia o reanuda la sesión actual

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
$contraseña = $_POST['contraseña'];

// Preparar consulta SQL
$sql = "SELECT * FROM usuarios WHERE correo = ? AND contraseña = ?";
$stmt = $conn->prepare($sql);
$stmt->bind_param("ss", $correo, $contraseña); 
$stmt->execute();
$result = $stmt->get_result();

// Verificar usuario
if ($result->num_rows > 0) {
    // Inicio de sesión exitoso, establecer variables de sesión
    $userData = $result->fetch_assoc();
    $_SESSION['usuario_id'] = $userData['id'];
    $_SESSION['usuario_nombre'] = $userData['usuario'];  

    // Verificar el tipo de usuario
    if ($userData['tipo_usuario'] === "administrador") {
        // Redirigir al administrador a la página de información
        header("Location: info.php");
        exit();
    }

    $stmt = $conn->prepare("SELECT id FROM Progreso WHERE id_usuario = ?");
    $stmt->bind_param("i", $_SESSION['usuario_id']);
    $stmt->execute();
    $progressResult = $stmt->get_result();

    if ($progressResult->num_rows > 0) {
        // Si hay un progreso asociado con el usuario, establece la sesión con ese ID
        $progressData = $progressResult->fetch_assoc();
        $_SESSION['progreso_id'] = $progressData['id'];
    } 
    // Redireccionar al juego Unity
    header("Location: http://localhost:61080"); // Asegúrate de que esta es la URL correcta de tu juego Unity
    exit();
} else {
    // Credenciales incorrectas
    echo "Correo electrónico o contraseña incorrectos";
}


// Cerrar la conexión y la declaración preparada
$stmt->close();
$conn->close();
?>
