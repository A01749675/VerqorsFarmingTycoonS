<?php
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

// Obtener el ID de usuario desde Unity
$user_id = $_GET['user_id'];

// Preparar y ejecutar la consulta SQL para obtener el financiamiento del usuario
$sql = $conn->prepare("SELECT financiamiento FROM Progreso WHERE id_usuario = ?");
$sql->bind_param("i", $user_id);
$sql->execute();
$result = $sql->get_result();

// Verificar si se encontraron resultados
if ($result->num_rows > 0) {
    // Obtener el financiamiento como un arreglo asociativo
    $row = $result->fetch_assoc();
    // Devolver el financiamiento como respuesta en formato JSON
    header('Content-Type: application/json');
    echo json_encode($row);
} else {
    echo json_encode(array("error" => "No se encontró financiamiento para el usuario"));
}

// Cerrar la conexión y liberar recursos
$sql->close();
$conn->close();
?>
