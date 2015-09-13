namespace FsPickler.XmlFormat

[<AutoOpen>]
module internal Utils =
    module Enum =
        /// <summary>
        ///     Checks that set of enumeration flags has given flag
        /// </summary>
        /// <param name="flags">Flags to be checked.</param>
        /// <param name="flag">Flag to be satisfied.</param>
        let inline hasFlag flags flag = (flags &&& flag) = flag