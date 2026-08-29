using UnityEngine;

public class SimonMoon : MonoBehaviour
{
    private SimonGameManager gameManager;
    private SpriteRenderer spriteRenderer;

    private Color color;
    private bool isEnabled;

    public void Init(SimonGameManager gameManager)
    {
        this.gameManager = gameManager;

        // turn moon white
        this.color = Color.white;

        spriteRenderer = GetComponent<SpriteRenderer>(); 

        // Moon starts on
        TurnOn();
    }

    public void TurnOff() {
        // Darken the original color
        spriteRenderer.color = color * 0.3f;
        isEnabled = false;
    }

    public void TurnOn() {
        // Set the original color
        spriteRenderer.color = color;
        isEnabled = true;
    }

    private void OnMouseDown()
    {
        if(isEnabled) {
            // Start the game when clicked
            gameManager.Play();
        }
    }

}
