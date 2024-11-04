using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Color = UnityEngine.Color;

public class Moviment : MonoBehaviour
{

    public static Moviment instance;

    //singleton
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

   
    
    public float velocidad;
    public float velocidadCaida;
    public float distanciaDeteccionObstaculo = 0.1f;
    public LayerMask capaObstaculos;
    //Script de la cámara
    public MovimentCamara movimentCamara;
    private SizeOfRoom sizeOfRoom;

    private Rigidbody2D rPlayer;
    private SpriteRenderer sprite;

    private float h;
    private Animator aPlayer;

    private bool enSuelo = false;
    private bool mirandoDerecha = true;
    private bool mirandoArriba = false;

    public Color damageColor = Color.red;
    public float blinkDuration = 0.1f;
    public int numberOfBlinks = 5;
    private bool isBlinking = false;

    public AudioSource sJumpPlayer;
    public AudioSource playerAudio;

    [SerializeField] private AudioClip walk;
    [SerializeField] private AudioClip jump;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        movimentCamara = Camera.main.GetComponent<MovimentCamara>();
        rPlayer = GetComponent<Rigidbody2D>();
        aPlayer = GetComponent<Animator>();
    }

    void Update()
    {

        //moviment del jugador

        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 5)
        {
            rPlayer.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            rPlayer.bodyType = RigidbodyType2D.Dynamic;
        }

        aPlayer.SetFloat("VelocidadX", Mathf.Abs(h));

        if(Input.GetKey(KeyCode.D) && !HayObstaculoDerecha())
        {
            transform.position = transform.position + new Vector3(velocidad, 0, 0) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A) && !HayObstaculoIzquierda())
        {
            transform.position = transform.position + new Vector3(-velocidad, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            sJumpPlayer.PlayOneShot(jump);  
            FlipGravity();
            mirandoArriba = !mirandoArriba;
        }

        h = Input.GetAxisRaw("Horizontal"); //component hotizontal del moviment
        ControlarCaida(); //controlar la caiguda del jugador
        FlipPlayer(h); //girar el jugador

        if(h!= 0) //si la horitxontal es diferent de 0, fem el audio de caminar
        {
            if (!playerAudio.isPlaying)
            {
                Debug.Log("Walk");
                playerAudio.loop = true;
                playerAudio.clip = walk;
                playerAudio.Play();
            }
        }
        else //si no, parem l'audio
        {
            if (playerAudio.isPlaying)
            {
                playerAudio.Stop();
            }
        }
    }

    private bool HayObstaculoDerecha() //aixo es un raycast que detecta si hi ha un obstacle a la dreta peruqe sino intentava atravessar parets i feia aquell pop tan lleig de collisionar amb les parets
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, distanciaDeteccionObstaculo, capaObstaculos);
        return hit.collider != null;  // Si el raycast toca algo, ho marca com a obstacle
    }

    // Obstacle a l'esquerra amb el raycast
    private bool HayObstaculoIzquierda() //aixo es un raycast que detecta si hi ha un obstacle a la dreta peruqe sino intentava atravessar parets i feia aquell pop tan lleig de collisionar amb les parets
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, distanciaDeteccionObstaculo, capaObstaculos);
        return hit.collider != null;  // Si el raycast toca algo, ho marca com a obstacle
    }



    //el personaje gira y canvia la gravetat al donarli al espai

    private void FlipGravity()
    {
        rPlayer.gravityScale *= -1; //canviar la gravetat
        
        Vector3 escalaGiro = transform.localScale;
        escalaGiro.y *= -1; //girar el jugador
        transform.localScale = escalaGiro;
        // Per tenir una velocitat de caiguda constant, sense acceleracio
        rPlayer.velocity = new Vector2(rPlayer.velocity.x, -Mathf.Sign(rPlayer.gravityScale) * velocidadCaida);

    }


    void FlipPlayer(float horizontal)//ja va
    {
        if(horizontal > 0  && !mirandoDerecha || horizontal < 0 && mirandoDerecha)
        {
            mirandoDerecha = !mirandoDerecha;
            Vector3 escalaGiro = transform.localScale;
            escalaGiro.x *= -1;
            transform.localScale = escalaGiro;
        }
    }

    private void ControlarCaida() //per que la caiguda no sigui tan tan rapida
    {
        // Si el nino esta caient, limitar la velocitat vertical
        if (Mathf.Abs(rPlayer.velocity.y) > velocidadCaida)
        {
            rPlayer.velocity = new Vector2(rPlayer.velocity.x, Mathf.Sign(rPlayer.velocity.y) * velocidadCaida);
        }
    }

    public void SetInGround(bool touchingGround) //si esta a terra o no
    {
        enSuelo = touchingGround;
    }

    public void CambiarHabitacio(TransicioPantalla transition) //cambiar de habitcacio quan detecta el collider, passanli el transform a la funcio de MoureCamara
    {
        if(movimentCamara == null)
        {
            movimentCamara = FindObjectOfType<MovimentCamara>();
            if (movimentCamara == null)
            {
                Debug.LogError("movimentCamara sigue siendo nulo. No se ha encontrado MovimentCamara en la escena.");
                return;
            }
        }
        Vector3 newCameraPos;
        SizeOfRoom targetRoomSize = null;

        if(transition.canviVertical)
        {
            if(mirandoArriba)
            {
                newCameraPos = new Vector3(transition.pantallaAbaix.position.x, transition.pantallaAbaix.position.y, Camera.main.transform.position.z);
                targetRoomSize = transition.roomAmunt;
            }
            else
            {
                newCameraPos = new Vector3(transition.pantallaAmunt.position.x, transition.pantallaAmunt.position.y, Camera.main.transform.position.z);
				targetRoomSize = transition.roomAbaix;
			}
        }
        else
        {
            if(mirandoDerecha)
            {
               
                newCameraPos = new Vector3(transition.pantallaDreta.position.x, transition.pantallaDreta.position.y, Camera.main.transform.position.z);
                targetRoomSize = transition.roomDreta;
            }
            else
            {
                newCameraPos = new Vector3(transition.pantallaEsquerra.position.x, transition.pantallaEsquerra.position.y, Camera.main.transform.position.z);
                targetRoomSize= transition.roomEsquerra;
            }
        }
        movimentCamara.MoureCamara(newCameraPos);
        
      

        if (transition.reSize && targetRoomSize != null)
        {
            movimentCamara.ChangeSizeCamera(targetRoomSize.size);
        }

    }

    // Detectar si el jugador entra a la zona de la tranzicio

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Transition"))
        {
            CambiarHabitacio(collision.GetComponent<TransicioPantalla>());
        }

        if(collision.gameObject.CompareTag("Checkpoint"))
        {
            GameManager.instance.respawnPos = collision.transform.position;
            GameManager.instance.checkPoint = true;
        }
        if(collision.gameObject.CompareTag("ChangeScale"))
        {
            Vector3 newScale = new Vector3(6f, 6f, 6f);
            velocidad = 20;
            velocidadCaida = 40;
            transform.localScale = newScale;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Transition"))
        {
            CambiarHabitacio(collision.GetComponent<TransicioPantalla>());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //detectar el layer de l'objecte amb el que colisiona
        if (collision.gameObject.layer == LayerMask.NameToLayer("Damage"))
        {
            GameManager.instance.RespawnPlayer();
            if(!isBlinking)
            {
                StartCoroutine(BlinkEffect());
            }
        }
    }

    IEnumerator BlinkEffect()
    {
        isBlinking = true;
        Color originalColor = sprite.color;
        for (int i = 0; i < numberOfBlinks; i++)
        {
            sprite.color = damageColor;
            yield return new WaitForSeconds(blinkDuration);
            sprite.color = originalColor;
            yield return new WaitForSeconds(blinkDuration);
        }
        isBlinking = false;
    }
}



