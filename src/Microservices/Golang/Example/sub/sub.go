package main

import (
	"context"
	"log"
	"net/http"

	"github.com/dapr/go-sdk/service/common"
	daprd "github.com/dapr/go-sdk/service/http"
)

// Subscription to tell the dapr what topic to subscribe.
// - PubsubName: is the name of the component configured in the metadata of pubsub.yaml.
// - Topic: is the name of the topic to subscribe.
// - Route: tell dapr where to request the API to publish the message to the subscriber when get a message from topic.
var sub = &common.Subscription{
	PubsubName: "pubsub",
	Topic:      "neworder",
	Route:      "/orders",
}

func main() {
	log.Printf("sub is running on port 8023")
	s := daprd.NewService(":8023")

	if err := s.AddTopicEventHandler(sub, eventHandler); err != nil {
		log.Fatalf("error adding topic subscription: %v", err)
	}

	if err := s.Start(); err != nil && err != http.ErrServerClosed {
		log.Fatalf("error listenning: %v", err)
	}
}

func eventHandler(ctx context.Context, e *common.TopicEvent) (retry bool, err error) {
	log.Printf("event - PubsubName: %s, Topic: %s, ID: %s, Data: %s", e.PubsubName, e.Topic, e.ID, e.Data)
	return false, nil
}

//dapr run --app-id sub --app-protocol http --app-port 8071 --dapr-http-port 3500 --log-level debug --components-path ./config go run sub/sub.go
//dapr run --app-id sub --app-protocol http --app-port 8071 --dapr-http-port 3500 --log-level debug go run sub/sub.go
