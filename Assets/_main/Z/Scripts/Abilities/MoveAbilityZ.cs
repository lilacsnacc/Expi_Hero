using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class MoveAbilityZ : AbilityZ {
  public AudioSource stepSound;
  public float turnSmoothTime = 0.05f;

  float turnSmoothSpeed;
  float angle;
  int stepCount;

  public override void FixedUpdate() {
    base.FixedUpdate();

    if (!isActive && !myCharacter.abilityManager.RequestTrigger(this)) return;

    Vector3 moveDirection = myCharacter.GetMoveDirection();
    float sqrMoveMagnitude = moveDirection.sqrMagnitude;

    if (sqrMoveMagnitude > 0) {
      float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
      angle = Mathf.SmoothDampAngle(myCharacter.transform.eulerAngles.y, targetAngle, ref turnSmoothSpeed, turnSmoothTime);
      myCharacter.transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    if (myCharacter.controller && myCharacter.controller.enabled)
      myCharacter.controller.SimpleMove(moveDirection * myCharacter.GetSpeedStat());

    if (myCharacter.animator?.GetBool("IsMoving") != sqrMoveMagnitude > 0)
      myCharacter.animator.SetBool("IsMoving", sqrMoveMagnitude > 0);

    playStepSoundOnImpact();
  }

  void playStepSoundOnImpact() {
    if (!myCharacter.animator || !stepSound) return;

    AnimatorStateInfo currentStateInfo = myCharacter.animator.GetCurrentAnimatorStateInfo(0);

    if (!currentStateInfo.IsName("Run")) return;

    float stepAnimTime = currentStateInfo.normalizedTime % 1;

    // hardcoded footstep timing: [0.3f, 0.8f]
    if ((stepCount == 0 && stepAnimTime > 0.3f) || (stepCount == 1 && stepAnimTime > 0.8f)) {
      stepSound.pitch = 0.5f + (Random.value - 0.5f) * 0.2f;
      stepSound.Play();
      stepCount++;
    } else if (stepCount > 1 && stepAnimTime < 0.8f)
      stepCount = 0;
  }

  public override void Stop() {
    base.Stop();
    myCharacter.animator.SetBool("IsMoving", false);
  }

  public override bool Trigger(bool isPermitted) {
    if (base.Trigger(isPermitted)) {
      /* make a puff of air particles*/
      return true;
    }

    return false;
  }

  public void OnMovement(InputValue value) {
    Vector2 inputDir = value.Get<Vector2>();
    myCharacter.SetMoveDirection(new Vector3(inputDir.x, 0, inputDir.y));

    Move();
  }

  public void Move() {
    myCharacter.abilityManager.RequestTrigger(this);
  }
}
