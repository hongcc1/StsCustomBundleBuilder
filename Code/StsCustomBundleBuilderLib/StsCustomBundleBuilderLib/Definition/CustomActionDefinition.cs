using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StsCustomBundleBuilderLib.Definition
{
    [XmlRoot("CustomActionDefinition", Namespace = "http://www.ni.com/STSSoftware/VersionSelector/STSSoftwareBundle.xsd")]
    public class CustomActionDefinition
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlAttribute("displayName")]
        public string DisplayName { get; set; }

        [XmlArray("Commands")]
        [XmlArrayItem("Command")]
        public List<CommandWithoutLog> Commands { get; set; } = new List<CommandWithoutLog>();

        [XmlIgnore]
        public string FileName
        {
            get { return $"{Key}.xml"; }
        }
    }
}
