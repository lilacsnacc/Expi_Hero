using UnityEngine;
using UnityEngine.UI;

public class SkillHUDButton : MonoBehaviour {
  public AbilityZ ability;
  public Image backgroundImageElement;
  public Image greyoutImage;
  public Slider cooldownSlider;

  void FixedUpdate() {
    if (ability && ability.getCooldownRemaining() > 0) {
      cooldownSlider.value = ability.getCooldownRemaining() / ability.cooldown;
      greyoutImage.CrossFadeAlpha(0.75f, 0, true);
    } else if (greyoutImage.color.a > 0.1f) {
      greyoutImage.CrossFadeAlpha(0, 0, true);
      cooldownSlider.value = 0;
    }
  }

  public void SetAbility(AbilityZ newAbility) {
    ability = newAbility;
    backgroundImageElement.sprite = ability.abilityImage;
  }
}
