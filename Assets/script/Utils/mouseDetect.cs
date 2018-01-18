using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseDetect : MonoBehaviour {

    public bool isSelect = false;
    
    public void LateUpdate()
    {
        if (isSelect && Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit))
            {
                Invoke("stopChoice", 0.1f);
            }
            else
                Debug.Log("false");

        }
    }

    public void stopChoice()
    {
        GameManager.instance.stopChoice();
    }
}
