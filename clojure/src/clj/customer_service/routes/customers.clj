(ns customer-service.routes.customers
    (:require
     [customer-service.customers :as customers]
     [clojure.tools.logging :as log]
     [customer-service.middleware :as middleware]
     [ring.util.response]
     [ring.util.http-response :as response]))



(defn get-all [request]
    (response/ok (customers/read-all)
    ))

;(defn save [request]
;    (if-let [current (customers/read id)]
;        (response/ok (customers/update id (merge current request-body)) request)
;        (response/not-found)))
    

(defn customer-routes []
    [ "/CustomerService.svc" 
        {:middleware [middleware/wrap-formats]}
        ["/GetAllCustomers" {:get get-all}]
        ;["/SaveCustomer" {:post save}]
        ])
    