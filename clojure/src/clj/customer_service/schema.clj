(ns customer-service.schema
    (:require [schema.core :as s]))
  
  (s/def AccountNumber s/Num)
  (s/def Title s/Str)
  (s/def City s/Str)
  (s/def Country s/Str)
  (s/def Street s/Str)
  (s/def Gender s/Num)
  (s/def PictureUri s/Str)

  (s/defschema Name
    {:first s/Str
     :last s/Str})

  (s/defschema Address
    {
    (s/optional-key :city) City
    (s/optional-key :country) Country
    (s/optional-key :street) Street
    })
    
  (s/defschema Customer
    {:account-number AccountNumber
     :name Name
     :address Address
     (s/optional-key :picture-uri) PictureUri
     (s/optional-key :gender) Gender
     })
