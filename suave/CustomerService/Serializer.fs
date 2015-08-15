namespace Customers
open System
open System.IO
open System.Text
open System.Text.RegularExpressions

module Serializer = 
    let ns = "http://schemas.datacontract.org/2004/07/Customers"
    let parseToNs str=
         let m = Regex.Match(str,@"xmlns\:?(\w+)?=""([^""]*)""")
         let g = m.Groups;
         let url= g.[2].Value
         let nsName = if g.[1].Success then Some(g.[1].Value) else None
         match nsName with
            | Some name -> XNamespace.createA name url
            | None -> XNamespace.createADefault url
    let fullNs = 
        ("xmlns:xsi=\""+XNamespace.xsi+"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"http://schemas.datacontract.org/2004/07/Customers\"").Split([|' '|])
            |> Array.map parseToNs |> Array.toList
    let nssExceptDefault =
        fullNs |> List.filter ( not << XNamespace.isADefault )
    let nsDefault =
        let ns' = fullNs 
                  |> List.find XNamespace.isADefault
        ns' |> (fun ns-> XNamespace.create( ns.Value ))
    
    let header = @"<?xml version=""1.0"" encoding=""utf-8""?>"
    
    let valuesXml (c:Customer) =
        [Xml.prop (XName.Namespaced(nsDefault,"AccountNumber")) c.AccountNumber ;
         Xml.nprop (XName.Namespaced(nsDefault,"AddressCity")) c.AddressCity ;
         Xml.nprop (XName.Namespaced(nsDefault,"AddressCountry")) c.AddressCountry ;
         Xml.nprop (XName.Namespaced(nsDefault,"AddressStreet")) c.AddressStreet ;
         Xml.nprop (XName.Namespaced(nsDefault,"FirstName")) c.FirstName ;
         Xml.prop (XName.Namespaced(nsDefault,"Gender")) c.Gender ;
         Xml.nprop (XName.Namespaced(nsDefault,"LastName")) c.LastName ;
         Xml.oprop (XName.Namespaced(nsDefault,"PictureUri")) c.PictureUri ;
        ]

    let toCustomerXml c =
        XElem.create (Namespaced(nsDefault,"Customer")) (valuesXml c)

    let fromCustomerXml (xml) =
        { AccountNumber= XElem.valueOf xml (XName.Namespaced(nsDefault,"AccountNumber")) |> Int32.Parse;
          AddressCity= XElem.valueOf xml (XName.Namespaced(nsDefault,"AddressCity")) ;
          AddressCountry=XElem.valueOf xml (XName.Namespaced(nsDefault,"AddressCountry"));
          AddressStreet=XElem.valueOf xml (XName.Namespaced(nsDefault,"AddressStreet"));
          FirstName=XElem.valueOf xml (XName.Namespaced(nsDefault,"FirstName"));
          Gender = XElem.valueOf xml (XName.Namespaced(nsDefault,"Gender")) |> Enum.parse;
          LastName = XElem.valueOf xml (XName.Namespaced(nsDefault,"LastName")) ;
          PictureUri = XElem.valueOf xml (XName.Namespaced(nsDefault,"PictureUri") ) |> Url.tryParse
        }

    let fromXml xml =
        xml |> XElem.elements |> Seq.map fromCustomerXml |> Seq.toList
        

    let toXml cs =
        cs |> List.map toCustomerXml

    let toCustomerArrayXml l=
        let nss = nssExceptDefault |> List.map box 
        let xdoc = XDoc.create([ XElem.create (XName.Namespaced(nsDefault, "ArrayOfCustomer")) ( nss |> List.append([toXml l])) ])
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
