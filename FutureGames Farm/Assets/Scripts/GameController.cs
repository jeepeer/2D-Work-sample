using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // building resources
    public int totalPower;
    public int totalFood;
    public int totalMoney;
    public int farmTotal;
    public int mineTotal;
    public int castleTotal;
    public int futureSpendableFood;
    public int futureSpendableMoney;
    public int futureCastles;
    public float enemyCurrentPower;
    // build mode
    public GameObject gameUI;
    public bool buildModeOn;
    private GameObject cursor;
    public GameObject[] cursors;
    // pause
    public bool gameIsPaused;
    public GameObject pauseScreen;
    //timer
    public int minutes;
    public float timer;
    //win lose
    public GameObject winUI;
    public GameObject loseUI;


    public GameObject spawnerObject;
    Spawner spawn;
    public GameObject BuildingObject;
    Building building;
    RoomController roomController;
    // Start is called before the first frame update
    void Start()
    {
        spawn = spawnerObject.GetComponent<Spawner>();
        building = BuildingObject.GetComponent<Building>();
        roomController = FindObjectOfType<RoomController>();
        cursor = cursors[1];
    }

    // Update is called once per frame
    void Update()
    {
        MouseCursor(); 
        ChangeMouseCursor();//changes mouse cursor
        GameTimer();
        PauseGame();


        if (Input.GetKeyDown(KeyCode.X))
        {
            totalPower += 1000;
        }
    }

    // "cursor" follows the mouse as a custom mousecursor
    private void MouseCursor()
    {
        cursor.transform.position = GetSquareClicked();
    }
    // switchting between cursors
    private void ChangeMouseCursor()
    {
        if (buildModeOn && roomController.mouseExitOn == false) { cursor = cursors[0]; ActiveCursor(0); }
        else if(roomController.mouseExitOn == false && !buildModeOn) { cursor = cursors[1]; ActiveCursor(1); }
    }
    //good
    public Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        Vector2 gridPos = SnapToGrid(worldPos);
        return gridPos;
    }
    //good
    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);
        return new Vector2(newX, newY);
    }
    //build mode
    public void BuildMode()
    {
        buildModeOn = !buildModeOn;
        gameUI.SetActive(!buildModeOn);
    }

    private void ActiveCursor(int index)// sets the active cursor in the cursor array to only be visible
    {
        for(int i = 0; i < cursors.Length; i++)
        {
            cursors[i].gameObject.SetActive(i == index);
        }
    }   

    public void WinGame()
    {
        gameIsPaused = true;
        winUI.SetActive(true);
    }
    private void LoseGame()
    {
        gameIsPaused = true;
        loseUI.SetActive(true);
    }
    private void GameTimer()
    {
        if (!gameIsPaused)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                minutes--;
                timer = 60;
            }
            else if (minutes < 0)
            {
                LoseGame();
            }
        }
    }
    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            pauseScreen.SetActive(gameIsPaused);
        }

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    
}
