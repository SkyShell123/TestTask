using UnityEngine;

public class AimController : MonoBehaviour
{
    private JoystickController joystick; // Reference to the joystick controller

    private void Start()
    {
        joystick = FindObjectOfType<JoystickController>();
    }

    void Update()
    {
        // Get the input from the joystick
        float horizontalInput = joystick.Horizontal();
        float verticalInput = joystick.Vertical();

        // Calculate the direction based on joystick input
        Vector3 direction = new(horizontalInput, verticalInput, 0f);

        // Rotate the player towards the calculated direction
        if (direction.magnitude >= 0.1f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Assuming the player is a child object of the AimController
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
