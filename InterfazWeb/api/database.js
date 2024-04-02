const mysql = require('mysql2');

const pool = mysql.createPool({
  host: 'localhost',
  user: 'root',
  database: 'Verqor',
  password: ''
});

module.exports = pool.promise();
