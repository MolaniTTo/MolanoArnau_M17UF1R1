
using UnityEngine;

public class Teletransport : MonoBehaviour
{ 
    public Transform TeleportPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = TeleportPosition.position; //teletransporta el jugador a la posicio de la porta
        }
    }
   
   
}
