using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private int moveAmount = 10;

    public GameObject[] upArray;
    public GameObject[] downArray;
    public GameObject[] leftArray;
    public GameObject[] rightArray;
    public GameObject startUp, startDown, startLeft, startRight;
    public GameObject bossRoom;

    private float tileOffSet = 0.5f; // the canvas is slightly off center

    private int generationDirection; // 1 up, 2 down, 3 left, 4 right

    public int previousRoomDirection;
    public int roomTotal;
    [SerializeField]private int roomCurrent;

    private int randomDirection;
    RoomController roomType;
    // Start is called before the first frame update
    void Start()
    {
        roomType = FindObjectOfType<RoomController>();
        StartGeneration();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) { GenerateNewRoom(); }
    }

    private void StartGeneration()
    {
        randomDirection = Random.Range(11, 15);
        GeneratorDirection(randomDirection);      
    }

    public void GenerateNewRoom()
    {
        GeneratorDirection(previousRoomDirection);
    }

    private void GeneratorDirection(int direction)
    {
        roomCurrent++;
        switch (direction)
        {
            case 1:
                Up();
                break;
            case 2:
                Down();
                break;
            case 3:
                Left();
                break;
            case 4:
                Right();
                break;
            case 5:
                VerticalRoom();
                break;
            case 6:
                HorizontalRoom();
                break;
            case 7:
                DLRoom();
                break;
            case 8:
                DRRoom();
                break;
            case 9:
                ULRoom();
                break;
            case 10:
                URRoom();
                break;
            case 11:
                StartUp();
                break;
            case 12:
                StartDown();
                break;
            case 13:
                StartLeft();
                break;
            case 14:
                StartRight();
                break;
        }
    }

    private void BossRoom()
    {
        Instantiate(bossRoom, transform.position, Quaternion.identity);
    }

    //moves and spawns in 4 directions
    private void Up()
    {
        if (roomCurrent <= roomTotal)
        {
            // moves generator
            Vector2 up = new Vector2(transform.position.x, transform.position.y + moveAmount);
            transform.position = up;
            generationDirection = 1;
            // selects room and spawns
            int randomUp = Random.Range(0, upArray.Length);
            Instantiate(upArray[randomUp], transform.position, Quaternion.identity);
        }
        // spawns boss room 
        else
        {
            Vector2 up = new Vector2(transform.position.x, transform.position.y + moveAmount);
            transform.position = up;
            generationDirection = 1;

            Instantiate(bossRoom, transform.position, Quaternion.identity);
        }
    }
    private void Down()
    {
        if (roomCurrent <= roomTotal)
        {
            Vector2 down = new Vector2(transform.position.x, transform.position.y - moveAmount);
            transform.position = down;
            generationDirection = 2;

            int randomDown = Random.Range(0, downArray.Length);
            Instantiate(downArray[randomDown], transform.position, Quaternion.identity);
        }
        else
        {
            Vector2 down = new Vector2(transform.position.x, transform.position.y - moveAmount);
            transform.position = down;
            generationDirection = 2;

            Instantiate(bossRoom, transform.position, Quaternion.identity);
        }
    }
    private void Left()
    {
        if (roomCurrent <= roomTotal)
        {
            Vector2 left = new Vector2(transform.position.x - moveAmount, transform.position.y);
            transform.position = left;
            generationDirection = 3;

            int randomLeft = Random.Range(0, leftArray.Length);
            Instantiate(leftArray[randomLeft], transform.position, Quaternion.identity);
        }
        else
        {
            Vector2 left = new Vector2(transform.position.x - moveAmount, transform.position.y);
            transform.position = left;
            generationDirection = 3;

            Instantiate(bossRoom, transform.position, Quaternion.identity);
        }
    }
    private void Right()
    {
        if (roomCurrent <= roomTotal)
        {
            Vector2 right = new Vector2(transform.position.x + moveAmount, transform.position.y);
            transform.position = right;
            generationDirection = 4;

            int randomRight = Random.Range(0, rightArray.Length);
            Instantiate(rightArray[randomRight], transform.position, Quaternion.identity);
        }
        else
        {
            Vector2 right = new Vector2(transform.position.x + moveAmount, transform.position.y);
            transform.position = right;
            generationDirection = 4;

            Instantiate(bossRoom, transform.position, Quaternion.identity);
        }
    }
    //looks at what direction the generator is spawning so that it can place rooms accordingly
    private void VerticalRoom()
    {
        if (generationDirection == 1) { Up(); }
        else if (generationDirection == 2) { Down(); }
    }
    private void HorizontalRoom()
    {
        if (generationDirection == 3) { Left(); }
        else if (generationDirection == 4) { Right(); }
    }
    private void DLRoom()
    {
        if (generationDirection == 1) { Left(); }
        else if (generationDirection == 4) { Down(); }
    }
    private void DRRoom()
    {
        if (generationDirection == 1) { Right(); }
        else if (generationDirection == 3) { Down(); }
    }
    private void ULRoom()
    {
        if (generationDirection == 2) { Left(); }
        else if (generationDirection == 4) { Up(); }
    }
    private void URRoom()
    {
        if (generationDirection == 2) { Right(); }
        else if (generationDirection == 3) { Up(); }
    }
    //the starting directions / special starting zones
    private void StartUp()
    {
        Vector2 up = new Vector2(tileOffSet, tileOffSet);
        transform.position = up;
        generationDirection = 1;

        Instantiate(startUp, transform.position, Quaternion.identity);
    }
    private void StartDown()
    {
        Vector2 down = new Vector2(tileOffSet, tileOffSet);
        transform.position = down;
        generationDirection = 2;

        Instantiate(startDown, transform.position, Quaternion.identity);
    }
    private void StartLeft()
    {
        Vector2 left = new Vector2(tileOffSet, tileOffSet);
        transform.position = left;
        generationDirection = 3;

        Instantiate(startLeft, transform.position, Quaternion.identity);
    }
    private void StartRight()
    {
        Vector2 right = new Vector2(tileOffSet, tileOffSet);
        transform.position = right;
        generationDirection = 4;

        Instantiate(startRight, transform.position, Quaternion.identity);
    }
}
