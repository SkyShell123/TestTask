using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float dumping = 1.5f;
    public Vector2 offset = new(2f, 1f);
    public bool isLeft;
    public Transform playerModel;
    private int lastX;

    void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        FindPlayer(isLeft);
    }

    void FindPlayer(bool playerIsLeft)
    {
        //playerModel = GameObject.FindGameObjectWithTag("Player").transform;
        lastX = Mathf.RoundToInt(playerModel.position.x);
        
        if (playerIsLeft)
        {
            transform.position = new Vector3(playerModel.position.x - offset.x, playerModel.position.y - offset.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(playerModel.position.x + offset.x, playerModel.position.y + offset.y, transform.position.z);
        }
    }

    void Update()
    {
        if (playerModel)
        {
            int currentX = Mathf.RoundToInt(playerModel.position.x);
            if (currentX > lastX) isLeft = false; else if (currentX < lastX) isLeft = true;
            lastX = Mathf.RoundToInt(playerModel.position.x);

            Vector3 target;
            if (isLeft)
            {
                target = new Vector3(playerModel.position.x - offset.x, playerModel.position.y + offset.y, transform.position.z);
            }
            else
            {
                target = new Vector3(playerModel.position.x + offset.x, playerModel.position.y + offset.y, transform.position.z);
            }

            Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
            transform.position = currentPosition;
        }
    }
}
