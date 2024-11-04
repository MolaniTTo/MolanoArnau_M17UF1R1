using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    public GameObject Player;
    public GameObject menuPausaUI;
    private bool IsPaused = false;

    void Start()
    {
        menuPausaUI.SetActive(false);
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    public void Resume() //Reanudar el joc
    {
        menuPausaUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    void Pause() //Pausar el joc
    {
        menuPausaUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void RestartLevel() //Reiniciar el nivell
    {
        if(Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
        Player.transform.position = new Vector3(-5, -3, 0); //Posicio inicial del jugador
    }

    public void MainMenu()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        Time.timeScale = 1f;
        Player.transform.position = new Vector3(-5, -3, 0);
        SceneManager.LoadScene(0);
    }

}
