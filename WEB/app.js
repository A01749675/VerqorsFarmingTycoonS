const express = require('express');
const session = require('express-session');
const path = require('path');
const mysql = require('mysql2');
const dotenv = require('dotenv');
const bcrypt = require('bcrypt');
const nodemailer = require('nodemailer');
const bodyParser = require('body-parser');
const cors = require('cors');
const e = require('express');
const compression = require('compression');

dotenv.config({ path: './.env' });

const app = express();

app.use(bodyParser.json());

app.use(express.json());
app.use(compression());

host_game = os.getenv("HOST_GAME")
email_sender = os.getenv("EMAIL_SENDER")

// Configuración de express-session
app.use(session({
    secret: 'secret',
    resave: false,
    saveUninitialized: true
}));

app.use(cors({
    origin : [
        `http://${process.env.HOST_GAME}/logout`,
        `http://${process.env.HOST_GAME}/VerqorsFarmingTycoon/`
    ],    
    methods: ['GET', 'POST', 'PUT', 'DELETE'],
    allowedHeaders: ['Content-Type', 'Authorization']
}));

app.use(cors());

// Configurar nodemailer con tus credenciales de correo electrónico
const transporter = nodemailer.createTransport({
    host: process.env.EMAIL_HOST,
    port: process.env.EMAIL_PORT,
    secure: true, // Usa SSL(true por seguridad xd)
    auth: {
        user: process.env.EMAIL_USER,
        pass: process.env.EMAIL_PASS
    }
});

const db = mysql.createConnection({
    host: process.env.HOST2,
    user: process.env.USER2,
    password: process.env.PASSWORD2,
    database: process.env.DATABASE2,
    port: process.env.PORT2
});

const publicDirectory = path.join(__dirname, './public');
app.use(express.static(publicDirectory));

express.static(__dirname + '/assets')

app.set('view engine', 'hbs');
app.use(express.urlencoded({ extended: false }));

db.connect((error) => {
    if (error) {
        console.log('Error connecting to MySQL database');
        console.log(error);
    } else {
        console.log('MySQL Connected...');
    }
});

// Ruta para el juego
app.use('/VerqorsFarmingTycoon', (req, res, next) => {
    if (req.session && req.session.userId) {
        express.static(__dirname + '/webTest')(req, res, next);
    } else {
        res.redirect('/');
    }
});

// Ruta para el formulario de registro
app.get('/register', (req, res) => {
    res.render('register');
});

// Ruta para manejar el registro de usuarios
app.post('/register', async (req, res) => {
    try {
        const { usuario, correo, contraseña, tipo_usuario, fecha_nacimiento } = req.body;
        const hashedPassword = await bcrypt.hash(contraseña, 10);

        db.query('INSERT INTO usuarios (usuario, correo, contraseña, tipo_usuario, fecha_nacimiento) VALUES (?, ?, ?, ?, ?)', [usuario, correo, hashedPassword, tipo_usuario, fecha_nacimiento], (error, results) => {
            if (error) {
                console.log(error);
                res.status(500).send('Error al registrar el usuario');
            } else {
                res.render('index');
            }
        });
    } catch (error) {
        console.log(error);
        res.status(500).send('Error en el servidor');
    }
});

// Ruta para manejar el inicio de sesión
app.post('/login', async (req, res) => {
    try {
        const { correo, contraseña } = req.body;

        db.query('SELECT * FROM usuarios WHERE correo = ?', [correo], async (error, results) => {
            if (error) {
                console.log(error);
                res.status(500).send('Error en el servidor');
            } else if (results.length === 0) {
                res.status(401).send('Correo electrónico o contraseña incorrectos');
            } else {
                const usuario = results[0];

                // Verificar la contraseña
                const passwordMatch = await bcrypt.compare(contraseña, usuario.contraseña);
                if (!passwordMatch) {
                    res.status(401).send('Correo electrónico o contraseña incorrectos');
                } else {
                    // Almacenar userId en la sesión
                    req.session.userId = usuario.id;
                    req.session.usuario = usuario.usuario;

                    // Verificar si el usuario tiene registros de financiamiento
                    db.query('SELECT * FROM progreso WHERE id_usuario = ?', [usuario.id], (error, progresoResults) => {
                        if (error) {
                            console.log(error)
                            res.status(500).send('Error en el servidor');
                        } else if (progresoResults.length > 0) {
                            res.redirect(`${process.env.HOST_GAME}/VerqorsFarmingTycoon/?user_id=${usuario.id}`);
                        } else {
                            // Si el usuario es administrador, redirigir a info.hbs
                            if (usuario.tipo_usuario === 'administrador') {
                                res.render('info');
                            } else {
                                res.render('historia', { usuario: req.session.usuario });
                            }
                        }
                    });
                }
            }
        });
    } catch (error) {
        console.log(error);
        res.status(500).send('Error en el servidor');
    }
});

// Ruta para manejar el inicio de sesión
app.post('/login', async (req, res) => {
    try {
        const { correo, contraseña } = req.body;

        db.query('SELECT * FROM usuarios WHERE correo = ?', [correo], async (error, results) => {
            if (error) {
                console.log(error);
                res.status(500).send('Error en el servidor');
            } else if (results.length === 0) {
                res.status(401).send('Correo electrónico o contraseña incorrectos');
            } else {
                const usuario = results[0];

                // Verificar la contraseña
                const passwordMatch = await bcrypt.compare(contraseña, usuario.contraseña);
                if (!passwordMatch) {
                    res.status(401).send('Correo electrónico o contraseña incorrectos');
                } else {
                    // Almacenar userId en la sesión
                    req.session.userId = usuario.id;
                    req.session.usuario = usuario.usuario;

                    // Verificar si el usuario tiene registros de financiamiento
                    db.query('SELECT * FROM progreso WHERE id_usuario = ?', [usuario.id], (error, progresoResults) => {
                        if (error) {
                            console.log(error)
                            res.status(500).send('Error en el servidor');
                        } else if (progresoResults.length > 0) {
                            res.redirect(`${process.env.HOST_GAME}/VerqorsFarmingTycoon/?user_id=${usuario.id}`);
                        } else {
                            res.render('historia', { usuario: req.session.usuario });
                        }
                    });
                }
            }
        });
    } catch (error) {
        console.log(error);
        res.status(500).send('Error en el servidor');
    }
});


// Ruta para manejar la selección de financiamiento
app.post('/select-financing', async (req, res) => {
    try {
        // Acceder al userId almacenado en la sesión
        const userId = req.session.userId;

        // Verificar si userId es válido
        if (!userId) {
            console.log('El userId no está definido');
            return res.status(400).send('Usuario no autenticado');
        }

        const { option } = req.body;
        let dinero, seguro, practica = '0';
        let deuda = 0;

        switch (option) {
            case '1':
                dinero = 1000000;
                seguro = '1';
                practica = '1';
                deuda =(parseInt(dinero) * 0.5+parseInt(dinero));
                break;
            /* case '2':
                dinero = 1200000;
                seguro = '1';
                practica = '0';
                break; */
            case '3':
                dinero = 800000;
                seguro = '0';
                practica = '0';
                deuda = (parseInt(dinero)*0.75 +parseInt(dinero));
                break;
        }

        //si la opcion es diferente de 2
        if (option != 2) {
            // Insertar en la tabla Progreso
            db.query('INSERT INTO Progreso (id_usuario, dinero, deuda, seguro, financiamiento, practica) VALUES (?, ?, ?, ?, ?, ?)', [userId, dinero, deuda, seguro, option, practica], (error, insertResult) => {
                if (error) {
                    console.log('Error al insertar en la tabla de Progreso:', error);
                    return res.status(500).send('Error en el servidor');
                }

                // Obtener el id_progreso recién insertado
                const id_progreso = insertResult.insertId;

                // Insertar en la tabla Semillas con el id_progreso correcto
                db.query('INSERT INTO Semillas (id_progreso, maiz, trigo, tomate, chile, aguacate, frijol) VALUES (?, 0, 0, 0, 0, 0, 0)', [id_progreso], (error, insertResult) => {
                    if (error) {
                        console.log('Error al insertar en la tabla de Semillas:', error);
                        return res.status(500).send('Error en el servidor');
                    }

                    // Insertar en la tabla Cosecha con el id_progreso correcto
                    db.query('INSERT INTO Cosecha (id_progreso, maiz, trigo, tomate, chile, aguacate, frijol) VALUES (?, 0, 0, 0, 0, 0, 0)', [id_progreso], (error, insertResult) => {
                        if (error) {
                            console.log('Error al insertar en la tabla de Cosecha:', error);
                            return res.status(500).send('Error en el servidor');
                        }

                        // Insertar en la tabla Parcela con el id_progreso correcto
                        db.query('INSERT INTO Parcela (id_progreso, id_parcela, estado, cantidad, agua) VALUES (?, 8, 0, 0, 0)', [id_progreso], (error, insertResult) => {
                            if (error) {
                                console.log('Error al insertar en la tabla de Parcela:', error);
                                return res.status(500).send('Error en el servidor');
                            }

                            res.redirect(`${process.env.HOST_GAME}/VerqorsFarmingTycoon/?user_id=${userId}`);
                        }
                        );
                    });
                });
            });
        }
        if (option == 2) {
            //banco pero mandarle el usuario
            res.render('banco', { usuario: req.session.usuario });
        }

    } catch (error) {
        console.log("Error en el catch:", error);
        res.status(500).send('Error en el servidor');
    }
});

// Ruta para insertar el crédito del banc
app.post('/bank-credit', async (req, res) => {
    try {
        const userId = req.session.userId;

        if (!userId) {
            console.log('El userId no está definido');
            return res.status(400).send('Usuario no autenticado');
        }

        const { montoAprobado } = req.body;
        let tasa = 0.35;
        if(montoAprobado>1000000){
            tasa = 0.35;
        }
        else if(montoAprobado>900000){
            tasa = 0.4;
        }
        else{
            tasa =0.5;
        }
        let dinero = montoAprobado;
        let seguro = '1';
        let practica = '0';
        let option = '2';
        let deuda = ((parseInt(dinero)*tasa) + parseInt(dinero));


        // Insertar en la tabla Progreso
        db.query('INSERT INTO Progreso (id_usuario, dinero, deuda, seguro, financiamiento, practica) VALUES (?, ?, ?, ?, ?, ?)', [userId, dinero, deuda, seguro, option, practica], (error, insertResult) => {
            if (error) {
                console.log('Error al insertar en la tabla de Progreso:', error);
                return res.status(500).send('Error en el servidor');
            }

            // Obtener el id_progreso recién insertado
            const id_progreso = insertResult.insertId;

            // Insertar en la tabla Semillas con el id_progreso correcto
            db.query('INSERT INTO Semillas (id_progreso, maiz, trigo, tomate, chile, aguacate, frijol) VALUES (?, 0, 0, 0, 0, 0, 0)', [id_progreso], (error, insertResult) => {
                if (error) {
                    console.log('Error al insertar en la tabla de Semillas:', error);
                    return res.status(500).send('Error en el servidor');
                }

                // Insertar en la tabla Cosecha con el id_progreso correcto
                db.query('INSERT INTO Cosecha (id_progreso, maiz, trigo, tomate, chile, aguacate, frijol) VALUES (?, 0, 0, 0, 0, 0, 0)', [id_progreso], (error, insertResult) => {
                    if (error) {
                        console.log('Error al insertar en la tabla de Cosecha:', error);
                        return res.status(500).send('Error en el servidor');
                    }

                    // Insertar en la tabla Parcela con el id_progreso correcto
                    db.query('INSERT INTO Parcela (id_progreso, id_parcela, estado, cantidad, agua) VALUES (?, 8, 0, 0, 0)', [id_progreso], (error, insertResult) => {
                        if (error) {
                            console.log('Error al insertar en la tabla de Parcela:', error);
                            return res.status(500).send('Error en el servidor');
                        }
                    });

                    // envia como respuesta el user id
                    res.json({ userId: userId });
                    
                });
            });
        });
    } catch (error) {
        console.log("Error en el catch:", error);
        res.status(500).send('Error en el servidor');
    }
});

// Ruta para enviar los rankings
app.get('/rankings', (req, res) => {
    const query = `
      SELECT usuarios.id AS id_usuario, usuarios.usuario, Progreso.dinero, Progreso.financiamiento
      FROM usuarios
      JOIN Progreso ON usuarios.id = Progreso.id_usuario
      ORDER BY Progreso.dinero DESC
      LIMIT 3
    `;

    db.query(query, (error, results) => {
        if (error) {
            res.status(500).json({ error: 'Error al obtener los rankings.' });
        } else {
            // Envolver los resultados en un objeto con la clave "rankings"
            const response = { rankings: results };
            res.json(response);
        }
    });
});


// Ruta para financiamiento-2
app.get('/financiamiento-2', (req, res) => {
    res.render('financiamiento-2');
});

// Ruta para enviar datos al juego en formato JSON
app.get('/game-data', async (req, res) => {
    try {
        const userId = req.query.user_id;

        const [usuarioResult, progresoResults, semillasResults, cosechaResults, parcelaResult, mejorasResult] = await Promise.all([
            new Promise((resolve, reject) => {
                db.query('SELECT usuario, tipo_usuario FROM usuarios WHERE id = ?', [userId], (error, results) => {
                    if (error) {
                        reject(error);
                    } else {
                        resolve(results[0]);
                    }
                });
            }),
            new Promise((resolve, reject) => {
                db.query('SELECT * FROM Progreso WHERE id_usuario = ?', [userId], (error, results) => {
                    if (error) {
                        reject(error);
                    } else {
                        resolve(results);
                    }
                });
            }),
            new Promise((resolve, reject) => {
                db.query('SELECT * FROM Semillas WHERE id_progreso IN (SELECT id FROM Progreso WHERE id_usuario = ?)', [userId], (error, results) => {
                    if (error) {
                        reject(error);
                    } else {
                        resolve(results);
                    }
                });
            }),
            new Promise((resolve, reject) => {
                db.query('SELECT * FROM Cosecha WHERE id_progreso IN (SELECT id FROM Progreso WHERE id_usuario = ?)', [userId], (error, results) => {
                    if (error) {
                        reject(error);
                    } else {
                        resolve(results);
                    }
                });
            }),
            new Promise((resolve, reject) => {
                db.query('SELECT * FROM Parcela WHERE id_progreso IN (SELECT id FROM Progreso WHERE id_usuario = ?)', [userId], (error, results) => {
                    if (error) {
                        reject(error);
                    } else {
                        resolve(results);
                    }
                });
            }),

            new Promise((resolve, reject) => {
                db.query('SELECT * FROM Mejoras WHERE id_progreso IN (SELECT id FROM Progreso WHERE id_usuario = ?)', [userId], (error, results) => {
                    if (error) {
                        reject(error);
                    } else {
                        resolve(results);
                    }
                });
            })
        ]);

        const gameData = {
            success: true,
            message: 'Datos del usuario recuperados con éxito',
            user_id: userId,
            usuario: usuarioResult.usuario,
            tipo_usuario: usuarioResult.tipo_usuario,
            progreso: progresoResults,
            semillas: semillasResults,
            cosecha: cosechaResults,
            parcela: parcelaResult,
            mejoras: mejorasResult
        };

        // Enviar el objeto JSON como respuesta
        res.json(gameData);
    } catch (error) {
        console.log("Error en la ruta '/game-data':", error);
        res.status(500).json({ error: 'Error en el servidor' });
    }
});

// Ruta guardar datos del juego en la base de datos
app.post('/game-data', async (req, res) => {
    try {
        const { user_id, progreso, semillas, cosecha, parcela, mejoras } = req.body;
        // Verificar si el usuario ya existe en la base de datos
        db.query('SELECT * FROM usuarios WHERE id = ?', [user_id], async (error, userResults) => {
            if (error) {
                console.log("Error al verificar si el usuario existe:", error);
                res.status(500).send('Error en el servidor');
            } else {
                // Si el usuario no existe, enviar un error
                if (userResults.length === 0) {
                    console.log("El usuario con ID", user_id, "no existe.");
                    res.status(404).send('El usuario no existe');
                    return;
                }

                // Obtener el ID de progreso del usuario
                db.query('SELECT id FROM Progreso WHERE id_usuario = ?', [user_id], async (error, progresoResults) => {
                    if (error) {
                        console.log("Error al obtener el ID de progreso:", error);
                        res.status(500).send('Error en el servidor');
                    } else {
                        // Si no hay resultados de progreso, enviar un error
                        if (progresoResults.length === 0) {
                            console.log("No se encontraron datos de progreso para el usuario con ID", user_id);
                            res.status(404).send('No se encontraron datos de progreso para el usuario');
                            return;
                        }

                        const progreso_id = progresoResults[0].id;
                        //console.log("ID de progreso:", progreso_id);

                        // Actualizar datos de progreso si están presentes en la solicitud
                        if (progreso && progreso.length > 0) {
                            progreso.forEach(async (item) => {
                                const { seguro, ciclo, dinero, deuda, financiamiento } = item;

                                // Insertar o actualizar datos de progreso
                                db.query('UPDATE Progreso SET seguro = ?, ciclo = ?, dinero = ?, deuda =?, financiamiento = ? WHERE id_usuario = ?', [seguro, ciclo, dinero, deuda, financiamiento, user_id], (error, updateProgresoResult) => {
                                    if (error) {
                                        console.log("Error al actualizar datos de progreso:", error);
                                    }
                                });
                            });
                        }

                        // Actualizar datos de semillas si están presentes en la solicitud
                        if (semillas && semillas.length > 0) {
                            semillas.forEach(async (semilla) => {
                                const { id_progreso, maiz, trigo, tomate, chile, aguacate, frijol } = semilla;
                                db.query('UPDATE Semillas SET maiz = ?, trigo = ?, tomate = ?, chile = ?, aguacate= ?, frijol = ? WHERE id_progreso = ?', [maiz, trigo, tomate, chile, aguacate, frijol, progreso_id], (error, updateSemillasResult) => {
                                    if (error) {
                                        console.log("Error al actualizar datos de semillas:", error);
                                    }
                                });
                            });
                        }

                        // Actualizar datos de cosecha si están presentes en la solicitud
                        if (cosecha && cosecha.length > 0) {
                            cosecha.forEach(async (cosecha) => {
                                const { id_progreso, maiz, trigo, tomate, chile, aguacate, frijol } = cosecha;
                                db.query('UPDATE Cosecha SET maiz = ?, trigo = ?, tomate = ?, chile = ?, aguacate=?, frijol=? WHERE id_progreso = ?', [maiz, trigo, tomate, chile, aguacate, frijol, progreso_id], (error, updateCosechaResult) => {
                                    if (error) {
                                        console.log("Error al actualizar datos de cosecha:", error);
                                    }
                                });
                            });
                        }

                        // Actualizar datos de parcela si están presentes en la solicitud
                        if (parcela && parcela.length > 0) {
                            db.query('DELETE FROM Parcela WHERE id_progreso = ?', [progreso_id], (deleteError, deleteParcelaResult) => {
                                if (deleteError) {
                                    console.log("Error al eliminar parcelas:", deleteError);
                                    return;
                                }
                                parcela.forEach(async (item) => {
                                    const { id_parcela, estado, cantidad, agua } = item;
                                    db.query('INSERT INTO Parcela (id_progreso, id_parcela, estado, cantidad, agua) VALUES (?, ?, ?, ?, ?)', [progreso_id, id_parcela, estado, cantidad, agua], (error, insertParcelaResult) => {
                                        if (error) {
                                            console.log("Error al insertar datos de parcela:", error);
                                        }
                                    });
                                });
                            });
                        }

                        // Actualizar datos de mejoras si están presentes en la solicitud
                        if (mejoras && mejoras.length > 0) {
                            db.query('DELETE FROM Mejoras WHERE id_progreso = ?', [progreso_id], (deleteError, deleteMejorasResult) => {
                                if (deleteError) {
                                    console.log("Error al eliminar mejoras:", deleteError);
                                    return;
                                }
                                mejoras.forEach(async (item) => {
                                    const { id_mejora, estado } = item;
                                    db.query('INSERT INTO Mejoras (id_progreso, id_mejora, estado) VALUES (?, ?, ?)', [progreso_id, id_mejora, estado], (error, insertMejorasResult) => {
                                        if (error) {
                                            console.log("Error al insertar datos de mejoras:", error);
                                        }
                                    });
                                });
                            });
                        }
                        res.status(200).send('Datos insertados o actualizados correctamente.');
                    }
                });
            }
        });
    } catch (error) {
        console.log("Error en la ruta '/game-data':", error);
        res.status(500).send('Error en el servidor');
    }
});





// Ruta para manejar el formulario de solicitud de recuperación de contraseña
app.get('/forgot-password', (req, res) => {
    res.render('forgot-password');
});

// Ruta para manejar la solicitud de recuperación de contraseña
app.post('/forgot-password', async (req, res) => {
    try {
        const { correo } = req.body;

        // Verificar si el correo electrónico existe en la base de datos
        db.query('SELECT * FROM usuarios WHERE correo = ?', [correo], async (error, results) => {
            if (error) {
                console.log(error);
                res.status(500).send('Error en el servidor');
            } else if (results.length === 0) {
                res.status(404).send('El correo electrónico no está registrado');
            } else {
                const usuario = results[0];
                const correoDestino = usuario.correo;

                // Generar un token de recuperación de contraseña TEMPORAL 
                const token = Math.random().toString(36).substring(2, 15) + Math.random().toString(36).substring(2, 15);
                const tokenExpiracion = new Date(Date.now() + 3600000); // Token válido por 1 hora

                db.query('UPDATE usuarios SET token_recuperacion = ?, token_expiracion = ? WHERE id = ?', [token, tokenExpiracion, usuario.id], async (error, updateResult) => {
                    if (error) {
                        console.log(error);
                        res.status(500).send('Error en el servidor');
                    } else {
                        // Configurar el correo electrónico de recuperación
                        const mailOptions = {
                            from: process.env.EMAIL_SENDER,
                            to: correoDestino,
                            subject: 'Recuperación de contraseña',
                            text: `Hola ${usuario.usuario},\n\nPara restablecer tu contraseña, haz clic en el siguiente enlace:\n\n${process.env.HOST_GAME}/reset-password/${token}\n\nEl enlace expirará en 1 hora. Si no solicitaste este cambio, puedes ignorar este correo.\n\nSaludos,\nTu equipo de soporte`
                        };

                        // Enviar el correo electrónico
                        transporter.sendMail(mailOptions, (error, info) => {
                            if (error) {
                                console.log(error);
                                res.status(500).send('Error en el servidor');
                            } else {
                                console.log('Correo de recuperación enviado: ' + info.response);
                                res.status(200).send('Correo de recuperación enviado. Revisa tu bandeja de entrada.');
                            }
                        });
                    }
                });
            }
        });
    } catch (error) {
        console.log(error);
        res.status(500).send('Error en el servidor');
    }
});

// Ruta para manejar la solicitud de restablecimiento de contraseña
app.get('/reset-password/:token', (req, res) => {
    const token = req.params.token;

    // Verificar si el token de recuperación es válido y no ha expirado
    db.query('SELECT * FROM usuarios WHERE token_recuperacion = ? AND token_expiracion > NOW()', [token], (error, results) => {
        if (error) {
            console.log(error);
            res.status(500).send('Error en el servidor');
        } else if (results.length === 0) {
            res.status(401).send('El token de recuperación es inválido o ha expirado');
        } else {
            res.render('reset-password', { token: token });
        }
    });
});

// Ruta para manejar el proceso de restablecimiento de contraseña
app.post('/reset-password', async (req, res) => {
    try {
        const { token, nueva_contraseña } = req.body;

        // Verificar si el token de recuperación es válido y no ha expirado
        db.query('SELECT * FROM usuarios WHERE token_recuperacion = ? AND token_expiracion > NOW()', [token], async (error, results) => {
            if (error) {
                console.log(error);
                res.status(500).send('Error en el servidor');
            } else if (results.length === 0) {
                res.status(401).send('El token de recuperación es inválido o ha expirado');
            } else {
                const usuario = results[0];
                const hashedPassword = await bcrypt.hash(nueva_contraseña, 10);

                db.query('UPDATE usuarios SET contraseña = ?, token_recuperacion = NULL, token_expiracion = NULL WHERE id = ?', [hashedPassword, usuario.id], async (error, updateResult) => {
                    if (error) {
                        console.log(error);
                        res.status(500).send('Error en el servidor');
                    } else {
                        res.status(200).send('Contraseña restablecida correctamente');
                    }
                });
            }
        });
    } catch (error) {
        console.log(error);
        res.status(500).send('Error en el servidor');
    }
});

app.get('/getUserData', (req, res) => {
    const { maxResults, moneyFilter } = req.query;
    generateUserTable(maxResults, moneyFilter, (error, tableHtml) => {
        if (error) {
            res.status(500).send('Error en el servidor');
        } else {
            res.send(tableHtml);
        }
    });
});

function generateUserTable(maxResults, moneyFilter, callback) {
    let sql = "SELECT u.id, u.usuario, " +
        "CASE u.tipo_usuario " +
        "WHEN 'cliente' THEN 'Cliente' " +
        "WHEN 'fabricante' THEN 'Fabricante de Agroinsumos' " +
        "WHEN 'cliente' THEN 'Cliente' " +
        "WHEN 'distribuidor' THEN 'Distribuidor de Agroinsumos' " +
        "WHEN 'proveedor_seguros' THEN 'Proveedor de Seguros' " +
        "WHEN 'financiera' THEN 'Financiera' " +
        "WHEN 'cpg' THEN 'Empresa CPG' " +
        "WHEN 'acopiador' THEN 'Acopiador' " +
        "WHEN 'inversionista' THEN 'Inversionista' " +
        "WHEN 'publico_general' THEN 'Publico general' " +
        "ELSE u.tipo_usuario " +
        "END AS tipo_usuario, " +
        "TIMESTAMPDIFF(YEAR, u.fecha_nacimiento, CURDATE()) AS edad, IFNULL(p.dinero, 0) AS dinero, " +
        "CASE p.financiamiento " +
        "WHEN 1 THEN 'Verqor' " +
        "WHEN 2 THEN 'Banco' " +
        "WHEN 3 THEN 'Coyote' " +
        "ELSE p.financiamiento " +
        "END AS financiamiento, " +
        "CASE p.seguro " +
        "WHEN 0 THEN 'No' " +
        "WHEN 1 THEN 'Sí' " +
        "ELSE p.seguro " +
        "END AS seguro, " +
        "CASE p.practica " +
        "WHEN 0 THEN 'Tradicional' " +
        "WHEN 1 THEN 'Regenerativa' " +
        "ELSE p.practica " +
        "END AS practica " +
        "FROM usuarios u " +
        "LEFT JOIN Progreso p ON u.id = p.id_usuario ";

    // Agregar condición para filtrar por dinero
    if (moneyFilter > 0) {
        sql += `WHERE p.dinero > ${moneyFilter} `;
    }

    sql += "ORDER BY dinero DESC ";

    if (maxResults !== 'todos') {
        sql += `LIMIT ${maxResults}`;
    }

    db.query(sql, (error, results) => {
        if (error) {
            callback(error, null);
        } else {
            let tableHtml = "<table>" +
                "<tr>" +
                "<th>Ranking</th>" +
                "<th>ID</th>" +
                "<th>Usuario</th>" +
                "<th>Tipo de Usuario</th>" +
                "<th>Edad</th>" +
                "<th>Dinero</th>" +
                "<th>Financiamiento</th>" +
                "<th>Seguro</th>" +
                "<th>Práctica</th>" +
                "</tr>";

            let ranking = 1;

            if (results && results.length > 0) {
                results.forEach((fila) => {
                    tableHtml += "<tr>" +
                        "<td>" + ranking++ + "</td>" +
                        "<td>" + fila['id'] + "</td>" +
                        "<td>" + fila['usuario'] + "</td>" +
                        "<td>" + fila['tipo_usuario'] + "</td>" +
                        "<td>" + fila['edad'] + "</td>" +
                        "<td>" + fila['dinero'] + "</td>" +
                        "<td>" + (fila['financiamiento'] !== null ? fila['financiamiento'] : "N/A") + "</td>" +
                        "<td>" + (fila['seguro'] !== null ? fila['seguro'] : "N/A") + "</td>" +
                        "<td>" + (fila['practica'] !== null ? fila['practica'] : "N/A") + "</td>" +
                        "</tr>";
                });
            } else {
                tableHtml += "<tr><td colspan='9'>No se encontraron resultados.</td></tr>";
            }

            tableHtml += "</table>";
            callback(null, tableHtml);
        }
    });
}

//Ruta para edirigir a financiamiento si hay sesión
app.get('/financiamiento', (req, res) => {
    if (req.session.userId) {
        res.render('financiamiento');
    } else {
        res.redirect('/');
    }
});

// Ruta para manejar la redirección al tutorial
app.get('/tutorial', (req, res) => {
    if (req.session.userId) {
        res.render('tutorial');
    } else {
        res.redirect('/');
    }
});

// Ruta para el banco
app.get('/banco', (req, res) => {
    if (req.session.userId) {
        res.render('banco');
    } else {
        res.redirect('/');
    }
});

// Ruta get para login
app.get('/login', (req, res) => {
    res.render('index');
});



app.get('/tutorialjuego', (req, res) => {
    res.render('tutorialjuego');
});



// Ruta para cerrar el juego
app.get('/logout', (req, res) => {
    req.session.destroy((error) => {
        if (error) {
            console.log(error);
            res.status(500).send('Error al cerrar la sesión');
        } else {
            res.redirect('/');
        }
    });
});

// Ruta principal que renderiza el archivo index.hbs
app.get('/', (req, res) => {
    res.render('index');
});

app.listen(3000, () => {
    console.log('Server is running on port 3000');
});
