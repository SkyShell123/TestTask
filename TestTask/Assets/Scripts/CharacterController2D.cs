using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    private Animator animator;              
    public int speed = 2;                   // Скорость перемещения персонажа.
    private JoystickController joystick;    // Ссылка на контроллер джойстика для управления персонажем.
    private bool playerStayLeft;            // Флаг, указывающий, смотрит ли игрок влево.

    private void Start()
    {
        animator = GetComponent<Animator>();                
        joystick = FindObjectOfType<JoystickController>();  // Находим объект JoystickController в сцене.
    }

    private void Update()
    {
        // Проверяем ввод с джойстика.
        if (joystick.Horizontal() != 0 || joystick.Vertical() != 0)
        {
            animator.SetBool("PlayerStayLeft", false);  // Отключаем анимацию стояния налево.

            // Проверяем направление движения по горизонтали.
            if (joystick.Horizontal() < 0)
            {
                animator.SetBool("PlayerWalkLeft", true);  // Включаем анимацию ходьбы влево.
                animator.SetBool("PlayerWalkRight", false); // Выключаем анимацию ходьбы вправо.
                playerStayLeft = true;  // Устанавливаем флаг, что игрок движется налево.
            }
            else if (joystick.Horizontal() > 0)
            {
                animator.SetBool("PlayerWalkLeft", false); // Выключаем анимацию ходьбы влево.
                animator.SetBool("PlayerWalkRight", true); // Включаем анимацию ходьбы вправо.
                playerStayLeft = false; // Устанавливаем флаг, что игрок движется вправо.
            }

            float moveHorizontal = joystick.Horizontal(); // Получаем значение горизонтального ввода.
            float moveVertical = joystick.Vertical();     // Получаем значение вертикального ввода.

            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f); // Создаем вектор движения.
            transform.Translate(speed * Time.deltaTime * movement); // Производим перемещение персонажа.
        }
        else
        {
            animator.SetBool("PlayerWalkLeft", false); // Выключаем анимацию ходьбы влево.
            animator.SetBool("PlayerWalkRight", false); // Выключаем анимацию ходьбы вправо.

            if (playerStayLeft)
            {
                animator.SetBool("PlayerStayLeft", true); // Включаем анимацию стояния налево, если игрок смотрит влево.
            }
            else
            {
                animator.SetBool("PlayerStayLeft", false); // Выключаем анимацию стояния налево, если игрок не двигается влево.
            }
        }
    }
}
