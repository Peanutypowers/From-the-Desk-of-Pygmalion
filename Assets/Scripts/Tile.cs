using UnityEngine;

public class Tile : MonoBehaviour
{
    private SimonGameManager gameManager;
    private SpriteRenderer spriteRenderer;

    private int tileId;
    private Color color;

    public void Init(SimonGameManager gameManager, int tileId, Color color)
    {
        this.gameManager = gameManager;
        this.tileId = tileId;
        this.color = color;

        spriteRenderer = GetComponent<SpriteRenderer>();

        // Tiles start off
        TurnOff();
    }

    public void TurnOff() {
        // Darken the original color
        spriteRenderer.color = color * 0.3f;
    }

    public void TurnOn() {
        // Set the original color
        spriteRenderer.color = color;
    }

    private void OnMouseDown()
    {
        // Turn on the tile when clicked
        gameManager.PlayLightAndTone(tileId);
    }
}
