using UnityEngine;

public class AimController : MonoBehaviour
{
    private JoystickController joystick;

    private void Start()
    {
        // Получаем ссылку на компонент JoystickController в сцене
        joystick = FindObjectOfType<JoystickController>();
    }

    void Update()
    {
        // Получаем значения горизонтального и вертикального ввода от джойстика
        float horizontalInput = joystick.Horizontal();
        float verticalInput = joystick.Vertical();

        // Создаем вектор направления из полученных значений ввода
        Vector3 direction = new(horizontalInput, verticalInput, 0f);

        // Проверяем, если ввод значительный (больше или равен 0.1), чтобы начать вращение
        if (direction.magnitude >= 0.1f)
        {
            // Вычисляем угол между направлением и осью X в радианах и преобразуем его в градусы
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Поворачиваем объект вокруг оси Z до заданного угла
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
