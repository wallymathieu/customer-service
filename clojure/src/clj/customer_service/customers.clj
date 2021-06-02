(ns customer-service.customers
    (:require
     [immutant.caching :as c]
     [mount.core :refer [defstate]]
     [clojure.string :as str]))

(defn split-into-columns [line] (map str/trim (str/split line #",")))
(def empty-address {:city "" :country "" :street ""})
(def empty-name {:first "" :last ""})
(comment 'type CustomerGender = |Male=0 |Female=1 |Boy=2 |Girl=3)
(def empty-customer {:account-number 0 :name empty-name :address empty-address :gender 0 :picture-uri ""})
(defn map-to-customer [columns] 
    (let [account-number (Integer/parseInt (nth columns 0))
        first-name (nth columns 1)
        last-name (nth columns 2)
        ] 
    (assoc empty-customer
        :account-number account-number
        :name {:first first-name :last last-name}
    )))
(def customer-csv "1,Oskar,Gewalli,
    2,Greta,Skogsberg,
    3,Matthias,Wallisson,
    4,Ada,Lundborg,
    5,Daniel,Örnvik,
    6,Johan,Irisson,
    7,Edda,Tyvinge")
(def inital-customers 
    (->> (str/split-lines customer-csv) (map split-into-columns) (map map-to-customer)))

(defstate cache
    :start (c/cache "customers")
    :stop (c/stop "customers"))

(defn read
    [id]
    (get cache id nil))
    
(defn read-all
    []
    (map (fn [[k v]] (assoc v :id k)) cache))
(defn update
    [id m]
    (when-let [current (read id)]
        (c/swap-in! cache id (constantly (merge current m)))))

