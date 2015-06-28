using System;
using Nancy;
using Nancy.TinyIoc;
using Nancy.Bootstrapper;
using Nancy.ViewEngines;
using System.Collections.Generic;
using Veil.Parser;
using Veil.Handlebars;

namespace Customers
{
    
    public class CustomParserRegistration : ITemplateParserRegistration
    {
        public IEnumerable<string> Keys { get { return new[] { "html" }; } }

        public Func<ITemplateParser> ParserFactory
        {
            get { return () => new HandlebarsParser(); }
        }
    }
}
