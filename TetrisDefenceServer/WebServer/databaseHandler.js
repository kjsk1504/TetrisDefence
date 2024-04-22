const mysql = require('mysql');
const connection = mysql.createConnection({
    user : "root",
    password : "0000",
    charset : "utf8",
});
function connectionCheck(request, response, json) {
    connection.connect((error) => {
        if (error) {
            console.log('Error on db connection: ' + error);
            response.writeHead(200, {"Content-Type" : "text/plain; charset=utf-8"});
            response.end("DB 서버 연결 실패 " + error)
        } else {
            console.log('DB connection completed');
            response.writeHead(200, {"Content-Type" : "text/plain; charset=utf-8"});
            response.end("DB 서버 연결 성공")
        }
    });
}
function queryToDB(request, response, dbQuery) {
    connection.query(dbQuery, function(error, results, fields) {
        if (error) {
            console.log("쿼리 처리 오류: " + error);
            response.writeHead(200, {"Content-Type" : "text/plain; charset=utf-8"});
            response.end("쿼리 처리 오류: " + error);
        } else {
            if (results.length > 0) {
                response.writeHead(200, {"Content-Type" : "text/plain; charset=utf-8"});
                response.end(JSON.stringify(results));
                console.log(results);
            } else {
                response.writeHead(200, {"Content-Type" : "text/plain; charset=utf-8"});
                response.end("결과 없음");
                console.log("결과 없음");
            }
        }
    });
}
module.exports = {
    queryToDB: queryToDB,
    connectionCheck: connectionCheck
}