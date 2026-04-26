# Hatracker Creation Kit

[English](./README.md) | 日本語

Hatracker Creation Kit（HaCK）は、 Unity でセットアップしたアバターを .hatom 形式でエクスポートするツールです。
VRM 形式では持ち運べないカスタムシェーダーや一部のアセットを使用できます。

.hatom 形式でエクスポートしたアバターは、バーチャルアバターカメラアプリ Hatracker で使用できます。

## 要件

- Unity 6000.3.9f1
- Universal Render Pipeline (URP)
- iOS Build Support モジュール

## インストール方法

Unity Package Manager の install URL は [最新のリリース](https://github.com/noir-neo/hatracker-creation-kit/releases/latest) を参照してください。

## プロジェクトのセットアップ

- Project Settings > Hatracker > Project Validation を開きます。
- 修正が必要な項目がある場合は、「Fix」ボタンをクリックして修正します。

## アバターのエクスポート

1. メニューから Hatracker > Exporter を開きます。
2. prefab を選択し、「Export」ボタンをクリックします。
3. 保存先を選択すると、エクスポートが開始されます。
4. Hatracker アプリのアバター選択から .hatom ファイルを選択して読み込みます。

## アバターの作成

### 使用可能なコンポーネント

- MagicaCloth2.MagicaCloth
- MagicaCloth2.MagicaSphereCollider
- MagicaCloth2.MagicaCapsuleCollider
- MagicaCloth2.MagicaPlaneCollider
- MagicaCloth2.MagicaWindZone
- UniHumanoid.Humanoid
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

### 使用可能なアセット

| アセット名          | バージョン   |
|----------------|---------|
| Magica Cloth 2 | 2.15.1  |
| UniGLTF        | 0.131.0 |
| VRM-1.0        | 0.131.0 |
