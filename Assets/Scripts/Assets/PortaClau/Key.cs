using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public float rotationSpeed = 100.0f;
    public bool isCollected = false;

    void Update()
    {
        if (!isCollected)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Key collected!");
            isCollected = true;
            gameObject.SetActive(false);
            GameManager.instance.hasKey = true;
        }
    }
}
