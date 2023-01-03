echo "started sub.go"
cd ./src/Microservices/GoLang/Example
dapr run --app-id sub --app-protocol http --app-port 8023 --dapr-http-port 3500 --log-level debug go run sub/sub.go