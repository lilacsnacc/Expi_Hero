using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbility : MonoBehaviour
{
    [Header("Enemy Ability Basics")]
    public bool AbilityDisabled;

    protected Room PatrolRoom;

    protected Vector3 originPos;

    private void Awake()
    {
        PatrolRoom = GetComponent<Enemy>().PatrolRoom;
        originPos = transform.position;
    }

    private void Update()
    {
        if (AbilityDisabled || PatrolRoom == null) return;

        if (PatrolRoom.Player != null)
        {
            AbilityActive();
        }
        else
        {
            AbilityInactive();
        }
    }

    protected abstract void AbilityActive();
    protected abstract void AbilityInactive();

    protected float SqrDistanceToPlayer()
    {
        return Vector3.SqrMagnitude(PatrolRoom.Player.position - transform.position);
    }

    protected bool InOriginalPos()
    {
        return transform.position == originPos;
    }
}
