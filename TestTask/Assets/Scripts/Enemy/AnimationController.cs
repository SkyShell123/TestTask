using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private HingeJoint2D hingeJoint;
    private JointMotor2D motor;

    void Start()
    {
        // Получаем компонент HingeJoint2D
        hingeJoint = GetComponent<HingeJoint2D>();

        // Инициализируем параметры мотора
        motor = hingeJoint.motor;
        motor.motorSpeed = 100.0f; // Скорость вращения сустава
        motor.maxMotorTorque = 100.0f; // Максимальный крутящий момент

        // Применяем мотор
        hingeJoint.motor = motor;
    }

    void Update()
    {
        // Вызываем этот метод для анимации вращения
        //hingeJoint.SetMotor(motor);
    }
}
