using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace HatrackerCreationKit.Editor.ProjectValidation
{
    static class ProjectValidationSettingsProvider
    {
        public const string SettingsPath = "Project/Hatracker/Project Validation";

        [SettingsProvider]
        public static SettingsProvider Create()
        {
            return new SettingsProvider(SettingsPath, SettingsScope.Project)
            {
                label = "Project Validation",
                keywords = new HashSet<string>(new[]
                {
                    "hatracker", "urp", "shader", "shaders", "validation"
                }),
                activateHandler = (_, root) =>
                {
                    var okIcon = EditorGUIUtility.IconContent("TestPassed").image as Texture2D;
                    var warnIcon = EditorGUIUtility.IconContent("console.warnicon").image as Texture2D;

                    var header = new VisualElement
                    {
                        style =
                        {
                            flexDirection = FlexDirection.Row,
                            alignItems = Align.Center,
                            marginTop = 4,
                            marginBottom = 4,
                            marginLeft = 8,
                            marginRight = 8
                        }
                    };
                    root.Add(header);

                    var summaryLabel = new Label
                    {
                        style =
                        {
                            flexGrow = 1,
                            unityFontStyleAndWeight = FontStyle.Bold
                        }
                    };
                    header.Add(summaryLabel);

                    var showAllToggle = new Toggle("Show all")
                    {
                        value = true,
                        style =
                        {
                            marginRight = 8
                        }
                    };
                    header.Add(showAllToggle);

                    var fixAllButton = new Button { text = "Fix All" };
                    header.Add(fixAllButton);

                    var listView = new ScrollView
                    {
                        style =
                        {
                            flexGrow = 1
                        }
                    };
                    root.Add(listView);

                    VisualElement BuildRow(ICompatRule rule, CheckResult result)
                    {
                        var row = new VisualElement
                        {
                            style =
                            {
                                paddingTop = 4,
                                paddingBottom = 4,
                                paddingLeft = 6,
                                paddingRight = 6,
                                borderBottomWidth = 1,
                                borderBottomColor = new Color(0f, 0f, 0f, 0.2f)
                            }
                        };

                        var line = new VisualElement
                        {
                            style =
                            {
                                flexDirection = FlexDirection.Row,
                                alignItems = Align.Center
                            }
                        };
                        row.Add(line);

                        var statusIcon = new Image
                        {
                            image = result.IsOk ? okIcon : warnIcon,
                            scaleMode = ScaleMode.ScaleToFit,
                            style =
                            {
                                width = 16,
                                height = 16,
                                marginRight = 6
                            }
                        };
                        line.Add(statusIcon);

                        var nameLabel = new Label(rule.Name)
                        {
                            style =
                            {
                                flexGrow = 1
                            }
                        };
                        line.Add(nameLabel);

                        var fixButton = new Button(() =>
                        {
                            rule.Fix();
                            Refresh();
                        })
                        {
                            text = "Fix",
                            style =
                            {
                                width = 60
                            }
                        };
                        fixButton.SetEnabled(!result.IsOk && rule.CanFix);
                        line.Add(fixButton);

                        if (!result.IsOk)
                        {
                            var detail = new VisualElement
                            {
                                style =
                                {
                                    marginLeft = 22,
                                    marginTop = 2
                                }
                            };
                            detail.Add(new Label($"Expected: {result.Expected}"));
                            detail.Add(new Label($"Actual:   {result.Actual}"));
                            row.Add(detail);
                        }

                        return row;
                    }

                    void Refresh()
                    {
                        listView.Clear();
                        var mismatchCount = 0;
                        var total = 0;
                        foreach (var (rule, result) in AssetBundleCompatSpec.CheckAll())
                        {
                            total++;
                            if (!result.IsOk) mismatchCount++;
                            if (showAllToggle.value || !result.IsOk)
                                listView.Add(BuildRow(rule, result));
                        }

                        summaryLabel.text = $"Issues ({mismatchCount}) of Checks ({total})";
                    }

                    void FixAll()
                    {
                        foreach (var (rule, result) in AssetBundleCompatSpec.CheckAll())
                            if (!result.IsOk && rule.CanFix)
                                rule.Fix();
                        Refresh();
                    }

                    fixAllButton.clicked += FixAll;
                    showAllToggle.RegisterValueChangedCallback(_ => Refresh());

                    Refresh();
                }
            };
        }
    }
}
