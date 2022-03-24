using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/*
 * This is where stuff specific for the enemy will be stored
 */
public class Enemy : Character
{
    [Header("Enemy Details")]
    public Room PatrolRoom;
    public int expiToGiveOnDeath = 1;
    // Start is called before the first frame update
    new private void Start()
    {
        base.Start();

        opposingLayer = LayerMask.NameToLayer("Player Ability");
    }

    // Update is called once per frame
    new private void Update()
    {
        base.Update();
    }
}
