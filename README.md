# Re:Dream

A 3D platforming game with procedural generation elements.


## Dependencies To Install Via The Asset Store

This project uses the following asset:

* [Starter Assets - Third Person Character Controller](https://assetstore.unity.com/packages/essentials/starter-assets-third-person-character-controller-196526) by Unity Technologies.

For the project's Player prefab to work correctly, please license this asset and import it into the project. When prompted, install it's dependencies too. Once everything's installed, move the `StarterAssets` folder it generates to the following path:

`Assets/_Imports/StarterAssets`  

Next, on the Player prefab, under Player Input, change the Actions scriptable object from `StarterAssets` to our own `StarterAssetsCustom`. The file is located at `Assets/Scripts/StarterAssets/InputSystem/StarterAssetsCustom.inputsettings` and this will allow our custom controls to work.


## Dependencies To Install Via Unity

You will need to install TMP Essentials too via Window > Text Mesh Pro and click "Import TMP Essential Resources". With the "TextMesh Pro" folder it generates, place the folder here:

`Assets/_Imports/TextMesh Pro`


## Unity Registry & Other Git Dependencies

These packages are noted in the json files and as such should be imported automatically when running the project for the first time:

* ProBuilder - installed via Unity Registry  
* ProGrids - installed via Git `com.unity.progrids`  


## Known Issues

If the player controller appears pink, please check the three materials on Player/Armature_Mesh. Each of them should be set to Standard.
