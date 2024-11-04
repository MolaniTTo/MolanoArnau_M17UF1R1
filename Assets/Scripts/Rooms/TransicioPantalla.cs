
using UnityEngine;

public class TransicioPantalla : MonoBehaviour
{

    //per cada transicio només utilitzo dos, ja que només passo d'una habitacio a l'altra
    public bool canviVertical = false;
    public bool reSize = false;

    public Transform pantallaDreta;
    public Transform pantallaEsquerra;
    public Transform pantallaAmunt;
    public Transform pantallaAbaix;

    public SizeOfRoom roomDreta;
    public SizeOfRoom roomEsquerra;
    public SizeOfRoom roomAmunt;
    public SizeOfRoom roomAbaix;
}
