<?php
function generateUserTable($max_results = 'todos', $money_filter = 0)
{
    $conexion = new mysqli('localhost', 'root', '', 'Verqor');
    if ($conexion->connect_error) {
        die("Conexión fallida: " . $conexion->connect_error);
    }
    $sql = "SELECT u.id, u.usuario, 
    CASE u.tipo_usuario 
        WHEN 'cliente' THEN 'Cliente' 
        WHEN 'fabricante' THEN 'Fabricante de Agroinsumos' 
        WHEN 'cliente' THEN 'Cliente' 
        WHEN 'distribuidor' THEN 'Distribuidor de Agroinsumos' 
        WHEN 'proveedor_seguros' THEN 'Proveedor de Seguros' 
        WHEN 'financiera' THEN 'Financiera' 
        WHEN 'cpg' THEN 'Empresa CPG' 
        WHEN 'acopiador' THEN 'Acopiador' 
        WHEN 'inversionista' THEN 'Inversionista' 
        WHEN 'publico_general' THEN 'Publico general'
        ELSE u.tipo_usuario 
    END AS tipo_usuario, 
    TIMESTAMPDIFF(YEAR, u.fecha_nacimiento, CURDATE()) AS edad, IFNULL(p.dinero, 0) AS dinero, 
    CASE p.financiamiento 
        WHEN 1 THEN 'Verqor' 
        WHEN 2 THEN 'Banco' 
        WHEN 3 THEN 'Coyote' 
        ELSE p.financiamiento 
    END AS financiamiento, 
    CASE p.seguro 
        WHEN 0 THEN 'No' 
        WHEN 1 THEN 'Sí' 
        ELSE p.seguro 
    END AS seguro, 
    CASE p.practica 
        WHEN 0 THEN 'Tradicional' 
        WHEN 1 THEN 'Regenerativa' 
        ELSE p.practica 
    END AS practica 
    FROM usuarios u 
    LEFT JOIN Progreso p ON u.id = p.id_usuario";

    // Agrega la condición para excluir a los usuarios 'Administrador'
    $whereConditions = [];
    if ($money_filter > 0) {
        $whereConditions[] = "p.dinero > $money_filter";
    }
    // Excluye a los administradores
    $whereConditions[] = "u.tipo_usuario != 'Administrador'";

    if (!empty($whereConditions)) {
        $sql .= " WHERE " . implode(' AND ', $whereConditions);
    }

    $sql .= " ORDER BY dinero DESC";
    if ($max_results !== 'todos') {
        $sql .= " LIMIT $max_results";
    }
    
    $resultado = $conexion->query($sql);

    $ranking = 1;

    echo "<table>
            <tr>
                <th>Ranking</th>
                <th>ID</th>
                <th>Usuario</th>
                <th>Tipo de Usuario</th>
                <th>Edad</th>
                <th>Dinero</th>
                <th>Financiamiento</th>
                <th>Seguro</th>
                <th>Práctica</th>
            </tr>";

    if ($resultado && $resultado->num_rows > 0) {
        while ($fila = $resultado->fetch_assoc()) {
            echo "<tr>
                    <td>" . $ranking++ . "</td>
                    <td>" . $fila['id'] . "</td>
                    <td>" . $fila['usuario'] . "</td>
                    <td>" . $fila['tipo_usuario'] . "</td>
                    <td>" . $fila['edad'] . "</td>
                    <td>" . $fila['dinero'] . "</td>
                    <td>" . ($fila['financiamiento'] !== NULL ? $fila['financiamiento'] : "N/A") . "</td>
                    <td>" . ($fila['seguro'] !== NULL ? $fila['seguro'] : "N/A") . "</td>
                    <td>" . ($fila['practica'] !== NULL ? $fila['practica'] : "N/A") . "</td>
                  </tr>";
        }
    } else {
        echo "<tr><td colspan='9'>No se encontraron resultados.</td></tr>";
    }
    echo "</table>";

    $conexion->close();
}
if (isset($_GET['ajax']) && $_GET['ajax'] == 1) {
    $max_results = $_GET['max_results'] ?? 'todos';
    $money_filter = isset($_GET['money_filter']) ? (int)$_GET['money_filter'] : 0;
    generateUserTable($max_results, $money_filter);
    exit;
}
?>
<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="https://fonts.cdnfonts.com/css/arcade-classic" rel="stylesheet">
    <link rel="stylesheet" href="style_info.css">
    <title>Información de Usuarios</title>
    <script>
        window.onload = function() {
            const rows = document.querySelectorAll('.tabla tr:not(:first-child)');
            const colorsForRow = ['#F9C068', '#F5E1C2', '#DBEBC7', '#C7ECD8', '#D5E8D4']; // Paleta de colores para filas
            const colorsForColumns = ['#FF5733', '#FFBD33', '#FFD133', '#FFEF33', '#E9FF33']; // Paleta de colores para columnas

            // Asignar colores a las filas
            rows.forEach((rowIndex, index) => {
                const row = rowIndex.querySelectorAll('td');
                const rowColor = colorsForRow[index % colorsForRow.length];
                row.forEach((cell, cellIndex) => {
                    if (cellIndex < 3) { // Solo para las primeras tres columnas
                        cell.style.backgroundColor = colorsForColumns[index % colorsForColumns.length];
                    } else {
                        cell.style.backgroundColor = rowColor;
                    }
                });
            });
        };

        function applyFilters() {
            const xhr = new XMLHttpRequest();
            xhr.onload = function() {
                if (this.status === 200) {
                    document.getElementById('results').innerHTML = this.responseText;
                    const rows = document.querySelectorAll('.tabla tr:not(:first-child)');
                    const colorsForRow = ['#F6E1C1', '#FCEECD', '#E8DBC9', '#E9D4C7', '#F8E7D6'];
                    const colorsForColumns = ['#F9C068', '#EABE5F', '#C29759', '#C98963', '#DAA36E'];

                    rows.forEach((rowIndex, index) => {
                        const row = rowIndex.querySelectorAll('td');
                        const rowColor = colorsForRow[index % colorsForRow.length];
                        row.forEach((cell, cellIndex) => {
                            if (cellIndex < 3) { // Solo para las primeras tres columnas
                                cell.style.backgroundColor = colorsForColumns[index % colorsForColumns.length];
                            } else {
                                cell.style.backgroundColor = rowColor;
                            }
                        });
                    });
                }
            };
            const maxResults = document.getElementById('max_results').value;
            const moneyFilter = document.getElementById('money_filter').value;
            xhr.open('GET', `info.php?ajax=1&max_results=${maxResults}&money_filter=${moneyFilter}`, true);
            xhr.send();
        }


        window.onload = applyFilters;
    </script>
</head>

<body>
    <div class="background-container"></div>
    <h2>Consultar<span style="opacity: 0;">_</span>Datos</h2>
    <div class="main-content">


        <form id="filter-form" class="filtros">
            <div class="numero_filas">
                <label for="max_results">Número de filas:</label>
                <select name="max_results" id="max_results">
                    <option value="5">5</option>
                    <option value="10">10</option>
                    <option value="20">20</option>
                    <option value="50">50</option>
                    <option value="100">100</option>
                    <option value="todos" selected>Todos</option>
                </select>
            </div>

            <div class="min_dinero">
                <label for="money_filter">Dinero mayor a:</label>
                <input type="number" id="money_filter" name="money_filter" min="0" value="0">
            </div>

            <button class="btn_filtros btn" type="button" onclick="applyFilters()">Filtrar</button>
        </form>

        <div id="results" class="tabla">
            <!-- La tabla de resultados se carga aquí -->
        </div>
    </div>
</body>

</html>