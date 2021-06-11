(ns customer-service.handler-test
  (:require
    [clojure.test :refer :all]
    [ring.mock.request :refer :all]
    [customer-service.handler :refer :all]
    [customer-service.middleware.formats :as formats]
    [customer-service.sample :refer [sample-customers]]
    [muuntaja.core :as m]
    [mount.core :as mount]))

(defn parse-json [body]
  (m/decode formats/instance "application/json" body))

(use-fixtures
  :once
  (fn [f]
    (mount/start #'customer-service.customers/cache
                 #'customer-service.handler/app-routes)
    (f)
    (mount/stop #'customer-service.customers/cache
                #'customer-service.handler/app-routes)))

(deftest test-app
  (testing "main route"
    (let [response ((app) (request :get "/"))]
      (is (= 200 (:status response)))))

  (testing "all customers route"
    (let [response ((app) (request :get "/CustomerService.svc/GetAllCustomers"))]
      (is (= 200 (:status response)))))
    
  (testing "not-found route"
    (let [response ((app) (request :get "/invalid"))]
      (is (= 404 (:status response)))))
)

(defn- post-customer
  [t]
  ((app) (json-body (request :post "/CustomerService.svc/SaveCustomer") t)))

(defn- parse-response-body
  [response]
  (-> (:body response)
      slurp
      parse-json))


(deftest test-add
  (testing "add and get a customer"
    (let [response (post-customer (first sample-customers) )]
      (is (= 200 (:status response)))
      (let [response ((app) (request :get (str "/CustomerService.svc/GetAllCustomers")))
            customer (first (parse-response-body response))]
        (is (= (first sample-customers) customer))
        (is (= 200 (:status response))))))
)
