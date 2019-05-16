var nodemailer = require('nodemailer');
var args = process.argv.slice(2);

var transporter = nodemailer.createTransport({
    service: 'gmail',
    auth: {
        user: 'panahi.7928@gmail.com',
        pass: '09393408112'
    }
});

var mailOptions = {
    from: 'panahi.7928@gmail.com',
    to: args[0],
    subject: args[1],
    text: args[2].replace(/\\n/gi,'\n')
};

transporter.sendMail(mailOptions, function (error, info) {
    if (error) {
        return 0;
    } else {
        return 1;
    }
});