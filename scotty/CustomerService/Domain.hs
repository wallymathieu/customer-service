module CustomerService.Domain where


data Gender = Male | Female | Boy | Girl
    deriving (Read, Show, Enum, Eq, Ord)

data Customer = Customer { accountNumber :: Int,
                           accountCity :: String,
                           accountCountry :: String,
                           accountStreet :: String,
                           firstName :: String,
                           gender :: Gender,
                           pictureUri :: String }
    deriving (Eq, Show, Read) 
