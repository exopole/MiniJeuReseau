using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LineController : NetworkBehaviour {

    public CityV2[] cities;
	[SyncVar]public int lineID;
    public bool isModified = false;

    private void OnMouseDown()
	{
		if (NetworkGameManager.instance.GameHasBegun) {
			
			if (!isModified) {
				if (NetworkGameManager.instance.isPlayer1Turn && isServer || !NetworkGameManager.instance.isPlayer1Turn && !isServer) {
					if (Input.GetKey (KeyCode.LeftControl)) {
						GameManager.instance.localPlayerObj.GetComponent<PlayerNetworkManager> ().CaptureLineMakeBarrage (lineID);
						isModified = true;
						return;

					} else {	
						GameManager.instance.localPlayerObj.GetComponent<PlayerNetworkManager> ().CaptureLineMakeRoad (lineID);
						isModified = true;
					}
				}
			}
		}
	}

	[ClientRpc]
	public void RpcChangeTheLineToRoad()
	{
		isModified = true;
		gameObject.GetComponent<LineRenderer>().material = GameManager.instance.road;
		cities[0].linkCities.Add(cities[1]);
		cities[1].linkCities.Add(cities[0]);
		cities[0].checkAppartenance();
		cities[1].checkAppartenance();

	}

	[ClientRpc]
	public void RpcChangeTheLineToBarrage()
	{
		isModified = true;
		gameObject.GetComponent<LineRenderer>().material = GameManager.instance.barrage;

	}
}
