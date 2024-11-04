using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cofre : MonoBehaviour
{
    public GameObject AudioController;
    public GameObject key;
    public SpriteRenderer spriteRenderer; // SpriteRenderer de la porta
    public Sprite OpenSprite; //sprite de la porta oberta
    public Sprite ClosedSprite; // El sprite de la porta tancada
    public float detectionRadius = 1f; // cercle de deteccio per al jugador
    public Collider2D Collider; // Collider de la porta
    public GameObject Player; // Jugador
    public Moviment Moviment; // movimiento del player

    private bool isOpen = false;

    private void Start()
    {
        key.SetActive(false);
        spriteRenderer.sprite = ClosedSprite;
        Player = GameObject.FindGameObjectWithTag("Player");
        Moviment = Player.GetComponent<Moviment>();
        AudioController = GameObject.FindGameObjectWithTag("AudioController");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isOpen)
            {
                Open();
                if (SceneManager.GetActiveScene().name == "LVL4")
                {
                    StartCoroutine(EndGame());
                }
            }
            else
            {
                Close();
            }
           
        }
    }


    private void Open()
    {
        // Cambiar el sprite de la porta quan està oberta
        spriteRenderer.sprite = OpenSprite;
        key.SetActive(true);
        isOpen = true;
    }

    private void Close()
    {
        // Cambiar el sprite de la porta quan està tancada
        spriteRenderer.sprite = ClosedSprite;
        isOpen = false;
    }

    IEnumerator EndGame()
    {
        //audioCocntroller PlayWinner
        AudioController.GetComponent<AudioController>().PlayWinner();

        yield return new WaitForSeconds(5);
        Time.timeScale = 1f;

        if (Player.transform.localScale == new Vector3(6, 6, 6))
        {
            Player.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Player.transform.localScale == new Vector3(6, -6, 6))
        {
            Player.transform.localScale = new Vector3(1, -1, 1);
        }
        else if (Player.transform.localScale == new Vector3(-6, 6, 6))
        {
            Player.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Player.transform.localScale == new Vector3(-6, -6, 6))
        {
            Player.transform.localScale = new Vector3(-1, -1, 1);
        }
        Player.transform.position = new Vector3(-5, -3, 0);
        
        //Tornar als valors default de velocitat i de tot
        Moviment.velocidad = 5f;
        Moviment.velocidadCaida = 20f;
        Debug.Log("Canvi velocitats");
        
        SceneManager.LoadScene("Winner");
    }
}
