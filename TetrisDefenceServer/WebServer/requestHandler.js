
const dbHandler = require('./databaseHandler');
function responseToRoot(request, response, json) {
    dbHandler.connectionCheck(request, response, json);
}
function responseToLogin(request, response, json) {       // id, pw 일치 여부 확인
    let dbQuery= 'SELECT * FROM `userdata`.`userinfo` WHERE `userid`="' + json.id + '" AND `password`="' + json.pw + '"';
    dbHandler.queryToDB(request, response, dbQuery);
}
function responseToSignup(request, response, json) {      // id, pw 저장
    let dbQuery= 'INSERT INTO `userdata`.`userinfo` (`userid`, `password`) VALUES ("'+ json.id +'", "'+ json.pw +'")';
    dbHandler.queryToDB(request, response, dbQuery);
}
function responseToGameData(request, response, json) {    // 게임 데이터 저장
    let dbQuery = 'UPDATE `gamedata`.`stageinfo` SET (`stage`, `wave`, `money`, `health`, `towerdata`) VALUES ("' 
    + json.stage + '", "' + json.wave + '", "' + json.money + '", "' + json.health + '", "' + json.towerdata + 
    '") WHERE `userid` = "' + json.userid + '"';
    dbHandler.queryToDB(request, response, dbQuery);
}
// 모듈 내보내기.
module.exports = {
    responseToRoot : responseToRoot,
    responseToLogin : responseToLogin,
    responseToSignup : responseToSignup,
    responseToGameData : responseToGameData,
}