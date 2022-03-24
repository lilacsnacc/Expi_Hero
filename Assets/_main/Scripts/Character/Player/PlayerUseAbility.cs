using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

public class PlayerUseAbility : MonoBehaviour
{
    private AbilityLoadout _abilityLoadout;

    private int abilityRequestNum;
    private bool abilityRequested;

    [Header("Ability indexes")]
    public int BasicIndex = 0;
    public int AbilityOneIndex = 1;
    public int AbilityTwoIndex = 2;
    public int AbilityThreeIndex = 3;
    public int AbilityFourIndex = 4;

    

    // Start is called before the first frame update
    void Start()
    {
        _abilityLoadout = GetComponent<AbilityLoadout>();

        Assert.IsNotNull(_abilityLoadout, "Player needs an ability loadout to choose from");

        abilityRequested = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!abilityRequested) return;
        _abilityLoadout.UseAbility(abilityRequestNum);
        abilityRequested = false;
    }

    //Look for a call to the slash button.
    public void OnSlash(InputValue value)
    {
        if(value.Get<float>() != 0)
        {
            //The index 
            abilityRequestNum = BasicIndex;
            abilityRequested = true;
        }
    }

    public void OnAbility1(InputValue value)
    {
        if (value.Get<float>() != 0)
        {
            abilityRequestNum = AbilityOneIndex;
            abilityRequested = true;
        }
    }

    public void OnAbility2(InputValue value)
    {
        if (value.Get<float>() != 0)
        {
            abilityRequestNum = AbilityTwoIndex;
            abilityRequested = true;
        }
    }

    public void OnAbility3(InputValue value)
    {
        if (value.Get<float>() != 0)
        {
            abilityRequestNum = AbilityThreeIndex;
            abilityRequested = true;
        }
    }

    public void OnAbility4(InputValue value)
    {
        if (value.Get<float>() != 0)
        {
            abilityRequestNum = AbilityFourIndex;
            abilityRequested = true;
        }
    }
}
