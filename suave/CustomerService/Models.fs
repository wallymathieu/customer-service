namespace Customers
open System
open System.Runtime.Serialization
open System.Xml.Serialization
open System.Xml.Schema
open System.Xml

type CustomerGender = 
    |Male=0
    |Female=1
    |Boy=2
    |Girl=3

type Url = {
    Value : string
} with
    static member Empty = { Value = String.Empty }
    static member tryParse v=
        match v with
            | null -> None
            | _ -> Some({ Value = v })
    static member parse v=
        (Url.tryParse v).Value
    override __.ToString()=
        __.Value

type Customer = {
    AccountNumber :int;
    FirstName : string;
    LastName : string;
    AddressCity : string;
    AddressCountry : string;
    AddressStreet : string;
    Gender : CustomerGender;
    PictureUri: Url option
} with 
    override __.ToString() =
        sprintf "%A" __
    static member Empty = {
        AccountNumber= 0;
        FirstName= null;
        LastName= null;
        AddressCity= null;
        AddressCountry= null;
        AddressStreet= null; 
        Gender= enum<CustomerGender> 0; 
        PictureUri = None
   }
    

type CustomerInput =
    | Single of Customer
    | Multiple of Customer list

type CustomerOutput =
    | Success of Boolean
    | Single of Customer
    | Multiple of Customer list
