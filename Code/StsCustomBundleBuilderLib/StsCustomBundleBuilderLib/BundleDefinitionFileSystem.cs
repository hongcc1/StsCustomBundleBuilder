using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StsCustomBundleBuilderLib
{
    public class BundleDefinitionFileSystem
    {

        public string RootPath { get; set; }
        public string BundleFileName { get; set; }

        public BundleDefinitionFileSystem(string rootPath, string bundleFileName) 
        {
            if (rootPath == null) throw new ArgumentNullException(nameof(rootPath));
            if (rootPath == string.Empty) throw new ArgumentException("Root path cannot be empty", nameof(rootPath));
            
            // Check if rootPath exists in the file system
            if (!System.IO.Directory.Exists(rootPath)) throw new ArgumentException($"Root path does not exist: {rootPath}", nameof(rootPath));

            RootPath = rootPath;
            BundleFileName = bundleFileName;
        }

        public string BundleDefinitionFilePath
        {
            get
            {
                return System.IO.Path.Combine(RootPath, "Bundle Definitions", BundleFileName);
            }
        }
        public string CustomDefinitionFolderPath
        {
            get
            {
                return System.IO.Path.Combine(RootPath, "Custom Definitions");
            }
        }
        public string NIProductDefinitionFolderPath
        {
            get
            {
                return System.IO.Path.Combine(RootPath, "NI Product Definitions");
            }
        }
        public string InstallersFolderPath
        {
            get
            {
                return System.IO.Path.Combine(RootPath, "Installers");
            }
        }
        public string BundleDefinitionFolderPath
        {
            get
            {
                return System.IO.Path.Combine(RootPath, "Bundle Definitions");
            }
        }
        
        public void CreateFoldersIfNotExist()
        {
            // Create Bundle Definitions folder if it does not exist
            if (!System.IO.Directory.Exists(BundleDefinitionFolderPath))
            {
                System.IO.Directory.CreateDirectory(BundleDefinitionFolderPath);
            }

            // Create Custom Definitions folder if it does not exist
            if (!System.IO.Directory.Exists(CustomDefinitionFolderPath))
            {
                System.IO.Directory.CreateDirectory(CustomDefinitionFolderPath);
            }

            // Create Custom Definition folder if it does not exist
            if (!System.IO.Directory.Exists(InstallersFolderPath))
            {
                System.IO.Directory.CreateDirectory(InstallersFolderPath);
            }

            // Create Product Definitions folder if it does not exist
            if (!System.IO.Directory.Exists(NIProductDefinitionFolderPath))
            {
                System.IO.Directory.CreateDirectory(NIProductDefinitionFolderPath);
            }

            // Create Installers folder if it does not exist
            if (!System.IO.Directory.Exists(InstallersFolderPath))
            {
                System.IO.Directory.CreateDirectory(InstallersFolderPath);
            }
        }
    }
}
