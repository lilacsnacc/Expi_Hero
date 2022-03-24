using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinAttack : Ability
{
    [Header("Spin Attack")]
    public GameObject abilitySub;
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
            this.gameObject.transform.position = abilityUserPos.position;
            this.gameObject.transform.rotation = Quaternion.Euler(0, this.gameObject.transform.rotation.eulerAngles.y + (rotationSpeed * Time.deltaTime), 0);
        }
    }


    protected override IEnumerator AbilityUsed()
    {
        OnCooldown = true;
        this.gameObject.transform.position = abilityUserPos.position;
        this.gameObject.transform.rotation = abilityUserPos.rotation;
        abilitySub.GetComponent<Ability>().UseAbility();
        startRotating = true;
        //Ability is over
        yield return new WaitForSeconds(AbilityDuration);
        startRotating = false;
        //Ability cooldown is over
        yield return new WaitForSeconds(AbilityCooldown);
        OnCooldown = false;
    }
}
