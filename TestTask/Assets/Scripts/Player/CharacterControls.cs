using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterControls : MonoBehaviour
{
    public static CharacterControls Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    public Vector3 SavePosition()
    {
        return transform.position;
    }

    public void LoadPosition(Vector3 point)
    {
        transform.position = point;
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
