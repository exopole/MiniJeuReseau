using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour {

    public CityV2[] cities;

    public bool isModifie = false;

    private void OnMouseUp()
    {
        if (!isModifie)
        {
            GameManager.instance.takeLink(gameObject);
        }

    }

    public void setLink(Material material, bool isRoad)
    {
        isModifie = true;
        gameObject.GetComponent<LineRenderer>().material = material;
        if (isRoad)
        { 
            cities[0].linkCities.Add(cities[1]);
            cities[1].linkCities.Add(cities[0]);
            cities[0].checkAppartenance();
            cities[1].checkAppartenance();
        }
    }
}
