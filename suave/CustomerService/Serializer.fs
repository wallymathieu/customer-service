namespace Customers
open System
open System.IO
open System.Text
open System.Text.RegularExpressions
open Perch

module Serializer = 
    let parseToNamespaces str=
         let m = Regex.Match(str,@"xmlns\:?(\w+)?=""([^""]*)""")
         let g = m.Groups;
         let url= g.[2].Value
         let nsName = if g.[1].Success then Some(g.[1].Value) else None
         match nsName with
            | Some name -> XNamespace.createA name url
            | None -> XNamespace.createADefault url

    let nssExceptDefault = 
        ("xmlns:xsi=\""+XNamespace.xsi+"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"").Split([|' '|])
            |> Array.map parseToNamespaces |> Array.toList
    let defaultNamespace =
         XNamespace.create( "http://schemas.datacontract.org/2004/07/Customers" )
    
    /// XName with default namespace
    let XNameNs = XName.ns defaultNamespace 

    let valuesXml (c:Customer) =
        [XElem.value (XNameNs "AccountNumber") c.AccountNumber ;
         XElem.value (XNameNs "AddressCity") c.AddressCity ;
         XElem.value (XNameNs "AddressCountry") c.AddressCountry ;
         XElem.value (XNameNs "AddressStreet") c.AddressStreet ;
         XElem.value (XNameNs "FirstName") c.FirstName ;
         XElem.value (XNameNs "Gender") c.Gender ;
         XElem.value (XNameNs "LastName") c.LastName ;
         XElem.optionValue (XNameNs "PictureUri") c.PictureUri ;
        ]

    let toCustomerXml c =
        XElem.create (XNameNs "Customer") (valuesXml c)

    let fromCustomerXml (xml) =
        let valueOf' name = 
            XElem.valueOf (XElem.withName name xml)

        { AccountNumber= valueOf' (XNameNs "AccountNumber") |> Int32.Parse;
          AddressCity= valueOf' (XNameNs "AddressCity") ;
          AddressCountry= valueOf' (XNameNs "AddressCountry");
          AddressStreet= valueOf' (XNameNs "AddressStreet");
          FirstName= valueOf' (XNameNs "FirstName");
          Gender = valueOf' (XNameNs "Gender") |> Enum.parse;
          LastName = valueOf' (XNameNs "LastName") ;
          PictureUri = valueOf' (XNameNs "PictureUri") |> Url.tryParse
        }

    let fromXml xml =
        xml |> XElem.elements |> Seq.map fromCustomerXml |> Seq.toList
        

    let toXml cs =
        cs |> List.map toCustomerXml

    let toCustomerArrayXml l=
        let nss = nssExceptDefault |> List.map box 
        let xdoc = XDoc.create([ XElem.create (XNameNs "ArrayOfCustomer") ( nss |> List.append([toXml l])) ])
        xdoc

    let serialize o =
        let d = 
            match o with
                | CustomerOutput.Success b -> XDoc.create([| XElem.create (XName.Simple "boolean") b |])
                | CustomerOutput.Multiple l -> toCustomerArrayXml l
                | CustomerOutput.Single c -> XDoc.create([ toCustomerXml(c) ])
        d.ToString()

    let deserialize (input:byte array) =
        let doc = Xml.parse(Encoding.UTF8.GetString(input))
        match doc.Root.Name.LocalName with
            | "Customer" -> CustomerInput.Single(fromCustomerXml doc.Root)
            | "ArrayOfCustomer" -> CustomerInput.Multiple(fromXml doc.Root)
            | v -> failwith("Unknown input! "+v)
