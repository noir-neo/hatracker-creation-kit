using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace HatrackerCreationKit.Editor.ProjectValidation
{
    public sealed class AlwaysIncludedShadersRule : ICompatRule
    {
        public string Name => "AlwaysIncludedShaders";
        public bool CanFix => true;

        readonly IReadOnlyList<string> expectedShaderNames;

        public AlwaysIncludedShadersRule(IReadOnlyList<string> expectedShaderNames)
        {
            this.expectedShaderNames = expectedShaderNames;
        }

        public CheckResult Check()
        {
            var actual = new HashSet<string>(GetIncludedShaderNames());
            var missing = expectedShaderNames.Where(s => !actual.Contains(s)).ToArray();
            return missing.Length == 0
                ? CheckResult.Ok()
                : CheckResult.Mismatch(
                    string.Join(", ", expectedShaderNames),
                    $"missing: {string.Join(", ", missing)}");
        }

        public void Fix()
        {
            var so = new SerializedObject(GraphicsSettings.GetGraphicsSettings());
            var prop = so.FindProperty("m_AlwaysIncludedShaders");
            var existing = new HashSet<string>();
            for (var i = 0; i < prop.arraySize; i++)
            {
                if (prop.GetArrayElementAtIndex(i).objectReferenceValue is Shader shader)
                    existing.Add(shader.name);
            }
            foreach (var name in expectedShaderNames)
            {
                if (existing.Contains(name)) continue;
                var shader = Shader.Find(name);
                if (!shader)
                {
                    Debug.LogWarning($"[AlwaysIncludedShadersRule] Shader not found: {name}");
                    continue;
                }
                prop.InsertArrayElementAtIndex(prop.arraySize);
                prop.GetArrayElementAtIndex(prop.arraySize - 1).objectReferenceValue = shader;
            }
            so.ApplyModifiedProperties();
            AssetDatabase.SaveAssets();
        }

        static IEnumerable<string> GetIncludedShaderNames()
        {
            var so = new SerializedObject(GraphicsSettings.GetGraphicsSettings());
            var prop = so.FindProperty("m_AlwaysIncludedShaders");
            for (var i = 0; i < prop.arraySize; i++)
            {
                var shader = prop.GetArrayElementAtIndex(i).objectReferenceValue as Shader;
                if (shader) yield return shader.name;
            }
        }
    }
}
