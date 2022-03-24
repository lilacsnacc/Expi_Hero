using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : Ability
{
    // Start is called before the first frame update
    [Header("Ranged Attack Stats")]
    public Transform position;
    public float distanceFromPlayer;
    public float attackSpeed;

    private bool attacking = false;
    private Vector3 attackDirection;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(attacking == true)
        {
            transform.position = transform.position + (attackDirection * attackSpeed * Time.deltaTime);
        }
    }

    public override void ChangeCollisionActive(bool activate)
    {
        if (activate)
        {
            transform.rotation = Quaternion.Euler(0, position.eulerAngles.y, 0);
            attackDirection = position.forward;
            transform.position = position.position + (position.forward * distanceFromPlayer) + new Vector3(0f,.5f,0f);
        }
        base.ChangeCollisionActive(activate);
        attacking = activate;
    }
}
