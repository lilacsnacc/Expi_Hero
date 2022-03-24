using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpgradeButton : MonoBehaviour {
  public Action<SkillUpgradeButton> onClick;
  public Text skillNameText;
  public Text skillCostText;

  public StatEnum improvesStat;

  public enum StatEnum {
    None = 0,
    Health,
    Strength,
    Speed,
  }

  AbilityZ myAbility;
  string skillName = "Skill Name";
  int skillCost = 0;

  public AbilityZ GetAbility() {
    return myAbility;
  }

  public void SetAbility(AbilityZ ability, bool strikeThrough = false) {
    myAbility = ability;
    SetName(ability.abilityName, strikeThrough);
    SetCost(ability.expiCost, strikeThrough);
  }

  public void SetName(string newName, bool strikeThrough = false) {
    skillName = newName;

    if (strikeThrough) {
      string endText = "";

      foreach (char c in skillName)
        endText = endText + c + '\u0336';

      skillNameText.text = endText;
    } else
      skillNameText.text = skillName;

    skillNameText.color = strikeThrough ? Color.gray : Color.white;
  }

  public int GetCost() {
    return skillCost;
  }

  public void SetCost(int newCost, bool strikeThrough = false) {
    skillCost = newCost;
    skillCostText.text = skillCost + " ✦ ";
    skillCostText.color = strikeThrough ? Color.gray : Color.white;
  }

  public void OnClick() {
    onClick.Invoke(this);
  }
}
