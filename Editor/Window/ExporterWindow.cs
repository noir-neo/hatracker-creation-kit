using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace HatrackerCreationKit.Editor.Window
{
    public sealed class ExporterWindow : EditorWindow
    {
        [MenuItem("Hatracker/Exporter")]
        public static void ShowWindow()
        {
            GetWindow<ExporterWindow>("Hatracker Exporter");
        }

        public void CreateGUI()
        {
            var root = rootVisualElement;

            var mainContainer = new VisualElement();
            root.Add(mainContainer);

            var prefabField = new ObjectField("Prefab")
            {
                objectType = typeof(GameObject),
                allowSceneObjects = false,
            };
            mainContainer.Add(prefabField);

            void OnExportClicked()
            {
                if (prefabField.value == null) return;
                Export((GameObject)prefabField.value);
            }

            var exportButton = new Button(OnExportClicked) { text = "Export" };
            mainContainer.Add(exportButton);
        }

        static void Export(GameObject prefab)
        {
            var outputPath = EditorUtility.SaveFilePanel("Export prefab as HATOM", "", $"{prefab.name}.hatom", "hatom");
            if (string.IsNullOrEmpty(outputPath))
            {
                return;
            }

            var builder = new Exporter.AssetBundleBuilder(prefab);
            var result = builder.Build();
            switch (result)
            {
                case Exporter.Result<Exporter.AssetBundleBuilder.Success>.Failure failure:
                    Debug.LogError(failure.Error);
                    return;
                case Exporter.Result<Exporter.AssetBundleBuilder.Success>.Success success:
                    FileUtil.ReplaceFile(success.Value.OutputPath, outputPath);
                    EditorUtility.RevealInFinder(outputPath);
                    break;
            }
        }
    }
}