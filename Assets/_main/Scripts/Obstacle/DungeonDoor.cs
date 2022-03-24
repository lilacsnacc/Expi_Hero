using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDoor : MonoBehaviour
{
    public enum DoorOpenDirection { up, down, left, right };
    public DoorOpenDirection DirectionToOpen = DoorOpenDirection.up;
    public bool DefaultClosed;

    private bool isOpen;
    private Vector3 closedPos;
    private Vector3 openedPos;

    private float lerpSecDuration = 1f;

    private Coroutine doorRoutine;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 up = Vector3.zero;
        Vector3 right = Vector3.zero;

        switch(DirectionToOpen)
        {
            case DoorOpenDirection.up:
                up = transform.up * transform.localScale.y;
                break;
            case DoorOpenDirection.down:
                up = transform.up * transform.localScale.y * -1;
                break;
            case DoorOpenDirection.left:
                right = transform.right * transform.localScale.x * -1;
                break;
            case DoorOpenDirection.right:
                right = transform.right * transform.localScale.x;
                break;
        }

        closedPos = transform.position;
        openedPos = transform.position + up + right;

        if (!DefaultClosed) MoveToOpened();
    }

    public void MoveToOpened()
    {
        if (doorRoutine != null) StopCoroutine(doorRoutine);

        isOpen = true;
        transform.position = closedPos;

        doorRoutine = StartCoroutine(MoveToPos(openedPos));
    }

    public void MoveToClosed()
    {
        if(doorRoutine != null) StopCoroutine(doorRoutine);

        isOpen = false;
        transform.position = openedPos;

        doorRoutine = StartCoroutine(MoveToPos(closedPos));
    }

    public bool IsOpen() {
        return isOpen;
    }

    private IEnumerator MoveToPos(Vector3 newPos)
    {
        Vector3 initialPos = transform.position;

        for(float i = 0; i < lerpSecDuration; i+=Time.deltaTime)
        {
            transform.position = Vector3.Lerp(initialPos, newPos, i / lerpSecDuration);

            yield return new WaitForEndOfFrame();
        }

        transform.position = newPos;
    }
}
