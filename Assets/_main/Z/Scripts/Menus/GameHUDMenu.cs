using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameHUDMenu : BaseMenu {
  public SkillHUDButton SkillHUDButtonPrefab;
  public Transform skillListContainer;
  public Text expiText;
  public Text messageText;

  AbilityManager characterAbilityManager;

  public override LTDescr Open(EventSystem eventSystem) {
    SkipTween();
    gameObject.SetActive(true);

    LTDescrList.Add(LeanTween.alphaCanvas(canvasGroup, 1, 0.1f));

    if (!characterAbilityManager) {
      characterAbilityManager = GameManager.gameManagerInstance.GetCharacter().abilityManager;

      characterAbilityManager.GetCharacterAbilities().ForEach(ability => {
        if (ability.abilityImage)
          AddAbility(ability);
      });

      expiText.text = characterAbilityManager.myCharacter.GetExpi() + " ✦ ";

      characterAbilityManager.myCharacter.onExpiChange += UpdateExpiText;
      characterAbilityManager.onAbilityGain += AddAbility;
    }

    return LTDescrList[0];
  }

  public void SetMessage(string newMessage, bool isOkay = true) {
    messageText.text = newMessage;
    messageText.color = isOkay ? Color.white : Color.red;

    LeanTween.cancel(messageText.gameObject);
    LeanTween.alphaText(messageText.rectTransform, 0, 1).setDelay(2);
  }

  void AddAbility(AbilityZ ability) {
    SkillHUDButton abilityButton = Instantiate(SkillHUDButtonPrefab, skillListContainer);

    abilityButton.SetAbility(ability);
  }

  void UpdateExpiText(float expiDifference) {
    float countTime = 1;

    LeanTween.cancel(expiText.gameObject);
    LeanTween.cancel(expiText.rectTransform);

    expiText.rectTransform.localScale *= 1.2f;
    expiText.color = Color.cyan;

    LeanTween.scale(expiText.rectTransform, Vector3.one, countTime);
    LeanTween.value(
        expiText.gameObject,
        characterAbilityManager.myCharacter.GetExpi() - expiDifference,
        characterAbilityManager.myCharacter.GetExpi(), countTime)
          .setOnUpdate((float val) => expiText.text = (int)val + " ✦ ");
    LeanTween.colorText(expiText.rectTransform, Color.white, countTime);
  }

  void OnDestroy() {
    if (characterAbilityManager) {
      characterAbilityManager.myCharacter.onExpiChange -= UpdateExpiText;
      characterAbilityManager.onAbilityGain -= AddAbility;
    }
  }
}
