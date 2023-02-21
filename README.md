# Re:Dream

A 3D platforming game with procedural generation elements.


## Dependencies To Install Via The Asset Store

This project uses the following assets:

* [Starter Assets - Third Person Character Controller](https://assetstore.unity.com/packages/essentials/starter-assets-third-person-character-controller-196526) by Unity Technologies. Note: The function `JumpAndGravity` on ThirdPersonController.cs has been modified slightly so as to be able to override it.

* TextMesh Pro by Unity Technologies


## Unity Registry & Other Git Dependencies

These packages are noted in the json files and as such should be imported automatically when running the project for the first time:

* NavMeshComponents - installed via Git `com.unity.ai.navigation`  
* ProBuilder - installed via Unity Registry  
* ProGrids - installed via Git `com.unity.progrids`  


## Known Issues

If the player controller appears pink, please check the three materials on Player/Armature_Mesh. Each of them should be set to Standard.
