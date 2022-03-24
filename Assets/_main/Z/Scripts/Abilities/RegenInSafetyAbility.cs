using UnityEngine;

public class RegenInSafetyAbility : AbilityZ {
  LTDescr healDelay;
  bool healing = false;

  public override void FixedUpdate() {
    base.FixedUpdate();

    if (myCharacter.GetNormalizedHealth() >= 1) return;

    if (cooldownRemaining > 0 && castTimeRemaining > 0) {
      if (healing)
        myCharacter.AffectHealth(myCharacter.GetHealthStat() * Time.deltaTime);
    } else {
      castTimeRemaining = castTime;
      healing = !healing;

      if (healing) {
        // release a puff of healing particles here
        // or like tween the mesh material to glow green
      }
    }
  }

  public override void Stop() {
    base.Stop();

    cooldownRemaining = 0;
    healing = false;
  }

  void OnDamage() {
    Stop();

    LeanTween.cancel(gameObject);
    LeanTween.value(gameObject, 0, 1, 5 / myCharacter.GetSpeedStat()).setOnComplete(() => Trigger(true));
  }
}
