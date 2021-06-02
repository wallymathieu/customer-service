(ns customer-service.handler-test
  (:require
    [clojure.test :refer :all]
    [ring.mock.request :refer :all]
    [customer-service.handler :refer :all]
    [customer-service.middleware.formats :as formats]
    [muuntaja.core :as m]
    [mount.core :as mount]))

(defn parse-json [body]
  (m/decode formats/instance "application/json" body))

(use-fixtures
  :once
  (fn [f]
    (mount/start #'customer-service.config/env
                 #'customer-service.customers/cache
                 #'customer-service.handler/app-routes)
    (f)
    (mount/stop #'customer-service.config/env
                #'customer-service.customers/cache
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
      (is (= 404 (:status response))))))
