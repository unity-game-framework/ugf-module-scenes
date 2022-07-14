# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [4.0.0-preview](https://github.com/unity-game-framework/ugf-module-scenes/releases/tag/4.0.0-preview) - 2022-07-14  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-scenes/milestone/20?closed=1)  
    

### Changed

- Change string ids to global id data ([#58](https://github.com/unity-game-framework/ugf-module-scenes/issues/58))  
    - Update dependencies: `com.ugf.application` to `8.3.0` and `com.ugf.editortools` to `2.8.1` versions.
    - Update package _Unity_ version to `2022.1`.
    - Change usage of ids as `GlobalId` structure instead of `string`.

## [3.0.0](https://github.com/unity-game-framework/ugf-module-scenes/releases/tag/3.0.0) - 2022-01-06  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-scenes/milestone/19?closed=1)  
    

### Added

- Add scene activate async extension method ([#56](https://github.com/unity-game-framework/ugf-module-scenes/issues/56))  
    - Add `Scene.ActivateAsync()` extension method to trigger activation and wait until scene is loaded.
    - Change `SceneModuleAsset` editor to make `Loaders` and `Groups` list to display selection preview.

## [3.0.0-preview.6](https://github.com/unity-game-framework/ugf-module-scenes/releases/tag/3.0.0-preview.6) - 2021-11-23  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-scenes/milestone/18?closed=1)  
    

### Added

- Add scene loader application register from context ([#55](https://github.com/unity-game-framework/ugf-module-scenes/pull/55))  
    - Update package _Unity_ version to `2021.2`.
    - Update dependencies: `com.ugf.application` to `8.0.0` version.
    - Add `ManagerSceneLoader.RegisterApplication` property to determine whether to register application for loaded scene.
    - Add `ManagerSceneLoader.OnRegisterApplication` and `OnUnregisterApplication` protected virtual methods to override application register logic.
    - Change `UnloadUnusedAfterUnload` property to be with setter and have `true` as default value.
    - Remove `unloadUnusedAfterUnload` constructor argument from `ManagerSceneLoader` class, use `UnloadUnusedAfterUnload` property instead.
    - Remove deprecated code.

## [3.0.0-preview.5](https://github.com/unity-game-framework/ugf-module-scenes/releases/tag/3.0.0-preview.5) - 2021-10-06  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-scenes/milestone/17?closed=1)  
    

### Added

- Add refresh all groups in project settings ([#53](https://github.com/unity-game-framework/ugf-module-scenes/pull/53))  
    - Add `Refresh All` button in project settings to refresh all groups in the project.
    - Deprecate `ManagerSceneEditorUtility.UpdateAllSceneGroups()` method, use `UpdateSceneGroupAll()` method instead.

## [3.0.0-preview.4](https://github.com/unity-game-framework/ugf-module-scenes/releases/tag/3.0.0-preview.4) - 2021-06-12  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-scenes/milestone/16?closed=1)  
    

### Fixed

- Fix scene module to unload tracked scenes using default parameters of loader ([#51](https://github.com/unity-game-framework/ugf-module-scenes/pull/51))  
    - Fix scene module to unload active scenes on uninitialization using default unload parameters from loader.

## [3.0.0-preview.3](https://github.com/unity-game-framework/ugf-module-scenes/releases/tag/3.0.0-preview.3) - 2021-06-11  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-scenes/milestone/15?closed=1)  
    

### Added

- Add scene info load and unload default parameters ([#49](https://github.com/unity-game-framework/ugf-module-scenes/pull/49))  
    - Add `DefaultLoadParameters` and `DefaultUnloadParameters` properties for `ISceneLoader` interface and class implementations.
    - Add `Load`, `LoadAsync`, `Unload` and `UnloadAsync` method overloads without load and unload parameters argument, which use default parameters from loader.
    - Add implementation of default parameters properties for `SceneLoaderBase` abstract class and virtual methods of load and unload overloads.
    - Add constructor with default parameters for `ManagerSceneLoader` class.
    - Add `DefaultLoadParameters` and `DefaultUnloadParameters` properties for `ManagerSceneLoaderAsset` asset.
    - Add `TryGetDefaultLoadParametersByScene`, `GetDefaultLoadParametersByScene`, `TryGetDefaultUnloadParametersByScene` and `GetDefaultUnloadParametersByScene` extension methods for `ISceneModule` to get default parameters of loader by the specified scene id.
    - Add `Load`, `LoadAsync`, `Unload` and `UnloadAsync` extension methods for `ISceneModule` to load and unload scenes without parameters arguments, which use default parameters from loader.

## [3.0.0-preview.2](https://github.com/unity-game-framework/ugf-module-scenes/releases/tag/3.0.0-preview.2) - 2021-05-25  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-scenes/milestone/14?closed=1)  
    

### Changed

- Change project settings root name ([#47](https://github.com/unity-game-framework/ugf-module-scenes/pull/47))  
    - Update dependencies: `com.ugf.application` to `8.0.0-preview.7` version.
    - Change project settings root name to `Unity Game Framework`.

## [3.0.0-preview.1](https://github.com/unity-game-framework/ugf-module-scenes/releases/tag/3.0.0-preview.1) - 2021-04-09  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-scenes/milestone/13?closed=1)  
    

### Added

- Add higher execute priority for SceneOperationProviderInstanceComponent ([#45](https://github.com/unity-game-framework/ugf-module-scenes/pull/45))  
    - Add `DefaultExecutionOrder` attribute for `SceneOperationProviderInstanceComponent` component with highest priority.

## [3.0.0-preview](https://github.com/unity-game-framework/ugf-module-scenes/releases/tag/3.0.0-preview) - 2021-02-09  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-scenes/milestone/12?closed=1)  
    

### Changed

- Update project registry ([#42](https://github.com/unity-game-framework/ugf-module-scenes/pull/42))  
    - Update package publish registry.
    - Update dependencies: `com.ugf.application` to `8.0.0-preview.4` version.
- Update to Unity 2021.1 ([#41](https://github.com/unity-game-framework/ugf-module-scenes/pull/41))  
- Update providers and application package ([#38](https://github.com/unity-game-framework/ugf-module-scenes/pull/38))  
    - Update dependencies: `com.ugf.application` to `8.0.0-preview.3` version.
    - Add dependencies: `com.ugf.runtimetools` of `2.0.0` version.
    - Add `SceneGroupAsset` abstract class to implement custom scene group asset.
    - Add `ManagerSceneGroupAsset` asset to define scenes to work with `ManagerSceneLoader`.
    - Add `ManagerSceneEditorUtility` class to work with `ManagerSceneGroupAsset` assets.
    - Add `ISceneLoadParameters` and `ISceneUnloadParameters` interfaces and default implementation to replace solution with structures.
    - Add `SceneOperationProviderInstanceComponent` component to create and register global provider for scene operations.
    - Add `SceneModuleExtensions` with `GetLoaderByScene` and `TryGetLoaderByScene` methods.
    - Change `SceneModuleAsset` to work with `SceneGroupAsset` instead of removed `SceneInfoAssetBase` class.
    - Change `ISceneModule` and implementation to use providers solution for loaders and scenes.
    - Change `ISceneModule` and `ISceneLoader` and implementations to work with scene load and unload parameter interfaces.
    - Change `ManagerSceneEditorSettings` to work with scene groups, settings contains `UpdateAllGroupsOnBuild` property to control update of all scene groups before player build.
    - Change name of abstract `SceneLoaderAssetBase` class to `SceneLoaderAsset`.
    - Remove `ISceneProvider` and `ISceneOperationProvider` interfaces and implementations, replaced by generic providers solution.
    - Remove `SceneInfoAssetBase` and related classes.
    - Remove `ManagerSceneSettings` settings and related classes.
    - Remove deprecated code.

## [2.2.0](https://github.com/unity-game-framework/ugf-module-scenes/releases/tag/2.2.0) - 2021-01-16  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-scenes/milestone/11?closed=1)  
    

### Changed

- Update application dependency ([#35](https://github.com/unity-game-framework/ugf-module-scenes/pull/35))  
    - Update dependencies: `com.ugf.application` to `7.1.0` version.
    - Deprecate `SceneModuleDescription` constructor with `registerType` argument, use properties initialization instead.

## [2.1.0](https://github.com/unity-game-framework/ugf-module-scenes/releases/tag/2.1.0) - 2020-12-18  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-scenes/milestone/10?closed=1)  
    

### Added

- Load scene with delayed activation ([#32](https://github.com/unity-game-framework/ugf-module-scenes/pull/32))  
    - Add `ISceneOperationProvider` with default implementation `SceneOperationProvider ` to store `AsyncOperation` for specific scene.
    - Add `SceneOperationProviderInstance` as static access to global scene operation provider instance.
    - Add `Activate`, `GetOperation` and `TryGetOperation` extensions method for scene.
    - Add `SceneLoadParameters.AllowActivation` property to determine whether to activate scene after loading.
    - Change `ManagerSceneLoader` to support delayed scene activation using `SceneLoadParameters.AllowActivation` parameter.
    - Change `ManagerSceneLoader` to support and manage scene operations.
- Add support for application access by scene ([#31](https://github.com/unity-game-framework/ugf-module-scenes/pull/31))  
    - Add `SceneModule.ApplicationSceneProvider` used to register application for loaded scenes.
    - Add `SceneModuleDescription.RegisterApplicationForScenes` and `SceneModuleAsset.RegisterApplicationForScenes` properties to determine whether to register application for loaded scenes.
    - Change dependencies: `com.ugf.application` to `6.1.0` version.

## [2.0.0](https://github.com/unity-game-framework/ugf-module-scenes/releases/tag/2.0.0) - 2020-12-05  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-scenes/milestone/9?closed=1)  
    

### Changed

- Update to support latest application package ([#28](https://github.com/unity-game-framework/ugf-module-scenes/pull/28))  
    - Update to use `UGF.Builder` and `UGF.Description` packages from the latest version of `UGF.Application` package.
    - Add component menu name for `SceneContainer` component.
    - Change dependencies: `com.ugf.application` to `6.0.0` and `com.ugf.logs` to `4.1.0`.
    - Change all assets to use and implement builders features.
    - Change name of the root of create asset menu, from `UGF` to `Unity Game Framework`.

## [1.1.0](https://github.com/unity-game-framework/ugf-module-scenes/releases/tag/1.1.0) - 2020-11-12  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-scenes/milestone/8?closed=1)  
    

### Added

- Add scene module runtime debug logs ([#25](https://github.com/unity-game-framework/ugf-module-scenes/pull/25))  
    - Add logs for `SceneModule` initialize and uninitialize events.
    - Add logs for `SceneModule` and `ManagerSceneLoader` of loading and unloading events.

## [1.0.0](https://github.com/unity-game-framework/ugf-module-scenes/releases/tag/1.0.0) - 2020-11-12  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-scenes/milestone/7?closed=1)  
    

### Changed

- Rework module to support updated UGF.Application package ([#21](https://github.com/unity-game-framework/ugf-module-scenes/pull/21))
  - Add scene loaders and scene information.
  - Add scene provider to manager scene loaders and scene information.
  - Add runtime settings for scene build list.
  - Add scene loader implementation to load and unload scene using `SceneManager` for scenes from build settings.
  - Change module and module creation to support updated `UGF.Application` package.
- Update to Unity 2020.2 ([#19](https://github.com/unity-game-framework/ugf-module-scenes/pull/19))

## [0.6.0-preview](https://github.com/unity-game-framework/ugf-module-scenes/releases/tag/0.6.0-preview) - 2020-02-21  

- [Commits](https://github.com/unity-game-framework/ugf-module-scenes/compare/0.5.0-preview...0.6.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-module-scenes/milestone/6?closed=1)

### Changed
- Rework `SceneModule` controllers events.
- Change `SceneContainer` to store containers by `Component` type, rather than `MonoBehaviour`.

## [0.5.0-preview](https://github.com/unity-game-framework/ugf-module-scenes/releases/tag/0.5.0-preview) - 2020-02-19  

- [Commits](https://github.com/unity-game-framework/ugf-module-scenes/compare/0.4.0-preview...0.5.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-module-scenes/milestone/5?closed=1)

### Added
- Package dependencies:
    - `com.ugf.application`: `4.1.0-preview`.
    - `com.ugf.editortools`: `0.6.0-preview`.
- Add `ISceneModule.GetController` and `ISceneModule.GetController`.
- Add `ISceneModule.ControllerAdd` and `ISceneModule.ControllerRemove` events

### Changed
- Rename load and unload methods in `SceneModule`.

### Removed
- Package dependencies:
    - `com.ugf.module.elements`: `0.3.0-preview`.
- Remove elements from `SceneController`, replaced by collection of `IInitialize` items.

## [0.4.0-preview](https://github.com/unity-game-framework/ugf-module-scenes/releases/tag/0.4.0-preview) - 2020-02-17  

- [Commits](https://github.com/unity-game-framework/ugf-module-scenes/compare/0.3.0-preview...0.4.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-module-scenes/milestone/4?closed=1)

### Added
- Package dependencies:
    - `com.ugf.module.elements`: `0.3.0-preview`.
- Add `ISceneModule` loading events such as `Loading`, `Loaded`, `Unloading` and `Unloaded`.
- Add `SceneLoadParameters` and `SceneUnloadParameters` to setup loading and unloading of a scene.
- Add `SceneDescription` to store scene settings as asset.
- Add `SceneModuleUtility.UnloadScene` method to `SceneManager` immediate scene unloading.
- Add `SceneRoot` to get gameobjects and components from scene.
- Add `SceneContainer` component to store collection containers of any type.
- Add `SceneController` with children elements creation using `SceneContainer` as `ElementBuilder` storage.

### Changed
- Change `ISceneModule` loading and unloading scene methods signature to use `SceneLoadParameters` and `SceneUnloadParameters`.

### Removed
- Package dependencies:
    - `com.ugf.application`: `3.0.0-preview`.

### Fixed
- Fix `ISceneModule.LoadScene` does not return `Scene`.

## [0.3.0-preview](https://github.com/unity-game-framework/ugf-module-scenes/releases/tag/0.3.0-preview) - 2019-12-09  

- [Commits](https://github.com/unity-game-framework/ugf-module-scenes/compare/0.2.0-preview...0.3.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-module-scenes/milestone/3?closed=1)

### Added
- Package dependencies:
    - `com.ugf.application`: `3.0.0-preview`.

### Changed
- Update `UGF.Application` package.

### Removed
- Package dependencies:
    - `com.ugf.module`: `0.2.0-preview`.

## [0.2.0-preview](https://github.com/unity-game-framework/ugf-module-scenes/releases/tag/0.2.0-preview) - 2019-11-19  

- [Commits](https://github.com/unity-game-framework/ugf-module-scenes/compare/0.1.0-preview...0.2.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-module-scenes/milestone/2?closed=1)

### Changed
- Package dependencies:
    - `com.ugf.module`: from `0.1.0-preview` to `2.0.0-preview`.
- Rework module to work with async / await instead of coroutines.

### Removed
- Package dependencies:
    - `com.ugf.coroutines`: from `0.1.0-preview`.

## [0.1.0-preview](https://github.com/unity-game-framework/ugf-module-scenes/releases/tag/0.1.0-preview) - 2019-10-08  

- [Commits](https://github.com/unity-game-framework/ugf-module-scenes/compare/09b9c96...0.1.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-module-scenes/milestone/1?closed=1)

### Added
- This is a initial release.


