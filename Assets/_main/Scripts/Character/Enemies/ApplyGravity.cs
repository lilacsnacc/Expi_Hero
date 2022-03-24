using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ApplyGravity : EnemyAbility
{
    CharacterController controller;

    protected override void AbilityActive()
    {
        controller.SimpleMove(Vector3.zero);
    }

    protected override void AbilityInactive()
    {
        controller.SimpleMove(Vector3.zero);
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        Assert.IsNotNull(controller, "Enemy needs a character controller");
    }
}
