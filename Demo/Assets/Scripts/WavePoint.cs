using UnityEngine;
using System.Collections;

namespace Node {
	public enum Type {
		Start,
		End,
		Middle,
		Lost
	}
}

public class WavePoint : MonoBehaviour {

	public WavePoint prev = null;
	public WavePoint next = null;

	private int id = 1;

	public int nextId() {
		if (prev == null) {
			return 1;
		} else {
			id = prev.nextId () + 1;
			return id;
		}
	}

	public Node.Type type() {
		if (prev && next) {
			return Node.Type.Middle;
		} else if (prev) {
			return Node.Type.Start;
		} else if (next) {
			return Node.Type.End;
		} else {
			return Node.Type.Lost;
		}
	}
		
	void Update () {
		Refresh ();
	}
	
	void OnDrawGizmos() {
		
		WavePointManager.addNode (this);

		Refresh ();

		Gizmos.color = Color.green;

		if(prev)
			Gizmos.DrawLine(transform.position, prev.gameObject.transform.position);

		if(next)
			Gizmos.DrawLine(transform.position, next.gameObject.transform.position);

	}

	void Refresh() {
		gameObject.name = nextId ().ToString();

		switch (type()) {
		case Node.Type.Start:
			RefreshStart();
			break;
			
		case Node.Type.Middle:
			RefreshMiddle();
			break;

		case Node.Type.End:
			RefreshEnd();
			break;
			
		case Node.Type.Lost:
			RefreshLost();
			break;
		}
	}

	void RefreshStart() {
		WavePointManager.setStart (this);
	}

	void RefreshMiddle() {
		WavePointManager.refershMiddle (this);
	}

	void RefreshEnd() {
		WavePointManager.setEnd (this);
	}

	void RefreshLost() {
		
	}
}
