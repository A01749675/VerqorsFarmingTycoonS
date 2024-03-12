<?php
session_start(); // Inicia o reanuda la sesión actual

// Verificar si hay una sesión activa y si hay un usuario conectado
if (isset($_SESSION['usuario_id'])) {
    // Crear un arreglo asociativo para la respuesta
    $response = array(
        "success" => true,
        "message" => "Usuario conectado",
        "user_id" => $_SESSION['usuario_id']
    );
} else {
    // No hay sesión activa o usuario conectado
    $response = array(
        "success" => false,
        "message" => "No hay usuario conectado"
    );
}

// Devolver la respuesta en formato JSON
echo json_encode($response);
?>
