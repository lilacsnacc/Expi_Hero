public class EndGameOnDeathAbility : AbilityZ {
  void OnHealthChange(float normalizedHealth) {
    GameManager.gameManagerInstance.ButtonQuit();
  }
}
