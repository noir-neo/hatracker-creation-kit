using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Rendering;

namespace HatrackerCreationKit.Editor.ProjectValidation
{
    public static class AssetBundleCompatSpec
    {
        const string ReferenceUrpAssetPath =
            "Packages/com.neoneobeam.hatracker-creation-kit/Settings/URPAsset.asset";

        static readonly IReadOnlyList<ICompatRule> Rules = new ICompatRule[]
        {
            new URPAssetRule(ReferenceUrpAssetPath),

            new ValueRule<ColorSpace>(
                name: "ColorSpace",
                expected: ColorSpace.Linear,
                getter: () => PlayerSettings.colorSpace,
                setter: v => PlayerSettings.colorSpace = v),

            new ValueRule<GraphicsDeviceType[]>(
                name: "iOSGraphicsAPIs",
                expected: new[] { GraphicsDeviceType.Metal },
                getter: () => PlayerSettings.GetGraphicsAPIs(BuildTarget.iOS),
                setter: v => PlayerSettings.SetGraphicsAPIs(BuildTarget.iOS, v),
                equals: (a, b) => a != null && b != null && a.SequenceEqual(b)),

            new ValueRule<NormalMapEncoding>(
                name: "iOSNormalMapEncoding",
                expected: NormalMapEncoding.XYZ,
                getter: () => PlayerSettings.GetNormalMapEncoding(NamedBuildTarget.iOS),
                setter: v => PlayerSettings.SetNormalMapEncoding(NamedBuildTarget.iOS, v)),

            new AlwaysIncludedShadersRule(new[]
            {
                "Universal Render Pipeline/Lit",
                "UniGLTF/UniUnlit",
                "VRM10/Universal Render Pipeline/MToon10",
            }),
        };

        public static IEnumerable<(ICompatRule rule, CheckResult result)> CheckAll()
        {
            foreach (var rule in Rules)
                yield return (rule, rule.Check());
        }
    }
}
