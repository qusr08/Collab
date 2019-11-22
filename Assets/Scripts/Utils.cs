﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class Utils {
	public static GameObject[ ] GetAllChildren (Transform transform) {
		/* Get an array with all the children of a transform */

		GameObject[ ] objects = new GameObject[transform.childCount];
		for (int i = 0; i < objects.Length; i++) {
			objects[i] = transform.GetChild(i).gameObject;
		}

		return objects;
	}

	public static int BoolToInt (bool boolean) {
		/* Convert a boolean to an int */

		return boolean ? 1 : 0;
	}

	public static bool AlmostEqual (Vector3 position, Vector3 target, float allowedDiff) {
		/* Get whether 2 positions are not exactly equal, but fairly close */

		return GetDistance(position, target) <= allowedDiff;
	}

	public static float GetDistance (Vector2 point1, Vector2 point2) {
		/* Get the distance between 2 points */

		return Mathf.Sqrt(Mathf.Pow(point2.y - point1.y, 2) + Mathf.Pow(point2.x - point1.x, 2));
	}

	public static int GetRandomInteger (int min, int max) {
		/* Get a random integer between 2 values */

		return Constants.RANDOM.Next(min, max + 1);
	}

	public static float GetRandomFloat (float min, float max) {
		/* Get a random float between 2 values */

		return (float) Constants.RANDOM.NextDouble( ) * (max - min) + min;
	}

	public static float GetRandomAngle ( ) {
		/* Get a random angle in radians */

		return GetRandomFloat(0, 360);
	}

	public static float GetAngle (float minAngle, float maxAngle) {
		/* Get a random angle between 2 values */

		return GetRandomFloat(minAngle, maxAngle);
	}

	public static Vector3Int WorldPosToTilemapPos (Vector3 vector) {
		/* Convert an objects position in the world to a tile position on a tilemap */

		return new Vector3Int((int) (vector.x - 0.5f), (int) (vector.y - 0.5f), 0);
	}

	public static Vector3 GetMidpoint (Vector3 point1, Vector3 point2) {
		/* Get the midpoint between two points */

		float x = (point1.x + point2.x) / 2;
		float y = (point1.y + point2.y) / 2;

		return new Vector3(x, y);
	}

	public static void ChangeTileTexture (Tilemap tilemap, Vector3Int coord, Sprite sprite) {
		/* Set the texture of a tile in a tilemap */

		Tile tile = ScriptableObject.CreateInstance<Tile>( );
		tile.sprite = sprite;

		tilemap.SetTile(coord, tile);
	}

	public static Vector3Int Vec3ToVec3Int (Vector3 vector) {
		/* Convert a regular Vector3 to a Vector3Int */

		return new Vector3Int((int) vector.x, (int) vector.y, (int) vector.z);
	}

	public static float Limit (float value, float min, float max) {
		/* Limit a value to be inside a specific range */

		if (value < min) {
			value = min;
		}
		if (value > max) {
			value = max;
		}

		return value;
	}

	public static float GetHorizontalDistance (Vector3 pos1, Vector3 pos2) {
		/* Get the x distance that the objects are apart */

		Bounds bounds = new Bounds(pos1, Vector3.zero);
		bounds.Encapsulate(pos2);

		return bounds.size.x;
	}

	public static bool GetButtonValue (string name, int playerID) {
		/* Get joystick button value */

		return Input.GetButtonDown(name + "-" + (playerID + 1));
	}

	public static float GetAxisRawValue (string name, int playerID) {
		/* Get joystick axis value */

		return Input.GetAxisRaw(name + "-" + (playerID + 1));
	}

	public static bool InRange (float value, float min, float max) {
		/* Check if a value is within the specified range */

		return value >= min && value <= max;
	}

	public static bool InRangePM (float value, float range) {
		/* Check if a value is within the range made by <range> and -<range> */

		return InRange(value, -range, range);
	}

	public static Vector3 Rotate2D (Vector3 eulerAngles, float angle) {
		/* Get the 2D rotated euler angle vector based on the angle to rotate by */

		return new Vector3(eulerAngles.x, eulerAngles.y, Mathf.Rad2Deg * angle);
	}

	public static Vector3 NoZ (Vector3 vector, float z) {
		/* Return a vector without a different z value */

		vector.z = z;

		return vector;
	}
}
