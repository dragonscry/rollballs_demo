using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

    [Serializable]
    public class Count
    {
        public int minimum;             //Minimum value for our Count class.
        public int maximum;             //Maximum value for our Count class.


        //Assignment constructor.
        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }


    public int columns = 17;                                         //Number of columns in our game board.
    public int rows = 17;                                            //Number of rows in our game board.

    public Count obstacleCount = new Count(50, 57);                      //Lower and upper limit for our random number of walls per level.
    public Count pickCount = new Count(20, 28);                      //Lower and upper limit for our random number of food items per level.

    public GameObject player;
    public GameObject exit;                                         //Prefab to spawn for exit.
    public GameObject obstacle;                                 //Array of floor prefabs.
    public GameObject pickup;                                  //Array of wall prefabs.
    public GameObject killbox;                                  //Array of food prefabs.

    private List<Vector3> gridPositions = new List<Vector3>();   //A list of possible locations to place tiles.


    //Clears our list gridPositions and prepares it to generate a new board.
    void InitialiseList()
    {
        //Clear our list gridPositions.
        gridPositions.Clear();

        //Loop through x axis (columns).
        for (int x = -8; x < columns; x++)
        {
            //Within each column, loop through y axis (rows).
            for (int y = -8; y < rows; y++)
            {
                //At each index add a new Vector3 to our list with the x and y coordinates of that position.
                gridPositions.Add(new Vector3(x, 0.5f, y));
            }
        }
    }

    //RandomPosition returns a random position from our list gridPositions.
    Vector3 RandomPosition()
    {
        //Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
        int randomIndex = Random.Range(0, gridPositions.Count);

        //Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
        Vector3 randomPosition = gridPositions[randomIndex];

        //Remove the entry at randomIndex from the list so that it can't be re-used.
        gridPositions.RemoveAt(randomIndex);

        //Return the randomly selected Vector3 position.
        return randomPosition;
    }


    //LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
    void LayoutObjectAtRandom(GameObject tileArray, int minimum, int maximum)
    {
        //Choose a random number of objects to instantiate within the minimum and maximum limits
        int objectCount = Random.Range(minimum, maximum + 1);

        //Instantiate objects until the randomly chosen limit objectCount is reached
        for (int i = 0; i < objectCount; i++)
        {
            //Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
            Vector3 randomPosition = RandomPosition();

            //Choose a random tile from tileArray and assign it to tileChoice
            GameObject tileChoice = tileArray;

            //Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }


    //SetupScene initializes our level and calls the previous functions to lay out the game board
    public void SetupScene(int level)
    {

        //Reset our list of gridpositions.
        InitialiseList();

        //Instantiate a random number of wall tiles based on minimum and maximum, at randomized positions.
        LayoutObjectAtRandom(obstacle, obstacleCount.minimum, obstacleCount.maximum);

        //Instantiate a random number of food tiles based on minimum and maximum, at randomized positions.
        LayoutObjectAtRandom(pickup, pickCount.minimum, pickCount.maximum);

        int killCount = level;

        //Instantiate a random number of food tiles based on minimum and maximum, at randomized positions.
        LayoutObjectAtRandom(killbox, killCount, killCount*killCount);



        //Instantiate the exit tile in the upper right hand corner of our game board
        Instantiate(exit, new Vector3(9f, 0.5f, 9f), Quaternion.identity);
        Instantiate(player, new Vector3(-9f, 0.5f, -9f), Quaternion.identity);

    }
	
}
