using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace HatrackerCreationKit.Editor.ProjectValidation
{
    public sealed class URPAssetRule : ICompatRule
    {
        public string Name => "URPAsset";
        public bool CanFix =>
            AssetDatabase.LoadAssetAtPath<UniversalRenderPipelineAsset>(referenceAssetPath) != null;

        readonly string referenceAssetPath;

        public URPAssetRule(string referenceAssetPath)
        {
            this.referenceAssetPath = referenceAssetPath;
        }

        public CheckResult Check()
        {
            var reference = AssetDatabase.LoadAssetAtPath<UniversalRenderPipelineAsset>(referenceAssetPath);
            var current = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
            if (!reference)
                return CheckResult.Mismatch($"<reference missing: {referenceAssetPath}>", current ? current.name : "<no URPAsset>");
            if (!current)
                return CheckResult.Mismatch(reference.name, "<no URPAsset>");
            if (current == reference)
                return CheckResult.Ok();

            var diffs = new List<string>();
            foreach (var (name, get) in Comparisons)
            {
                if (!Equals(get(current), get(reference)))
                    diffs.Add(name);
            }

            return diffs.Count == 0
                ? CheckResult.Ok()
                : CheckResult.Mismatch(reference.name, $"differs in {diffs.Count} field(s): {string.Join(", ", diffs)}");
        }

        public void Fix()
        {
            var reference = AssetDatabase.LoadAssetAtPath<UniversalRenderPipelineAsset>(referenceAssetPath);
            if (!reference)
                throw new InvalidOperationException($"Reference URPAsset not found at {referenceAssetPath}");

            var originalLevel = QualitySettings.GetQualityLevel();
            GraphicsSettings.defaultRenderPipeline = reference;
            for (var i = 0; i < QualitySettings.count; i++)
            {
                QualitySettings.SetQualityLevel(i);
                QualitySettings.renderPipeline = reference;
            }
            QualitySettings.SetQualityLevel(originalLevel);

            var graphicsSettings = GraphicsSettings.GetGraphicsSettings();
            if (graphicsSettings) EditorUtility.SetDirty(graphicsSettings);
            AssetDatabase.SaveAssets();
        }

        static readonly (string name, Func<UniversalRenderPipelineAsset, object> get)[] Comparisons =
        {
            ("supportsHDR", a => a.supportsHDR),
            ("supportsCameraDepthTexture", a => a.supportsCameraDepthTexture),
            ("supportsCameraOpaqueTexture", a => a.supportsCameraOpaqueTexture),
            ("mainLightRenderingMode", a => a.mainLightRenderingMode),
            ("supportsMainLightShadows", a => a.supportsMainLightShadows),
            ("additionalLightsRenderingMode", a => a.additionalLightsRenderingMode),
            ("supportsAdditionalLightShadows", a => a.supportsAdditionalLightShadows),
            ("supportsSoftShadows", a => a.supportsSoftShadows),
            ("reflectionProbeBlending", a => a.reflectionProbeBlending),
            ("reflectionProbeBoxProjection", a => a.reflectionProbeBoxProjection),
            ("supportsMixedLighting", a => a.supportsMixedLighting),
            ("supportsLightCookies", a => a.supportsLightCookies),
            ("useRenderingLayers", a => a.useRenderingLayers),
            ("enableLODCrossFade", a => a.enableLODCrossFade),
            ("lodCrossFadeDitheringType", a => a.lodCrossFadeDitheringType),
            ("shEvalMode", a => a.shEvalMode),
            ("useFastSRGBLinearConversion", a => a.useFastSRGBLinearConversion),
            ("allowPostProcessAlphaOutput", a => a.allowPostProcessAlphaOutput),
            ("probeVolumeSHBands", a => a.probeVolumeSHBands),
            ("lightProbeSystem", a => a.lightProbeSystem),
        };
    }
}
