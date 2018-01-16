using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour {

    // 0 => N; 1 => NE;  2 => E; 3 => SE; 4 =>S; 5 => SW; 6 => W; 7 => NW
    public GameObject[] listLinkGameObj = new GameObject[8];

    public int nbrLink = 0;

    public int line = 0;
    public int column = 0;

    public bool isGoodCity(int line, int column)
    {
        return this.line == line && this.column == column;
    }

    public void setCoordonate(int line, int column)
    {
        this.line = line;
        this.column = column;
    }

    public int buildALink()
    {
        GameObject obj;
        int indexCity = Random.Range(0, listLinkGameObj.Length - 1);
        do
        {
            
            obj = listLinkGameObj[indexCity];
            indexCity++;
            if (indexCity == listLinkGameObj.Length)
                indexCity = 0;
        }
        while (obj != null);
        return indexCity;
    }

    public void addCityLink(GameObject city, int indexLink)
    {
        listLinkGameObj[indexLink] = city;
        nbrLink++;
    }

    public void addCityLinkInverse(GameObject city, int indexLink)
    {
        if(indexLink > 3)
        {
            indexLink -= 4;
        }
        else
        {
            indexLink += 4;
        }
        listLinkGameObj[indexLink] = city;
        nbrLink++;
    }


}
