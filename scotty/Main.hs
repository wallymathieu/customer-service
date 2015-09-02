{-# LANGUAGE OverloadedStrings #-}
import Web.Scotty
import Web.Scotty.Internal.Types (ActionT)
import CustomerService.Domain
import CustomerService.CustomerService
import CustomerService.Xml as X
-- | Parse the request body as a XML object and return it. Raises an exception if parse is unsuccessful.
xmlData :: (X.FromXML a, ScottyError e, MonadIO m) => ActionT e m a
xmlData = do
    b <- body
    either (\e -> raise $ stringError $ "xmlData - no parse: " ++ e ++ ". Data was:" ++ BL.unpack b) return $ A.eitherDecode b

-- | Set the body of the response to the JSON encoding of the given value. Also sets \"Content-Type\"
-- header to \"application/json; charset=utf-8\" if it has not already been set.
xml :: (X.ToXML a, ScottyError e, Monad m) => a -> ActionT e m ()
xml v = do
    changeHeader addIfNotPresent "Content-Type" "application/json; charset=utf-8"
    raw $ A.encode v


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