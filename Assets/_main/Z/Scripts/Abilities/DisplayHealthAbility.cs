using UnityEngine;
using UnityEngine.UI;

public class DisplayHealthAbility : AbilityZ {
  public CanvasGroup canvasGroup;
  public CanvasGroup damageGroup;
  public Slider healthSlider;
  public Slider damageSlider;

  public void Start() {
    transform.position = transform.parent.position + Vector3.up * 0.2f;
    canvasGroup.alpha = 0;
  }

  public override bool Trigger(bool isPermitted) {
    if (base.Trigger(isPermitted)) {
      LeanTween.cancel(canvasGroup.gameObject);
      canvasGroup.alpha = damageGroup.alpha = 1;
      return true;
    }

    return false;
  }

  public override void FixedUpdate() {
    base.FixedUpdate();
    transform.LookAt(transform.position + Vector3.up);

    if (castTimeRemaining > 0)
      damageSlider.value += (healthSlider.value - damageSlider.value) * 4 * Time.deltaTime * castTime;
  }

  public override void Stop() {
    base.Stop();
    canvasGroup.LeanAlpha(1 - healthSlider.value, 1).setDelay(cooldown);
    damageGroup.alpha = 0;
  }

  void OnHealthChange(float normalizedHealth) {
    healthSlider.value = normalizedHealth;
    cooldownRemaining = 0;
    
    Trigger(true);
  }
}
