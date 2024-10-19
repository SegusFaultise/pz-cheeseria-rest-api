FROM mongo:latest

ENV MONGO_INITDB_ROOT_USERNAME=root
ENV MONGO_INITDB_ROOT_PASSWORD=example

COPY mongodb_test_init.js /docker-entrypoint-initdb.d/

EXPOSE 27017

CMD ["mongod"]
