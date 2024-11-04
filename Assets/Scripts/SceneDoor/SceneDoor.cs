
using UnityEngine;

public class SceneDoor : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // SpriteRenderer de la porta
    public Sprite doorOpenSprite; //sprite de la porta oberta
    public Sprite doorClosedSprite; // El sprite de la porta tancada
    public float detectionRadius = 1f; // Radi de deteccio del jugador
    public Collider2D doorCollider; // Collider de la porta
    private bool isOpen = false; // Controlar si la porta esta oberta

    private void Start()
    {
        spriteRenderer.sprite = doorClosedSprite;
        doorCollider.enabled = true;
    }

    void Update()
    {
        if (!isOpen)
        {
            // Detectar si el jugador está cerca de la puerta
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    OpenDoor();
                    break;
                }
            }

        }
       
    }

    private void OpenDoor()
    {
        // Cambiar el sprite de la porta quan està oberta
        spriteRenderer.sprite = doorOpenSprite;
        doorCollider.enabled = false;
        isOpen = true;
    }
}

