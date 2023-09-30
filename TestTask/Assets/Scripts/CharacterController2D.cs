using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    private Animator animator;
    public int speed = 2;
    private JoystickController joystick; // Reference to the joystick controller
    private bool playerStayLeft;

    private void Start()
    {
        animator=GetComponent<Animator>();
        joystick = FindObjectOfType<JoystickController>();
    }

    private void Update()
    {
        
        if (joystick.Horizontal() != 0 || joystick.Vertical() != 0)
        {
            animator.SetBool("PlayerStayLeft", false);

            if (joystick.Horizontal() < 0) //&& !animator.GetBool("PlayerWalkRight"))
            {
                animator.SetBool("PlayerWalkLeft", true);
                animator.SetBool("PlayerWalkRight", false);

                playerStayLeft =true;
            }
            else if (joystick.Horizontal() > 0)
            {
                animator.SetBool("PlayerWalkLeft", false);
                animator.SetBool("PlayerWalkRight", true);
                playerStayLeft = false;
            }

            float moveHorizontal = joystick.Horizontal();
            float moveVertical = joystick.Vertical();

            Vector3 movement = new(moveHorizontal, moveVertical, 0.0f);
            transform.Translate(speed * Time.deltaTime * movement);
        }
        else
        {
            animator.SetBool("PlayerWalkLeft", false);
            animator.SetBool("PlayerWalkRight", false);

            if (playerStayLeft)
            {
                animator.SetBool("PlayerStayLeft", true);

            }
            else
            {
                animator.SetBool("PlayerStayLeft", false);
            }
        }
    }
}
