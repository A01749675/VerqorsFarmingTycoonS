<?php
// Agregar encabezados CORS
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: GET, POST, OPTIONS");
header("Access-Control-Allow-Headers: Origin, X-Requested-With, Content-Type, Accept");

session_start();

require 'db.php'; 

if (isset($_GET['user_id'])) {
    $userId = $_GET['user_id'];
} elseif (isset($_SESSION['usuario_id'])) {
    $userId = $_SESSION['usuario_id'];
} else {
    // No se proporcionó user_id y no hay sesión activa
    $response = array(
        "success" => false,
        "message" => "No se proporcionó un ID de usuario y no hay sesión activa"
    );
    echo json_encode($response);
    exit;
}

try {
    $stmt = $pdo->prepare("SELECT id, tipo_usuario, usuario FROM usuarios WHERE id = ?");
    $stmt->execute([$userId]);
    $usuario = $stmt->fetch(PDO::FETCH_ASSOC);

    if ($usuario) {
        // Obtener datos relacionados como antes
        $stmtProgreso = $pdo->prepare("SELECT * FROM Progreso WHERE id_usuario = ?");
        $stmtProgreso->execute([$userId]);
        $progreso = $stmtProgreso->fetchAll(PDO::FETCH_ASSOC);

        $stmtSemillas = $pdo->prepare("SELECT * FROM Semillas WHERE id_progreso IN (SELECT id FROM Progreso WHERE id_usuario = ?)");
        $stmtSemillas->execute([$userId]);
        $semillas = $stmtSemillas->fetchAll(PDO::FETCH_ASSOC);

        $stmtCultivos = $pdo->prepare("SELECT * FROM Cultivo WHERE id_progreso IN (SELECT id FROM Progreso WHERE id_usuario = ?)");
        $stmtCultivos->execute([$userId]);
        $cultivos = $stmtCultivos->fetchAll(PDO::FETCH_ASSOC);

        // Agregar los datos recuperados al arreglo de respuesta
        $response = array(
            "success" => true,
            "message" => "Datos del usuario recuperados con exito",
            "user_id" => $userId,
            "usuario" => $usuario,
            "progreso" => $progreso,
            "semillas" => $semillas,
            "cultivos" => $cultivos
        );
    } else {
        // El usuario no se encuentra en la base de datos
        $response = array(
            "success" => false,
            "message" => "Usuario no encontrado en la base de datos"
        );
    }
} catch (PDOException $e) {
    $response = array(
        "success" => false,
        "message" => "Error al recuperar datos: " . $e->getMessage()
    );
}


// Devolver la respuesta en formato JSON
echo json_encode($response);
?>
