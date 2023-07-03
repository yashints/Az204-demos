const { lstat, read } = require("fs");
const Jimp = require("jimp");
const stream = require("stream");

module.exports = async function (context, image) {
  context.log(`Triggered with ${context.bindings.blobTrigger}`);
  const widthInPixels = 100;

  Jimp.read(image).then((thumbnail) => {
    thumbnail.resize(widthInPixels, Jimp.AUTO);

    thumbnail.getBuffer(Jimp.MIME_PNG, (err, buffer) => {
      const readStream = stream.PassThrough();
      readStream.end(buffer);

      context.bindings.thumbnailOut = readStream;
    });
  });
};
