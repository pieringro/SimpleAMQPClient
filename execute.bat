
docker build -t my_rabbitmq .

docker run -d -p 5672:5672 -p 15672:15672 my_rabbitmq

pause