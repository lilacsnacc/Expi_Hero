using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Teleport : EnemyAbility
{
    [Header("Teleport Stuff")]
    public float TeleportDelay = 3.5f;
    public float RoomWallBuffer = 2f;

    private bool onCooldown;

    private float minZ;
    private float maxZ;
    private float minX;
    private float maxX;

    private CharacterController controller;
    private Animator animator;

    protected override void AbilityActive()
    {
        if(!onCooldown)
        {
            Vector3 posToTeleport = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
            StartCoroutine(TeleportTo(posToTeleport));
        }
    }

    private IEnumerator TeleportTo(Vector3 newPos)
    {
        onCooldown = true;
        Vector3 movementPos = newPos - transform.position;
        animator.SetTrigger("Teleport");

        controller.Move(movementPos);

        GetComponent<Health>().Damage(20);

        yield return new WaitForSeconds(TeleportDelay);
        onCooldown = false;
    }

    protected override void AbilityInactive()
    {
        if(!InOriginalPos())
            TeleportTo(originPos);
    }

    // Start is called before the first frame update
    void Start()
    {
        maxX = PatrolRoom.transform.position.x + (PatrolRoom.GetWidth() / 2) - RoomWallBuffer;
        minX = PatrolRoom.transform.position.x - (PatrolRoom.GetWidth() / 2) + RoomWallBuffer;
        maxZ = PatrolRoom.transform.position.z + (PatrolRoom.GetHeight() / 2) - RoomWallBuffer;
        minZ = PatrolRoom.transform.position.z - (PatrolRoom.GetHeight() / 2) + RoomWallBuffer;

        onCooldown = false;

        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        AssertionCheck();
    }

    void AssertionCheck()
    {
        Assert.IsNotNull(controller, "Enemy needs a character controller to use teleport");
        Assert.IsNotNull(animator, "Enemy needs an animator to use teleport");
    }
}
