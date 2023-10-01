using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
    public EnemyAI enemyAI;     // ������ �� ��������� EnemyAI, ������� ��������� ������.
    public Transform RayDir;    // ����������� ����, ������������� ��� ����������� �����������.

    // �������, ����������� ����������� �������� � ��������� ����� ������� ����.
    private bool ReyDetector(string tag)
    {
        // ������� ���, ���������� �� ������� ����� ������� � ��������� �����������.
        RaycastHit2D HitInfo = Physics2D.Raycast(transform.position, RayDir.transform.right);

        // ���� ��� ���������� � �����-�� ��������:
        if (HitInfo.collider != null)
        {
            // ���������, ����� �� ������������� ������ ��������� ���.
            if (HitInfo.transform.CompareTag(tag))
            {
                return true;  // ���������� true, ���� ������ � ��������� ����� ���������.
            }
            else
            {
                return false; // ���������� false, ���� ������ �� ����� ��������� ���.
            }
        }
        else
        {
            return false; // ���������� false, ���� ��� �� ���������� � ��������.
        }
    }

    // �����, ���������� ��� ������������ ����� ������� � ������ �����������.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���� ������������� ������ ����� ��� "Player":
        if (other.CompareTag("Player"))
        {
            // ���� ��� �� ������������ ������ � ����� "Wall":
            if (!ReyDetector("Wall"))
            {
                // �������� ����� PlayerDetected() �� ���������� EnemyAI.
                enemyAI.PlayerDetected();
            }
        }
    }
}
