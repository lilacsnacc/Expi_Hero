using System.Collections.Generic;
using UnityEngine;

public class StabbyBoi : MonoBehaviour {
  public AbilityZ proccer;
  public float speed = 1;
  public float damage = 1;
  public float turnSpeed = 30;
  public float timeToLive = 1;
  public bool shouldPreventRecollision = true;

  CharacterZ myCharacter;
  List<CharacterZ> hitList;
  float lifeTime = 0;

  void Start() {
    if (!proccer)
      proccer = GetComponentInParent<AbilityZ>();

    if (proccer)
      myCharacter = proccer.GetCharacter();

    hitList = new List<CharacterZ>();
  }

  void Update() {
    if (speed != 0)
      transform.position += transform.forward * speed * Time.deltaTime;

    if (turnSpeed != 0)
      transform.Rotate(0, turnSpeed * Time.deltaTime, 0);

    if (timeToLive > 0) {
      lifeTime += Time.deltaTime;

      if (lifeTime >= timeToLive)
        Destroy(gameObject);
    }
  }

  void OnDisable() {
    hitList?.Clear();
  }

  void OnTriggerStay(Collider other) {
    CharacterZ otherCharacter = other.GetComponent<CharacterZ>();
    Health otherCharacterL = other.GetComponent<Health>();

    if ((!myCharacter || !otherCharacter || otherCharacter.gameObject.tag == myCharacter.gameObject.tag || hitList.Contains(otherCharacter)) && !otherCharacterL) return;

    if (otherCharacter) {
      if (shouldPreventRecollision)
        hitList.Add(otherCharacter);

      otherCharacter.AffectHealth(-damage);
    } else
      otherCharacterL.Damage((int)damage * 4);
  }
}
