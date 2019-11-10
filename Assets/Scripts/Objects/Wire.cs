﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : Component {
	[Header("Variables")]
	[SerializeField] private bool isBackground;
	[SerializeField] private int formation; // 0 = short, 1 = straight, 2 = turn

	[Header("Sprites")]
	// 0 = off foreground, 1 = on foreground, 2 = off background, 3 = on background
	[SerializeField] private Sprite[ ] shortVariations;
	[SerializeField] private Sprite[ ] straightVariations;
	[SerializeField] private Sprite[ ] turnVariations;
	private Sprite[ ][ ] variations;

	private void Start ( ) {
		variations = new Sprite[ ][ ] {
			shortVariations, straightVariations, turnVariations
		};

		if (isBackground) {
			spriteRenderer.sortingLayerName = "Background Objects";
		}

		SetIsActive(isActive);
	}

	protected override void UpdateSprites ( ) {
		spriteRenderer.sprite = variations[formation][(isBackground ? 2 : 0) + (isActive ? 1 : 0)];
	}
}
