/*
* Modeled after singleton pattern
* Sources used: Unity 2d Roguelike tutorial Game Manager pages
* Unity tanks tutorial Game Manager
* wiki.unity3d.com/index.hp/Singleton
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public int num_of_lvls = 1;
	public int playerLives;
	public int currlvl;
	public static GameManager instance = null;	//static instance of GameManager
	public LevelManager levelScript;

	void OnEnable() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		if (GameObject.FindWithTag("lvl") != null) {
			levelScript = GameObject.FindWithTag("lvl").GetComponent<LevelManager>();
		}
		else
		levelScript = null;
	}

	private void Awake() {
		//If there is no GameManager, make a new one
		if (instance == null) {
			//Set instance to this
			instance = this;
		}
		//If instance already exists and it's not this
		else if (instance != this) {
			//Destroy this (the about to be made GameManager)
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject); 	//Don't destroy when reloading the scene
		InitializeGame();		//Intialize the game, beginning at Main Menu
	}
	void Update() {
		//If the level is the main menu, there is no level manager
		//If there is a level script, play the game and track progress
		if (levelScript != null) {
			if (levelScript.level_complete) {
				currlvl++;
				playerLives = 3;
				if (SceneManager.sceneCountInBuildSettings == currlvl) {
					GameDone();
				}
				else {
					SceneManager.LoadScene(currlvl);
				}

			}
			else if (levelScript.is_dead) {
				playerLives--;
				if (playerLives != 0) {
					SceneManager.LoadScene(currlvl);
					levelScript.is_dead = false;
				}
			}

			if (playerLives == 0) {
				GameOver();
			}
		}
	}

	public void InitializeGame() {
		currlvl = 1;
		SceneManager.LoadScene("main_menu");
	}

	void GameDone() {
		levelScript = null;
		SceneManager.LoadScene("game_won");
	}

	public void PlayGame() {
		//If the current level is the start menu, initialize currlvl to 1 for 1st level
		//Otherwise current level is an actual game level and leave as is
		if (currlvl == 1) {
			currlvl = 2;
		}
		playerLives = 3;
		//Load the current level and get the level manager
		SceneManager.LoadScene(currlvl);
		//If not in the main menu
		//Get a component reference to the attached level manager script
		//levelScript = SceneManager.GetSceneAt(0).GetRootGameObjects()[0].GetComponent<LevelManager>();
	}

	public void GameOver() {
		SceneManager.LoadScene("game_over");
	}
}
