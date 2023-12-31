using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    // ������ �� ����������� ��� ���� � ������ ���������
    private Image bgImage;
    private Image joystickImage;

    // ������ ��� �������� ������� ������ �� ���������
    private Vector3 inputVector;

    private void Start()
    {
        // ������������� ������ �� ����������� ������ �������
        bgImage = GetComponent<Image>();
        joystickImage = transform.GetChild(0).GetComponent<Image>();
    }

    // ��������� �������������� ���������
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;

        // �������������� ������� � ��������� ����������� ������ ���� ���������
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImage.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            // ������������ ������� � ��������� [-1, 1]
            pos.x /= bgImage.rectTransform.sizeDelta.x;
            pos.y /= bgImage.rectTransform.sizeDelta.y;

            // �������� ������� ������� ������ � ��� ������������
            inputVector = new Vector3(pos.x * 2, 0, pos.y * 2);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            // ����������� ����������� ��������� � ������������ � �������� �������
            joystickImage.rectTransform.anchoredPosition =
                new Vector3(inputVector.x * (bgImage.rectTransform.sizeDelta.x / 2.0f),
                            inputVector.z * (bgImage.rectTransform.sizeDelta.y / 2.0f));
        }
    }

    // ��������� ������� �� ��������
    public virtual void OnPointerDown(PointerEventData ped)
    {
        // ������ �������� OnDrag ��� ������ ����������� ���������
        OnDrag(ped);
    }

    // ��������� ���������� ���������
    public virtual void OnPointerUp(PointerEventData ped)
    {
        // ����� ������� ������ � ������� ���������
        inputVector = Vector3.zero;
        joystickImage.rectTransform.anchoredPosition = Vector3.zero;
    }

    // ���������� �������������� ���������� ������� ������
    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }

    // ���������� ������������ ���������� ������� ������
    public float Vertical()
    {
        if (inputVector.z != 0)
            return inputVector.z;
        else
            return Input.GetAxis("Vertical");
    }
}
