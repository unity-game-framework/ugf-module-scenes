# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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


