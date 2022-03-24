using UnityEngine;
using UnityEngine.InputSystem;

public class SlashAbilityZ : AbilityZ {
  public StabbyBoi sword;
  public AudioSource attackSound;

  int swingNum = 0;

  public void Start() {
    sword.gameObject.SetActive(false);
  }

  public override void FixedUpdate() {
    base.FixedUpdate();

    if (!isActive && !(cooldownRemaining <= 0 && isHeldDown && myCharacter.abilityManager.RequestTrigger(this))) return;
  }

  void LateUpdate() {
    if (cooldownRemaining > 0 && isHeldDown) {
      Ray ray = myCharacter.cameraController.GetCamera().ScreenPointToRay(Mouse.current.position.ReadValue());
      Plane plane = new Plane(Vector3.up, Vector3.zero);
      float distance;

      if (plane.Raycast(ray, out distance)) {
        Vector3 target = ray.GetPoint(distance);
        Vector3 direction = target - myCharacter.transform.position;
        float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        myCharacter.transform.rotation = Quaternion.Euler(0, rotation, 0);
      }
    }
  }

  public override void Stop() {
    base.Stop();
    sword.gameObject.SetActive(false);
  }

  public override bool Trigger(bool isPermitted) {
    if (base.Trigger(isPermitted)) {
      myCharacter.animator.SetTrigger("Swing" + (swingNum++ % 2));

      attackSound.pitch = 1.6f + (Random.value > 0.4f ? 0 : 0.2f);
      attackSound.Play();

      LeanTween.rotateLocal(gameObject, new Vector3(0, (swingNum % 2) == 1 ? -170 : -10, 0), castTime * 0.75f);

      sword.damage = myCharacter.GetStrengthStat();
      sword.gameObject.SetActive(true);

      return true;
    }

    return false;
  }

  public void OnPrimary(InputValue value) {
    if (isHeldDown = value.Get<float>() != 0)
      Attack();
  }

  public void Attack() {
    if (!isActive)
      myCharacter.abilityManager.RequestTrigger(this);
  }
}
