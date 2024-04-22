const http = require('http');
const url = require('url');
const querystring = require('querystring');
function start(port, handler) {
    http.createServer(function (request, response) {
        let path = url.parse(request.url).pathname;
        if (request.method === 'GET') {
            if (typeof(handler[path]) === 'function') {
                let json = querystring.parse(url.parse(request.url).query);
                console.log(json);
                handler[path](request, response, json);
            } else {
                console.log("path 확인 바람");
            }
        } else if (request.method === 'POST') {
            let downloadData = "";
            let json;
            request.on('data', function(chunk) {
                downloadData += chunk;
            });
            request.on('end', function() {
                console.log("데이터 전송 완료.");
                json = querystring.parse(downloadData);
                console.log(json);

                if (typeof(handler[path]) === 'function') {
                    handler[path](request, response, json);
                } else {
                    console.log("path 확인 바람");
                }

            });
        }
    }).listen(port);
}
module.exports = {
    start : start
}