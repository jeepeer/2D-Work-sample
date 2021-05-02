using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public int roomDirection; // 1 up, 2 down, 3 left, 4 right 
    //5 vertical, 6 horizontal, 7 DL, 8 DR, 9 UL, 10 UR

    public bool mouseExitOn;

    LevelGenerator generator;

    Spawner spawn;
    GameController game;
    // Start is called before the first frame update
    void Start()
    {
        spawn = FindObjectOfType<Spawner>();
        game = FindObjectOfType<GameController>();

        generator = FindObjectOfType<LevelGenerator>();
        generator.previousRoomDirection = roomDirection;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            spawn.SpawnBuilding(game.GetSquareClicked());
        }
    }
    private void OnMouseDown()
    {
        //spawns the selected building and removes cost from approriate "total"
        if (game.buildModeOn)
        {
            //farm
            if (spawn.building == spawn.buildings[0] && game.totalMoney >= spawn.cost)
            {
                spawn.SpawnBuilding(game.GetSquareClicked());
                game.BuildMode();
                game.totalMoney -= spawn.cost;
                game.mineTotal++;
            }
            //mine
            if (spawn.building == spawn.buildings[1] && game.totalFood >= spawn.cost)
            {
                spawn.SpawnBuilding(game.GetSquareClicked());
                game.BuildMode();
                game.totalFood -= spawn.cost;
                game.farmTotal++;
            }
            //castle
            if (spawn.building == spawn.buildings[2] && game.totalMoney >= spawn.cost && game.totalFood >= spawn.cost)
            {
                spawn.SpawnBuilding(game.GetSquareClicked());
                game.BuildMode();
                game.totalMoney -= spawn.cost;
                game.totalFood -= spawn.cost;
                game.castleTotal++;
            }
        }
    }

}
