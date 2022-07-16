package main

import (
	"fmt"
	"net/http"
)

func handler(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintf(w, "Hello %s", r.URL.Path[1:])
}

func main() {

	http.HandleFunc("/api/examplego/1", handler)
	http.HandleFunc("/api/examplego/2", handler)

	http.ListenAndServe(":9000", nil)

	fmt.Println("Web Server")
}
