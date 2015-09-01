{-# LANGUAGE OverloadedStrings #-}
import Web.Scotty
import Web.Scotty.Internal.Types (ActionT)
import CustomerService.Domain
import CustomerService.CustomerService
main = scotty 3000 $ do
    get "/CustomerService.svc/GetAllCustomers" $ do
        xml $ getAllCustomers 
    post "/CustomerService.svc/SaveCustomer" $ do
        customer <- getCustomerParam
        xml $ saveCustomer customer

getCustomerParam :: ActionT TL.Text IO (Maybe Customer)
getCustomerParam = undefined

{-
getCustomerParam = do b <- body
                    return $ (decode b :: Maybe Customer)
                    where makeCustomer s = ""
-}