using System.Xml.Serialization;
using StsCustomBundleBuilderLib.Definition;

namespace StsCustomBundleBuilderLib
{
    public static class BundleDefinitionDeserializer
    {
        public static void Deserialize(BundleDefinitionFileSystem bundleFileSystem, out STSSoftwareBundleDefinition bundleDefinition)
        {
            bundleDefinition = Deserialize<STSSoftwareBundleDefinition>(bundleFileSystem.BundleDefinitionFilePath);            
            
            foreach (var product in bundleDefinition.ProductList)
            {
                if (product is NIInstaller)
                {
                    var path = System.IO.Path.Combine(bundleFileSystem.NIProductDefinitionFolderPath, $"{product.Key}{product.Version}.xml");
                    product.Definition = Deserialize<NIInstallerDefinition>(path);
                }
                else
                {
                    var path = System.IO.Path.Combine(bundleFileSystem.CustomDefinitionFolderPath, $"{product.Key}{product.Version}.xml");
                    product.Definition = Deserialize<CustomInstallerDefinition>(path);
                }    
            }

            foreach (var customAction in bundleDefinition.CustomActions)
            {
                var path = System.IO.Path.Combine(bundleFileSystem.CustomDefinitionFolderPath, $"{customAction.Key}.xml");
                customAction.Definition = Deserialize<CustomActionDefinition>(path);
            }
        }

        private static T Deserialize<T>(string path)
        {
            var serializer = new XmlSerializer(typeof(T));
            var fileReader = new System.IO.StreamReader(path);
            var definition = (T) serializer.Deserialize(fileReader);
            fileReader.Close();
            return definition;
        }
    }
}
