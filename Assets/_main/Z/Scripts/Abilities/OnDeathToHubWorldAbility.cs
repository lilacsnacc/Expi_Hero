public class OnDeathToHubWorldAbility : AbilityZ {
  void OnHealthChange(float normalizedHealth) {
    if (normalizedHealth <= 0) {
      GameManager.gameManagerInstance.GoToScene("Hub Scene");
    }
  }
}
