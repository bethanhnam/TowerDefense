using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;
	public static GameManager Instance => instance;
    public GameState gameState;
	public bool isPaused = false;
	private void Awake()
	{
		Time.timeScale = 1;
	}
	private void Start()
	{
		if(instance == null)
		{
			instance = this;
		}
		gameState = GameState.Playing;
	}
	public void EndGame()
	{
		//End Game Panel Pop Up
		Time.timeScale = 0;
	}
	public void WinGame()
	{
		// Win Panel Pop Up
		Time.timeScale = 0;
	}
	public void PauseGame()
	{
		// Pause Menu change by isPaused
	}
	public void RePlay()
	{
		SceneManager.LoadScene("GamePlay");
	}
}
