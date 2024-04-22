const server = require('./server');
const requestHandler = require('./requestHandler');
const handler = {
    '/' : requestHandler.responseToRoot,
    '/gamedata' : requestHandler.responseToGameData,
    '/login' : requestHandler.responseToLogin,
    '/signup' : requestHandler.responseToSignup,
}
server.start(3000, handler);