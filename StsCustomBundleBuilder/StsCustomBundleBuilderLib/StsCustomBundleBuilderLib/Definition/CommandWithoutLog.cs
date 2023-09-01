using System.Xml.Serialization;

namespace StsCustomBundleBuilderLib.Definition
{
    public class CommandWithoutLog
    {
        [XmlAttribute("path")]
        public string Path { get; set; }

        [XmlAttribute("parameters")]
        public string Parameters { get; set; }

        [XmlAttribute("successfulExitCodes")]
        public string SuccessfulExitCodes { get; set; }
    }
}
