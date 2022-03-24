using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayer : EnemyAbility
{
    [Header("Shooting Stuff")]
    public int ProjectileIndex = 0;
    public float DelayOnAttack = 0.3f;

    private AbilityLoadout _loadout;


    // Start is called before the first frame update
    void Start()
    {
        _loadout = GetComponent<AbilityLoadout>();
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(DelayOnAttack);
        _loadout.UseAbility(ProjectileIndex);
    }

    protected override void AbilityActive()
    {
        if(_loadout.IsAbilityReady(ProjectileIndex))
        {
            StartCoroutine(Shoot());
        }
    }

    protected override void AbilityInactive()
    {
        
    }
}
