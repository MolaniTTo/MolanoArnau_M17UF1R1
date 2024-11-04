using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Porta : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // SpriteRenderer de la porta
    public Sprite doorOpenSprite; //sprite de la porta oberta
    public Sprite doorClosedSprite; // El sprite de la porta ancada
    public float detectionRadius = 1f; // Cercle de deteccio per al jugador
    public Collider2D doorCollider; // Collider de la porta
    private string CurrentLevel;

    private bool isOpen = false;

    private void Start()
    {
        spriteRenderer.sprite = doorClosedSprite;
        doorCollider.enabled = true;
    }
    


    void Update()
    {
        CurrentLevel = SceneManager.GetActiveScene().name;

        if(CurrentLevel != "LVL4")
        {
            // Detectar si el jugador está cerca de la puerta
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    // Verificar si el jugador tiene la llave
                    if (GameManager.instance.hasKey && !isOpen)
                    {
                        OpenDoor();
                    }
                }
            }
        }

        else
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
            bool playerNerby = false;
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    playerNerby = true;

                    if (!isOpen)
                    {
                        OpenDoor();
                    }
                }
            }

            if (isOpen && !playerNerby)
            {
                CloseDoor();
            }
        }
    }

    private void OpenDoor()
    {
        // Cambiar el sprite quan la porta està oberta
        spriteRenderer.sprite = doorOpenSprite;
        doorCollider.enabled = false;
        isOpen = true;
    }

    private void CloseDoor()
    {
        // Cambiar el sprite
        spriteRenderer.sprite = doorClosedSprite;
        doorCollider.enabled = true;
        isOpen = false;
    }

    private void OnDrawGizmosSelected() //el cercle de detecció només es veu a l'editor
    {
        // Dibujar un círculo en el editor para mostrar el radio de detección
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }


}
