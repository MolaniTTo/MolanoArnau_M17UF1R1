
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public MovimentCamara MovimentCamara;
    public GameObject player;
    public GameObject _cam;
    private Dictionary<GameObject, Vector3> keys = new Dictionary<GameObject, Vector3>();
    public MovimentCamara movimentCamara;
    public bool checkPoint = false;
    public Vector3 newCameraPos;
    public Vector3 respawnPos;
    public Vector3 lastLevelPositionCam;
    public string lastLevel;
    
    public float camSize;
    public bool hasKey = false;

    //Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            SceneManager.sceneLoaded += OnSceneLoaded; //per guardar les posicions de les habitacions cada cop que carreguem una escena (intents per moure la camara al canvi d'escena enrere)
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null && SceneManager.GetActiveScene().name == "LVL1") //si no hi ha jugador a la escena i estem a la primera escena l'instacia
        {
            player = Instantiate(player, respawnPos, Quaternion.identity);
        }
        instance = this;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) //per guardar les posicions de les habitacions cada cop que carreguem una escena (intents per moure la camara al canvi d'escena enrere)
    {
        keys.Clear();

        foreach (GameObject room in GameObject.FindGameObjectsWithTag("Room"))
        {
            keys.Add(room, room.transform.position);
        }
    }
    private void OnDestroy() { SceneManager.sceneLoaded -= OnSceneLoaded; } //per evitar que es guardin les posicions quan no calgui


    public void RespawnPlayer() //respawn del jugador
    {
        if (!checkPoint)
        {
            movimentCamara.MoureCamara(keys[GameObject.Find("Room1")]);
            player.transform.position = new Vector3(-5.05000019f, -1.31900001f, 0f);

			SizeOfRoom roomInicial = GameObject.Find("Room1").GetComponent<SizeOfRoom>();
			if (roomInicial != null)
			{
				movimentCamara.ChangeSizeCamera(roomInicial.size);
			}
		}
        if (checkPoint)
        {
            movimentCamara.MoureCamara(newCameraPos);
            player.transform.position = new Vector3(respawnPos.x, respawnPos.y, 0f);
			movimentCamara.ChangeSizeCamera(camSize);
		}
    }
}
