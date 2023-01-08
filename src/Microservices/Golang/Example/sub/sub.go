package main

import (
	"context"
	"fmt"
	"log"
	"net/http"
	"net/smtp"
	"os"

	"github.com/dapr/go-sdk/service/common"
	daprd "github.com/dapr/go-sdk/service/http"
	twilio "github.com/twilio/twilio-go"
	openapi "github.com/twilio/twilio-go/rest/api/v2010"
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

	//TODO - just for example -> will change it
	if e.Topic == "neworder" {
		log.Printf("sending mail")

		from := "malik.masis@gmail.com"
		password := "psw"

		// Receiver email address.
		to := []string{
			"malik.masis@hotmail.com",
		}

		// smtp server configuration.
		smtpHost := "smtp.gmail.com"
		smtpPort := "587"

		// Message.
		message := []byte("This is a test email message.")

		// Authentication.
		auth := smtp.PlainAuth("", from, password, smtpHost)

		// Sending email.
		errMail := smtp.SendMail(smtpHost+":"+smtpPort, auth, from, to, message)
		if errMail != nil {
			fmt.Println(errMail)
			return
		}
		fmt.Println("Email Sent Successfully!")
		log.Printf("Email Sent Successfully!")
	} else {
		//export TWILIO_ACCOUNT_SID=xxxxxxxxx or setx TWILIO_ACCOUNT_SID xxxxxxxxx (windows) - need to restart pc
		log.Printf("Sending sms: " + os.Getenv("TWILIO_ACCOUNT_SID") + " " + os.Getenv("TWILIO_AUTH_TOKEN"))

		client := twilio.NewRestClient()

		params := &openapi.CreateMessageParams{}

		params.SetTo(os.Getenv("TWILIO_PHONE_NUMBER"))
		params.SetFrom(os.Getenv("TWILIO_PHONE_NUMBER"))
		params.SetBody("Hello from Golang!")

		_, errPhone := client.Api.CreateMessage(params)
		if errPhone != nil {
			fmt.Println(errPhone.Error())
		} else {
			fmt.Println("SMS sent successfully!")
		}
	}
	return false, nil
}

//dapr run --app-id sub --app-protocol http --app-port 8023 --dapr-http-port 3500 --log-level debug --components-path ./config go run sub/sub.go
//dapr run --app-id sub --app-protocol http --app-port 8023 --dapr-http-port 3500 --log-level debug go run sub/sub.go
