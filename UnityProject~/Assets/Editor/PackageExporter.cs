using UnityEditor;

public static class PackageExporter
{
    public static void Export()
    {
        var assetPaths = new[]
        {
            "Assets/HatrackerCreationKit"
        };

        const string outputPath = "HatrackerCreationKit.unitypackage";
        AssetDatabase.ExportPackage(
            assetPaths,
            outputPath,
            ExportPackageOptions.Recurse
        );
    }
}