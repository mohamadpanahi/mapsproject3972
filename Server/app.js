var http = require('http');
var url = require('url');
const { promisify } = require('util');
const exec = promisify(require('child_process').exec)

async function run(dir) {
    let result = await exec("E:/VS-projects/2019/query/Debug/query.exe " + dir);
    return result.stdout;
}

const colors = {
    Reset: "\x1b[0m",
    Bright: "\x1b[1m",
    Dim: "\x1b[2m",
    Underscore: "\x1b[4m",
    Blink: "\x1b[5m",
    Reverse: "\x1b[7m",
    Hidden: "\x1b[8m",
    fg: {
        Black: "\x1b[30m",
        Red: "\x1b[31m",
        Green: "\x1b[32m",
        Yellow: "\x1b[33m",
        Blue: "\x1b[34m",
        Magenta: "\x1b[35m",
        Cyan: "\x1b[36m",
        White: "\x1b[37m",
        Crimson: "\x1b[38m"
    },
    bg: {
        Black: "\x1b[40m",
        Red: "\x1b[41m",
        Green: "\x1b[42m",
        Yellow: "\x1b[43m",
        Blue: "\x1b[44m",
        Magenta: "\x1b[45m",
        Cyan: "\x1b[46m",
        White: "\x1b[47m",
        Crimson: "\x1b[48m"
    }
};

http.createServer(async function (req, res) {
    res.writeHead(200, { 'Content-Type': 'text/html' });

    var date = new Date(Date.now());
    var time = date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();

    let ur = url.parse(req.url, true);
    var q = ur.query;
    var cmd;
    //======================== user ================================
    if (q.type == "usersignin" && q.user != undefined && q.pass != undefined) {
        cmd = `user signin "${q.user}" "${q.pass}"`;
    }
    else if (q.type == "userinfo" && q.user != undefined && q.pass != undefined) {
        cmd = `user info "${q.user}" "${q.pass}"`;
    }
    else if (q.type == "usersignup" && q.user != undefined && q.pass != undefined && q.email!=undefined && q.info!=undefined) {
        cmd = `user signup "${q.user}" "${q.pass}" "${q.email}" "${q.info}"`;
    }
    else if (q.type == "userfind" && q.user != undefined) {
        cmd = `user find "${q.user}"`;
    }
    else if (q.type == "userdelete" && q.user != undefined && q.pass != undefined) {
        cmd = `user delete "${q.user}" "${q.pass}"`;
    }
    else if (q.type == "useredit" && q.user != undefined && q.pass != undefined && q.newinfo != undefined) {
        cmd = `user edit "${q.user}" "${q.pass}" "${q.newinfo}"`;
    }
    else if (q.type == "useractive" && q.user != undefined && q.pass != undefined && q.code != undefined) {
        cmd = `user active "${q.user}" "${q.pass}" "${q.code}"`;
    }
    else if (q.type == "usergenerate" && q.user != undefined && q.pass != undefined) {
        cmd = `user generate "${q.user}" "${q.pass}"`;
    }
    else if (q.type == "userretrieve" && q.user != undefined && q.email != undefined) {
        cmd = `user retrieve "${q.user}" "${q.email}"`;
    }
    else if (q.type == "useraddfavorite" && q.user != undefined && q.pass != undefined && q.base != undefined && q.id != undefined) {
        cmd = `user addfavorite "${q.user}" "${q.pass}" "${q.base}" "${q.id}"`;
    }
    else if (q.type == "userdelfavorite" && q.user != undefined && q.pass != undefined && q.base != undefined && q.id != undefined) {
        cmd = `user delfavorite "${q.user}" "${q.pass}" "${q.base}" "${q.id}"`;
    }
    else if (q.type == "show")
        cmd = "user show";
    else if (q.type == "showjs")
        cmd = "user showjs";
    //======================== sport ================================
    else if (q.type == "sportadd" && q.sport != undefined) {
        cmd = `sport add "${q.sport}"`;
    }
    else if (q.type == "sportname") {
        cmd = "sport name";
    }
    else if (q.type == "sportactivename") {
        cmd = "sport activename";
    }
    else if (q.type == "sportupc" && q.sport != undefined) {
        cmd = `sport upc "${q.sport}"`;
    }
    //======================== league ================================
    else if (q.type == "leagueadd" && q.sport != undefined && q.league != undefined && q.info != undefined) {
        cmd = `league add "${q.sport}" "${q.league}" "${q.info}"`;
    }
    else if (q.type == "leaguedelete" && q.sport != undefined && q.league != undefined) {
        cmd = `league delete "${q.sport}" "${q.league}"`;
    }
    else if (q.type == "leagueend" && q.sport != undefined && q.league != undefined) {
        cmd = `league end "${q.sport}" "${q.league}"`;
    }
    else if (q.type == "leagueedit" && q.sport != undefined && q.league != undefined && q.newinfo != undefined) {
        cmd = `league edit "${q.sport}" "${q.league}" "${q.newinfo}"`;
    }
    else if (q.type == "leagueactive" && q.sport != undefined && q.league != undefined) {
        cmd = `league active "${q.sport}" "${q.league}"`;
    }
    else if (q.type == "leaguerank" && q.sport != undefined && q.league != undefined) {
        cmd = `league rank "${q.sport}" "${q.league}"`;
    }
    else if (q.type == "leagueshow") {
        cmd = "league show";
    }
    //======================== team ================================
    else if (q.type == "teamadd" && q.sport != undefined && q.league != undefined && q.team != undefined && q.info != undefined) {
        cmd = `team add "${q.sport}" "${q.league}" "${q.team}" "${q.info}"`;
    }
    else if (q.type == "teamdelete" && q.sport != undefined && q.league != undefined && q.team != undefined) {
        cmd = `team delete "${q.sport}" "${q.league}" "${q.team}"`;
    }
    else if (q.type == "teamedit" && q.sport != undefined && q.league != undefined && q.team != undefined && q.newinfo != undefined) {
        cmd = `team edit "${q.sport}" "${q.league}" "${q.team}" "${q.newinfo}"`;
    }
    else if (q.type == "teamactive" && q.sport != undefined && q.league != undefined && q.team != undefined) {
        cmd = `team active "${q.sport}" "${q.league}" "${q.team}"`;
    }
    else if (q.type == "teamaddmember" && q.sport != undefined && q.league != undefined && q.team != undefined && q.player != undefined) {
        cmd = `team addmember "${q.sport}" "${q.league}" "${q.team}" "${q.player}"`;
    }
    else if (q.type == "teamdelmember" && q.sport != undefined && q.league != undefined && q.team != undefined && q.player != undefined) {
        cmd = `team delmember "${q.sport}" "${q.league}" "${q.team}" "${q.player}"`;
    }
    //======================== competition ================================
    else if (q.type == "competitionadd" && q.sport != undefined && q.league != undefined && q.competition != undefined && q.info != undefined) {
        cmd = `competition add "${q.sport}" "${q.league}" "${q.competition}" "${q.info}"`;
    }
    else if (q.type == "competitiondelete" && q.sport != undefined && q.league != undefined && q.competition != undefined) {
        cmd = `competition delete "${q.sport}" "${q.league}" "${q.competition}"`;
    }
    else if (q.type == "competitionedit" && q.sport != undefined && q.league != undefined && q.competition != undefined && q.newinfo != undefined) {
        cmd = `competition edit "${q.sport}" "${q.league}" "${q.competition}" "${q.newinfo}"`;
    }
    else if (q.type == "competitionactive" && q.sport != undefined && q.league != undefined && q.competition != undefined) {
        cmd = `competition active "${q.sport}" "${q.league}" "${q.competition}"`;
    }
    else if (q.type == "competitionresult" && q.sport != undefined && q.league != undefined && q.competition != undefined && q.result != undefined && q.info != undefined) {
        cmd = `competition result "${q.sport}" "${q.league}" "${q.competition}" "${q.result}" "${q.info}"`;
    }
    else
        cmd = "error";
    //==============================================================
    var result = await run(cmd);

    console.log(colors.bg.White, colors.fg.Red, "---------------", colors.fg.Black, time, colors.fg.Red, "---------------", colors.Reset);
    console.log(colors.bg.White, colors.fg.Black, "Request -> ", colors.Reset, q);
    console.log(colors.bg.White, colors.fg.Black, "cmd     -> ",  colors.bg.Black, colors.fg.Red, cmd);
    console.log(colors.bg.White, colors.fg.Black, "Result  -> ", colors.bg.Black , colors.fg.Cyan, result,colors.Reset, "\n");
   
    
    res.end(result);
}).listen(1379);
