using UnityEngine;
using UnityEngine.InputSystem;

public class DodgeAbilityZ : AbilityZ {
  public float distance;

  Vector3 dodgeDirection;

  public override bool Trigger(bool isPermitted) {
    if (base.Trigger(isPermitted)) {
      /* make a puff of air particles*/
      /* play some kind of dodge sound here */
      myCharacter.animator?.SetBool("IsDodging", true);
      return true;
    }

    return false;
  }

  public override void FixedUpdate() {
    base.FixedUpdate();

    if (!isActive) return;

    myCharacter.controller?.SimpleMove(dodgeDirection * myCharacter.GetSpeedStat() * 3);
  }

  public override void Stop() {
    base.Stop();
    myCharacter.animator?.SetBool("IsDodging", false);
  }

  public void OnDodge(InputValue value) {
    if (value.Get<float>() != 0)
      Dodge();
  }

  public void Dodge() {
    dodgeDirection = myCharacter.GetMoveDirection().sqrMagnitude > 0 ? myCharacter.GetMoveDirection() : myCharacter.transform.forward;

    if (!isActive)
      myCharacter.abilityManager.RequestTrigger(this);
  }
}
