using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityV2 : MonoBehaviour {

    public List<LineController> links;
    public bool isTaken = false;

    private void OnMouseDown()
    {
        if (!isTaken)
        {
            isTaken = true;
            if (GameManager.instance.isPlayer1)
            {
                gameObject.GetComponent<MeshRenderer>().material =GameManager.instance.player1.material;
                GameManager.instance.addCity(this);
            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().material = GameManager.instance.player2.material;
                GameManager.instance.addCity(this);
            }
            GameManager.instance.isPlayer1 = !GameManager.instance.isPlayer1;
        }
        
    }
}
