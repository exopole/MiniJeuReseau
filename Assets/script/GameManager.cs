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
    public Image player1Image;
    public Image player2Image;

	public GameObject backToMenuEndGameButton;

    private int pointsP1 = 0;
    private int pointsP2 = 0;

    public Link link;

    public CityV2[] cities;
    
	public List<CityV2> citiesPlayer1;
    public List<CityV2> citiesPlayer2;

	public string[] AINames;

    public LineController[] lines;
    
    public int positionPossible = 0;

    public GameObject objectSelect;

    public GameObject panelLink;
    public GameObject panelCity;

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
        player1Image.color = player1.material.color;
        player2Image.color = player2.material.color;
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
        panelLink.SetActive(false);
        objectSelect = null;
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
    
    public void takeCity(GameObject obj)
    {
        objectSelect = obj;
        gameObject.GetComponent<mouseDetect>().isSelect = true;
        panelCity.SetActive(true);
    }

    public void takeLink(GameObject obj)
    {
        if (objectSelect && objectSelect.GetComponent<LineRenderer>())
        {
            objectSelect.GetComponent<LineRenderer>().material = link.neutralMaterial;
        }

        objectSelect = obj;
        obj.GetComponent<LineRenderer>().material = link.waitingMaterial;
        gameObject.GetComponent<mouseDetect>().isSelect = true;
        panelLink.SetActive(true);
    }

    public void setLink(bool isRoad)
    {
        if (isRoad)
        {
            objectSelect.GetComponent<LineController>().setLink(link.roadMaterial, true);
        }
        else
        {
            objectSelect.GetComponent<LineController>().setLink(link.barrageMaterial, false);
        }
        
        objectSelect.GetComponent<LineController>().isModifie = true;
        panelLink.SetActive(false);
        ChangeTurn();
    }

    public void changeCity()
    {
        if (isPlayer1Turn)
        {
            objectSelect.GetComponent<MeshRenderer>().material = player1.material;
            addCity(objectSelect.GetComponent<CityV2>());
            objectSelect.GetComponent<CityV2>().isP1 = true;
        }
        else
        {
            objectSelect.GetComponent<MeshRenderer>().material = player2.material;
            addCity(objectSelect.GetComponent<CityV2>());
            objectSelect.GetComponent<CityV2>().isP1 = false;
        }
        
        objectSelect.GetComponent<CityV2>().isTaken = true;
        panelCity.SetActive(false);
        ChangeTurn();

    }

    public void stopChoice()
    {
        if (objectSelect)
        {
            objectSelect.GetComponent<LineRenderer>().material = link.neutralMaterial;
            gameObject.GetComponent<mouseDetect>().isSelect = false;
            panelLink.SetActive(false);
        }
        
    }
    
}
