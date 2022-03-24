using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralingRangedAttack : Ability
{
    [Header("Spiraling Ranged Attack")]
    public GameObject[] rangedAttack;
    public Transform abilityUserPos;
    public float rotationSpeed;
    private bool startRotating = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (startRotating)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, this.gameObject.transform.rotation.eulerAngles.y + (rotationSpeed * Time.deltaTime), 0);
        }
    }
    public override void ChangeCollisionActive(bool activate)
    {
        return;
    }


    protected override IEnumerator AbilityUsed()
    {
        OnCooldown = true;
        
        this.gameObject.transform.position = abilityUserPos.position;
        this.gameObject.transform.rotation = abilityUserPos.rotation;
        for(int i = 0; i < rangedAttack.Length; i++)
        {

            rangedAttack[i].GetComponent<RangedAttack2>().ChangeForward(this.gameObject.transform.rotation.eulerAngles.y + (60 * i));
            rangedAttack[i].GetComponent<RangedAttack2>().UseAbility();
        }
        startRotating = true;
        //Ability is over
        yield return new WaitForSeconds(AbilityDuration);
        startRotating = false;

        //Ability cooldown is over
        yield return new WaitForSeconds(AbilityCooldown);
        OnCooldown = false;
    }
}
