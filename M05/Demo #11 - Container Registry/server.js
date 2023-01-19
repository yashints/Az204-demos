const express = require("express");
const app = express();
const path = require("path");
const router = express.Router();

console.log(__dirname);

router.get("/favicon.svg", function (req, res) {
  res.sendFile(path.join(__dirname + "/favicon.svg"));
});

router.get("/", function (req, res) {
  res.sendFile(path.join(__dirname + "/index.html"));
});

app.use("/", router);
app.listen(80);

console.log("Listening on port 80.");
