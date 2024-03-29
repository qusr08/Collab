﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : WireComponent {
	[Header("Variables")]
	[SerializeField] bool isBackground = false;
	[SerializeField] int formation = -1; // 0 = short, 1 = straight, 2 = turn
	[Header("Sprites")]
	[SerializeField] Sprite[ ] shortVariations = null; // 0 = off foreground, 1 = on foreground, 2 = off background, 3 = on background
	[SerializeField] Sprite[ ] straightVariations = null;
	[SerializeField] Sprite[ ] turnVariations = null;

	Sprite[ ][ ] variations;

	#region Unity Methods

	void Start ( ) {
		variations = new Sprite[ ][ ] {
			shortVariations, straightVariations, turnVariations
		};

		if (isBackground) {
			spriteRenderer.sortingLayerName = "Background Objects";
		}

		SetActive(isActive);
	}

	#endregion

	#region Methods

	protected override void UpdateSprites ( ) {
		spriteRenderer.sprite = variations[formation][(isBackground ? 2 : 0) + (isActive ? 1 : 0)];
	}

	#endregion
}
