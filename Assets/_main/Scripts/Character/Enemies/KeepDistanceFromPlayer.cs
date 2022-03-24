using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class KeepDistanceFromPlayer : EnemyAbility
{
    [Header("Keep Distance Properties")]
    public float DistanceToMaintain = 5f;
    public float Speed = 5f;

    [Header("Weighted Values")]
    public float AvoidWeight = 5f;
    public float ObstacleWeight = 2f;
    public float RoomBorderWeight = 7f;
    //Room border weight not implemented yet        HACK

    private float distSqr;

    private Vector3 movementVector;

    private CharacterController controller;

    protected override void AbilityActive()
    {
        movementVector = Vector3.zero;

        if (SqrDistanceToPlayer() < distSqr)
        {
            KeepDistanceFrom(PatrolRoom.Player.position);
            Move();
        }
    }

    protected override void AbilityInactive()
    {
        //Do nothing
    }

    // Start is called before the first frame update
    void Start()
    {
        distSqr = DistanceToMaintain * DistanceToMaintain;
        movementVector = Vector3.zero;

        controller = GetComponent<CharacterController>();

        Assert.IsNotNull(PatrolRoom, "Enemy needs a room to patrol");
    }

    void KeepDistanceFrom(Vector3 target)
    {
        Vector3 awayPointer = transform.position - target;

        awayPointer.y = 0;

        awayPointer = awayPointer.normalized * AvoidWeight;

        Vector3 obstaclePointer = Vector3.zero;
        for (int i = 0; i < PatrolRoom.RoomObstacles.Length; i++)
        {
            obstaclePointer += (transform.position - PatrolRoom.RoomObstacles[i].position);
        }
        obstaclePointer.y = 0;
        obstaclePointer = obstaclePointer.normalized * ObstacleWeight;

        movementVector += (awayPointer + obstaclePointer);
    }

    void Move()
    {
        controller.SimpleMove(movementVector.normalized * Speed);
    }
}
