using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private HingeJoint2D hingeJoint;
    private JointMotor2D motor;

    void Start()
    {
        // �������� ��������� HingeJoint2D
        hingeJoint = GetComponent<HingeJoint2D>();

        // �������������� ��������� ������
        motor = hingeJoint.motor;
        motor.motorSpeed = 100.0f; // �������� �������� �������
        motor.maxMotorTorque = 100.0f; // ������������ �������� ������

        // ��������� �����
        hingeJoint.motor = motor;
    }

    void Update()
    {
        // �������� ���� ����� ��� �������� ��������
        //hingeJoint.SetMotor(motor);
    }
}
