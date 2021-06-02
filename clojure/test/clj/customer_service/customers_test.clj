(ns customer-service.customers-test
    (:require
      [clojure.test :refer :all]
      [customer-service.customers :as customers]
      [mount.core :as mount]))
  
  
  (use-fixtures
    :each
    (fn [f]
      (mount/start #'customer-service.customers/cache)
      (f)
      (mount/stop #'customer-service.customers/cache)))
    
  (deftest test-read-all
    (testing "reading all customers"
      (is (= true (empty? (customers/read-all))))
      (is (= 0 (count (customers/read-all))))
      ))
  

  