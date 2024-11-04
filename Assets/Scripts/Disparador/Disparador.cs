using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparador : MonoBehaviour
{
    public GameObject bulletPref;
    public Transform spawnPoint;
    public float bulletSpeed = 10f;
    public float timeBetShoot = 1f;
    public int maxBullets = 10;

    private Stack<GameObject> bulletStack; //pila de bales

    void Start()
    {
        bulletStack = new Stack<GameObject>(); //inicialitzem la pila


        for (int i = 0; i < maxBullets; i++) //omplim la pila amb les bales
        {
            GameObject bullet = Instantiate(bulletPref);
            bullet.SetActive(false);
            bulletStack.Push(bullet); 
        }

        StartCoroutine(ShootInf()); //comença a disparar

    }

    IEnumerator ShootInf() //corutina per disparar
    {
        while (true)
        {
            if (bulletStack.Count > 0)
            {
                Shoot();
            }
            yield return new WaitForSeconds(timeBetShoot);
        }
    }

    public void Shoot() //dispara una bala
    {
        GameObject bullet = bulletStack.Pop();
        bullet.transform.position = spawnPoint.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.SetActive(true); //Activa la bala

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDisparador(this);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
    }

    public void RechargeBullet(GameObject bullet) //recarrega la bala
    {
        bullet.SetActive(false);
        bulletStack.Push(bullet);
    }
}
