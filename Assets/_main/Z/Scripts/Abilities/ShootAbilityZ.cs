using UnityEngine;
using UnityEngine.InputSystem;

public class ShootAbilityZ : AbilityZ {
  public StabbyBoi sword;
  public AudioSource attackSound;

  Quaternion lastShotDirection;

  public void Start() {
    sword.gameObject.SetActive(false);
  }

  public override void FixedUpdate() {
    base.FixedUpdate();

    if (!isActive && !(cooldownRemaining <= 0 && isHeldDown && myCharacter.abilityManager.RequestTrigger(this))) return;
  }

  void LateUpdate() {
    if (castTimeRemaining > 0)
      myCharacter.transform.rotation = lastShotDirection;
  }

  public override bool Trigger(bool isPermitted) {
    if (base.Trigger(isPermitted)) {
      myCharacter.animator.SetTrigger("Shoot");

      attackSound.Play();

      Ray ray = myCharacter.cameraController.GetCamera().ScreenPointToRay(Mouse.current.position.ReadValue());
      Plane plane = new Plane(Vector3.up, Vector3.zero);
      float distance;

      if (plane.Raycast(ray, out distance)) {
        Vector3 target = ray.GetPoint(distance);
        Vector3 direction = target - myCharacter.transform.position;
        float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        lastShotDirection = Quaternion.Euler(0, rotation, 0);
        myCharacter.transform.rotation = lastShotDirection;
      }

      sword.speed = myCharacter.GetSpeedStat() * 2;
      sword.damage = myCharacter.GetStrengthStat() * 0.25f;
      Instantiate(sword, sword.transform.position, sword.transform.rotation).gameObject.SetActive(true);

      return true;
    }

    return false;
  }


  public void OnSecondary(InputValue value) {
    if ((isHeldDown = value.Get<float>() != 0) && !isActive)
      myCharacter.abilityManager.RequestTrigger(this);
  }
}
