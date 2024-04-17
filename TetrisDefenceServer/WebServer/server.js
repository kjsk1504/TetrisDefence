// NodeJS 기본 서버.
// http 모듈 가져오기.
const http = require('http');
// 경로를 구분하기 위한 모듈 가져오기.
const url = require('url');
// querystring 모듈.
const querystring = require('querystring');
// 서버 생성.
// 응답 콜백 전달.
// port: 외부에서 전달받을 포트 번호.
// handler: 함수 분기 객체
function start(port, handler) {
    http.createServer(function (request, response) {
        let path = url.parse(request.url).pathname;
        if (request.method === 'GET') {         // Get 요청.
            if (typeof(handler[path]) === 'function') {
                // 요청 응답 함수 호출.
                let json = querystring.parse(url.parse(request.url).query);
                console.log(json);
                handler[path](request, response, json);
            } else {
                console.log("path 확인 바람");
            }
        } else if (request.method === 'POST') { // Post 요청.
            // 데이터를 전달 받을 빈 변수 선언.
            let downloadData = "";
            let json;
            // 이벤트 등록. .on 함수를 활용.
            // 'data' 이벤트 -> 데이터 전송 이벤트.
            request.on('data', function(chunk) {
                // 작은 단위로 분리되어 전달되는 데이터를 누적해 저장.
                downloadData += chunk;
            });
            // 'end' 이벤트 -> 데이터 전송 종료 이벤트.
            request.on('end', function() {
                // 데이터 전송이 완료되면 우리에게 필요한 일을 진행.
                console.log("데이터 전송 완료.");
                // console.log(downloadData);
                json = querystring.parse(downloadData);
                console.log(json);

                if (typeof(handler[path]) === 'function') {
                    // 요청 응답 함수 호출.
                    handler[path](request, response, json);
                } else {
                    console.log("path 확인 바람");
                }

            });
        }
    }).listen(port);
}
// 모듈로 내보내기 (NodeJS 기능)
module.exports = {
    start : start
}