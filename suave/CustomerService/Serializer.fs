namespace Customers
open System
open System.IO
open System.Text
open System.Text.RegularExpressions

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
    let ns =
         XNamespace.create( "http://schemas.datacontract.org/2004/07/Customers" )
    
    let valuesXml (c:Customer) =
        [XElem.value (XName.ns ns "AccountNumber") c.AccountNumber ;
         XElem.value (XName.ns ns "AddressCity") c.AddressCity ;
         XElem.value (XName.ns ns "AddressCountry") c.AddressCountry ;
         XElem.value (XName.ns ns "AddressStreet") c.AddressStreet ;
         XElem.value (XName.ns ns "FirstName") c.FirstName ;
         XElem.value (XName.ns ns "Gender") c.Gender ;
         XElem.value (XName.ns ns "LastName") c.LastName ;
         XElem.value (XName.ns ns "PictureUri") c.PictureUri ;
        ]

    let toCustomerXml c =
        XElem.create (Namespaced(ns,"Customer")) (valuesXml c)

    let fromCustomerXml (xml) =
        { AccountNumber= XElem.valueOf xml (XName.ns ns "AccountNumber") |> Int32.Parse;
          AddressCity= XElem.valueOf xml (XName.ns ns "AddressCity") ;
          AddressCountry=XElem.valueOf xml (XName.ns ns "AddressCountry");
          AddressStreet=XElem.valueOf xml (XName.ns ns "AddressStreet");
          FirstName=XElem.valueOf xml (XName.ns ns "FirstName");
          Gender = XElem.valueOf xml (XName.ns ns "Gender") |> Enum.parse;
          LastName = XElem.valueOf xml (XName.ns ns "LastName") ;
          PictureUri = XElem.valueOf xml (XName.ns ns "PictureUri") |> Url.tryParse
        }

    let fromXml xml =
        xml |> XElem.elements |> Seq.map fromCustomerXml |> Seq.toList
        

    let toXml cs =
        cs |> List.map toCustomerXml

    let toCustomerArrayXml l=
        let nss = nssExceptDefault |> List.map box 
        let xdoc = XDoc.create([ XElem.create (XName.Namespaced(ns, "ArrayOfCustomer")) ( nss |> List.append([toXml l])) ])
        xdoc

    let serialize o =
        let d = 
            match o with
                | CustomerOutput.Success b -> XDoc.create([| XElem.create (XName.Simple "boolean") b |])
                | CustomerOutput.Multiple l -> toCustomerArrayXml l
                | CustomerOutput.Single c -> XDoc.create([ toCustomerXml(c) ])
        d.ToString()

    let deserialize input =
        let doc = Xml.parse(Encoding.UTF8.GetString(input))
        match doc.Root.Name.LocalName with
            | "Customer" -> CustomerInput.Single(fromCustomerXml doc.Root)
            | "ArrayOfCustomer" -> CustomerInput.Multiple(fromXml doc.Root)
            | v -> failwith("Unknown input! "+v)
