﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : WireComponent {
	[Header("Sprites")]
	[SerializeField] Sprite[ ] variations = null; // 0 = off, 1 = on
	[Header("Children")]
	[SerializeField] Transform door = null;
	[Header("Variables")]
	[SerializeField] bool isHorizontal = false;

	void Start ( ) {
		SetActive(isActive);
	}

	protected override void UpdateSprites ( ) {
		spriteRenderer.sprite = variations[(isActive ? 1 : 0)];
	}

	void Update ( ) {
		Vector3 doorMove = (isActive ? 1 : -1) * new Vector3(0, Constants.DOOR_MOVESPEED);
		door.localPosition = new Vector3(door.localPosition.x, Utils.Limit(door.localPosition.y + doorMove.y, -2, 1));
	}
}
