# Changelog

## [0.12.1-preview] - 2020-10-28

### Changed
* considerpaths  under "Library" to be normalized 

## [0.12.0-preview] - 2020-10-28

### Added
* internal: add GameObjectUtility to find/create GameObjects by path
* internal: add EnumUtility function to convert enum to a list of inspector names
* internal: add EnumUtility function to convert enum values to a list
* internal: add TransformExtensions and find/create child and set the parent of a Transform
* internal: add functions to add fields in UIElementsEditorUtility 
* internal: add EditorTestUtility.WaitForFrames() 
* internal: add BaseJsonSettings
* internal: add AnimationCurveExtension
* internal: add a TimelineEditor utility function to show TimelineClip in the inspector
* internal: add a TimelineEditorReflection function to create a TimelineClip on Track
* internal: add TimelineEditor utility functions to show/refresh TimelineWindow
* internal: add SerializedDictionary class
* internal: add MonoBehaviourSingleton class

### Changed
* consider files under "Packages" to be normalized as well 
* make path functions in AssetUtility to be obsolete, and create their replacements in AssetEditorUtility
* let PackageVersion handle "x" token 
* add default parameters to OneTimeLogger::Update() 
* call CreateClipOnTrack() reflection code in TimelineEditorUtility.CreateTrackAndClip(), which will trigger ClipEditor.OnCreate()
* set IAnimationCurveOwner to internal 
* open FilmInternalUtilities.Editor assembly to AnimeToolbox runtime code
* make CreateGameObjectWithComponent() obsolete

## [0.11.1-preview] - 2020-10-18

### Fixed
* fix: GetOrAddComponent() was not working properly


## [0.11.0-preview] - 2020-09-02

### Added
* feat: add utilities to create/delete Timeline assets
* feat: add a RenderTexture extension to write to a file

### Changed
* make EditorGUIDrawerUtility::DrawUndoableGUI return success or not (bool)
* deps: make FilmInternalUtilities directly depend on Timeline package

## [0.10.2-preview] - 2020-08-17

### Changed
* test against 2021.2 too

### Fixed
* ensure that FilmInternalUtilities works on all platforms

## [0.10.1-preview] - 2020-07-01

### Changed
* make TimelineClipExtensions to internal

### Fixed
* fix warnings when using Timeline 1.6.x

## [0.10.0-preview] - 2020-07-01

### Added
* internal: add ListExtensions class with RemoveNullMembers() function 
* internal: add AssetUtility.IsAssetPath() 
* internal: add TimelineUtility class
* internal: add forceImmediate parameter to ObjectUtility::Destroy()

### Changed
* internal: open internals to com.unity.selection-groups
* refactor: simplify DrawFolderSelectorGUI() and DrawFileSelectorGUI() 

### Fixed
* fix:  NormalizeAssetPath() to normalize paths under the project path

## [0.9.0-preview] - 2020-04-15

### Added
* internal: EditorGUIDrawerUtility::DrawScrollableTextAreaGUI()
* internal: OneTimeLogger class to do logging once

### Changed
* internal: Simplify EditorGUIDrawerUtility::DrawUndoableGUI()

## [0.8.4-preview] - 2020-03-22

### Changed
* internal: refactor virtual methods in timeline-related classes

## [0.8.3-preview] - 2020-03-22

### Added
* internal: add ObjectUtility utility script and its FindSceneComponents method 

### Changed
* internal: change the functions names for serialization in BaseClipData 

## [0.8.2-preview] - 2020-03-03

### Changed
* internal: open internals of FilmInternalUtilities to MaterialSwitch

## [0.8.1-preview] - 2020-03-01

### Added
* internal: add TimelineClipExtensions 

## [0.8.0-preview] - 2020-02-24

### Added
* add ExtendedClipEditorUtility, containing utility functions to modify curves on ClipData or TimelineClip

### Changed
* simplify BaseExtendedClipTrack

## [0.7.1-preview] - 2020-02-18

### Changed
* change some functions in BaseClipData into abstract functions explicitly

## [0.7.0-preview] - 2020-02-10

### Added
* add DrawUndoableGUI() function to draw GUI which can be undoable

## [0.6.0-preview] - 2020-01-29

### Added
* add scripts for adding data to TimelineClip (only loaded when a project uses Timeline)

## [0.5.1-preview] - 2020-01-26

### Fixed
* fix license
* fix warning in changelogs

## [0.5.0-preview] - 2020-01-18

### Changed
* rename package name to FilmInternalUtilities
* change all public APIs to internal, and open them only to known film assemblies

## [0.4.0-preview] - 2020-01-08

### Added
* add a PackageVersion class to parse package version (semver) 

### Changed
* change the class names of PackageRequest related classes

## [0.3.1-preview] - 2020-12-14

### Changed
* include UIElements as a dependency of AnimeToolbox
* cleanup internal functions 

## [0.3.0-preview] - 2020-10-29

### Added
* add ObjectExtensions, RenderTextureExtensions, Texture2DExtensions classes 
* add PathUtility::GenerateUniqueFolder() utility function
* add a notifier to notify users to restart Unity after script compilation


## [0.2.1-preview] - 2020-10-13

### Removed
* remove unsupported/unused window

## [0.2.0-preview] - 2020-10-01

### Added
* add utility functions from StreamingImageSequence
* add utility functions from MeshSync (AssetUtility, AssetEditorUtility, EditorGUIDrawerUtility) 
* doc: add package badge in the top readme

### Changed
* test com.unity.anime-toolbox against Unity 2020 and 2021
* chore: use new Yamato conf template and reapply the existing settings

### Fixed
* fix package warnings

### Removed
* delete unused legacy functions
* remove dependency to recorder. No longer required.

## [0.1.6-preview] - 2020-08-26

### Changed
* update package info 

### Fixed
* fix test code on Linux
* fix doc warnings


## [0.1.5-preview] - 2020-08-14

### Removed
* remove obsolete/unsupported tracks from the menu

## [0.1.4-preview] - 2020-07-27

### Changed
* make UIElementsUtility into a public class 

## [0.1.3-preview] - 2020-07-27

### Added
* add UIElementsUtility which provides several utility UIElements-related utility functions
* add more error handling in FileUtility 

### Fixed
* fix build error when building applications


## [0.1.2-preview] - 2020-05-20

### Added
* test: add PathUtilityTest for testing PathUtility

### Changed
* open UIElementsEditorUtility to public	
* open PathUtility functions to public
* rename runtime assembly to Unity.AnimeToolbox without Runtime

## [0.1.1-preview] - 2020-05-20

### Changed
* change dependency of com.unity.recorder to version 2.1.0-preview.1


## [0.1.0-preview] - 2020-05-19

### Added
* add new utility scripts (FileUtility, PathUtility, UIElementsEditorUtility)
* add PackageRequest classes 

### Changed
* rename editor namespace to Unity.AnimeToolbox.Editor

## [0.0.2-preview] - 2020-04-08

### Added
* The first release of *Anime Toolbox \<com.unity.anime-toolbox\>*.

