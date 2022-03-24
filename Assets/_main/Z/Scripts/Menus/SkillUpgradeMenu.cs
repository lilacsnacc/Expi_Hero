using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillUpgradeMenu : BaseMenu {
  public SkillUpgradeButton skillButtonPrefab;
  public Transform skillList;
  public Transform skillStatsUpgradeContainer;
  public Text expiText;
  public Text messageText;

  AbilityManager characterAbilityManager;

  public override LTDescr Open(EventSystem eventSystem) {
    base.Open(eventSystem);

    if (!characterAbilityManager)
      characterAbilityManager = GameManager.gameManagerInstance.GetCharacter().abilityManager;

    expiText.text = characterAbilityManager.myCharacter.GetExpi() + " ✦ ";

    foreach (Transform child in skillList)
      if (child != skillStatsUpgradeContainer)
        Destroy(child.gameObject);

    foreach (Transform child in skillStatsUpgradeContainer) {
      SkillUpgradeButton statButton = child.GetComponent<SkillUpgradeButton>();

      if (statButton.improvesStat == SkillUpgradeButton.StatEnum.Health)
        statButton.SetCost((int)characterAbilityManager.myCharacter.GetHealthStat());
      else if (statButton.improvesStat == SkillUpgradeButton.StatEnum.Strength)
        statButton.SetCost((int)characterAbilityManager.myCharacter.GetStrengthStat());
      else if (statButton.improvesStat == SkillUpgradeButton.StatEnum.Speed)
        statButton.SetCost((int)characterAbilityManager.myCharacter.GetSpeedStat());

      statButton.onClick += AttemptPurchase;
    }

    AbilityManager.GetAbilities().ForEach(ability => {
      if (ability.isBuyable) {
        SkillUpgradeButton newButton = Instantiate(skillButtonPrefab, skillList);

        newButton.SetAbility(ability, characterAbilityManager.Has(ability));
        newButton.onClick += AttemptPurchase;
      }
    });

    LTDescrList[0].setOnComplete(() => eventSystem.SetSelectedGameObject(skillStatsUpgradeContainer.GetChild(0).gameObject));

    return LTDescrList[0];
  }

  public override LTDescr Close() {
    base.Close();

    return LTDescrList[LTDescrList.Count - 1].setOnComplete(() => gameObject.SetActive(false));
  }
  
  public void AttemptPurchase(SkillUpgradeButton button) {
    if (button.improvesStat > 0) {
      if (!characterAbilityManager.CanAfford(button.GetCost()))
        SetMessage("you need more expi for that", false);
      else {
        characterAbilityManager.PlusOneStats(
          button.improvesStat == SkillUpgradeButton.StatEnum.Health,
          button.improvesStat == SkillUpgradeButton.StatEnum.Strength,
          button.improvesStat == SkillUpgradeButton.StatEnum.Speed);

        characterAbilityManager.myCharacter.AffectExpi(-button.GetCost());
        expiText.text = characterAbilityManager.myCharacter.GetExpi() + " ✦ ";

        button.SetCost(button.GetCost() + 1);
      }

      return;
    }

    AbilityZ ability = button.GetAbility();

    if (characterAbilityManager.Has(ability))
      SetMessage("you already have ✦  " + ability.abilityName + " ✦ ");
    else if (!characterAbilityManager.CanAfford(ability))
      SetMessage("you need more expi for that", false);
    else {
      characterAbilityManager.Purchase(ability);

      button.SetAbility(ability, true);
      expiText.text = characterAbilityManager.myCharacter.GetExpi() + " ✦ ";
      SetMessage("New Skill Acquired!! ✦  " + ability.abilityName + " ✦ ");
    }
  }

  public void SetMessage(string newMessage, bool isOkay = true) {
    messageText.text = newMessage;
    messageText.color = isOkay ? Color.white : Color.red;

    LeanTween.cancel(messageText.gameObject);
    LeanTween.alphaText(messageText.rectTransform, 0, 1).setDelay(2);
  }
}
