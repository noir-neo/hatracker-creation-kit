# Hatracker Creation Kit

The Hatracker Creation Kit (HaCK) lets you export Unity assets (avatars, props) as .hatom to Hatracker.

[Hatracker](https://scrapbox.io/hatracker/hatracker) is a virtual avatar camera app.

## Getting Started

### Current Unity Version

Unity 2022.3.22f1

- Required module: iOS Build Support

### Installation

- Via Unity Package Manager
  - `https://github.com/noir-neo/hatracker-creation-kit.git` 

### Exporting Avatars

1. Open the Exporter window from the "Hatracker > Exporter" menu.
2. Select your prefab and click "Export."
3. Choose a destination folder for the .hatom file.
4. Import the .hatom file into Hatracker.


## Creating Avatars

### Allowed Components

- UniHumanoid.Humanoid
- UnityEngine.Animator
- UnityEngine.MeshFilter
- UnityEngine.MeshRenderer
- UnityEngine.SkinnedMeshRenderer
- UnityEngine.Transform
- UniVRM10.Vrm10Instance
- UniVRM10.VRM10SpringBoneCollider
- UniVRM10.VRM10SpringBoneColliderGroup
- UniVRM10.VRM10SpringBoneJoint
