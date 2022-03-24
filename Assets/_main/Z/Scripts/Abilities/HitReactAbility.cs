using UnityEngine;

public class HitReactAbility : AbilityZ {
  public Color flashColor = new Color(255, 0, 0);
  public int timesToflash = 3;
  public bool shouldShake;
  public float shakeStrength = 10;

  LTDescr shakeTween;
  Material material;
  Color originalColor;
  float lastDamageAmt;
  int flashCount = 0;
  bool colorToggle;

  public void Start() {
    material = myCharacter.GetComponentInChildren<Renderer>().material;
    originalColor = material.color;
  }

  public override bool Trigger(bool isPermitted) {
    if (base.Trigger(isPermitted)) {
      // thanks to dentedpixel for a majority of this shaking code <3 <3 <3
      if (shouldShake) {
        float dropOffTime = castTime * 2;

        if (shakeTween == null) {
          float shakePeriodTime = 0.2f;

          shakeTween = LeanTween.rotateAroundLocal(myCharacter.gameObject, Vector3.right, shakeStrength, shakePeriodTime)
              .setEase(LeanTweenType.easeShake) // this is a special ease that is good for shaking
              .setLoopClamp()
              .setRepeat(-1);
        }

        LeanTween.cancel(gameObject);

        // Slow the shake down to zero
        LeanTween.value(gameObject, shakeStrength * lastDamageAmt, 0f, dropOffTime)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnUpdate(
              (float val) => {
                shakeTween.setTo(Vector3.right * val);
              });
      }

      return true;
    }

    return false;
  }

  public override void FixedUpdate() {
    base.FixedUpdate();

    if (!isActive) return;

    if (1 - flashCount / (2.0f * timesToflash) > castTimeRemaining / castTime) {
      material.color = (colorToggle = !colorToggle) ? originalColor : flashColor;
      flashCount++;
    }
  }

  public override void Stop() {
    base.Stop();
    
    if (material)
      material.color = originalColor;

    flashCount = 0;
    colorToggle = false;
  }

  void OnDamage(float damageAmt) {
    cooldownRemaining = 0;
    lastDamageAmt = damageAmt;
    Trigger(true);
  }
}
