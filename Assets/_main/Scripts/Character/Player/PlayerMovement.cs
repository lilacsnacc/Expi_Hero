using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

/// <summary>
/// Handles the player's movement and inputs related to movement
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    private Animator animator;

    [Header("Player Stats")]
    public GameObject playerModel;
    public float Speed = 3f;
    public float dodgeSpeed = 40f;
    public Vector3 gravity = new Vector3(0, -25.0f, 0);
    public float turnSmoothTime = 0.05f;
    public float dodgeMax = 0.2f;
    public float angle = 0.0f;


    //private variables
    private CharacterController controller;
    private Vector3 moveDirection;
    private Vector3 dodgeDirection;
    private float turnSmoothSpeed;
    private float dodgeTime;
    private bool dodging;
    private bool canDodgeAgain;
    private bool requestDodge;
    private Vector3 mousePos;
    private bool attacking = false;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = playerModel.GetComponent<Animator>();
        Assert.IsNotNull(controller, "Player needs a Character Controller for movement");
        dodging = false;
        moveDirection = Vector3.zero;
        CharacterStats cStats = GetComponent<CharacterStats>();
        Speed = Speed + (0.1f * cStats.Spd);
        dodgeSpeed = dodgeSpeed + (0.1f * cStats.Spd);
    }

    /// <summary>
    /// Need to use fixed update in order to properly use physics 
    /// this is to prevent a jittering problem that occurs
    /// </summary>
    private void FixedUpdate()
    {
        
        if (attacking)
        {
            lookAtMouse();
        }
        if (dodging != true)
        {
            Movement();
        }
        else
        {
            DodgingFunc();
        }
        if (requestDodge && canDodgeAgain)
        {
            DodgingFunc();
        }
        canDodgeAgain = !requestDodge;
        //handles the dodge
        if (animator && animator.GetBool("isMoving") != moveDirection.sqrMagnitude > 0)
            animator.SetBool("isMoving", moveDirection.sqrMagnitude > 0);
    }

    /// <summary>
    /// Called when player puts in an input related to movement.
    /// </summary>
    /// <param name="value"></param>
    public void OnMovement(InputValue value)
    {
        Vector2 inputDir = value.Get<Vector2>();
        moveDirection = new Vector3(inputDir.x, 0, inputDir.y);
    }

    /// <summary>
    /// Called when player puts in an input related to dodging
    /// </summary>
    /// <param name="value"></param>
    public void OnDodge(InputValue value)
    {
        requestDodge = value.Get<float>() != 0;
    }

    /// <summary>
    /// Basic movement for the character based on the input along with changing where the character is facing
    /// </summary>
    private void Movement()
    {
        if (moveDirection.sqrMagnitude > 0)
        {
            if (attacking == false)
            {
                float targetAngle = Mathf.Atan2(moveDirection.x,moveDirection.z) * Mathf.Rad2Deg;
                angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothSpeed, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0, angle, 0);
            }
            controller.SimpleMove(moveDirection * Speed);

        }
        else
        {
            controller.SimpleMove(Vector3.zero);
        }
    }

    public void AttackingMethod()
    {
        if (attacking == false)
        {
            attacking = true;
            lookAtMouse();
            StartCoroutine(AttackDuration());
        }
    }
    private IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(0.3f);
        attacking = false;
    }
    private void lookAtMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - transform.position;
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }
    /// <summary>
    /// Controls the dodging functionality
    /// </summary>
    private void DodgingFunc()
    {
        if (!dodging)
        {
            dodgeDirection = moveDirection;
            dodgeTime = 0.0f;
            dodging = true;
        }
        if(dodgeTime < dodgeMax)
        {
            controller.SimpleMove(dodgeDirection * dodgeSpeed);
            dodgeTime += Time.deltaTime;
        }
        else
        {
            dodging = false;
        }
        
    }

}
