using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour 
{
	public AudioClip clic1Snd;

	public string playerName;
	public Text playerNameDisplay;
	public bool FirstTimePlaying;
	public int numberOfGamesPlayed;
	public int numberOfWins;
	public int numberOfLosses;
	public Button startOnlineGameBtn;

	public Text winsTxt;
	public Text lossesTxt;
	public Text gamesPlayedTxt;

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
		winsTxt.text = "Wins: "+numberOfWins;
		lossesTxt.text = "Losses: " + numberOfLosses;
		gamesPlayedTxt.text = numberOfGamesPlayed + " Games played!";
	}
//
//	public void StartNewGame()
//	{
//		SceneManager.LoadScene (1);
//	} 
//
	public void QuitGame()
	{
		GetComponent<AudioSource> ().PlayOneShot (clic1Snd);

		Application.Quit ();
	}
	public void ChangeMyPlayerName(string name)
	{
		playerName = name;
		PlayerPrefs.SetString ("PLAYER_NAME", name);
		playerNameDisplay.text = playerName;
		GetComponent<AudioSource> ().PlayOneShot (clic1Snd);

	}
		
}
