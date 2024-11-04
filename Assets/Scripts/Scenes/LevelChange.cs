
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{ 
    public GameManager GameManager;

    public Vector3 playerPosition;

    public string CurrentLevel;
    public string LevelToLoad;


    private void OnTriggerEnter2D(Collider2D collision) //si el jugador colisiona amb la porta, carrega la següent escena
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(LevelToLoad);
            collision.transform.position = playerPosition;
        }
    }
}
