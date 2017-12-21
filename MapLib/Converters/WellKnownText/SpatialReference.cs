

using System.IO;
using System.Reflection;
using System.Xml;

namespace EasyMap.Converters.WellKnownText
{
    public class SpatialReference
    {
        public static string SridToWkt(int srid)
        {
            XmlDocument xmldoc = new XmlDocument();

            string file = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) + "\\SpatialRefSys.xml";
            xmldoc.Load(file);
            XmlNode node =
                xmldoc.DocumentElement.SelectSingleNode("/SpatialReference/ReferenceSystem[SRID='" + srid + "']");
            if (node != null)
                return node.LastChild.InnerText;
            else
                return "";
        }
    }
}
