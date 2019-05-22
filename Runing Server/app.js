var http = require('http');
var url = require('url');
const { promisify } = require('util');
const exec = promisify(require('child_process').exec)

async function run(dir) {
    let result = await exec("E:/VS-projects/2019/query/Debug/query.exe " + dir);
    return result.stdout;
}

http.createServer(async function (req, res) {
    res.writeHead(200, { 'Content-Type': 'text/html' });

    let ur = url.parse(req.url, true);
    var q = ur.query;
    //==============================================================
    console.log(q);

    var cmd;
    //======================== user ================================
    if (q.type == "usersignin" && q.user != undefined && q.pass != undefined) {
        cmd = "user signin " + q.user + " " + q.pass;
    }
    else if (q.type == "usersignup" && q.user != undefined && q.pass != undefined && q.email!=undefined && q.info!=undefined) {
        cmd = "user signup " + q.user + " " + q.pass + " " + q.email + " " + q.info;
    }
    else if (q.type == "userdelete" && q.user != undefined && q.pass != undefined) {
        cmd = "user delete " + q.user + " " + q.pass;
    }
    else if (q.type == "useredit" && q.user != undefined && q.pass != undefined && q.newinfo != undefined) {
        cmd = "user edit " + q.user + " " + q.pass + " " + q.newinfo;
    }
    else if (q.type == "useractive" && q.user != undefined && q.pass != undefined && q.code != undefined) {
        cmd = "user active " + q.user + " " + q.pass + " " + q.newinfo;
    }
    else if (q.type == "usergenerate" && q.user != undefined && q.pass != undefined) {
        cmd = "user generate " + q.user + " " + q.pass;
    }
    else if (q.type == "userretrieve" && q.user != undefined && q.email != undefined) {
        cmd = "user retrieve " + q.user + " " + q.email;
    }
    else if (q.type == "useraddfavorite" && q.user != undefined && q.pass != undefined && q.base != undefined && q.id != undefined) {
        cmd = "user addfavorite " + q.user + " " + q.pass + " " + q.base + " " + q.id;
    }
    else if (q.type == "userdelfavorite" && q.user != undefined && q.pass != undefined && q.base != undefined && q.id != undefined) {
        cmd = "user delfavorite " + q.user + " " + q.pass + " " + q.base + " " + q.id;
    }
    else if (q.type == "show")
        cmd = "user show";
    else if (q.type == "showjs")
        cmd = "user showjs";
    //======================== sport ================================
    else if (q.type == "sportadd" && q.sport != undefined) {
        cmd = "sport add " + q.sport;
    }
    //======================== league ================================
    else if (q.type == "leagueadd" && q.sport != undefined && q.league != undefined && q.info != undefined) {
        cmd = "league add " + q.sport + " " + q.league + " " + q.info;
    }
    else if (q.type == "leaguedelete" && q.sport != undefined && q.league != undefined) {
        cmd = "league delete " + q.sport + " " + q.league;
    }
    else if (q.type == "leagueedit" && q.sport != undefined && q.league != undefined && q.newinfo != undefined) {
        cmd = "league edit " + q.sport + " " + q.league + " " + q.newinfo;
    }
    else if (q.type == "leagueactive" && q.sport != undefined && q.league != undefined) {
        cmd = "league active " + q.sport + " " + q.league;
    }
    else if (q.type == "leagueshow") {
        cmd = "league show";
    }
    //======================== team ================================
    else if (q.type == "teamadd" && q.sport != undefined && q.league != undefined && q.team != undefined && q.info != undefined) {
        cmd = "team add " + q.sport + " " + q.league + " " + q.team + " " + q.info;
    }
    else if (q.type == "teamdelete" && q.sport != undefined && q.league != undefined && q.team != undefined) {
        cmd = "team delete " + q.sport + " " + q.league + " " + q.team;
    }
    else if (q.type == "teamedit" && q.sport != undefined && q.league != undefined && q.team != undefined && q.newinfo != undefined) {
        cmd = "team edit " + q.sport + " " + q.league + " " + q.team + " " + q.newinfo;
    }
    else if (q.type == "teamactive" && q.sport != undefined && q.league != undefined && q.team != undefined) {
        cmd = "team active " + q.sport + " " + q.league + " " + q.team;
    }
    else if (q.type == "teamaddmember" && q.sport != undefined && q.league != undefined && q.team != undefined && q.player != undefined) {
        cmd = "team addmember " + q.sport + " " + q.league + " " + q.team + " " + q.player;
    }
    else if (q.type == "teamdelmember" && q.sport != undefined && q.league != undefined && q.team != undefined && q.player != undefined) {
        cmd = "team delmember " + q.sport + " " + q.league + " " + q.team + " " + q.player;
    }
    //======================== competition ================================
    else if (q.type == "competitionadd" && q.sport != undefined && q.league != undefined && q.competition != undefined && q.info != undefined) {
        cmd = "competition add " + q.sport + " " + q.league + " " + q.competition + " " + q.info;
    }
    else if (q.type == "competitiondelete" && q.sport != undefined && q.league != undefined && q.competition != undefined) {
        cmd = "competition delete " + q.sport + " " + q.league + " " + q.competition;
    }
    else if (q.type == "competitionedit" && q.sport != undefined && q.league != undefined && q.competition != undefined && q.newinfo != undefined) {
        cmd = "competition edit " + q.sport + " " + q.league + " " + q.competition + " " + q.newinfo;
    }
    else if (q.type == "competitionactive" && q.sport != undefined && q.league != undefined && q.competition != undefined) {
        cmd = "competition active " + q.sport + " " + q.league + " " + q.competition;
    }
    else if (q.type == "competitionresult" && q.sport != undefined && q.league != undefined && q.competition != undefined && q.result != undefined) {
        cmd = "competition result " + q.sport + " " + q.league + " " + q.competition + " " + q.result;
    }
    else
        cmd = "error";
    //==============================================================
    var result = await run(cmd);
    console.log(result);
    res.end(result);
}).listen(1379);
