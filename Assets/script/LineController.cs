using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour {

    public CityV2[] cities;

    public bool isModifie = false;

    private void OnMouseDown()
    {
        if (!isModifie)
        {
            isModifie = true;
            if (Input.GetKey(KeyCode.LeftControl))
            {
                gameObject.GetComponent<LineRenderer>().material = GameManager.instance.barrage;
            }
            else
            {
                gameObject.GetComponent<LineRenderer>().material = GameManager.instance.road;

                cities[0].linkCities.Add(cities[1]);
                cities[1].linkCities.Add(cities[0]);
                cities[0].checkAppartenance();
                cities[1].checkAppartenance();
            }
			GameManager.instance.ChangeTurn();
        }

    }
}
