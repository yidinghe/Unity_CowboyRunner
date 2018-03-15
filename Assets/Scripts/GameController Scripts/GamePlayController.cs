using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayController : MonoBehaviour
{

	[SerializeField]
	private GameObject pausePanel;

	[SerializeField]
	private Button restartGameButton;

	[SerializeField]
	private Text scoreText, pauseText, highScoreText;

	private int score;

	// Use this for initialization
	void Start ()
	{
		scoreText.text = score + "M"; 
		StartCoroutine (CountScore ());
	}

	IEnumerator CountScore ()
	{
		yield return new WaitForSeconds (0.6f);
		score++;
		scoreText.text = score + "M";
		StartCoroutine (CountScore ());
	}

	void OnEnable ()
	{
		PlayerDied.endGame += PlayDiedEndTheGame;
	}

	void OnDisable ()
	{
		PlayerDied.endGame -= PlayDiedEndTheGame;
	}

	void PlayDiedEndTheGame ()
	{
		if (!PlayerPrefs.HasKey ("Score")) {
			PlayerPrefs.SetInt ("Score", 0);
		} else {
			int highScore = PlayerPrefs.GetInt ("Score");
			if (highScore < score) {
				PlayerPrefs.SetInt ("Score", score);
			}
		}

		pauseText.text = "Game Over";
		highScoreText.gameObject.SetActive (true);
		highScoreText.text = "BestScore:  " + PlayerPrefs.GetInt ("Score") + "M";
		pausePanel.SetActive (true);
		restartGameButton.onClick.RemoveAllListeners ();
		restartGameButton.onClick.AddListener (() => RestartGame ());
		Time.timeScale = 0f;
	}

	public void GoToMenu ()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene ("MainMenu");
	}


	public void PauseGame ()
	{
		Time.timeScale = 0f;
		pausePanel.SetActive (true);
		highScoreText.gameObject.SetActive (false);
		restartGameButton.onClick.RemoveAllListeners ();
		restartGameButton.onClick.AddListener (() => ResumeGame ());
	}

	public void RestartGame ()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene ("GamePlay");
	}

	public void ResumeGame ()
	{
		Time.timeScale = 1f;
		pausePanel.SetActive (false);
	}
}
