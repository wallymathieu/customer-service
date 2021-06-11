(ns customer-service.customers-test
    (:require
      [clojure.test :refer :all]
      [customer-service.customers :as customers]
      [customer-service.sample :refer [sample-customers]]
      [mount.core :as mount]))
  
  (use-fixtures
    :each
    (fn [f]
      (mount/start #'customer-service.customers/cache)
      (f)
      (mount/stop #'customer-service.customers/cache)))

  (deftest test-create
    (testing "creating a customer"
      (let [customer (customers/update (first sample-customers))]
        (is (= 1 (:account-number customer))))))
      
  (deftest test-read-all
    (testing "reading all customers"
      (is (= true (empty? (customers/read-all))))
      (customers/update (first sample-customers))
      (is (= 1 (count (customers/read-all))))
      (customers/update (second sample-customers))
      (is (= 2 (count (customers/read-all))))))
  
  (deftest test-update
    (testing "updating a customer"
      (let [customer (customers/update (first sample-customers))
            customer1 (customers/update (assoc customer :name {:first "first" :last "last"}))]
        (is (= (customers/read (:account-number customer)) customer1)))))
  