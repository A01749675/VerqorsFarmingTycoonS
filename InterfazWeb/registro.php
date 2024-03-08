<?php
// Conexión a la base de datos (cambiar estos valores por los correspondientes a tu servidor MySQL)
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

// Preparar y ejecutar la consulta SQL para insertar el registro
$sql = "INSERT INTO usuarios (usuario, correo, contraseña, tipo_usuario) VALUES ('$usuario', '$correo', '$contraseña', '$tipo_usuario')";

if ($conn->query($sql) === TRUE) {
    echo "<script>
            alert('Usuario registrado');
            window.location.href='login.html';
          </script>";
} else {
    echo "Error: " . $sql . "<br>" . $conn->error;
}

// Cerrar la conexión
$conn->close();
?>
