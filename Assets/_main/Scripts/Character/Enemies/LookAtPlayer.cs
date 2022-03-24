using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : EnemyAbility
{
    [Header("Face Player Stuff")]
    public bool BackTowardsPlayer;
    protected override void AbilityActive()
    {
        if (BackTowardsPlayer) FacePlayer(-1);
        else FacePlayer(1);
    }

    protected override void AbilityInactive()
    {

    }

    void FacePlayer(float direction)
    {
        Vector3 playerPointer = (PatrolRoom.Player.position - transform.position).normalized * direction;
        playerPointer.y = transform.position.y;
        transform.forward = playerPointer;
    }
}
