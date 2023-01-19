FROM node:erbium-alpine3.15 as build

WORKDIR /usr/src/app

COPY . .

RUN npm install

FROM node:erbium-stretch-slim

COPY --from=build /usr/src/app /app

CMD node app/server.js