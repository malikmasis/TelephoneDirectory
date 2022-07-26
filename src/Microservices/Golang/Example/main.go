package main

import (
	"fmt"
	"log"
	"net/http"
)

func handler(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintf(w, "Hello %s", r.URL.Path[1:])
}

func main() {

	log.Printf("This is start of the project")

	http.HandleFunc("/api/examplego/1", handler)
	http.HandleFunc("/api/examplego/2", handler)

	log.Fatal(http.ListenAndServe(":80", nil))

	fmt.Println("Web Server")
	log.Printf("This is end of the project")
}
