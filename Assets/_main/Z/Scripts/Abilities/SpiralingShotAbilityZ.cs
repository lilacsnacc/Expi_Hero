using UnityEngine;
using UnityEngine.InputSystem;

public class SpiralingShotAbilityZ : AbilityZ {
  public StabbyBoi sword;
  public AudioSource attackSound;

  public int swordCount = 5;

  Quaternion lastShotDirection;
  bool attacking;

  public void Start() {
    sword.gameObject.SetActive(false);
  }

  public override bool Trigger(bool isPermitted) {
    if (base.Trigger(isPermitted)) {
      myCharacter.animator.SetTrigger("Shoot");

      attackSound.Play();

      int swordsCreated = 0;

      while(swordsCreated++ < swordCount) {
        sword.speed = myCharacter.GetSpeedStat() * 2;
        sword.damage = myCharacter.GetStrengthStat() * 0.05f;
        sword.timeToLive = cooldown * 0.5f;
        sword.turnSpeed = sword.timeToLive * 90;
        
        StabbyBoi newSword = Instantiate(sword, sword.transform.position, sword.transform.rotation);
        newSword.transform.Rotate(0, swordsCreated * 360f / swordCount, 0);

        newSword.gameObject.SetActive(true);
      }

      return true;
    }

    return false;
  }


  public void OnAbility2(InputValue value) {
    if ((attacking = value.Get<float>() != 0) && !isActive)
      myCharacter.abilityManager.RequestTrigger(this);
  }
}
