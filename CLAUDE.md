# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

The Hatracker Creation Kit (HaCK) is a Unity editor extension that exports Unity assets (avatars, props) as `.hatom` files for use in the Hatracker virtual avatar camera iOS app. The package targets Unity 2022.3.22f1 with iOS Build Support required.

## Architecture

### Core Components

1. **ExporterWindow.cs** (Editor/Window/)
   - Unity Editor window UI using UIElements/UIToolkit
   - Menu: "Hatracker > Exporter"
   - Handles prefab selection and export initiation
   - File save dialog and result display

2. **AssetBundleBuilder.cs** (Editor/Exporter/)
   - Builds iOS-targeted asset bundles
   - Creates bundles with format: `avatar_{assetGuid}`
   - Uses temporary cache directory for builds
   - Returns Result<string> with built bundle path

3. **Result.cs** (Editor/Exporter/)
   - Functional error handling pattern
   - Generic Result<T> with Success/Failure implementations
   - Used throughout for error propagation

### Export Process Flow

1. User selects prefab in ExporterWindow
2. AssetBundleBuilder validates and builds iOS asset bundle
3. Bundle built to temp directory, then moved to user-selected `.hatom` location
4. File explorer opens to show exported file

### Key Technical Details

- **Assembly**: `HatrackerCreationKit.Editor` (Editor-only)
- **Build Target**: iOS only (BuildTarget.iOS)
- **Bundle Options**: BuildAssetBundleOptions.None
- **Supported Components**: Humanoid, Animator, MeshFilter, MeshRenderer, SkinnedMeshRenderer, VRM components

## Development Commands

### Unity Project Setup
```bash
# Open Unity project for development/testing
# Path: UnityProject~/
# Unity Version: 2022.3.22f1
# The package is locally referenced via file:../../ in manifest.json
```

### Building .unitypackage
The project includes a GitHub Actions workflow for building releases:
- Manually trigger via GitHub Actions UI: `.github/workflows/export-unitypackage.yml`
- Uses `PackageExporter.Export()` build method
- Creates `HatrackerCreationKit.unitypackage` artifact

For local .unitypackage export:
1. Open UnityProject~ in Unity
2. Run `PackageExporter.Export()` via Unity's ExecuteMenuItem or custom menu

### Package Installation Methods
1. **UPM**: `https://github.com/noir-neo/hatracker-creation-kit.git`
2. **Local Development**: Package is symlinked in UnityProject~/Packages/manifest.json
3. **Release**: Download .unitypackage from GitHub Releases

## Important Notes

- No automated tests exist in the codebase
- No linting or code formatting tools configured
- Export functionality specifically targets iOS platform only
- The UnityProject~ directory is for development/testing only
- Package follows Unity Package Manager (UPM) structure standards