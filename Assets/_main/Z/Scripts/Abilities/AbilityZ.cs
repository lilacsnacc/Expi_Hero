using UnityEngine;


public class AbilityZ : MonoBehaviour {
  public string abilityName = "Ability Name Here";
  public Sprite abilityImage;

  public TriggerRequirements overrides = TriggerRequirements.Nothing;
  public int priority = 0;

  public float castTime = 0;
  public float cooldown = 0;

  public int expiCost = 0;
  public bool isBuyable = false;

  public bool isHeldDown = false;

  protected CharacterZ myCharacter;
  protected float castTimeRemaining = 0;
  protected float cooldownRemaining = 0;
  protected bool isActive = false;

  public virtual void Awake() {
    myCharacter = transform.parent.GetComponent<CharacterZ>();
  }

  public virtual void FixedUpdate() {
    if (cooldownRemaining > 0)
      cooldownRemaining -= Time.deltaTime;

    if (!isActive) return;
    
    if (castTimeRemaining > 0)
      castTimeRemaining -= Time.deltaTime;
    else
      Stop();
  }

  public enum TriggerRequirements {
    Nothing = 0,
    LowerBody = 1,
    UpperBody = 2,
    Everything = 4
  }

  public virtual bool Trigger(bool isPermitted) {
    if (isPermitted && cooldownRemaining <= 0) {
      castTimeRemaining = castTime;
      cooldownRemaining = cooldown;
      return isActive = true;
    }

    return false;
  }

  public virtual void Stop() {
    castTimeRemaining = 0;
    isActive = false;
  }

  public CharacterZ GetCharacter() {
    return myCharacter;
  }

  public float getCooldownRemaining() {
    return cooldownRemaining;
  }

  public bool IsActive() {
    return isActive;
  }

  public bool IsReady() {
    return cooldownRemaining <= 0;
  }

  public bool Is(AbilityZ otherAbility) {
    return GetType() == otherAbility.GetType();
  }
}
