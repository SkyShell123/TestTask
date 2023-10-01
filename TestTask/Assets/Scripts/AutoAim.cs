using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    public AimController additionalObject; // ������ �� AimController, ������� ����� ��������� �������������� ��������
    private AimController aimController; // ������ �� AimController, �������������� � ����� �������
    private GameObject target; // ���� ��� ��������������� ������������

    void Start()
    {
        aimController = GetComponent<AimController>(); // �������� ������ �� AimController ��� ������� �����
    }

    void Update()
    {
        if (target != null)
        {
            OnAutoAim(); // ���� ���� ����, ��������� �������������� ������������
        }
    }

    private void OnAutoAim()
    {
        // ������������ ����������� � ���� � ������������� ��������������� ���� �������� ��� �������
        Vector3 direction = new(target.transform.position.x, target.transform.position.y, 0f);
        Vector3 movePosition = direction - transform.position;
        float rotateZ = Mathf.Atan2(movePosition.y, movePosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotateZ);

        // ���� ���� �������������� ������, ������������� ��� ����� �� ���� ��������
        if (additionalObject != null)
        {
            additionalObject.gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotateZ);
        }
    }

    private void OffAutoAim()
    {
        aimController.enabled = true; // �������� AimController ��� ���������� ��������������� ������������
        if (additionalObject != null)
        {
            additionalObject.enabled = true; // �������� �������������� AimController (���� ����) ��� ���������� ��������������� ������������
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && target == null)
        {
            target = other.gameObject; // ������������� ����� ����, ���� ������ � ����� "Enemy" ������ � ���� ��������
            aimController.enabled = false; // ��������� AimController ��� ����� � ���� ��������
            if (additionalObject != null)
            {
                additionalObject.enabled = false; // ��������� �������������� AimController (���� ����) ��� ����� � ���� ��������
            }
            OnAutoAim(); // ��������� �������������� ������������
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (target != null && other.gameObject == target)
        {
            target = null; // ���������� ����, ����� ������ �������� ���� ��������
            OffAutoAim(); // ��������� �������������� ������������ � �������� AimController
        }
    }
}
