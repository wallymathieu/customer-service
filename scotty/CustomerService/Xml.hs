module CustomerService.Xml where

data Xml = Xml { rawXml::String }
    deriving (Eq, Show, Read) 

class ToXml a where
    toXml :: a -> Xml

class FromXML a where
    parseXml :: Xml -> a

instance (ToXml a) => ToXml (Identity a) where
    toXml (Identity a) = toXml a

instance (FromXml a) => FromXml (Identity a) where
    parseXml a      = Identity <$> parseXML a
