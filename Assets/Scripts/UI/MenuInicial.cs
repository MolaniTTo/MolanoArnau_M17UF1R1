
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void Jugar()
    {
       SceneManager.LoadScene("LVL1"); //carrega la primera escena
    }

    public void Salir()
    {
        Application.Quit(); //nomes funciona ala build
    }
   
   
}
