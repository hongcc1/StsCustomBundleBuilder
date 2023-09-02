using System.Xml.Serialization;
using StsCustomBundleBuilderLib.Definition;

namespace StsCustomBundleBuilderLib
{
    public static class BundleDefinitionDeserializer
    {
        public static void Deserialize(BundleDefinitionFileSystem bundleFileSystem, out STSSoftwareBundleDefinition bundleDefinition, out bool missingDefinitionFiles)
        {
            bundleDefinition = Deserialize<STSSoftwareBundleDefinition>(bundleFileSystem.BundleDefinitionFilePath);
            missingDefinitionFiles = false;

            foreach (var product in bundleDefinition.ProductList)
            {
                if (product is NIInstaller)
                {
                    var path = System.IO.Path.Combine(bundleFileSystem.NIProductDefinitionFolderPath, $"{product.Key}{product.Version}.xml");

                    // Check if the product definition exists
                    if (!System.IO.File.Exists(path))
                    {
                        // Set the flag to indicate that the product definition file is missing, but create the empty definition object so that the bundle can be built
                        missingDefinitionFiles = true;
                        product.Definition = new NIInstallerDefinition() { Key = product.Key, Version = product.Version };
                    }
                    else
                        product.Definition = Deserialize<NIInstallerDefinition>(path);
                }
                else
                {
                    var path = System.IO.Path.Combine(bundleFileSystem.CustomDefinitionFolderPath, $"{product.Key}{product.Version}.xml");
                    
                    // Check if the product definition exists
                    if (!System.IO.File.Exists(path))
                    {
                        // Set the flag to indicate that the product definition file is missing, but create the empty definition object so that the bundle can be built
                        missingDefinitionFiles = true;
                        product.Definition = new CustomInstallerDefinition() { Key = product.Key, Version = product.Version };
                    }
                    else
                        product.Definition = Deserialize<CustomInstallerDefinition>(path);
                }    
            }

            foreach (var customAction in bundleDefinition.CustomActions)
            {
                var path = System.IO.Path.Combine(bundleFileSystem.CustomDefinitionFolderPath, $"{customAction.Key}.xml");

                // Check if the custom action definition exists
                if (!System.IO.File.Exists(path))
                {
                    // Set the flag to indicate that the custom action definition file is missing, but create the empty definition object so that the bundle can be built
                    missingDefinitionFiles = true;
                    customAction.Definition = new CustomActionDefinition() { Key = customAction.Key };
                }
                else

                customAction.Definition = Deserialize<CustomActionDefinition>(path);
            }
        }

        private static T Deserialize<T>(string path)
        {
            var serializer = new XmlSerializer(typeof(T), "");
            var fileReader = new System.IO.StreamReader(path);
            var definition = (T) serializer.Deserialize(fileReader);
            fileReader.Close();
            return definition;
        }
    }
}
