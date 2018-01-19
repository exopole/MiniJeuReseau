using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkGameManager : NetworkBehaviour {

	//le serveur est toujours "player1"
	//on utilise donc isServer pour savoir si on a affaire au joueur1 ou 2.
	//on utilise "isplayer1Turn" et isServer pour savoir si c'est ton tour ou pas.
	//une commande sur PlayerNetworkManager permet de changer le isplayer1Turn (sur le playerObj trouvable dans Gamemanager)
	public static NetworkGameManager instance;
	[SyncVar]public bool GameHasBegun;
	[SyncVar(hook = "ActuPlayerTurn")] public bool isPlayer1Turn;
	public Material[] matPlayerTurn;
	public Material[] matOpponentTurn;

	//référencement de la fonction static.
	void Awake()
	{
		if (instance != null) 
		{
			Destroy (this);
		}
		instance = this;
	}
		
	//arrive qu'une fois que chez le serveur.
	public override void OnStartServer ()
	{
		base.OnStartServer ();

		//on initialise les ID des points d'interets qui sont des syncvar.
		for (int i = 0; i < GameManager.instance.cities.Length; i++) 
		{
			GameManager.instance.cities [i].cityID = i;

		}
		for (int j = 100; j < GameManager.instance.lines.Length+100; j++) 
		{
			int k = j - 100;
			GameManager.instance.lines [k].lineID = j;

		}

	}
		
	//hook. Pas besoin de run sur le serveur.
	public void ActuPlayerTurn(bool isP1Turn)
	{
		if (!isServer) 
		{
			ChangePlayerTurn ();
		}
	}

	public void BeginTheGame()
	{
		GameHasBegun = true;
		StartCoroutine(GameManager.instance.ShowInfo("You play first!", 1f));
		GameManager.instance.PlateauMeshR.materials = matPlayerTurn;

	}

	//appeler chez le serveur pour changer le tour.
	//appeler chez le client qui est pas serveur via hook callback.
	public void ChangePlayerTurn()
	{
		isPlayer1Turn = !isPlayer1Turn;
		GameManager.instance.ChangePositionPossible (-1);
		if (isServer) 
		{
			if (isPlayer1Turn) //si c'est ton tour et t'es serveur
			{
				StartCoroutine( GameManager.instance.ShowInfo ("Your Turn", 1f));
				GameManager.instance.PlateauMeshR.materials = matPlayerTurn;
			} else 
			{
				GameManager.instance.PlateauMeshR.materials= matOpponentTurn;

			}
		} else 
		{
			if (!isPlayer1Turn) //si c'est ton tour que t'es pas serveur.
			{
				StartCoroutine( GameManager.instance.ShowInfo ("Your Turn", 1f));
				GameManager.instance.PlateauMeshR.materials = matOpponentTurn;

			} else 
			{
				GameManager.instance.PlateauMeshR.materials = matPlayerTurn;

			}
		}

	}
}
