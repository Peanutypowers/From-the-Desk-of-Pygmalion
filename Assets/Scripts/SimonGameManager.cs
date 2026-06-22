using System.Collections;
using UnityEngine;

public class SimonGameManager : MonoBehaviour
{
    [Header("Game Setup")]
    [SerializeField] private int numRows = 2;
    [SerializeField] private int numCols = 2;
    private int numTiles;
    private Tile[] tile;

    [Header("Game Objects")]
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform gameArea;

    [Header("Audio Setup")]
    [SerializeField] private float duration = 0.2f;
    [SerializeField] private AudioSource audioSource;

    enum GameMode {
        None, // no game in progress
        Menu, // waiting to play game
        Listening, // playing the pattern
        Playing // entering the pattern
    }

    private GameMode gameMode = GameMode.None;

    void Start() {
        numTiles = numRows * numCols;
        tile = new Tile[numTiles];

        // Create the grid of tiles
        for(int row = 0; row < numRows; row++) {
            for(int col = 0; col < numCols; col++) {
                int index = (row * numCols) + col;

                // Instantiate the tile objects
                tile[index] = Instantiate(tilePrefab, gameArea);
                tile[index].Init(this, index, Color.HSVToRGB((float)index / numTiles, 0.8f, 0.8f)); // random different colors

                // Center the tiles in the game area
                float rowStart = (numRows / 2f) - 0.5f;
                float colStart = (-numCols / 2f) + 0.5f;
                tile[index].transform.localPosition = new Vector3(colStart + col, rowStart - row, 0); // change z
            }
        }

        // Scale the tiles to fit our vertical space
        float scale = 6f / numRows;
        gameArea.localScale = Vector3.one * scale;

        // Start in the menu game mode (flashing lights and no sound)
        gameMode = GameMode.Menu;
        StartCoroutine(MenuTileAnimation()); // duration being 1.0f during this might be nice
    }

    private IEnumerator MenuTileAnimation() {
        while(gameMode == GameMode.Menu) {
            // Light a random tile
            yield return FlashTile(Random.Range(0, numTiles));
            // Wait before flashing the next one
            yield return new WaitForSeconds(duration);
        }
        }

    private IEnumerator FlashTile(int index) {
        tile[index].TurnOn();
        yield return new WaitForSeconds(duration);
        tile[index].TurnOff();
    }

    public void PlayLightAndTone(int index) {
        StartCoroutine(FlashTile(index));
        PlayTone(index);
    }

    private void PlayTone(int index) {
        // Adjust pitch to create unique sound for each tile
        if(numTiles > 1) {
            audioSource.pitch = Mathf.Lerp(0.5f, 2.0f, index / (numTiles - 1f));
        }

        // Schedule the tone to play
        double currentTime = AudioSettings.dspTime;
        audioSource.PlayScheduled(currentTime);
        audioSource.SetScheduledEndTime(currentTime + duration);
    }
}
