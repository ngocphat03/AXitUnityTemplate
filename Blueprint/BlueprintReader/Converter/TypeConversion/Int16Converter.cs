// Copyright 2009-2021 Josh Close
// This file is a part of CsvHelper and is dual licensed under MS-PL and Apache 2.0.
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html for MS-PL and http://opensource.org/licenses/Apache-2.0 for Apache 2.0.
// https://github.com/JoshClose/CsvHelper

namespace AXitUnityTemplate.Blueprint.BlueprintReader.Converter.TypeConversion
{
    using System;

    /// <summary>
    ///     Converts a <see cref="short" /> to and from a <see cref="string" />.
    /// </summary>
    public class Int16Converter : DefaultTypeConverter
    {
        /// <summary>
        ///     Converts the string to an object.
        /// </summary>
        /// <param name="text">The string to convert to an object.</param>
        /// <param name="typeInfo"></param>
        /// <returns>The object created from the string.</returns>
        public override object ConvertFromString(string text, Type typeInfo)
        {
            if (short.TryParse(text, out var s)) return s;

            return base.ConvertFromString(text, typeInfo);
        }
    }
}