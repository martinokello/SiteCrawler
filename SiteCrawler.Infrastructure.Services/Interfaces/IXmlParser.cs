using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SiteCrawler.Infrastructure.Services.Interfaces
{
    public interface IXmlParser
    {
        XDocument Parse(string xmlContent);

        List<string> GetLocalLinks();
    }
}
