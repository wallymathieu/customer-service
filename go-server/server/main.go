/*
 * API
 *
 * Some API
 *
 * API version: 1.0.0
 * Generated by: Swagger Codegen (https://github.com/swagger-api/swagger-codegen.git)
 */

package main

import (
	"log"
	"net/http"

	// WARNING!
	// Change this to a fully-qualified import path
	// once you place this file into your project.
	// For example,
	//
	//    sw "github.com/myname/myrepo/go"
	//
	sw "./go"
)

func main() {
	log.Printf("Server started")
	svc := new(sw.InMemoryCustomerService)
	svc.Add(sw.Customer{FirstName: "first name", LastName: "last name", Gender: 1})
	router := sw.NewRouter(svc)

	log.Fatal(http.ListenAndServe(":8080", router))
}