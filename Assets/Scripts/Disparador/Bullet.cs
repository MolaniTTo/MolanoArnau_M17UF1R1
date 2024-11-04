using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask layerToCollide;
    private Disparador disparador;
    public float rotationSpeed = 200f;


   
    public void SetDisparador(Disparador disparador) //per saber quin disparador ha disparat el barril
    {
        this.disparador = disparador;
    }   

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime); //rota el barril
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            disparador.RechargeBullet(gameObject); //si colisiona amb el jugador, torna a carregar la bala
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EndBullet")) //si colisiona amb un objecte de la capa EndBullet torna a carregar la bala
        {
            disparador.RechargeBullet(gameObject);
        }

    }
}

