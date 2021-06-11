(ns customer-service.customers
    (:require
     [immutant.caching :as c]
     [mount.core :refer [defstate]]
     [clojure.string :as str]))

(def empty-address {:city "" :country "" :street ""})
(def empty-name {:first "" :last ""})
(comment 'type CustomerGender = |Male=0 |Female=1 |Boy=2 |Girl=3)
(def empty-customer {:account-number 0 :name empty-name :address empty-address :gender 0 :picture-uri ""})


(defstate cache
    :start (c/cache "customers")
    :stop (c/stop "customers"))

(defn read
    [id]
    (get cache id nil))
    
(defn read-all
    []
    (map (fn [[k v]] (assoc v :account-number k)) cache))

(defn update
    [m]
    (when-let [id (:account-number m)]
    (let [current (read id)]
        (c/swap-in! cache id (constantly (if (nil? current) m (merge current m)))))))

