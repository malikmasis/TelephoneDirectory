package main

import (
	"context"
	"fmt"

	dapr "github.com/dapr/go-sdk/client"
)

var (
	// set the environment as instructions.
	pubsubName = "pubsub"
	topicName  = "neworder"
)

func main() {
	ctx := context.Background()
	data := []byte("ping")

	client, err := dapr.NewClient()
	if err != nil {
		panic(err)
	}
	defer client.Close()

	if err := client.PublishEvent(ctx, pubsubName, topicName, data); err != nil {
		panic(err)
	}
	fmt.Println("data published")

	fmt.Println("Done (CTRL+C to Exit)")
}

//dapr run --app-id pub --log-level debug --components-path ./config go run pub/pub.go
//dapr run --app-id pub --log-level debug go run pub/pub.go