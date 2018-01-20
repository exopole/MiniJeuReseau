using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LineController : NetworkBehaviour {

    public CityV2[] cities;
	[SyncVar]public int lineID;
    public bool isModified = false;
	public Material matNormal;
	public Material matHover;
	public Material matHoverBarrage;
	LineRenderer lineR;
	Material tmpMat; //utiliser pour faire clignoter.

	void Start()
	{
		lineR = GetComponent<LineRenderer> ();
	}
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
	void OnMouseEnter()
	{
		if (!isModified) {
			if (Input.GetKey (KeyCode.LeftControl)) 
			{
				lineR.material = matHoverBarrage;

				return;
			}
			GameManager.instance.ChangeCursor (true);
			lineR.material = matHover;
		}
	}

	void OnMouseExit()
	{
		if (!isModified) {
			
			GameManager.instance.ChangeCursor (false);
			lineR.material = matNormal;

		}
	}

	[ClientRpc]
	public void RpcChangeTheLineToRoad()
	{
		isModified = true;
		lineR.material = GameManager.instance.road;
		cities[0].linkCities.Add(cities[1]);
		cities[1].linkCities.Add(cities[0]);
		cities[0].checkAppartenance();
		cities[1].checkAppartenance();
		StartCoroutine (AfterCaptureProcedure ());

	}

	[ClientRpc]
	public void RpcChangeTheLineToBarrage()
	{
		isModified = true;
		lineR.material = GameManager.instance.barrage;
		StartCoroutine (AfterCaptureProcedure ());

	}
	IEnumerator AfterCaptureProcedure()
	{
		tmpMat = lineR.material;
		yield return new WaitForSecondsRealtime (.3f);
		lineR.material = matNormal;
		yield return new WaitForSecondsRealtime (.3f);
		lineR.material = tmpMat;
		yield return new WaitForSecondsRealtime (.3f);
		lineR.material = matNormal;
		yield return new WaitForSecondsRealtime (.2f);
		lineR.material = tmpMat;
		yield return new WaitForSecondsRealtime (.2f);
		lineR.material = matNormal;
		yield return new WaitForSecondsRealtime (.1f);
		lineR.material = tmpMat;

	}
}
