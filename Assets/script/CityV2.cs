using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CityV2 : NetworkBehaviour {

    public List<CityV2> linkCities;
    public bool isTaken = false;
    public bool isP1 = false;
	[SyncVar]public int cityID;
	bool isP1Turn;


    private void OnMouseDown()
	{
		if (NetworkGameManager.instance.GameHasBegun) {
			if (!isTaken) {
				isTaken = true;
				if (NetworkGameManager.instance.isPlayer1Turn && isServer || !NetworkGameManager.instance.isPlayer1Turn && !isServer) {
					isP1Turn = NetworkGameManager.instance.isPlayer1Turn;
					GameManager.instance.localPlayerObj.GetComponent<PlayerNetworkManager> ().CaptureCity (cityID, isP1Turn);

				}

//            if (GameManager.instance.isPlayer1Turn)
//            {
//                gameObject.GetComponent<MeshRenderer>().material =GameManager.instance.player1.material;
//                GameManager.instance.addCity(this);
//                isP1 = true;
//            }
//            else
//            {
//                gameObject.GetComponent<MeshRenderer>().material = GameManager.instance.player2.material;
//                GameManager.instance.addCity(this);
//                isP1 = false;
//            }
//			GameManager.instance.ChangeTurn();
			}
        
		}
	}

	[ClientRpc]
	public void RpcCaptureCity(bool wasP1Turn)
	{
		isTaken = true;

		if (wasP1Turn) 
		{
			gameObject.GetComponent<MeshRenderer>().material =GameManager.instance.player1.material;
			isP1 = true;
			GameManager.instance.addCity(this);
			GameManager.instance.AddPointP1 (false);
		} 
		else 
		{
			gameObject.GetComponent<MeshRenderer>().material = GameManager.instance.player2.material;
			isP1 = false;
			GameManager.instance.addCity(this);
			GameManager.instance.AddPointP2 (false);

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
				GameManager.instance.AddPointP1 (true);

//                GameManager.instance.setPoint(+1, true);
//                GameManager.instance.setPoint(-1, false);
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
				GameManager.instance.AddPointP2 (true);
//                GameManager.instance.setPoint(-1, true);
//                GameManager.instance.setPoint(+1, false);
                isP1 = false;
                foreach (CityV2 city in linkCities)
                {
                    city.checkAppartenance();
                }
            }
        }

    }
}
