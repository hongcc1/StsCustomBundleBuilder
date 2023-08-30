using StsCustomBundleBuilderLib.Definition;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace StsCustomBundleBuilderLib
{
    public static class BundleDefinitionSerializer
    {
        public static void Serialize(STSSoftwareBundleDefinition bundleDefinition, string path)
        {
            var serializer = new XmlSerializer(typeof(STSSoftwareBundleDefinition));
            var fileWriter = new FileStream(path, FileMode.Create);
            serializer.Serialize(fileWriter, bundleDefinition, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));
            fileWriter.Close();
        }

        public static void Serialize(IProductInstallerDefinition installerDefinition, string niProductPath, string customProductPath)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(installerDefinition.GetType());

            // If the installer is a NI installer, we want to save it in the NI product path
            var folderPath = installerDefinition is NIInstallerDefinition ? niProductPath : customProductPath;

            var path = Path.Combine(folderPath, $"{installerDefinition.Key}{installerDefinition.Version}.xml");
            var fileWriter = new FileStream(path, FileMode.Create);
            serializer.Serialize(fileWriter, installerDefinition, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));
            fileWriter.Close();
        }

        public static void Serialize(CustomActionDefinition customActionDefinition, string folderPath)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(CustomActionDefinition));

            var path = Path.Combine(folderPath, $"{customActionDefinition.Key}.xml");
            var fileWriter = new FileStream(path, FileMode.Create);
            serializer.Serialize(fileWriter, customActionDefinition, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));
            fileWriter.Close();
        }
    }
}
