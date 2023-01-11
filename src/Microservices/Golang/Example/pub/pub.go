package main

import (
	"context"
	"fmt"

	dapr "github.com/dapr/go-sdk/client"
)

type Email struct {
	Email string
	no    int
}

var (
	// set the environment as instructions.
	pubsubName = "pubsub"
	topicName  = "neworder"
)

func main() {
	ctx := context.Background()
	// data := []byte("14")

	client, err := dapr.NewClient()
	if err != nil {
		panic(err)
	}
	defer client.Close()

	he := Email{Email: "+90123456789", no: 14}
	if err := client.PublishEvent(ctx, pubsubName, topicName, he); err != nil {
		panic(err)
	}
	fmt.Println("data published")

	fmt.Println("Done (CTRL+C to Exit)")
}

//dapr run --app-id pub --log-level debug --components-path ./config go run pub/pub.go
//dapr run --app-id pub --log-level debug go run pub/pub.go
