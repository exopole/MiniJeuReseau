using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    static public GameManager instance;

    public bool isPlayer1Turn = true;
    public Player player1;
    public Player player2;

	public Text MainTextInfoDisplay;
	public Text positionsLeftCount;
    public Text textScoreP1;
    public Text textScoreP2;
	public Text player1Name;
	public Text player2Name;

	public GameObject backToMenuEndGameButton;

    private int pointsP1 = 0;
    private int pointsP2 = 0;

    public Material road;
    public Material barrage;

    public CityV2[] cities;
    
	public List<CityV2> citiesPlayer1;
    public List<CityV2> citiesPlayer2;

	public string[] AINames;

    public LineController[] lines;



    public int positionPossible = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            positionPossible += cities.Length + lines.Length;
			positionsLeftCount.text = positionPossible.ToString ();
        }
        else
            Destroy(gameObject);
    }

	void Start()
	{
		StartProcedure();
	}


	public void StartProcedure()
	{
		player1Name.text = player1.name;
        player2.name = AINames[Random.Range(0, AINames.Length)];
        player2Name.text = player2.name;
        MainTextInfoDisplay.text = "Your Turn " + player1.name + "!";
				
	}

    public IEnumerator ShowInfo(string info, float displayTime)
    {
        MainTextInfoDisplay.enabled = true;
        MainTextInfoDisplay.text = info;
        yield return new WaitForSecondsRealtime(displayTime);
        MainTextInfoDisplay.enabled = false;

    }
    

    public void addCity(CityV2 city)
    {
        if (isPlayer1Turn)
        {
            if (!citiesPlayer1.Contains(city))
            {
                citiesPlayer1.Add(city);
                setPoint(1);
                textScoreP1.text = pointsP1.ToString();
            }
            else
            {
                Debug.Log("Player : Try to add a City already add");
            }
        }
        else
        {
            if (!citiesPlayer2.Contains(city))
            {
                citiesPlayer2.Add(city);
                setPoint(1);
            }
            else
            {
                Debug.Log("Player : Try to add a City already add");
            }
        }
    }

    public void removeCity(CityV2 city)
    {
        if (isPlayer1Turn)
        {
            if (citiesPlayer1.Contains(city))
            {
                citiesPlayer1.Remove(city);
                setPoint(-1);
            }
            else
            {
                Debug.Log("Player : Try to remove a City not here");
            }
        }
        else
        {
            if (citiesPlayer2.Contains(city))
            {
                citiesPlayer2.Remove(city);
                setPoint(-1);
            }
            else
            {
                Debug.Log("Player : Try to remove a City not here");
            }
        }
    }

    public void printCities()
    {
        string result = "players 1 cities => ";
        foreach (CityV2 city in citiesPlayer1)
        {
            result += city.gameObject.name;
        }
        Debug.Log(result);

        result = "players 2 cities => ";
        foreach (CityV2 city in citiesPlayer2)
        {
            result += city.gameObject.name;
        }
        Debug.Log(result);
    }

    

    public void setPoint(int point)
    {
        if (isPlayer1Turn)
        {
            pointsP1 += point;
            textScoreP1.text = pointsP1.ToString();
        }
        else
        {
            pointsP2 += point;
            textScoreP2.text = pointsP2.ToString();
        }
    }

    public void setPoint(int point, bool isP1)
    {
        if (isP1)
        {
            pointsP1 += point;
            textScoreP1.text = pointsP1.ToString();
        }
        else
        {
            pointsP2 += point;
            textScoreP2.text = pointsP2.ToString();
        }
    }

	public void ChangeTurn()
	{
		isPlayer1Turn = !isPlayer1Turn;
		if (isPlayer1Turn) 
		{
			MainTextInfoDisplay.text = "YOUR TURN " + player1.name + "!";
		}
        else
        {
            MainTextInfoDisplay.text = "YOUR TURN " + player2.name + "!";
        }
		ChangePositionPossible (-1);
	}

	public void ChangePositionPossible(int i)
	{
		positionPossible += i;
		positionsLeftCount.text = positionPossible.ToString ();
		if (positionPossible == 0) 
		{
			FinishTheGame ();
		}
	}

	public void FinishTheGame()
	{
		StartCoroutine (EndOfGame ());
	}

	IEnumerator EndOfGame()
	{
		backToMenuEndGameButton.SetActive (true);
		StartCoroutine( ShowInfo ("The Game will restart in 10 seconds...", 10f));
		yield return new WaitForSecondsRealtime (10f);
		SceneManager.LoadScene (1);
	}

	public void GoBackToMenu()
	{
		StopCoroutine ("EndOfGame");
		SceneManager.LoadScene (0);
	}
}
