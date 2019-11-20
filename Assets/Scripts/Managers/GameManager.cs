﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	[Header("Environment")] // Environment GameObjects
	[SerializeField] Transform leverParent = null;
	[SerializeField] Transform doorParent = null;
	[SerializeField] Transform wireParent = null;
	[Header("UI")] // UI Canvases
	[SerializeField] GameObject pauseMenu = null;
	[SerializeField] GameObject completedMenu = null;
	[SerializeField] GameObject inGameUI = null;
	[Header("First Buttons")] // The buttons to be selected first when changing menus
	[SerializeField] GameObject pauseFirstObject = null;
	[SerializeField] GameObject completedFirstObject = null;
	[Header("Level")] // Level variables
	[SerializeField] int movesLeft = 0;
	[SerializeField] Text movesText = null;

	bool isPaused = false; // If the game is paused
	bool isCompleted = false; // If both players has reached the end
	int playerPaused = -1; // The player that has paused the game

	GameObject[ ] levers = null;
	GameObject[ ] doors = null;
	GameObject[ ] wires = null;

	Player player1 = null;
	Player player2 = null;
	Transform spawnpoint = null;
	Transform objective = null;

	EventSystem eventSystem = null;
	StandaloneInputModule inputModule = null;

	#region Unity Methods

	void Awake ( ) {
		levers = Utils.GetAllChildren(leverParent);
		doors = Utils.GetAllChildren(doorParent);
		wires = Utils.GetAllChildren(wireParent);

		player1 = GameObject.Find("Player 1").GetComponent<Player>( );
		player2 = GameObject.Find("Player 2").GetComponent<Player>( );
		spawnpoint = GameObject.Find("Spawnpoint").GetComponent<Transform>( );
		objective = GameObject.Find("Objective").GetComponent<Transform>( );

		eventSystem = EventSystem.current;
		inputModule = eventSystem.GetComponent<StandaloneInputModule>( );
	}

	void Start ( ) {
		completedMenu.SetActive(false);
		pauseMenu.SetActive(false);
		inGameUI.SetActive(true);
	}

	void Update ( ) {
		// Update the moves left text
		movesText.text = movesLeft + "";

		// Check if the players have completed the level
		if (player1.IsAtEnd( ) && player2.IsAtEnd( )) {
			if (!isCompleted) {
				isCompleted = true;

				completedMenu.SetActive(true);
				inGameUI.SetActive(false);

				if (isCompleted) {
					MenuManager.SetInputs(Constants.PLAYER_1_ID, completedFirstObject);
				}
			}
		}
	}

	#endregion

	#region Methods

	public void CompleteLevel () {
		SaveManager.CompleteLevel(SceneManager.GetActiveScene().buildIndex - 1);
	}

	public void Interact (Collider2D collider) {
		for (int i = 0; i < levers.Length; i++) {
			if (collider.bounds.Intersects(levers[i].GetComponent<Collider2D>( ).bounds)) {
				Lever lever = levers[i].GetComponent<Lever>( );

				lever.Toggle( );
				SetWireGroup(lever.IsActive( ), lever.GetID( ));
			}
		}
	}

	public void Pause (int playerID) {
		if (!isPaused) {
			isPaused = true;
			playerPaused = playerID;

			pauseMenu.SetActive(isPaused);
			inGameUI.SetActive(!isPaused);

			MenuManager.SetInputs(playerID, pauseFirstObject);
		}
	}

	public void UnPause ( ) {
		isPaused = false;
		playerPaused = -1;

		pauseMenu.SetActive(isPaused);
		inGameUI.SetActive(!isPaused);
	}

	public void DecrementMoves ( ) {
		if (movesLeft > 0) {
			movesLeft--;
		}

		if (movesLeft == 0) {
			player1.SetModeMenu(false);
			player2.SetModeMenu(false);
		}
	}

	void SetWireGroup (bool isActive, int groupID) {
		for (int i = 0; i < wires.Length; i++) {
			Wire wire = wires[i].GetComponent<Wire>( );

			if (wire.GetID( ) == groupID) {
				wire.SetActive(isActive);
			}
		}

		for (int i = 0; i < doors.Length; i++) {
			Door door = doors[i].GetComponent<Door>( );

			if (door.GetID( ) == groupID) {
				door.SetActive(isActive);
			}
		}
	}

	#endregion

	#region Setters



	#endregion

	#region Getters

	public Transform GetSpawnpoint ( ) {
		return spawnpoint;
	}

	public Transform GetObjective ( ) {
		return objective;
	}

	public GameObject GetPlayer1 ( ) {
		return player1.gameObject;
	}

	public GameObject GetPlayer2 ( ) {
		return player2.gameObject;
	}

	public bool IsPaused ( ) {
		return isPaused;
	}

	public bool IsOutOfMoves ( ) {
		return movesLeft == 0;
	}

	public int GetMovesLeft ( ) {
		return movesLeft;
	}

	#endregion
}
