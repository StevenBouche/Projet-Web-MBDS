FROM node:14.17-alpine

RUN npm install -g @angular/cli@13.2.0

WORKDIR /app

COPY ./package.json ./package-lock.json ./
RUN npm install

COPY . .

ENTRYPOINT ["npm", "run", "dockerSslProd"]
