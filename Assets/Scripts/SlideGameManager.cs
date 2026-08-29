//important for lists or arrays
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideGameManager : MonoBehaviour
{
    [SerializeField] private Transform gameTransform;
    [SerializeField] private Transform piecePrefab;

    private List<Transform> pieces;
    private int emptyLocation;
    private int size;
    private int completions;
    private bool shuffling = false;

    //creates the game, the parameter being the thickness between tiles
    private void CreateGamePieces(float gapThickness)
    {
        //the width of the tiles
        float width = 1 / (float)size;
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Transform piece = Instantiate(piecePrefab, gameTransform);
                pieces.Add(piece);
                piece.localPosition = new Vector3(-1 + (2 * width * col) + width, +1 - (2 * width * row) - width, 0);
                piece.localScale = ((2 * width) - gapThickness) * Vector3.one;
                piece.name = $"{(row * size) + col}";
                //we want a space empty on the bottom right, this is what does it
                if ((row == size -1) && (col == size -1))
                {
                    emptyLocation = (size * size) - 1;
                    piece.gameObject.SetActive(false);
                } else
                {
                    //This is making the UV coordinate appropriate
                    float gap = gapThickness / 2;
                    Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                    Vector2[] uv = new Vector2[4];
                    // UV coord order : (0,1),(1,1),(0,0),(1,0)
                    uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap));
                    uv[1] = new Vector2((width * (col + 1)) - gap, 1 - ((width * (row + 1)) - gap));
                    uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap));
                    uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap));
                    //Assigns the new UVs to the mesh
                    mesh.uv = uv;
                }
            }
        }      
    }

    private bool CheckCompletion()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].name != $"{i}")
            {
                //returns false until puzzle is completed
                return false;
            }
        }
        //returns true when the puzzle is completed, can be used here if you want to do other things/activate other thigns once the puzzle is completed
        Debug.Log("completed");
        return true;
    }

    private IEnumerator WaitShuffle(float duration)
    {
        yield return new WaitForSeconds(duration);
        Shuffle();
        shuffling = false;
    }

    private void Shuffle()
    {
        int count = 0;
        int last = 0;
        while (count < (size * size * size))
        {
            //picks a random location
            int rnd = Random.Range(0, size * size);
            if (rnd == last) { continue; }
            last = emptyLocation;
            //tries surrounding spaces looking for valid moves
            if (SwapIfValid(rnd, -size, size))
            {
                count++;
            } else if (SwapIfValid(rnd, +size, size))
            {
                count++;
            } else if (SwapIfValid(rnd, -1, 0))
            {
                count++;
            } else if (SwapIfValid(rnd, +1, size-1))
            {
                count++;
            }
        }
    }
    private int difficulty;
    private GameObject roomNum;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pieces = new List<Transform>();
        roomNum = GameObject.Find("Doorway");
        //difficulty = roomNum.GetComponent<NewRoomTrigger>().rooms;
        pieces = new List<Transform>();
        //size of the board, increment these if statements based on how many scenes are being laoded/unloaded
        /*if (difficulty < 4)
        {
            size = 3;
        }
        else if (difficulty > 3 && difficulty < 8)
        {
            size = 4;
        }
        else if (difficulty > 8 && difficulty < 14)
        {
            size = 5;
        }*/
        size = 5;
        completions = 0;
        CreateGamePieces(0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!shuffling && CheckCompletion())
        {
            //checks if the puzzle is completed and shuffled, since the shuffle function shuffles the puzzle and then sets shuffling to false
            //this then sets shuffling to true so the puzzle gets shuffled again, which within the shuffle function it sets itself to false again
            //i noticed it starts completed, and then shuffles after being "completed" once
            //so what this completions value is for is to check if the player themself has actually completed the puzzle, 
            //since it needs to be completed twice in order to actually have been completed by the player
            completions++;
            if (completions == 2)
            {
                // this could also just be destroy, i dont think it makes much of a difference
                this.gameObject.SetActive(false);
            }
            shuffling = true;
            StartCoroutine(WaitShuffle(0.5f));
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                for (int i = 0; i < pieces.Count; i++)
                {
                    if (pieces[i] == hit.transform)
                    {
                        //checks each direction to see if it can move
                        //breaks on success
                        if (SwapIfValid(i, -size, size)) { break; }
                        if (SwapIfValid(i, +size, size)) { break; }
                        if (SwapIfValid(i, -1, 0)) { break; }
                        if (SwapIfValid(i, +1, size - 1)) { break; }

                    }
                }
            }
        }
    }
    private bool SwapIfValid(int i, int offset, int colCheck)
    {
        if ((i % size != colCheck) && ((i + offset) == emptyLocation))
        {
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
            (pieces[i].localPosition, pieces[i + offset].localPosition) = ((pieces[i + offset].localPosition, pieces[i].localPosition));
            emptyLocation = i;
            return true;
        }
        return false;
    }
}
