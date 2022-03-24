using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AbilityLoadout))]
public class SlashPlayer : EnemyAbility
{
    [Header("Slash Player Stuff")]
    public int AttackIndex = 0;
    public float MaxDistanceFromPlayer = 1f;
    public float DelayOnAttack = 0.3f;

    private AbilityLoadout _loadout;
    private float maxDistSqr;

    protected override void AbilityActive()
    {
        if (SqrDistanceToPlayer() <= maxDistSqr && _loadout.IsAbilityReady(AttackIndex))
        {
            StartCoroutine(AttackPlayer());
        }
    }

    protected override void AbilityInactive()
    {

    }

    private IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(DelayOnAttack);
        _loadout.UseAbility(AttackIndex);
    }


    // Start is called before the first frame update
    void Start()
    {
        _loadout = GetComponent<AbilityLoadout>();
        maxDistSqr = MaxDistanceFromPlayer * MaxDistanceFromPlayer;
    }
}
