(ns customer-service.sample
  (:require
    [customer-service.customers :as customers]
    [clojure.string :as str]))

(defn split-into-columns [line] (map str/trim (str/split line #",")))
(defn map-to-customer [columns] 
    (let [account-number (Integer/parseInt (nth columns 0))
        first-name (nth columns 1)
        last-name (nth columns 2)
        ] 
    (assoc customers/empty-customer
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
(def sample-customers
    (->> (str/split-lines customer-csv) (map split-into-columns) (map map-to-customer)))
  