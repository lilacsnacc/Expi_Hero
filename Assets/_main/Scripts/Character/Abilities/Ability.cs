using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : MonoBehaviour
{
    [Header("Instantiation")]
    public bool IsDetached;

    [Header("Ability Default Stats")]
    public int Damage = 0;
    public int damageBase = 1;
    public float Knockback = 0f;
    public float AbilityDuration = 0.2f;
    public float AbilityCooldown = 0.5f;

    private MeshRenderer _rendered;
    private Collider _collider;

    [Header("UI")]
    public Sprite AbilityImage;

    [HideInInspector]
    public bool OnCooldown;

    public delegate void AbilityInUse();
    public AbilityInUse OnAbilityInUse;


    protected virtual void Awake()
    {
        if (IsDetached) transform.parent = null;

        _rendered = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();

        ChangeCollisionActive(false);

        OnCooldown = false;

    }

    public int GetDamage()
    {
        return Damage;
    }

    public float GetKnockback()
    {
        return Knockback;
    }

    public virtual void ChangeCollisionActive(bool activate)
    {
        _rendered.enabled = activate;
        _collider.enabled = activate;
    }

    //Override this for abilities that require more than just a coroutine to operate
    public virtual void UseAbility()
    {
        OnAbilityInUse?.Invoke();
        StartCoroutine(AbilityUsed());
    }

    //Override this for abilities that only require a change in the way the coroutine works
            //**If overridden do not forget to alter cooldown boolean
    protected virtual IEnumerator AbilityUsed()
    {
        OnCooldown = true;
        ChangeCollisionActive(true);

        //Ability is over
        yield return new WaitForSeconds(AbilityDuration);
        ChangeCollisionActive(false);

        //Ability cooldown is over
        yield return new WaitForSeconds(AbilityCooldown);
        OnCooldown = false;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {

    }
}
