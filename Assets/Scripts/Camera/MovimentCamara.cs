using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimentCamara : MonoBehaviour
{
    //Mou la camara a la possicio del transform que li passem
    public void MoureCamara(Vector3 novaPosicio)
	{
		Camera.main.transform.position = new Vector3(novaPosicio.x, novaPosicio.y, novaPosicio.z);
	}

	//per si la room es de mida diferent a la de la camara
	public void ChangeSizeCamera(float size)
	{
		Debug.Log("Cambiando tamaño de la cámara a: " + size);
		Camera.main.orthographicSize = size;
	}
}
