using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
  public static GameManager gameManagerInstance;

  // Thanks to KilluaBoy (Sleep Talking) and FoolBoyMedia (SkyLoop) of freeesound.org for the bgms
  public AudioSource hubBGM;
  public AudioSource dungeonBGM;

  public AudioSource symbolCrash;

  public OpenSkillsInteractable openSkillsInteractable;

  public Text promptText;

  public BaseMenu titleScreenMenu;
  public BaseMenu gameHUDMenu;
  public BaseMenu gamePauseMenu;
  public BaseMenu gameSkillMenu;
  public BaseMenu blackScreenMenu;

  public EventSystem eventSystem;

  public Texture2D cursorTexture;

  BaseMenu currentMenu;
  CharacterZ playerCharacter;
  PlayerInput playerInput;
  Interactable currentInteractable;

  // thanks to zeid87 of StackOverflow for the snippet! <3
  void Awake() {
    if (gameManagerInstance == null)
      gameManagerInstance = this;
    else
      Destroy(gameObject);

    DontDestroyOnLoad(transform.gameObject);

    Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    SceneManager.sceneLoaded += OnSceneLoaded;
  }

  void OnSceneLoaded(Scene currScene, LoadSceneMode aMode) {
    if (!playerCharacter) {
      playerCharacter = PlayerBehavior.playerInstance.myCharacter;
      playerInput = playerCharacter.GetComponent<PlayerInput>();
      playerCharacter.onDeath += () => GoToScene("Hub Scene");
    }

    if (playerCharacter.GetNormalizedHealth() < 1)
      playerCharacter.AffectHealth(9999);

    if (dungeonBGM) dungeonBGM.Stop();
    if (hubBGM) hubBGM.Stop();

    if (currScene.name.Contains("Title")) {
      openSkillsInteractable.Interact(playerCharacter);
      playerInput.enabled = false;

      symbolCrash.pitch = -1;
      symbolCrash.time = 1.4f;
      symbolCrash.Play();

      SetMenu(blackScreenMenu);

      gameSkillMenu.SkipTween();

      SetMenu(titleScreenMenu).setOnComplete(() => {
        symbolCrash.pitch = 0.5f;
        symbolCrash.time = 0;
        symbolCrash.Play();
        hubBGM.Play();
      });
    } else if (currScene.name.Contains("Hub") || currScene.name.Contains("Dungeon")) {
      SetMenu(blackScreenMenu);
      SetMenu(gameHUDMenu);

      playerCharacter.ResetCamera();
      playerCharacter.AffectHealth(9999);

      AudioSource src = (currScene.name.Contains("Hub") ? hubBGM : dungeonBGM);
      if (src) src.Play();        //HACK sometimes could not find audio source
    }
  }

  LTDescr SetMenu(BaseMenu newMenu = null) {
    currentMenu?.Close();
    eventSystem.SetSelectedGameObject(null);

    currentMenu = newMenu;

    if (playerCharacter.controller.enabled = playerInput.enabled = (!currentMenu || currentMenu == gameHUDMenu)) {
      playerCharacter.animator.SetTrigger("Idle");
      playerCharacter.ResetCamera();
    } else
      playerCharacter.abilityManager.StopAll();

    if (currentMenu)    //HACK Breaks when calling certain menus. Says object was already destroyed
      return currentMenu?.Open(eventSystem);
      
    return null;
  }

  public CharacterZ GetCharacter() {
    return playerCharacter;
  }

  public void SetPrompt(string newPrompt = "") {
    promptText.text = newPrompt;
  }

  public void GoToScene(string sceneName) {
    SetMenu(blackScreenMenu).setOnComplete(() => SceneManager.LoadScene(sceneName));
  }

  public void OnEscape(InputValue value) {
    if (value.Get<float>() != 0) {
      if (!currentMenu || currentMenu == gameHUDMenu)
        SetMenu(gamePauseMenu);
      else if (SceneManager.GetActiveScene().name.Contains("Title"))
        SetMenu(titleScreenMenu);
      else if (currentMenu.escapeable)
        SetMenu(gameHUDMenu);
    }
  }

  public void OnSkip() {
    currentMenu?.SkipTween();
  }

  public void OpenSkillMenu() {
    SetMenu(gameSkillMenu);
  }

  public void ButtonTitleScreen() {
    SetMenu(titleScreenMenu);
  }

  public void ButtonStartGame() {
    GoToScene("Hub Scene");
  }

  public void ButtonPauseGame() {
    SetMenu(gamePauseMenu);
  }

  public void ButtonResumeGame() {
    SetMenu(gameHUDMenu);
  }

  public void ButtonToggleAudio(bool isEnabled) {
    AudioListener.pause = !isEnabled;
    AudioListener.volume = isEnabled ? 1 : 0;
  }

  public void ButtonToggleBGM(bool isEnabled) {
    hubBGM.mute = dungeonBGM.mute = !isEnabled;
  }

  public void ButtonQuit() {
    SetMenu(blackScreenMenu).setOnComplete(() => {

      #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
      #endif

      Application.Quit();
    });
  }
}
