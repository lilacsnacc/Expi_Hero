using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEnemies : EnemyAbility
{
    private bool fullyHealed;
    private Coroutine healingRoutine;

    [Header("Healing Properties")]
    public float HealInterval = 0.5f;
    public int HealAmount = 1;

    [Header("Animation")]
    public GameObject HealAnimationPrefab;
    public float AnimationDuration = 0.75f;

    protected override void AbilityActive()
    {
        fullyHealed = false;

        if(healingRoutine == null)
        {
            healingRoutine = StartCoroutine(HealLoop());
        }
    }

    protected override void AbilityInactive()
    {
        if(!fullyHealed)
        {
            StopCoroutine(healingRoutine);
            healingRoutine = null;

            for(int i = 0; i < PatrolRoom.Enemies.Count; i++)
            {
                PatrolRoom.Enemies[i].GetComponent<Health>().Heal(1000);
            }

            fullyHealed = true;
        }
    }

    private IEnumerator HealLoop()
    {
        while(true)
        {
            yield return new WaitForSeconds(HealInterval);

            for (int i = 0; i < PatrolRoom.Enemies.Count; i++)
            {
                if (PatrolRoom.Enemies[i].GetComponent<HealEnemies>()) continue;

                PatrolRoom.Enemies[i].GetComponent<Health>()?.Heal(HealAmount);
                StartCoroutine(HealAnimation(PatrolRoom.Enemies[i].transform.position));
            }
        }
    }

    private IEnumerator HealAnimation(Vector3 healPos)
    {
        GameObject newAnimation = Instantiate(HealAnimationPrefab, healPos, Quaternion.identity);

        yield return new WaitForSeconds(AnimationDuration);

        Destroy(newAnimation);
    }

    // Start is called before the first frame update
    void Start()
    {
        fullyHealed = true;
    }
}
