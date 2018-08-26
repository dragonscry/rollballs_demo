using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public BoardManager boardScript;

    [SerializeField]
    private GameObject[] whenLeft;

    private int level = 1;

    [SerializeField]
    private int picks;

    [SerializeField]
    GameObject exit;


	// Use this for initialization
	void Awake () {

        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        boardScript = GetComponent<BoardManager>();
        InitGame();
		
	}

    private void Start()
    {
        
  
        howManyLeft();


    }

    private void Update()
    {
        howManyLeft();
    }

    private void OnLevelWasLoaded(int index)
    {
        level++;
        InitGame();

    }

    void InitGame()
    {
        boardScript.SetupScene(level);
        Debug.Log(level);
        exit = GameObject.FindWithTag("Finish");
        exit.SetActive(false);
        whenLeft = GameObject.FindGameObjectsWithTag("Pick Up");
        picks = whenLeft.Length;

    }

    public void GameOver()
    {
        level = 0;
        SceneManager.LoadScene(0);
    }

    void howManyLeft()
    {
        whenLeft = GameObject.FindGameObjectsWithTag("Pick Up");
        if (whenLeft.Length < 0.5 * picks)
        {
            exit.SetActive(true);

        }
    }
	
	
}
