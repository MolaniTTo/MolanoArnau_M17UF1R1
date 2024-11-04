using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.VFX;

public class CheckPoint : MonoBehaviour
{
    public MovimentCamara movimentCamara;
    public GameObject _cam;

    private void Awake()
    {
        movimentCamara = Camera.main.GetComponent<MovimentCamara>();

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SendCameraPosCheck();
        }
    }

    private void SendCameraPosCheck()
    {
        GameManager.instance.respawnPos = transform.position; // Guarda la posicio del player al checkpoint
        GameManager.instance.newCameraPos = _cam.transform.position; // Guarda la posicio de la camara
        GameManager.instance.checkPoint = true;
        GameManager.instance.camSize = Camera.main.orthographicSize;
	}
}
