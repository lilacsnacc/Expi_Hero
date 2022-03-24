using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class CooldownAbilityLoader : MonoBehaviour
{
    public int Index;

    private AbilityLoadout playerAbilities;
    private Image abilityImage;

    private void Awake()
    {
        playerAbilities = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityLoadout>();
        abilityImage = GetComponentInChildren<Image>();

        AssertionChecks();

        Sprite i = playerAbilities.Abilities[Index].AbilityImage;
        if (i) abilityImage.sprite = i;
    }

    private void AssertionChecks()
    {
        Assert.IsNotNull(playerAbilities, "CooldownAbility " + Index + " cannot get reference to player's ability loadout");
        Assert.IsNotNull(abilityImage, "CooldownAbility " + Index + " cannot find image");
        Assert.IsTrue(Index > 0 && Index <= playerAbilities.Abilities.Length, "Index for CooldownAbility is out of range");
    }
}
