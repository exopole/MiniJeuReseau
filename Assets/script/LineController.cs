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
            if (Input.GetMouseButton(0))
            {
                gameObject.GetComponent<LineRenderer>().material = GameManager.instance.road;
            }
            else
            {
                gameObject.GetComponent<LineRenderer>().material = GameManager.instance.barrage;
            }
            GameManager.instance.isPlayer1 = !GameManager.instance.isPlayer1;
        }

    }
}
