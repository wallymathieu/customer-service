namespace FsPickler.XmlFormat
type internal OAttribute = System.Runtime.InteropServices.OptionalAttribute
type internal DAttribute = System.Runtime.InteropServices.DefaultParameterValueAttribute
/// <summary>
///     XML pickler instance.
/// </summary>
[<Sealed; AutoSerializable(false)>]
type public XmlSerializer =
    inherit Nessos.FsPickler.FsPicklerTextSerializer
        
    val private format : XmlPickleFormatProvider

    /// <summary>
    ///     Define a new Xml pickler instance.
    /// </summary>
    /// <param name="indent">Enable indentation of output XML pickles.</param>
    /// <param name="typeConverter">Define a custom type name converter.</param>
    new ([<O;D(null)>] ?indent : bool, [<O;D(null)>] ?typeConverter : Nessos.FsPickler.ITypeNameConverter) =
        let xml = new XmlPickleFormatProvider(defaultArg indent false)
        { 
            inherit Nessos.FsPickler.FsPicklerTextSerializer(xml, ?typeConverter = typeConverter)
            format = xml    
        }

    /// Gets or sets indentation of serialized pickles.
    member x.Indent
        with get () = x.format.Indent
        and set b = x.format.Indent <- b

    /// <summary>
    ///     Create a new FsPickler serializer instance that uses the XML format.
    /// </summary>
    /// <param name="tyConv">optional type name converter implementation.</param>
    static member Create([<O;D(null)>]?typeConverter : Nessos.FsPickler.ITypeNameConverter, [<O;D(null)>]?indent : bool) = 
        new XmlSerializer(?typeConverter = typeConverter, ?indent = indent)