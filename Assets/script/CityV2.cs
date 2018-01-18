﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityV2 : MonoBehaviour {

    public List<CityV2> linkCities;
    public bool isTaken = false;
    public bool isP1 = false;

    private void OnMouseUp()
    {
        if (!isTaken)
        {
            isTaken = true;
            if (GameManager.instance.isPlayer1Turn)
            {
                gameObject.GetComponent<MeshRenderer>().material = GameManager.instance.player1.material;
                GameManager.instance.addCity(this);
                isP1 = true;
            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().material = GameManager.instance.player2.material;
                GameManager.instance.addCity(this);
                isP1 = false;
            }
            GameManager.instance.ChangeTurn();
            //GameManager.instance.takeCity(gameObject);
        }
        
    }

	void OnMouseEnter()
	{
		
	}

	void OnMouseExit()
	{
		
	}

    public void checkAppartenance()
    {
        if (isTaken)
        {

            int cityP1 = (isP1) ? 1 : 0;
            int cityP2 = (isP1) ? 0 : 1;
            foreach (CityV2 city in linkCities)
            {
                if (city.isP1)
                {
                    cityP1++;
                }
                else
                {
                    cityP2++;
                } 
            }
            if(cityP1>cityP2 && !isP1)
            {
                gameObject.GetComponent<MeshRenderer>().material = GameManager.instance.player1.material;
                GameManager.instance.addCity(this);
                GameManager.instance.setPoint(+1, true);
                GameManager.instance.setPoint(-1, false);
                isP1 = true;
                foreach (CityV2 city in linkCities)
                {
                    city.checkAppartenance();
                }
            }
            else if (cityP1 < cityP2 && isP1)
            {
                gameObject.GetComponent<MeshRenderer>().material = GameManager.instance.player2.material;
                GameManager.instance.addCity(this);
                GameManager.instance.setPoint(-1, true);
                GameManager.instance.setPoint(+1, false);
                isP1 = false;
                foreach (CityV2 city in linkCities)
                {
                    city.checkAppartenance();
                }
            }
        }

    }
}
