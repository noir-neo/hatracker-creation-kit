# Hatracker Creation Kit

English | [日本語](./README-ja.md)

The Hatracker Creation Kit (HaCK) exports avatars set up in Unity to the .hatom format.
It supports custom shaders and certain assets that cannot be carried over via the VRM format.

Avatars exported as .hatom can be used in Hatracker, a virtual avatar camera app.

## Requirements

- Unity 6000.3.9f1
- Universal Render Pipeline (URP)
- iOS Build Support module

## Installation

See the [latest release](https://github.com/noir-neo/hatracker-creation-kit/releases/latest) for the Unity Package Manager install URL.

## Project Setup

- Open Project Settings > Hatracker > Project Validation.
- If any items need fixing, click the "Fix" button to resolve them.

## Exporting Avatars

1. Open the Exporter window from the Hatracker > Exporter menu.
2. Select a prefab and click the "Export" button.
3. Choose a destination, and the export will begin.
4. Load the .hatom file from the avatar selection in the Hatracker app.

## Creating Avatars

### Allowed Components

- MagicaCloth2.MagicaCloth
- MagicaCloth2.MagicaSphereCollider
- MagicaCloth2.MagicaCapsuleCollider
- MagicaCloth2.MagicaPlaneCollider
- MagicaCloth2.MagicaWindZone
- UniHumanoid.Humanoid
- UnityEngine.Animations.AimConstraint
- UnityEngine.Animations.LookAtConstraint
- UnityEngine.Animations.ParentConstraint
- UnityEngine.Animations.PositionConstraint
- UnityEngine.Animations.RotationConstraint
- UnityEngine.Animations.ScaleConstraint
- UnityEngine.Animator
- UnityEngine.MeshFilter
- UnityEngine.MeshRenderer
- UnityEngine.SkinnedMeshRenderer
- UnityEngine.Transform
- UniVRM10.Vrm10AimConstraint
- UniVRM10.Vrm10Instance
- UniVRM10.Vrm10RollConstraint
- UniVRM10.Vrm10RotationConstraint
- UniVRM10.VRM10SpringBoneCollider
- UniVRM10.VRM10SpringBoneColliderGroup
- UniVRM10.VRM10SpringBoneJoint

### Supported Assets

| Asset          | Version |
|----------------|---------|
| Magica Cloth 2 | 2.15.1  |
| UniGLTF        | 0.131.0 |
| VRM-1.0        | 0.131.0 |
