# Hatracker Creation Kit

Hatracker Creation Kit（HaCK）は、 Unity のアセット（アバターやプロップ）を .hatom 形式で hatracker 向けにエクスポートするツールです。

[hatracker](https://scrapbox.io/hatracker/hatracker) は、バーチャルアバターカメラアプリです。

## はじめに

### 要件

- Unity 6000.3.9f1
- Universal Render Pipeline (URP)
- iOS Build Support モジュール

### インストール方法

- Unity Package Manager 経由
  - 次のURLを入力します: `https://github.com/noir-neo/hatracker-creation-kit.git` 

### Exporting Avatars

1. メニューから「Hatracker > Exporter」を開く
2. prefab を選択し、「Export」ボタンをクリック
3. .hatom ファイルの保存先を選択
4. hatracker のアバター選択から .hatom ファイルを開く

## アバターの作成

### 使用可能なコンポーネント

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
