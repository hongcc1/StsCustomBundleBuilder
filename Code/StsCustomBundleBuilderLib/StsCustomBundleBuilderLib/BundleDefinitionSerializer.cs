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
            SerializeCore(bundleDefinition, path);
        }       

        public static void Serialize(IProductInstallerDefinition installerDefinition, string niProductPath, string customProductPath)
        {
            // If the installer is a NI installer, we want to save it in the NI product path
            var folderPath = installerDefinition is NIInstallerDefinition ? niProductPath : customProductPath;
            var path = Path.Combine(folderPath, $"{installerDefinition.Key}{installerDefinition.Version}.xml");

            SerializeCore(installerDefinition, path);
        }

        public static void Serialize(CustomActionDefinition customActionDefinition, string folderPath)
        {
            var path = Path.Combine(folderPath, $"{customActionDefinition.Key}.xml");
            SerializeCore(customActionDefinition, path);
        }

        private static void SerializeCore<T>(T definition, string path)
        {
            var serializer = new XmlSerializer(definition.GetType());
            var fileWriter = new FileStream(path, FileMode.Create);
            serializer.Serialize(fileWriter, definition, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));
            fileWriter.Close();
        }
    }
}
