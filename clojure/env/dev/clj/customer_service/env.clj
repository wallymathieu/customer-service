(ns customer-service.env
  (:require
    [selmer.parser :as parser]
    [clojure.tools.logging :as log]
    [customer-service.dev-middleware :refer [wrap-dev]]))

(def defaults
  {:init
   (fn []
     (parser/cache-off!)
     (log/info "\n-=[customer-service started successfully using the development profile]=-"))
   :stop
   (fn []
     (log/info "\n-=[customer-service has shut down successfully]=-"))
   :middleware wrap-dev})
