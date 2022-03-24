using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/*
 * This is the script that all Characters will be based on. Every enemy, NPC, and player should be a subset of this class
 */
//Ensures Characters have the required components
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CharacterController))]
public abstract class Character : MonoBehaviour
{
    [Header("Character Stats")]
    public float mass = 1f;

    protected Health cHealth;
    protected CharacterController controller;

    protected int opposingLayer = 0;
    private bool knockbackApplied;

    // Start is called before the first frame update
    protected void Start()
    {
        cHealth = GetComponent<Health>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if(cHealth.IsDead())
        {
            OnDeath();
        }
    }

    //Default death method, override for personal effects
    private void OnDeath()
    {
        if(this.gameObject.tag == "Enemy")
        {
            CharacterStats stats = GameObject.FindGameObjectWithTag("Player")?.GetComponent<CharacterStats>();
            
            if (stats)
              stats.Expi += this.gameObject.GetComponent<Enemy>().expiToGiveOnDeath;
            else
              GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterZ>()?.AffectExpi(this.gameObject.GetComponent<Enemy>().expiToGiveOnDeath);
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == opposingLayer)
        {
            Debug.Log("does this happen");
            Ability c_ability = other.transform.GetComponent<Ability>();
            Assert.IsNotNull(c_ability, "Object on layer " + opposingLayer + " needs an Ability script");

            cHealth.Damage(c_ability.GetDamage());
            if(!knockbackApplied) StartCoroutine(ApplyKnockback(c_ability.GetKnockback(), other.transform.position));
        }
    }

    private IEnumerator ApplyKnockback(float knockback, Vector3 origin)
    {
        knockbackApplied = true;

        Vector3 direction = transform.position - origin;
        direction.y = 0;
        direction.Normalize();

        float lerpThreshold = 0.001f;

        //lowers the knockback applied each frame and applies
        for (float i = knockback; i > lerpThreshold; i = Mathf.Lerp(i, 0, 0.25f))
        {
            Vector3 appliedForce = direction * i / mass;
            controller.SimpleMove(appliedForce);
            yield return new WaitForEndOfFrame();
        }

        knockbackApplied = false;
    }
}
