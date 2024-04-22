
const dbHandler = require('./databaseHandler');
function responseToRoot(request, response, json) {
    dbHandler.connectionCheck(request, response, json);
}
function responseToLogin(request, response, json) {
    let dbQuery= 'SELECT * FROM `userdata`.`userinfo` WHERE `userid`="' + json.id + '" AND `password`="' + json.pw + '"';
    dbHandler.queryToDB(request, response, dbQuery);
}
function responseToSignup(request, response, json) {
    let dbQuery= 'INSERT INTO `userdata`.`userinfo` (`userid`, `password`) VALUES ("'+ json.id +'", "'+ json.pw +'")';
    dbHandler.queryToDB(request, response, dbQuery);
}
function responseToGameData(request, response, json) {
    let dbQuery = 'UPDATE `gamedata`.`stageinfo` SET (`stage`, `wave`, `money`, `health`, `towerdata`) VALUES ("' 
    + json.stage + '", "' + json.wave + '", "' + json.money + '", "' + json.health + '", "' + json.towerdata + 
    '") WHERE `userid` = "' + json.userid + '"';
    dbHandler.queryToDB(request, response, dbQuery);
}
module.exports = {
    responseToRoot : responseToRoot,
    responseToLogin : responseToLogin,
    responseToSignup : responseToSignup,
    responseToGameData : responseToGameData,
}