using UnityEngine;
using UnityEngine.InputSystem;

public class LoomingShotAbilityZ : AbilityZ {
  public StabbyBoi sword;
  public AudioSource attackSound;

  Quaternion lastShotDirection;
  bool attacking;

  public void Start() {
    sword.gameObject.SetActive(false);
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

      sword.speed = myCharacter.GetSpeedStat() * 0.5f;
      sword.damage = 0;
      sword.timeToLive = cooldown * 0.5f;
      
      StabbyBoi newSword = Instantiate(sword, sword.transform.position, sword.transform.rotation);

      newSword.gameObject.SetActive(true);
      LeanTween.scale(newSword.gameObject, new Vector3(5, 1, 5), newSword.timeToLive);
      LeanTween.value(newSword.gameObject, 0, 0.05f, newSword.timeToLive).setOnUpdate((float val) => newSword.damage = myCharacter.GetStrengthStat() * val);

      return true;
    }

    return false;
  }


  public void OnAbility1(InputValue value) {
    if ((attacking = value.Get<float>() != 0) && !isActive)
      myCharacter.abilityManager.RequestTrigger(this);
  }
}
