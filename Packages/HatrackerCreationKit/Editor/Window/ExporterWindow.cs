using System;
using System.Linq;
using HatrackerCreationKit.Editor.ProjectValidation;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace HatrackerCreationKit.Editor.Window
{
    public sealed class ExporterWindow : EditorWindow
    {
        Action refreshValidationBanner;

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

            refreshValidationBanner = BuildValidationBanner(mainContainer);

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

            refreshValidationBanner();
        }

        void OnFocus()
        {
            refreshValidationBanner?.Invoke();
        }

        static Action BuildValidationBanner(VisualElement parent)
        {
            var banner = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    alignItems = Align.Center,
                    marginBottom = 4,
                }
            };
            parent.Add(banner);

            var helpBox = new HelpBox("", HelpBoxMessageType.Warning)
            {
                style = { flexGrow = 1 }
            };
            banner.Add(helpBox);

            banner.Add(new Button(() => SettingsService.OpenProjectSettings(ProjectValidationSettingsProvider.SettingsPath))
            {
                text = "Open"
            });

            var lastMismatches = -1;
            return () =>
            {
                var mismatches = AssetBundleCompatSpec.CheckAll().Count(pair => !pair.result.IsOk);
                if (mismatches == lastMismatches) return;
                lastMismatches = mismatches;

                if (mismatches == 0)
                {
                    banner.style.display = DisplayStyle.None;
                    return;
                }

                banner.style.display = DisplayStyle.Flex;
                helpBox.text = $"Project Validation: {mismatches} issue(s) found";
            };
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