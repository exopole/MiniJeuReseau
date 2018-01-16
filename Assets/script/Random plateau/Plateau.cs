using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateau : MonoBehaviour {

    public int nbrVille;
    public int widthMax = 4;
    public int hightMax = 5;

    public int hight = 1;
    public int width = 1;

    public GameObject cityPrefab;
    public List<City> cities;
    List<City> citiesEdge;

    private void Start()
    {
        cities = new List<City>();
        City city = Instantiate(cityPrefab).GetComponent<City>();
        City linkCity;
        cities.Add(city);
        citiesEdge = new List<City>() { cities[0]}; 
        int countVille = 1;
        int link;
        int lineNewCity;
        int colNewCity;

        float separation = 3;

        while (countVille < nbrVille)
        {
            int indexCity = Random.Range(0, cities.Count -1);
            city = cities[indexCity];
            link = city.buildALink();
            lineNewCity = city.line;
            colNewCity = city.column;
            getCoordonateFromLink(ref lineNewCity,ref colNewCity, link);
            linkCity = CityIsExiting(lineNewCity, colNewCity);

            if (linkCity)
            {
                city.addCityLink(linkCity.gameObject, link);
                city.addCityLinkInverse(linkCity.gameObject, link);
            }
            else
            {
                linkCity = Instantiate(cityPrefab).GetComponent<City>();
                linkCity.gameObject.transform.position = new Vector3(colNewCity * separation + separation, 0, lineNewCity * separation + separation);

                linkCity.setCoordonate(lineNewCity, colNewCity);

                city.addCityLink(linkCity.gameObject, link);
                linkCity.addCityLinkInverse(linkCity.gameObject, link);

                citiesEdge.Add(linkCity);
                cities.Add(linkCity);
                countVille++;
            }
            if(city.nbrLink == 8)
            {
                citiesEdge.Remove(city);
            }
            if(linkCity.nbrLink == 8)
            {
                citiesEdge.Remove(linkCity);
            }
            //return;
            
        }


    }


    public void getCoordonateFromLink(ref int line,ref int column, int link)
    {
        switch (link)
        {
            case 0:
                line++;
                break;
            case 1:
                line++;
                column++;
                break;
            case 2:
                column++;
                break;
            case 3:
                line--;
                column++;
                break;
            case 4:
                line--;
                break;
            case 5:
                line--;
                column--;
                break;
            case 6:
                column--;
                break;
            case 7:
                line++;
                column--;
                break;
            default:
                break;
        }
    }



    

    public City CityIsExiting(int line, int column)
    {
        int i = 0;
        while(i < citiesEdge.Count && !citiesEdge[i].isGoodCity(line, column))
        {
            i++;
        }
        return (i != citiesEdge.Count) ? citiesEdge[i] : null;
    }
}
