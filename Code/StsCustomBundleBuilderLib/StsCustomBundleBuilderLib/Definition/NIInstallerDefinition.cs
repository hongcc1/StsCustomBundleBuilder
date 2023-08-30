using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StsCustomBundleBuilderLib.Definition
{
    [XmlRoot("NIInstallerDefinition", Namespace = "http://www.ni.com/STSSoftware/VersionSelector/STSSoftwareBundle.xsd")]
    public class NIInstallerDefinition : IProductInstallerDefinition
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlAttribute("version")]
        public string Version { get; set; }

        [XmlAttribute("displayName")]
        public string DisplayName { get; set; }

        [XmlArray("InstallCommands")]
        [XmlArrayItem("Command")]
        public List<Command> InstallCommands { get; set; } = new List<Command>();

        [XmlArray("UninstallCommands")]
        [XmlArrayItem("Command")]
        public List<Command> UninstallCommands { get; set; } = new List<Command>();
    }
}
