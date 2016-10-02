using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class BoardData {

	private static readonly IntPair[] NON_TILES = {
		new IntPair(0,0), new IntPair(0,1), new IntPair(1,0), //Top Left
		new IntPair(0,5), new IntPair(0,6), new IntPair(1,6), //Bottom Left
		new IntPair(7,0), new IntPair(8,1), new IntPair(8,0), //Top Right
		new IntPair(7,6), new IntPair(8,6), new IntPair(8,5), //Bottom Right
	};

	public Dictionary<IntPair, ISprite> pieces;

	public ISprite this[int x, int y] {
		get {
			return this[new IntPair(x, y)];
		}
	}

	public ISprite this[IntPair p] {
		get {
			if (pieces.ContainsKey(p)) return pieces[p];
			return null;
		}
	}

	public IntPair this[ISprite s] {
		get {
			return pieces.FirstOrDefault(t => t.Value == s).Key;
		}
	}

	public BoardData(int width, int height) {
		pieces = new Dictionary<IntPair, ISprite>();
		for (int b = 0; b < height; b++) {
			for(int a = 0; a < width; a++) {
				if (NON_TILES.ToList().Any(n => n.a == a && n.b == b)) continue;
				pieces.Add(new IntPair(a, b), null);
			}
		}
	}

	public void Add(ISprite sprite, IntPair location) {
		if (!pieces.ContainsKey(location)) Debug.LogError("Location not on board.");
		if (pieces[location] != null) Debug.LogError("Attempted to fill an occupied space");
		pieces[location] = sprite;
	}

	public void Move(IntPair origin, IntPair destination) {
		if (!pieces.ContainsKey(origin)) Debug.LogError("Origin value not in board.");
		if (!pieces.ContainsKey(destination)) Debug.LogError("Destination value not in board.");
		if (pieces[destination] != null) Debug.LogError("Attempted to move to occupied space");
		if (pieces[origin] == null) Debug.LogError("Attempted to move nothing");
		pieces[destination] = pieces[origin];
		pieces[origin] = null;

	}

	public void Move(ISprite piece, IntPair destination) {
		Move(this[piece], destination);
	}

	public void RemoveAt(IntPair location) {
		if (!pieces.ContainsKey(location)) Debug.LogError("Location not in board.");
		if (pieces[location] == null) Debug.LogError("Nothing here to remove.");
		pieces[location] = null;
	}

	public void Remove(ISprite piece) {
		RemoveAt(this[piece]);
	}

	private List<IntPair> GLIR(IntPair start, int range) {
		if (range < 0) Debug.LogError("Range cannot be negative");
		List<IntPair> results = new List<IntPair>();
		if (pieces[start] != null) return results;
		if (range != 0) {
			foreach (IntPair offset in IntPair.directions) {
				if (pieces.ContainsKey(start + offset)) {
					List<IntPair> l = GLIR(start + offset, range - 1);
					if (l != null) results.AddRange(l);
				}
			}
		}
		if (pieces[start] == null) results.Add(start);
		return results.Distinct().ToList();
	}

	public List<IntPair> GetLocationsInRange(IntPair start, int range) {
		if (range < 0) Debug.LogError("Range cannot be negative");
		List<IntPair> results = new List<IntPair>();
		foreach (IntPair offset in IntPair.directions) {
			if (pieces.ContainsKey(start + offset)) {
				List<IntPair> l = GLIR(start + offset, range - 1);
				if (l != null) results.AddRange(l);
			}
		}
		return results.Distinct().ToList();
	}

	public List<IntPair> GetLocationsInRange(ISprite piece, int range) {
		return GetLocationsInRange(this[piece], range);
	}

	private List<IntPair> GHLIR(IntPair start, Team myTeam, int range) {
		if (range < 0) Debug.LogError("Range cannot be negative");
		List<IntPair> results = new List<IntPair>();
		if (pieces[start] != null) {
			if (pieces[start].Team == myTeam.Opposite()) {
				results.Add(start);
			}
			return results;
		}
		if (range != 0) {
			foreach (IntPair offset in IntPair.directions) {
				if (pieces.ContainsKey(start + offset)) {
					List<IntPair> l = GHLIR(start + offset, myTeam, range - 1);
					if (l != null) results.AddRange(l);
				}
			}
		}
		return results.Distinct().ToList();

	}

	public List<IntPair> GetHostileLocationsInRange(IntPair start, Team myTeam, int range) {
		if (range < 0) Debug.LogError("Range cannot be negative");
		List<IntPair> results = new List<IntPair>();
		foreach (IntPair offset in IntPair.directions) {
			if (pieces.ContainsKey(start + offset)) {
				List<IntPair> l = GHLIR(start + offset, myTeam, range - 1);
				if (l != null) results.AddRange(l);
			}
		}
		return results.Distinct().ToList();
	}

	public List<IntPair> GetHostileLocationsInRange(ISprite piece, int range) {
		return GetHostileLocationsInRange(this[piece], piece.Team, range);
	}
}