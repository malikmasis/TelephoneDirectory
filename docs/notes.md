docker build --tag docker-main-golang .
docker run -d -p 9000:80 docker-main-golang

docker-compose up -d
docker-compose down

docker volume prune -f
docker system prune -f