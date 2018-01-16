using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlateauScript : MonoBehaviour {

    public int[,] plateau;
    public int size = 4;
    public int size2 = 5;
    public int nbrePiont;
    public int percentTaken = 60;
    public Text txt;

    public GameObject[] position;
    
    // Use this for initialization
	void Start () {
        nbrePiont = size * size2 * percentTaken /100 ;
        plateau = new int[size, size2];

        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size2; j++)
            {
                plateau[i,j] = ((Random.value *100) <= percentTaken) ? 1 : 0;
            }
        }
        Debug.Log(plateau);

        txt.text = ArrayUtils.printArray2D(plateau, size, size2);


        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size2; j++)
            {
                if (plateau[i, j] == 1)
                {
                    position[i * size2 + j].SetActive(true);
                }
            }
        }
    }
	
	
}
