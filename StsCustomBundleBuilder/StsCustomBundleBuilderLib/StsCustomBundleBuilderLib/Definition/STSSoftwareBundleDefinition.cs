using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace StsCustomBundleBuilderLib.Definition
{
    [XmlRoot("STSSoftwareBundleDefinition", Namespace = "http://www.ni.com/STSSoftware/VersionSelector/STSSoftwareBundle.xsd")]
    public class STSSoftwareBundleDefinition
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlAttribute("displayName")]
        public string DisplayName { get; set; }
        
        [XmlAttribute(AttributeName = "schemaVersion")]
        public string SchemaVersion { get; set; } = "19.5";

        [XmlElement("STSSoftware")]
        public STSSoftware StsSoftware { get; set; }

        [XmlArray("ProductList")]
        [XmlArrayItem("NIInstaller", typeof(NIInstaller))]
        [XmlArrayItem("CustomInstaller", typeof(CustomInstaller))]
        public List<ProductInstaller> ProductList { get; set; } = new List<ProductInstaller>();

        [XmlArray("CustomActions")]
        [XmlArrayItem("Action")]
        public List<CustomAction> CustomActions { get; set; } = new List<CustomAction>();

        [XmlIgnore]
        public string FileName
        {
            get { return $"{Key}.xml"; }
        }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public STSSoftwareBundleDefinition()
        {
            
        }

        /// <summary>
        /// Constructor for creating a new bundle definition programmatically
        /// </summary>
        /// <param name="key"></param>
        /// <param name="displayName"></param>
        public STSSoftwareBundleDefinition(string key, string displayName)
        {
            Key = key;
            DisplayName = displayName;
        }

        /// <summary>
        /// Check if the product list keys are unique. If the product list is null, return true.
        /// </summary>
        /// <returns></returns>
        public bool IsProductListKeyUnique()
        {
            if (ProductList == null) { return true; }
            else
            {
                var keys = ProductList.Select(x => x.Key);
                return keys.Distinct().Count() == keys.Count();
            }
        }

        /// <summary>
        /// Check if the Custom Actions keys are unique. If the custom actions is null, return true.
        /// </summary>
        /// <returns></returns>
        public bool IsCustomActionsKeyUnique()
        {
            if (CustomActions == null) { return true; }
            else
            {
                var keys = CustomActions.Select(x => x.Key);
                return keys.Distinct().Count() == keys.Count();
            }
        }        
    }

    public class STSSoftware
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public STSSoftware()
        {

        }

        public STSSoftware(string name)
        {
            Name = name;
        }
    }

    public abstract class ProductInstaller
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlAttribute("version")]
        public string Version { get; set; }

        [XmlIgnore]
        public IProductInstallerDefinition Definition { get; set; }

        [XmlIgnore]
        public string InstallerType { get; set; }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public ProductInstaller()
        { }
    }

    public class NIInstaller : ProductInstaller
    {
        public NIInstaller()
        {
            Definition = new NIInstallerDefinition();
            InstallerType = "NIInstaller";
        }

        public NIInstaller(string key, string version)
        {
            Key = key;
            Version = version;
            Definition = new NIInstallerDefinition() { Key = key, Version = Version };
            InstallerType = "NIInstaller";
        }
    }

    public class CustomInstaller : ProductInstaller
    {
        public CustomInstaller()
        {
            Definition = new CustomInstallerDefinition();
            InstallerType = "CustomInstaller";
        }

        public CustomInstaller(string key, string version)
        {
            Key = key;
            Version = version;
            Definition = new CustomInstallerDefinition() { Key = key, Version = Version };
            InstallerType = "CustomInstaller";
        }
    }

    public class CustomAction
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlAttribute("timing")]
        public string Timing { get; set; }

        [XmlIgnore]
        public CustomActionDefinition Definition { get; set; }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public CustomAction()
        {
            Definition = new CustomActionDefinition();
        }

        /// <summary>
        /// Custom action
        /// </summary>
        /// <param name="key">Unique key for this custom action</param>
        /// <param name="timingIsPostInstall">True if timing = postinstall. Otherwise timing = preuninstall.</param>
        public CustomAction(string key, bool timingIsPostInstall)
        {
            Key = key;
            Timing = timingIsPostInstall ? "postinstall" : "preuninstall";
            Definition = new CustomActionDefinition() { Key = key };
        }
    }
}




