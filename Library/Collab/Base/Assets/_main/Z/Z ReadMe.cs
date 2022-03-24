/*
Our current scene structure is too chaotic.
We need to reduce, refactor, and simplify.
This is my attempt.

Done:
    Created Z folder to sync everything
    Created ReadMe.cs
    Created Title Screen scene
    Re-Imported player character model
    Re-Imported hubworld props
    Finished campfire asset with flames and particles
    Player character sit standardized
        Player character sits for Title Screen
    Finished Title Screen / Title Options UI
    Added LeanTween for subtle and performant UI animation
    Found and added button-select, button-enter, and game-enter sounds
    Both sounds and UI animations are triggered when they should be
    Options screen mute button fully functional
    Quit functionality works in Player and in Editor
    Added fire sound to campfire prefab
    Added background music audio to menu manager
    Fixed environmental lighting {ambient(50, 25, 100), fog(175, 25, 75), fogDensity: 0.05}
    Complete hub word decoration
        Added a tree to basemodel
        Added new tent to basemodel
        Added punching dummy to basemodel
        Decorated hub world with trees and rocks
        Created tree and rocks prefabs
        Turned full hub world into a prefab
    Created Hub World Scene
    Disabled player Game controls when in a menu
    Loaded Hub Scene in Title Scene
    Opened Hub World scene from Title Scene (OnButtonStartGame)
    --- Title Screen scene is 100% done ---
Todo:
    Finish Skill Menu (https://www.youtube.com/watch?v=lUun2xW6FJ4)
    fix all UI for different screen sizes
    Add the player abilities that have been created
    Make the dummies attackable
    Fix scaling issues with UI
    Readd dungeon door with prompt
    Add dungeon door entry animation
    Readd sphere with prompt
Later:
    use LoadSceneAsync to have scenes ready before needed
    fix shadows, which are blurring and sharpening in an ugly way based on proximity to camera
    have footsteps play when foot is down, rather than from hardcoded values
    Get cursor to work with menus (possible lead: Player Input Component > UI Input Module)
        Following that, add a custom cursor
    Add volume slider and background music mute to options
    Make interaction based off colliders/triggers instead of hardcoded transforms and distances
    Make everything within the Hub World PREFAB use the prefab versions of themselves
        (land, tent, campfire, woodbench, dummy, rock, tree)
    TitleScene and Hub Scene can potentially be merged
        (just consider interaction when going from Dungeon Scenes back to Title/Hub Scene)

*/