// 서버 모듈 가져오기.
const server = require('./server');
// 요청 핸들러 가져오기.
const requestHandler = require('./requestHandler');
// 경로에 따라 함수를 분기하는 객체 선언.
const handler = {
    '/' : requestHandler.responseToRoot,
    '/gamedata' : requestHandler.responseToGameData,
    '/login' : requestHandler.responseToLogin,
    '/signup' : requestHandler.responseToSignup,
}
// 서버 시작.
server.start(3000, handler);