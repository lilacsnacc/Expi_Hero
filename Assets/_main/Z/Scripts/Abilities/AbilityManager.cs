using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour {
  static List<AbilityZ> abilityPrefabArray;
  
  public CharacterZ myCharacter;

  public Action<AbilityZ> onAbilityGain;

  List<AbilityZ> myAbilities = new List<AbilityZ>();

  void Awake() {
    if (abilityPrefabArray is null)
      abilityPrefabArray = new List<AbilityZ>(Resources.LoadAll<AbilityZ>("AbilityPrefabs"));
    
    myAbilities.AddRange(GetComponentsInChildren<AbilityZ>());
    myCharacter = GetComponent<CharacterZ>();
  }

  void Add(params AbilityZ[] abilityArr) {
    foreach (AbilityZ newAbility in abilityArr)
      if (this.Has(newAbility))
        Debug.Log(newAbility + " already exists on " + name);
      else {
        AbilityZ newlyAddedAblity = Instantiate(newAbility, transform);
        myAbilities.Add(newlyAddedAblity);
        onAbilityGain.Invoke(newlyAddedAblity);
      }
  }

  public bool Has(AbilityZ anAbility) {
    return myAbilities.Exists(ability => ability.Is(anAbility));
  }

  public bool CanAfford(AbilityZ anAbility) {
    return CanAfford(anAbility.expiCost);
  }

  public bool CanAfford(int expiCost) {
    return expiCost <= myCharacter.GetExpi();
  }

  public void Purchase(AbilityZ newAbility) {
    if (!this.Has(newAbility) && this.CanAfford(newAbility)) {
      myCharacter.AffectExpi(-newAbility.expiCost);
      Add(newAbility);
    }
  }

  public bool RequestTrigger(AbilityZ desiredAbility) {
    List<AbilityZ> stopThese = new List<AbilityZ>();

    foreach (AbilityZ ability in myAbilities) {
      bool overridesNothing = desiredAbility.overrides == AbilityZ.TriggerRequirements.Nothing || ability.overrides == AbilityZ.TriggerRequirements.Nothing;
      bool overridesAll = desiredAbility.overrides == AbilityZ.TriggerRequirements.Everything || ability.overrides == AbilityZ.TriggerRequirements.Everything;
      bool overridesSame = desiredAbility.overrides != AbilityZ.TriggerRequirements.Nothing && desiredAbility.overrides == ability.overrides;

      if (!ability.Is(desiredAbility) && !overridesNothing && ability.IsActive() && (overridesAll || overridesSame))
        if (desiredAbility.priority > ability.priority)
          stopThese.Add(ability);
        else
          return desiredAbility.Trigger(false);
    }

    foreach (AbilityZ ability in stopThese)
      if (!ability.Is(desiredAbility))
        ability.Stop();

    return desiredAbility.Trigger(true);
  }

  public void PlusOneStats(bool Health, bool Strength, bool Speed) {
    AffectStats(Health ? 1 : 0, Strength ? 1 : 0, Speed ? 1 : 0);
  }

  public void AffectStats(float Health, float Strength, float Speed) {
    myCharacter.AffectStats(Health, Strength, Speed);
  }

  public List<AbilityZ> GetCharacterAbilities() {
    return myAbilities;
  }

  public static List<AbilityZ> GetAbilities() {
    return abilityPrefabArray;
  }

  public void StopAll() {
    myAbilities.ForEach(ability => {
      ability.Stop();
      ability.isHeldDown = false;
    });
  }
}
