using System.Xml.Serialization;

namespace StsCustomBundleBuilderLib.Definition
{
    public class Command
    {
        [XmlAttribute("path")]
        public string Path { get; set; }

        [XmlAttribute("parameters")]
        public string Parameters { get; set; }

        [XmlAttribute("successfulExitCodes")]
        public string SuccessfulExitCodes { get; set; }

        [XmlElement("LogCommand")]
        public LogCommand LogCommand { get; set; }

        [XmlIgnore]
        public bool LogCommandSpecified
        {
            get
            {
                // Log Command is an optional element to serialize
                return LogCommand != null;
            }
        }
    }

    public class LogCommand
    {
        [XmlAttribute("flag")]
        public string Flag { get; set; } = "/log";

        [XmlAttribute("fileName")]
        public string FileName { get; set; }
    }
}
