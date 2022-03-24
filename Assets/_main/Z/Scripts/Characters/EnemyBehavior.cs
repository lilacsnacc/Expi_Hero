using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBehavior : MonoBehaviour {
  public Action onDeath;
  public DodgeAbilityZ dodgeAbility;
  public MoveAbilityZ moveAbility;
  public SlashAbilityZ slashAbility;
  public float attackRange = 7.5f;
  public int sideDodgeChance = 10;
  public int stutterChance = 5;

  protected int opposingLayer = 0;

  CharacterZ currentTarget;
  CharacterZ myCharacter;
  float stutterTime;
  bool mustDodge;

  void Start() {
    myCharacter = GetComponent<CharacterZ>();
    
    if (myCharacter) {
      myCharacter.SetStats(3, 4, 5);
      myCharacter.onDeath += OnDeath;
    }

    if (!currentTarget)
      currentTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterZ>();

    stutterTime = Random.Range(0, 2f);

    opposingLayer = LayerMask.NameToLayer("Player Ability");
  }

  void Update() {
    if (!myCharacter || myCharacter.GetNormalizedHealth() <= 0 || ((stutterTime -= Time.deltaTime) > 0)) return;

    if (currentTarget && currentTarget.GetNormalizedHealth() > 0) {
      Vector3 targetPosition = currentTarget.transform.position;
      Vector3 directionVector = new Vector3(targetPosition.x, transform.position.y, targetPosition.z) - transform.position;

      if (!dodgeAbility || !dodgeAbility.IsActive())
        transform.LookAt(transform.position + directionVector);

      if (dodgeAbility && dodgeAbility.IsReady() && (mustDodge || directionVector.sqrMagnitude > attackRange * 2)) {
        if (mustDodge) {
          directionVector = RandomSign() * transform.right;
          transform.LookAt(transform.position + directionVector);
          myCharacter?.SetMoveDirection(directionVector.normalized);
          mustDodge = false;
        }

        RollRandomVariables();
        dodgeAbility.Dodge();
      } else if (moveAbility && moveAbility.IsReady() && directionVector.sqrMagnitude > attackRange) {
        myCharacter?.SetMoveDirection(directionVector.normalized);
        moveAbility.Move();
        RollRandomVariables();
      } else if (slashAbility && slashAbility.IsReady() && directionVector.sqrMagnitude < attackRange) {
        myCharacter?.SetMoveDirection(Vector3.zero);
        RollRandomVariables();
        slashAbility?.Attack();
      }
    } else if (dodgeAbility && dodgeAbility.IsReady()) {
      RollRandomVariables();
      myCharacter?.SetMoveDirection(Vector3.zero);
      transform.Rotate(0, UnityEngine.Random.Range(-120, 120), 0);
      dodgeAbility.Dodge();
    }
  }

  void RollRandomVariables() {
    if (Random.Range(0, 100) < stutterChance)
      stutterTime = Random.Range(0, 1f);
    if (Random.Range(0, 100) < sideDodgeChance)
      mustDodge = true;
  }

  void OnDeath() {
    myCharacter.SetMoveDirection(Vector3.zero);
    myCharacter.TeleportTo(Vector3.one * -1000);

    if (currentTarget)
      currentTarget.AffectExpi(myCharacter.GetExpi());

    onDeath.Invoke();
  }

  void SetCurrentTarget(CharacterZ character = null) {
    currentTarget = character;
  }

  int RandomSign(int positiveChance = 50) {
    return Random.Range(0, 100) > positiveChance ? -1 : 1;
  }
}
