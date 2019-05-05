var http = require('http');
var url = require('url');

const { promisify } = require('util');
const exec = promisify(require('child_process').exec)

async function run(dir) {
    let result = await exec("E:/PROJECT/Query.exe/Project1/Debug/Project1.exe " + dir);
    return result.stdout;
}

http.createServer(async function (req, res) {
    res.writeHead(200, { 'Content-Type': 'text/html' });

    let ur = url.parse(req.url, true);
    var q = ur.query;
    var pth = ur.pathname;
    //==============================================================
    //type=user&command=signin&user/pass
    //type=user&command=signup&user/pass/phone/name/moaref
    //type=user&command=delete&user/pass
    //type=user&command=edit&user/pass/phone/name/moaref

    //==============================================================
    var x = q.type + "";
    console.log(q);
    var cmd;
    if (q.type == "usersignin" && q.user != undefined && q.pass != undefined) {
        cmd = "user signin " + q.user + " " + q.pass;
    }
    else if (q.type == "usersignup" && q.user != undefined && q.pass != undefined) {
        cmd = "user signup " + q.user + " " + q.pass;
    }
    else if (q.type == "userdelete" && q.user != undefined && q.pass != undefined) {
        cmd = "user delete " + q.user + " " + q.pass;
    }
    else if (q.type == "useredit" && q.olduser != undefined && q.oldpass != undefined && q.user != undefined && q.pass != undefined) {
        cmd = "user edit " + q.olduser + " " + q.oldpass + " " + q.user + " " + q.pass;
    }
    else if (q.type == "usereditinfo" && q.id!=undefined/*must be changed to "~" and ... && q.olduser != undefined && q.oldpass != undefined && q.user != undefined && q.pass != undefined*/) {
        let phone = q.phone, name = q.name, email = q.email;
        if (phone == undefined) phone = "~";
        if (name == undefined) name = "~";
        if (email == undefined) email = "~";

        cmd = "user editinfo " + q.id + " " + name + " " + email + " " + phone;
    }
    else if (q.type == "2000279")
        cmd = "user show";
    else
        cmd = "error";
    //==============================================================
    var result = await run(cmd);
    console.log(result);
    res.end(result);
}).listen(1379);
