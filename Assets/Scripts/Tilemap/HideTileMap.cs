
using UnityEngine;
using UnityEngine.Tilemaps;

public class HideTileMap : MonoBehaviour
{

    public float transparentAlpha = 0.5f;  // Transparencia al aproparse
    public float normalAlpha = 1.0f;       // Transparencia normal
    public float fadeSpeed = 2.0f;         // Velocitat de transicio de la transparencia

    private TilemapRenderer tilemapRenderer;
    private Material tilemapMaterial;
    private bool isPlayerNear = false;

    void Start()
    {
        // Obtenir el TilemapRenderer i el material del Tilemap
        tilemapRenderer = GetComponent<TilemapRenderer>();
        if (tilemapRenderer != null)
        {
            tilemapMaterial = tilemapRenderer.material;
        }
        else
        {
            Debug.LogError("No se encontró un TilemapRenderer en el GameObject.");
        }
    }

    void Update()
    {
        if (tilemapMaterial == null) return;

        // Cambia la transparencia del material del Tilemap en funccio si esta aprop o no
        float targetAlpha = isPlayerNear ? transparentAlpha : normalAlpha;
        Color color = tilemapMaterial.color;
        color.a = Mathf.Lerp(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
        tilemapMaterial.color = color;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}

