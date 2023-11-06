# drawing-studying

This repository is a Test task of mini-game that teaches kids to draw symbols (letters/numbers or shapes).

What was made:

There is 2 scenes: Menu and Game scene.
Scene 1. Menu.
On the first scene there are level icons that contain information (SO named "MiniGameContent") about the level Type, draw color and symbol to draw that will be used in the Game scene to provide game content. 
After level become chosen - information about trace symbol type, color to draw is just become placed in SO DataContainer called "LevelConfig" which aggregate all the level information in one place   
and  allows to us in future easely swap or extend it with any kind of save system. 

Scene 2. Game.
This scene on the Start load the LevelConfig and place relevant symbol to draw. Drawing process is handled by "LineGenerator" and "SpellChecker" entities. I decided to implement this feature with physics based approach.
So, our SpellChecker is a component that placed on the TraceSymbol prefab. It contains "GuideLine"s (which consist of "GuidePoint"s with colliders) and when LineGenerator update "Line" (with EdgeCollider) it checks if there are collisions whith awaited GuidePoint.
If there are not collision, or collision is not happen at first with awaited GuidePoint, after player release mouse button (or touch is finished) line will be deleted. 
In such a way, there is a guaranty that for succesfull passing of the level, kid, should precisely pass through all the GuidePoints on the path.
After the first one GuideLine finished, another one shows on the the similar rules. If player inactive for 7 seconds, start message with level goal repeats. After 14 seconds with lack of activity, "HintPresenter" entity shows "Hint" hand which go through the all GuidePoints on the current active GuideLine.
After the las GuideLine complete and motivation phrase fired, level reloads with next MiniGameContent in order settings.

For more comfortable new TraceSymbol creation, there is some Gizmo visualisations of GuideLines and its order in Unity Editor.

NOTES:
- Because the task scope is to check competencies of employee, and because of small project size and different thoughts about usage of external plugins and frameworks as Extenject or DOTWeen, this project does not contain it.
But in my opinion, if we want to extend this project or it's content - using of some kind of DependencyInjection is a good decision. As well as more interactive animations, more animations with some unified approach of it's creation.
- In the task materials there were not Background sprite for the menu scene, so I made similar one on my own, so final visualisation of background is a little bit different from the refference.
- Test task require project to be compatible with Iphone 12 and Ipad4. I don't have MAC OS, so I built and test it on Android/Windows and with built in device Simulator.
- Some statements in test task, as "loading of Trace symbol or loading of level icons" was a little bit unclear and confusing. So, as this elements is created dynamicaly, I understood it as loading, but for TraceSymbol was added some FadeOut effect.
