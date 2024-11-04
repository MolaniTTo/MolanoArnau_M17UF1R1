
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Moviment player;

    void Start()
    {
        player = GetComponentInParent<Moviment>();

    }

    // variable booleana que indica si es a terra o no
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo")) //si colisiona amb el terra, es posa a true
        {
            player.SetInGround(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo")) //si surt del terra, es posa a false
        {
            player.SetInGround(false);
        }
    }

}
