﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    static public GameManager instance;

    public bool isPlayer1 = true;
    public Player player1;
    public Player player2;

    public Text textScoreP1;
    public Text textScoreP2;

    
    private int pointsP1 = 0;
    private int pointsP2 = 0;

    public Material road;
    public Material barrage;

    public CityV2[] cities;
    public List<CityV2> citiesPlayer1;
    public List<CityV2> citiesPlayer2;


    public LineController[] lines;



    public int positionPossible = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            positionPossible += cities.Length + lines.Length;
        }
        else
            Destroy(gameObject);
    }


    public void addCity(CityV2 city)
    {
        if (isPlayer1)
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
        if (isPlayer1)
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
        if (isPlayer1)
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

}
