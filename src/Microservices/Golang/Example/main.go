package main

import (
	"fmt"
	"log"
	"net/http"
)

type Sms struct {
	No string
}

type Email struct {
	Email string
}

type ISender interface {
	Send()
}

func (s Sms) Send() {
	log.Printf("sending sms, no: %s", s)
}

func (m Email) Send() {
	log.Printf("sending mail, address: %s", m)
}

func handler(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintf(w, "Hello %s", r.URL.Path[1:])
}

func main() {

	log.Printf("This is start of the project")

	Sms{No: "+90123456789"}.Send()
	Email{Email: "malik.masis@gmail.com"}.Send()

	http.HandleFunc("/api/examplego/1", handler)
	http.HandleFunc("/api/examplego/2", handler)

	log.Fatal(http.ListenAndServe(":80", nil))

	fmt.Println("Web Server")
	log.Printf("This is end of the project")
}
