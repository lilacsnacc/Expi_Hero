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
    fix shadows, which were blurring and sharpening in an ugly way
    --- 03/11/2020 (just started using this format)
    Make everything within the Hub World PREFAB use the prefab versions of themselves
        (land, tent, campfire, woodbench, dummy, rock, tree)
    fix all UI for different screen sizes
    Re-add dungeon door with prompt
    Add dungeon door entry animation
    Fix scaling issues with UI
    --- 05/11/2020
    Make the dummies attackable
    Make interaction based off colliders/triggers instead of hardcoded transforms and distances
    Re-add sphere with prompt
    Add quickheal / regen ability (like for the hubworld dummies)
    --- 06/11/2020
    Finish Skill Menu (https://www.youtube.com/watch?v=lUun2xW6FJ4)
    Add the player abilities that have been created
    Use tags / layers for Damaging script instead of "proccer.character"
    Got cursor to work with menus
    opening the skill menu would immediately push the first skill button
      fixed for now by waiting for animation to complete before setting button select
     
Todo:
    Add a custom cursor
    PlayerBehavior and EnemyBehavior should inherit CharacterZ
    adjust hubworld land model to make walls steeper
    Add volume slider and background music mute to options
    CharacterZ and AbilityManager are never apart except for Skill Menu, so we can merge them
    TitleScene and Hub Scene can potentially be merged
        (just consider interaction when going from Dungeon Scenes back to Title/Hub Scene)
    fix camera jumping bug; occurs after opening skill menu a few times
    stopping play while the skill menu is open causes Unity Editor to crash
    make movement relative to view, so camera can be rotated with awkward player controls
    Remove as many instances of BroadcastMessage as possible in favor of directly linked Actions
    Have all abilities be calleable without InputValue param (see difference between Slash and Shoot)
Later:
    use LoadSceneAsync to have scenes ready before needed
    have footsteps play when foot is down, rather than at hardcoded animation timestamps
    we might be able to get one-line functions to take up a single line with () => 

*/