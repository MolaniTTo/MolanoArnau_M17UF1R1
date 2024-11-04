
using UnityEngine;

public class GoBack : MonoBehaviour
{
    public float camSize;
    public MovimentCamara movimentCamara;
    public Vector3 CamPosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Camera.main.transform.position = CamPosition; //mou la camara a la posicio de la porta
            movimentCamara.ChangeSizeCamera(camSize);

        }
    }

}
