const express = require('express');
const mysql = require('mysql2/promise');
const session = require('express-session');

const app = express();
const port = 8080; // El puerto en el que se ejecutará tu servidor

// Configuración de la sesión
app.use(session({
    secret: 'tu_secreto',
    resave: false,
    saveUninitialized: true,
}));

// Configuración de la base de datos
const db = mysql.createPool({
    host: 'localhost',
    user: 'root',
    database: 'Verqor',
    password: '',
});

app.get('/usuario/datos', async (req, res) => {
    if (req.session.usuario_id) {
        const userId = req.session.usuario_id;
        try {
            const [usuario] = await db.query("SELECT id, tipo_usuario, usuario FROM usuarios WHERE id = ?", [userId]);

            if (usuario.length > 0) {
                const [progreso] = await db.query("SELECT * FROM Progreso WHERE id_usuario = ?", [userId]);
                const [semillas] = await db.query("SELECT * FROM Semillas WHERE id_progreso IN (SELECT id FROM Progreso WHERE id_usuario = ?)", [userId]);
                const [cultivos] = await db.query("SELECT * FROM Cultivo WHERE id_progreso IN (SELECT id FROM Progreso WHERE id_usuario = ?)", [userId]);

                res.json({
                    success: true,
                    message: "Datos del usuario recuperados con éxito",
                    user_id: userId,
                    usuario: usuario[0],
                    progreso,
                    semillas,
                    cultivos,
                });
            } else {
                res.json({
                    success: false,
                    message: "Usuario no encontrado en la base de datos",
                });
            }
        } catch (error) {
            console.error('Error al recuperar datos:', error);
            res.json({
                success: false,
                message: "Error al recuperar datos",
            });
        }
    } else {
        res.json({
            success: false,
            message: "No hay usuario conectado",
        });
    }
});

app.listen(port, () => {
    console.log(`Servidor ejecutándose en http://localhost:${port}`);
});
