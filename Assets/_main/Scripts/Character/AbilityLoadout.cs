using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityLoadout : MonoBehaviour
{
    [Header("Array of abilities")]
    public Ability[] Abilities;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement pMove = GetComponent<PlayerMovement>();
        PlayerCharacter pChar = GetComponent<PlayerCharacter>();
        if(pMove)
        {
            CharacterStats cStats = GetComponent<CharacterStats>();
            for (int i = 0; i < Abilities.Length; i++)
            {
                Abilities[i].OnAbilityInUse += pMove.AttackingMethod;
                Abilities[i].Damage = cStats.Str + Abilities[i].damageBase;
                if (i == 1)
                {
                    Abilities[i].OnAbilityInUse += pChar.Ability1OnCooldown;
                    pChar.cooldownArray.Add(Abilities[i].AbilityDuration + Abilities[i].AbilityCooldown);
                    
                }
                else if (i == 2)
                {
                    Abilities[i].OnAbilityInUse += pChar.Ability2OnCooldown;
                    pChar.cooldownArray.Add(Abilities[i].AbilityDuration + Abilities[i].AbilityCooldown);
                }
                else if (i == 3)
                {
                    Abilities[i].OnAbilityInUse += pChar.Ability3OnCooldown;
                    pChar.cooldownArray.Add(Abilities[i].AbilityDuration + Abilities[i].AbilityCooldown);
                }
                else if (i == 4)
                {
                    Abilities[i].OnAbilityInUse += pChar.Ability4OnCooldown;
                    pChar.cooldownArray.Add(Abilities[i].AbilityDuration + Abilities[i].AbilityCooldown);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Check the index to make sure it is in bounds. Should be called every time an ability is trying to be accessed
    private void IndexCheck(int index)
    {
        if (index < 0 || index > Abilities.Length)
        {
            Debug.LogWarning("Ability at index " + index + " does not exist, but is attempting to be accessed.");
            return;
        }
    }

    /// <summary>
    /// Change the ability at the given index with the new ability provided
    /// </summary>
    /// <param name="index">Index of the old ability</param>
    /// <param name="newAbility">The new ability to give the player</param>
    public void ChangeAbility(int index, Ability newAbility)
    {
        IndexCheck(index);

        Destroy(Abilities[index].gameObject);
        Abilities[index] = newAbility;
    }

    /// <summary>
    /// Use the ability at the given index, if not on cooldown
    /// </summary>
    /// <param name="index">The index of the ability to use</param>
    public void UseAbility(int index)
    {
        IndexCheck(index);

        if (!Abilities[index].OnCooldown)
        {
            Abilities[index].UseAbility();
        }
    }

    public bool IsAbilityReady(int index)
    {
        return !Abilities[index].OnCooldown;
    }
}
