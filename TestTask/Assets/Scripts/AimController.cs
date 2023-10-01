using UnityEngine;

public class AimController : MonoBehaviour
{
    private JoystickController joystick;

    private void Start()
    {
        // �������� ������ �� ��������� JoystickController � �����
        joystick = FindObjectOfType<JoystickController>();
    }

    void Update()
    {
        // �������� �������� ��������������� � ������������� ����� �� ���������
        float horizontalInput = joystick.Horizontal();
        float verticalInput = joystick.Vertical();

        // ������� ������ ����������� �� ���������� �������� �����
        Vector3 direction = new(horizontalInput, verticalInput, 0f);

        // ���������, ���� ���� ������������ (������ ��� ����� 0.1), ����� ������ ��������
        if (direction.magnitude >= 0.1f)
        {
            // ��������� ���� ����� ������������ � ���� X � �������� � ����������� ��� � �������
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // ������������ ������ ������ ��� Z �� ��������� ����
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
