using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public static float forwardSpeed = 10.5f;
    public float maxSpeed;

    private int desiredLane = 1; // 0 : Left, 1 : Middle, 2 : Right
    public float laneDistance = 2.5f; // the distance between two lanes

    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public float jumpForce;
    public float gravity = -20f;

    public Animator animator;
    private bool isSliding = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        if (!PlayerManager.isGameStarted)
        {
            return;
        }

        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += 0.1f * Time.deltaTime;
        }

        animator.SetBool("isGameStarted", true);

        direction.z = forwardSpeed;

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);

        if (isGrounded)
        {
            if (SwipeManager.swipeUp)
            {
                Jump();
            }
            if (SwipeManager.swipeDown && !isSliding)
            {
                StartCoroutine(Slide());
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
            if (SwipeManager.swipeDown && !isSliding)
            {
                StartCoroutine(Slide());
                direction.y = -8;
            }
        }

        // Gathering the input on which lane we should be
        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }
        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        // Calculate where we should be

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = 25 * Time.deltaTime * diff.normalized;
            if (moveDir.sqrMagnitude < diff.magnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
        }
        
        //Move Player
        controller.Move(direction * Time.deltaTime);

    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Obstacle") && !PlayerManager.isRocketed)
        {
            PlayerManager.gameOver = true;
            FindAnyObjectByType<AudioManager>().PlaySound("GameOver");
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;

        yield return new WaitForSeconds(0.7f);

        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;
        animator.SetBool("isSliding", false);
        isSliding = false;
    }

}
