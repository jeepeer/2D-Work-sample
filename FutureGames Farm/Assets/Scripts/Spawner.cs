using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // buildings
    public GameObject building;
    public GameObject farm;
    public GameObject mine;
    public GameObject castle;
    public GameObject[] buildings;
    public int cost = 5;

    public GameObject AttackerPrefab;

    public GameObject gameControllerObject;
    GameController game;

    // Start is called before the first frame update
    void Start()
    {
        game = gameControllerObject.GetComponent<GameController>();

    }

    // Update is called once per frame
    void Update()
    {

        SelectBuilding(); // spawn button
    }

    public void SelectBuilding()//selects which building should be placed down deppending on number pressed
    {
        if(!game.gameIsPaused)
        {   //mine
            if (Input.GetKeyDown(KeyCode.Alpha1)) { Farm(); }
            //farm
            else if (Input.GetKeyDown(KeyCode.Alpha2)) { Mine(); }
            //castle
            else if (Input.GetKeyDown(KeyCode.Alpha3)) { Castle(); }
        }
    }
    // how buildings spawn
    public void SpawnBuilding(Vector2 worldPos)
    {
        Instantiate(building, worldPos, Quaternion.identity);
    }

    public void Farm()
    {
        building = buildings[0];
        game.BuildMode();
    }
    public void Mine()
    {
        building = buildings[1];
        game.BuildMode();
    }
    public void Castle()
    {
        building = buildings[2];
        game.BuildMode();
    }


    public void AttackerSpawner()
    {
        float randomX = Random.Range(10, 31);
        float randomY = Random.Range(10, 31);
        Vector2 randomVector = new Vector2(randomX, randomY);
        Instantiate(AttackerPrefab, randomVector, Quaternion.identity);
    }

}
