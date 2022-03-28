/* Przykład pozyskiwania rekordów z bazy danych za pomocą await */

var sql = require('mssql/msnodesqlv8');
const pool = new sql.ConnectionPool({
    database: 'WEPPO_8',
    server: "(localdb)\\MSSQLLocalDB",
    driver: 'msnodesqlv8',
    options: {
      trustedConnection: true
    }
  });

(async function() {
    await pool.connect();
    await pool.request().query(
      `SELECT * FROM dbo.Osoba`,
      (err,results) => {
      if(err) res.send(err);
      else {console.dir(results.recordset);}
      
    });

})();