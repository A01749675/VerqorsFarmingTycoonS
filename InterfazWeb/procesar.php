<?php
session_start();

// Asegúrate de que el usuario haya iniciado sesión
if (!isset($_SESSION['usuario_id'])) {
    header("Location: login.html");
    exit;
}

ini_set('display_errors', 1);
error_reporting(E_ALL);

// Conexión a la base de datos
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "Verqor";
$conn = new mysqli($servername, $username, $password, $dbname);

// Verificar la conexión
if ($conn->connect_error) {
    die("La conexión falló: " . $conn->connect_error);
}

$valor = isset($_POST['valor']) ? (int)$_POST['valor'] : 0;

// ID del usuario actual
$usuarioId = $_SESSION['usuario_id'];

// Inicializar la variable seguro
$seguro = 0; 
if ($valor === 1) {
    $seguro = 1; 
}

// Insertar el valor en la base de datos
$sql = "INSERT INTO Progreso (id_usuario, financiamiento, seguro) VALUES (?, ?, ?)";
$stmt = $conn->prepare($sql);
if ($stmt === false) {
    die("Error preparando la consulta: " . $conn->error);
}

$stmt->bind_param("iii", $usuarioId, $valor, $seguro);

if ($stmt->execute()) {
    // Redirección después de insertar con éxito
    if ($valor === 2) {
        header("Location: banco.html");
    } else {
        header("Location: game.html");
    }
    exit;
} else {
    die("Error al insertar el registro de financiamiento: " . $stmt->error);
}

$stmt->close();
$conn->close();
?>
