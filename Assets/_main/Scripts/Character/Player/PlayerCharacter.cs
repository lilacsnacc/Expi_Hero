using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * This is where stuff personal to the player character will be
 */
public class PlayerCharacter : Character
{
    public Health healthOfPlayer;
    public Image healthImage;
    public List<float> cooldownArray;
    public Image[] coolDownImage;
    // Start is called before the first frame update
    new private void Start()
    {
        base.Start();

        opposingLayer = LayerMask.NameToLayer("Enemy Ability");
    }

    // Update is called once per frame
    new private void Update()
    {
        if(healthImage) healthImage.fillAmount = healthOfPlayer.currentHealth / healthOfPlayer.MaxHealth;
        base.Update();
    }

    protected virtual IEnumerator CoolDown(int i)
    {
        float timer = 0.0f;
        while(timer < cooldownArray[i])
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            coolDownImage[i].fillAmount = timer / cooldownArray[i];
        }
        Debug.Log("Timer: " + timer + " cooldown: " + cooldownArray[i]);
        coolDownImage[i].fillAmount = 1;
        coolDownImage[i].color = Color.white;
    }

    private void AbilityOnCooldown(int i)
    {
        Debug.Log(i);
        coolDownImage[i].fillAmount = 0;
        coolDownImage[i].color = Color.gray;
        StartCoroutine(CoolDown(i));
    }

    public void Ability1OnCooldown()
    {
        AbilityOnCooldown(0);
    }

    public void Ability2OnCooldown()
    {
        AbilityOnCooldown(1);
    }

    public void Ability3OnCooldown()
    {
        AbilityOnCooldown(2);
    }

    public void Ability4OnCooldown()
    {
        AbilityOnCooldown(3);
    }
}
