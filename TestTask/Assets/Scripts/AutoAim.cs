using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    private AimController aimController;
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        aimController=GetComponent<AimController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            OnAutoAim();
        }
    }

    private void OnAutoAim()
    {
        Vector3 direction = new(target.transform.position.x, target.transform.position.y, 0f);
        Vector3 MovePosition = direction - transform.position;
        float RotateZ = Mathf.Atan2(MovePosition.y, MovePosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, RotateZ);
    }

    private void OffAutoAim()
    {
        aimController.enabled = true;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && target == null)
        {
            target=other.gameObject;
            aimController.enabled = false;
            OnAutoAim();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (target!=null && other.gameObject == target)
        {
            target = null;
            OffAutoAim();
        }
    }
}
