using System.Collections.Generic;
using System.Xml.Serialization;

namespace StsCustomBundleBuilderLib.Definition
{
    public interface IProductInstallerDefinition
    {
        string Key { get; set; }
        string Version { get; set; }
        string DisplayName { get; set; }
        List<Command> InstallCommands { get; set; }
        List<Command> UninstallCommands { get; set; }
        string FileName { get; }
    }
}