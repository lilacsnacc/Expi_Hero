using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandingRangedAttack : Ability
{
    // Start is called before the first frame update
    [Header("Ability Basic Stats")]
    public Transform position;
    public float distanceFromPlayer;
    public float attackSpeed;
    public float expansionSpeed;
    public float originalScale;

    private bool attacking = false;
    private Vector3 attackDirection;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (attacking == true)
        {
            transform.position = transform.position + (attackDirection * attackSpeed * Time.deltaTime);
            transform.localScale = new Vector3(transform.localScale.x + (expansionSpeed * Time.deltaTime), transform.localScale.y, transform.localScale.z + (expansionSpeed *Time.deltaTime));
        }
    }

    public override void ChangeCollisionActive(bool activate)
    {
        if (activate)
        {
            transform.rotation = Quaternion.Euler(0, position.eulerAngles.y, 0);
            attackDirection = position.forward;
            transform.localScale = new Vector3(originalScale, transform.localScale.y, originalScale);
            transform.position = position.position + (position.forward * distanceFromPlayer);
        }
        base.ChangeCollisionActive(activate);
        attacking = activate;
    }
}
