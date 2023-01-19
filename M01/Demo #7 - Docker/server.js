const express = require("express");
const path = require("path");
const app = express();
console.log(__dirname);
app.use(express.static(path.join(__dirname, "./public")));

const port = process.env.PORT || 80;

app.listen(port, () => console.log(`listening on port ${port}!`));
