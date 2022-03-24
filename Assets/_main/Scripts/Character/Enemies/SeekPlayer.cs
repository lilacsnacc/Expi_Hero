using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SeekPlayer : EnemyAbility
{
    [Header("Movement Stats")]
    public float Speed = 0f;
    public float MinDistanceFromPlayer = 0f;

    [Header("Weighted Values")]
    public float PlayerWeight = 5f;
    public float ObstacleWeight = 2f;

    private CharacterController controller;
    private Vector3 movementVector;
    private float distSqr;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        Assert.IsNotNull(PatrolRoom, "Enemy needs a room to patrol");

        distSqr = MinDistanceFromPlayer * MinDistanceFromPlayer;
    } 

    private void Move()
    {
        controller.SimpleMove(movementVector.normalized * Speed);
    }

    void Seek(Vector3 target)
    {
        Vector3 playerPointer = target - transform.position;

        playerPointer.y = 0;

        playerPointer = playerPointer.normalized * PlayerWeight;

        Vector3 obstaclePointer = Vector3.zero;
        for(int i = 0; i < PatrolRoom.RoomObstacles.Length; i++)
        {
            obstaclePointer += (transform.position - PatrolRoom.RoomObstacles[i].position);
        }
        obstaclePointer.y = 0;
        obstaclePointer = obstaclePointer.normalized * ObstacleWeight;

        movementVector += (playerPointer + obstaclePointer);
    }

    protected override void AbilityActive()
    {
        movementVector = Vector3.zero;

        if(SqrDistanceToPlayer() >= distSqr)
        {
            Seek(PatrolRoom.Player.position);
            Move();
        }
    }

    protected override void AbilityInactive()
    {
        if (InOriginalPos()) return;

        //Don't know why this aint working but I'm not gonna figure it out yet
        if ((originPos - transform.position).sqrMagnitude < 1f)
        {
            Seek(originPos);
            Move();
        }
        else
        {
            transform.position = originPos;
        }
    }
}
