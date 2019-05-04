docker stop my-rabbitmq
docker container prune

docker build -t my_rabbitmq .

docker run --name my-rabbitmq -d -p 5672:5672 -p 15672:15672 my_rabbitmq

echo ">>>>> Wait 20 seconds, until rabbitmq in container is ready."
sleep 20s

curl -i -u guest:guest -H "content-type:application/json" \
 -XPUT -d'{"type":"fanout","durable":true}' \
 http://localhost:15672/api/exchanges/%2f/my-new-exchange

read
read
