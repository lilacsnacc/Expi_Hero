using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This is where Health is controlled. All characters will need health references for combat to work
 * Alternatively, other entities might have health, such as breakable walls or crates
 */
public class Health : MonoBehaviour
{
    [Header("Character Health")]
    public float MaxHealth = 100;
    public bool IsInvincible = false;

    public float currentHealth;
    private bool isDead = false;

    public Renderer GameMesh;
    private Color normalColor;
    public Color flashColor = Color.red;
    public int TimesToFlash = 3;

    public delegate void OnHitDelegate();
    public OnHitDelegate OnHit;

    public delegate void OnDeathDelegate();
    public OnDeathDelegate OnDeath;

    public delegate void OnHealDelegate();
    public OnHealDelegate OnHeal;


    [Header("Animation")]
    //Invincibility frames after getting hit with an attack. Leave at 0 if there should not be any.
    public float IFrames = 0;

    // Start is called before the first frame update
    void Start()
    {
        CharacterStats cStats = GetComponent<CharacterStats>();
        normalColor = GameMesh.material.color;

        if (!cStats) return;

        MaxHealth = cStats.healthBase + (cStats.healthGainPerPoint * cStats.Vit);
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Heals based on parameter
    public void Heal(int health)
    {
        currentHealth += health;
        OnHeal?.Invoke();

        if (currentHealth > MaxHealth)
        {
            currentHealth = MaxHealth;
        }
    }

    public void FullHeal()
    {
        currentHealth = MaxHealth;
        OnHeal?.Invoke();
    }

    //Damages based on parameter
    public void Damage(int dmg)
    {
        if (IsInvincible) return;

        currentHealth -= dmg;
        OnHit?.Invoke();
        
        //If player is still alive
        if(currentHealth > 0)
        {
            StartCoroutine(HitStun());
            return;
        }
        else
        {
            currentHealth = 0;
            isDead = true;

            OnDeath?.Invoke();
        }
        
    }

    //Enumerator for taking hit stun. Keeps player invincible until number of frames is reached
    //Coroutine ensures player can still take other actions while for loop is running
    private IEnumerator HitStun()
    {
        IsInvincible = true;
        GameMesh.material.color = flashColor;
        for (int i = 1; i <= TimesToFlash; i++)
        {
            GameMesh.material.color = flashColor;
            yield return new WaitForSeconds(0.05f);
            GameMesh.material.color = normalColor;
            yield return new WaitForSeconds(0.05f);
        }

        GameMesh.material.color = normalColor;
        IsInvincible = false;
        Debug.Log(IsInvincible + " does this happen");
    }

    public bool IsDead()
    {
        return isDead;
    }
}
