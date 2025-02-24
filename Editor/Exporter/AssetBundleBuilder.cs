using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace HatrackerCreationKit.Editor.Exporter
{
    public sealed class AssetBundleBuilder
    {
        static readonly BuildTarget BuildTarget = BuildTarget.iOS;

        public readonly struct Success
        {
            public string OutputPath { get; }

            public Success(string outputPath)
            {
                OutputPath = outputPath;
            }
        }

        readonly GameObject prefab;

        public AssetBundleBuilder(GameObject prefab)
        {
            this.prefab = prefab;
        }

        public Result<Success> Build()
        {
            if (prefab == null)
            {
                return Result<Success>.FromException(new ArgumentNullException(nameof(prefab)));
            }

            var assetPath = AssetDatabase.GetAssetPath(prefab);
            if (string.IsNullOrEmpty(assetPath))
            {
                return Result<Success>.FromException(new ArgumentException("Invalid prefab path."));
            }

            var assetGuid = AssetDatabase.AssetPathToGUID(assetPath);
            var bundleName = $"avatar_{assetGuid}";

            var buildMap = new[]
            {
                new AssetBundleBuild
                {
                    assetBundleName = bundleName,
                    assetNames = new[] { assetPath }
                }
            };

            var tmpOutputDir = Path.Combine(Application.temporaryCachePath, BuildTarget.ToString());

            if (!Directory.Exists(tmpOutputDir))
            {
                Directory.CreateDirectory(tmpOutputDir);
            }

            BuildPipeline.BuildAssetBundles(
                tmpOutputDir,
                buildMap,
                BuildAssetBundleOptions.None,
                BuildTarget
            );

            var outputPath = Path.Combine(tmpOutputDir, bundleName);
            return Result<Success>.FromValue(new Success(outputPath));
        }
    }
}