// See https://aka.ms/new-console-template for more information

using StsCustomBundleBuilderLib;
using StsCustomBundleBuilderLib.Definition;
using System;
using System.Diagnostics;
using static StsCustomBundleBuilderLib.Definition.STSSoftwareBundleDefinition;

namespace StsCustomBundleBuilderConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var bundleFileSystem = new BundleDefinitionFileSystem(@"C:\Temp\TestBundleBuilder", "CustomBundle1.0.0.xml");

            #region Create Bundle
            bundleFileSystem.CreateFoldersIfNotExist();

            var bundleDefinition = new STSSoftwareBundleDefinition("CustomBundle1.0.0", "Custom Bundle 1.0.0");
            bundleDefinition.StsSoftware = new STSSoftwareBundleDefinition.STSSoftware("NISTS0.0.0");

            AddProductsToBundle(bundleDefinition);
            AddCustomActionsToBundle(bundleDefinition);


            BundleDefinitionSerializer.Serialize(bundleDefinition, bundleFileSystem.BundleDefinitionFilePath);

            foreach (var product in bundleDefinition.ProductList)
            {
                BundleDefinitionSerializer.Serialize(product.Definition, bundleFileSystem.NIProductDefinitionFolderPath, bundleFileSystem.CustomDefinitionFolderPath);
            }

            foreach (var customAction in bundleDefinition.CustomActions)
            {
                BundleDefinitionSerializer.Serialize(customAction.Definition, bundleFileSystem.CustomDefinitionFolderPath);
            }
            #endregion

            #region Deserialize Bundle
            BundleDefinitionDeserializer.Deserialize(bundleFileSystem, out bundleDefinition);

            #endregion

        }

        private static void AddCustomActionsToBundle(STSSoftwareBundleDefinition bundleDefinition)
        {
            // Add Custom Action A
            var cAction = new STSSoftwareBundleDefinition.CustomAction("CustomActionA", true);
            cAction.Definition.DisplayName = "Custom Action A";
            cAction.Definition.Commands.Add(new Command() { Path = "Custom Action A/A.exe", Parameters = "/silent", SuccessfulExitCodes = "0" });
            bundleDefinition.CustomActions.Add(cAction);

            // Add Custom Action B
            cAction = new STSSoftwareBundleDefinition.CustomAction("CustomActionB", true);
            cAction.Definition.DisplayName = "Custom Action B";
            cAction.Definition.Commands.Add(new Command() { Path = "Custom Action B/B.bat", Parameters = "", SuccessfulExitCodes = "0" });
            bundleDefinition.CustomActions.Add(cAction);

            // Add Custom Action C1.0.0
            cAction = new STSSoftwareBundleDefinition.CustomAction("CustomActionC1.0.0", false);
            cAction.Definition.DisplayName = "Custom Action C 1.0.0";
            cAction.Definition.Commands.Add(new Command() { Path = "Custom Action C/1.0.0/C.exe", Parameters = "/silent", SuccessfulExitCodes = "0" });
            bundleDefinition.CustomActions.Add(cAction);

            // Add Custom Action C2.0.0
            cAction = new STSSoftwareBundleDefinition.CustomAction("CustomActionC2.0.0", false);
            cAction.Definition.DisplayName = "Custom Action C 2.0.0";
            cAction.Definition.Commands.Add(new Command() { Path = "Custom Action C/2.0.0/C.exe", Parameters = "/silent", SuccessfulExitCodes = "0" });
            bundleDefinition.CustomActions.Add(cAction);
        }

        private static void AddProductsToBundle(STSSoftwareBundleDefinition bundleDefinition)
        {
            // Add NI Product A
            ProductInstaller product = new STSSoftwareBundleDefinition.NIInstaller("NIProductA", "1.1.1");
            product.Definition.DisplayName = "NI Product A";
            product.Definition.InstallCommands.Add(new Command() { Path = "NI Product A/1.1.1/install.exe", Parameters = "--passive --accept-eulas --prevent-reboot", SuccessfulExitCodes = "0,-125071" });
            product.Definition.UninstallCommands.Add(new Command() { Path = "C:\\Program Files\\National Instruments\\NI Package Manager\\nipkg.exe", Parameters = "remove ni-productA-1.1.1 --allow-uninstall -y", SuccessfulExitCodes = "0,-125071" });
            bundleDefinition.ProductList.Add(product);

            // Add Custom Installer Product
            product = new STSSoftwareBundleDefinition.CustomInstaller("CustomProduct", "2.2.2");
            product.Definition.DisplayName = "Custom Product";
            product.Definition.InstallCommands.Add(new Command()
            {
                Path = "Custom Product/2.2.2/custom.exe",
                Parameters = "/silent /norestart",
                SuccessfulExitCodes = "0,3010",
                LogCommand = new LogCommand() { Flag = "<enable-logging flag specific to this installer>", FileName = "Custom_Product_install.log" }
            });
            product.Definition.UninstallCommands.Add(new Command()
            {
                Path = "Custom Product/2.2.2/custom.exe",
                Parameters = "/uninstall /silent /norestart",
                SuccessfulExitCodes = "0,3010",
                LogCommand = new LogCommand() { Flag = "<enable-logging flag specific to this installer>", FileName = "Custom_Product_uninstall.log" }
            });
            bundleDefinition.ProductList.Add(product);

            // Add NI Product B
            product = new STSSoftwareBundleDefinition.NIInstaller("NIProductB", "2.2.2");
            product.Definition.DisplayName = "NI Product B";
            product.Definition.InstallCommands.Add(new Command()
            {
                Path = "NI Product B/2.2.2/setup.exe",
                Parameters = "/qb /AcceptLicenses yes /r:n",
                SuccessfulExitCodes = "0,3010",
                LogCommand = new LogCommand() { Flag = "/log", FileName = "NIProductB_install.log" }
            });
            product.Definition.UninstallCommands.Add(new Command()
            {
                Path = "C:\\Program Files (x86)\\National Instruments\\Shared\\NIUninstaller\\uninst.exe",
                Parameters = "/qb /ForceDependents /x \"NI Product B 2.2.2\"",
                SuccessfulExitCodes = "0,3010",
                LogCommand = new LogCommand() { Flag = "/log", FileName = "NIProductB_uninstall.log" }
            });
            bundleDefinition.ProductList.Add(product);
        }
    }
}


