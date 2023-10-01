using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    // Ссылки на изображения для фона и самого джойстика
    private Image bgImage;
    private Image joystickImage;

    // Вектор для хранения входных данных от джойстика
    private Vector3 inputVector;

    private void Start()
    {
        // Инициализация ссылок на изображения внутри объекта
        bgImage = GetComponent<Image>();
        joystickImage = transform.GetChild(0).GetComponent<Image>();
    }

    // Обработка перетаскивания джойстика
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;

        // Преобразование позиции к локальным координатам внутри фона джойстика
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImage.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            // Нормализация позиции в диапазоне [-1, 1]
            pos.x /= bgImage.rectTransform.sizeDelta.x;
            pos.y /= bgImage.rectTransform.sizeDelta.y;

            // Создание вектора входных данных и его нормализация
            inputVector = new Vector3(pos.x * 2, 0, pos.y * 2);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            // Перемещение изображения джойстика в соответствии с входными данными
            joystickImage.rectTransform.anchoredPosition =
                new Vector3(inputVector.x * (bgImage.rectTransform.sizeDelta.x / 2.0f),
                            inputVector.z * (bgImage.rectTransform.sizeDelta.y / 2.0f));
        }
    }

    // Обработка нажатия на джойстик
    public virtual void OnPointerDown(PointerEventData ped)
    {
        // Просто вызываем OnDrag для начала перемещения джойстика
        OnDrag(ped);
    }

    // Обработка отпускания джойстика
    public virtual void OnPointerUp(PointerEventData ped)
    {
        // Сброс входных данных и позиции джойстика
        inputVector = Vector3.zero;
        joystickImage.rectTransform.anchoredPosition = Vector3.zero;
    }

    // Возвращает горизонтальную компоненту входных данных
    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }

    // Возвращает вертикальную компоненту входных данных
    public float Vertical()
    {
        if (inputVector.z != 0)
            return inputVector.z;
        else
            return Input.GetAxis("Vertical");
    }
}
