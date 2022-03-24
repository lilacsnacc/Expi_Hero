using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AbilityBasic : Ability
{
    [Header("Ability Basic Stats")]
    public Transform CharacterPos;
    public float Offset;
    protected override void Awake()
    {
        base.Awake();

        Assert.IsNotNull(CharacterPos, "The basic ability needs reference the character's position");

        SetTransform();
    }

    void Update()
    {
        SetTransform();
    }

    void SetTransform()
    {
        transform.rotation = Quaternion.Euler(0, CharacterPos.eulerAngles.y, 0);
        transform.position = CharacterPos.position + (CharacterPos.forward * Offset) + new Vector3(0,0.4f,0);
    }
}
