const fs = require('fs');

var s = fs.readFileSync('deploymentfiles/asa-transform-query.txt', 'utf8');

console.log(JSON.stringify(s));
