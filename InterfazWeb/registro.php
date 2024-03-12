<?php
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
$usuario = $_POST['usuario'];
$correo = $_POST['correo'];
$contraseña = $_POST['contraseña']; 
$tipo_usuario = $_POST['tipo_usuario'];
$fecha_nacimiento = $_POST['fecha_nacimiento']; 

// Aplicar hash a la contraseña
$contraseñaHash = password_hash($contraseña, PASSWORD_DEFAULT);

$sql = $conn->prepare("INSERT INTO usuarios (usuario, correo, contraseña, tipo_usuario, fecha_nacimiento) VALUES (?, ?, ?, ?, ?)");
$sql->bind_param("sssss", $usuario, $correo, $contraseñaHash, $tipo_usuario, $fecha_nacimiento); 

if ($sql->execute()) {
    echo "<script>
            alert('Usuario registrado');
            window.location.href='login.html';
          </script>";
} else {
    echo "Error: " . $sql->error;
}

// Cerrar la conexión
$sql->close();
$conn->close();
?>
