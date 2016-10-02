using System;

[Serializable]
public struct IntPair {
	public static readonly IntPair Up = new IntPair(0, 1);
	public static readonly IntPair Down = new IntPair(0, -1);
	public static readonly IntPair Left = new IntPair(-1, 0);
	public static readonly IntPair Right = new IntPair(1, 0);

	public static readonly IntPair[] directions = { Up, Down, Left, Right };

	public int a, b;
	public IntPair(int a, int b) {
		this.a = a;
		this.b = b;
	}

	public static IntPair operator +(IntPair x, IntPair y) {
		return new IntPair(x.a + y.a, x.b + y.b);
	}

	public static IntPair operator -(IntPair x, IntPair y) {
		return new IntPair(x.a - y.a, x.b - y.b);
	}

	public static bool operator ==(IntPair x, IntPair y) {
		return (x.a == y.a) && (x.b == y.b);
	}

	public static bool operator !=(IntPair x, IntPair y) {
		return (x.a != y.a) || (x.b != y.b);
	}

	public override bool Equals(object obj) {
		if (!(obj is IntPair)) return false;
		IntPair objI = (IntPair) obj;
		if (objI.a == a && objI.b == b)
			return true;
		return false;
	}

	public override int GetHashCode() {
		return base.GetHashCode();
	}

	public override string ToString() {
		return "(" + a + ", " + b + ")";
	}
}