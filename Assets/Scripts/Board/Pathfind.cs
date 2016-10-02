using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Pathfind {

	private Node startLocation;
	private Node destination;
	private Node[,] nodes;

	public Pathfind(IntPair start, IntPair end) {
		List<IntPair> locations = GameController.instance.Board.data.pieces.Keys.ToList();
		nodes = new Node[9, 7];
		foreach(IntPair l in locations) {
			nodes[l.a, l.b] = new Node(l, this);
		}
		startLocation = nodes[start.a, start.b];
		destination = nodes[end.a, end.b];
	}

	public List<IntPair> GetDirections() {
		IntPair[] path = FindPath().ToArray();
		List<IntPair> directions = new List<IntPair>();
		for(int x = 1; x < path.Count(); x++) {
			directions.Add(path[x] - path[x - 1]);
		}
		//foreach (IntPair d in directions) Debug.Log(d.ToString());
		return directions;
	}

	public List<IntPair> FindPath() {
		List<IntPair> path = new List<IntPair>();
		if (Search(startLocation)) {
			Node node = destination;
			while (node.ParentNode != null) {
				path.Add(node.Location);
				node = node.ParentNode;
			}
			path.Add(node.Location);
			path.Reverse();
		}
		return path;
	}

	private bool Search(Node currentNode) {
		currentNode.State = NodeState.Closed;
		List<Node> nextNodes = GetAdjacentNodes(currentNode);
		nextNodes.Sort((node1, node2) => node1.F.CompareTo(node2.F));
		foreach(Node n in nextNodes) {
			if (n.Location == destination.Location)
				return true;
			if (!Search(n))
				return false;
		}
		return true;
	}

	private List<Node> GetAdjacentNodes(Node currentNode) {
		List<Node> adjNode = new List<Node>();
		foreach(IntPair direction in IntPair.directions) {
			IntPair newLocation = currentNode.Location + direction;
			if (GameController.instance.Board.data.pieces.ContainsKey(newLocation)) {
				Node node = nodes[newLocation.a, newLocation.b];
				if (node.State == NodeState.Closed || (!(node == destination) && !node.isWalkable)) continue;
				if(node.State == NodeState.Open) {
					if(currentNode.G < node.ParentNode.G) {
						node.ParentNode = currentNode;
						adjNode.Add(node);
					}
				} else {
					node.ParentNode = currentNode;
					node.State = NodeState.Open;
					adjNode.Add(node);
				}
			}
		}
		return adjNode;
	}

	public class Node {
		private Pathfind p;
		public IntPair Location { get; protected set; }
		public bool isWalkable {
			get {
				return GameController.instance.Board.data[Location] == null;
			}
		}
		public float G { get; protected set; }
		public float H {
			get {
				return Mathf.Abs(p.startLocation.Location.a - Location.a) +
					Mathf.Abs(p.startLocation.Location.b - Location.b);
			}
		}
		public float F { get { return G + H; } }
		public NodeState State { get; set; }
		private Node parentNode;
		public Node ParentNode {
			get {
				return parentNode;
			}
			set {
				G = value.G + 1;
				parentNode = value;
			}
		}

		public Node(IntPair location, Pathfind p) {
			this.p = p;
			Location = location;
			State = NodeState.Untested;
		}
	}

	public enum NodeState {
		Untested,
		Open,
		Closed
	}
}
