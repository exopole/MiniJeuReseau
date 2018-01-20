using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour 
{
	public string playerName;
	public Text playerNameDisplay;
	public bool FirstTimePlaying;
	public int numberOfGamesPlayed;
	public int numberOfWins;
	public int numberOfLosses;
	public Button startOnlineGameBtn;

	void Start()
	{
		if(!PlayerPrefs.HasKey("PLAYER_NAME"))
		{
			PlayerPrefs.SetString ("PLAYER_NAME", "NewPlayer");
			PlayerPrefs.SetInt ("WINS", 0);
			PlayerPrefs.SetInt ("LOSSES", 0);

		}
		playerName = PlayerPrefs.GetString ("PLAYER_NAME");
		playerNameDisplay.text = playerName;
		numberOfWins = PlayerPrefs.GetInt ("WINS");
		numberOfLosses = PlayerPrefs.GetInt ("LOSSES");
		numberOfGamesPlayed = numberOfWins + numberOfLosses;

	}
//
//	public void StartNewGame()
//	{
//		SceneManager.LoadScene (1);
//	} 
//
	public void QuitGame()
	{
		Application.Quit ();
	}
	public void ChangeMyPlayerName(string name)
	{
		playerName = name;
		PlayerPrefs.SetString ("PLAYER_NAME", name);
		playerNameDisplay.text = playerName;

	}
		
}
