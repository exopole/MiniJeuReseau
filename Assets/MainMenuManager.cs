using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour 
{
	public Player player;
	public Text playerNameDisplay;
	public bool FirstTimePlaying;

	void Start()
	{
		playerNameDisplay.text = player.name;
	}

	public void StartNewGame()
	{
		SceneManager.LoadScene (1);
	} 

	public void QuitGame()
	{
		Application.Quit ();
	}
	public void ChangeMyPlayerName(string name)
	{
		player.name = name;
		playerNameDisplay.text = name;

	}

}
