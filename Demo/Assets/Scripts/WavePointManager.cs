using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WavePointManager {

	public WavePoint start = null;
	public WavePoint end = null;

	private List<WavePoint> nodes = new List<WavePoint>();

	public void Start() {
/*		if (!instance) {
			instance = this;
		} else {
			GameObject.Destroy(gameObject);
		}*/
	}

	private static WavePointManager instance = null;
	private static WavePointManager get() {
		if (instance == null) {
			instance = new WavePointManager();
		}

		return instance;
	}

	public static void addNode(WavePoint node) {
		if (!get().nodes.Contains(node)) {
			get().nodes.Add(node);
		}
	}
	
	public static WavePoint getStart() {
		return get().start;
	}

	public static void setStart(WavePoint node) {
		get().start = node;
	}

	public static void refershMiddle(WavePoint rhs) {

		foreach(var lhs in get().nodes) {
			if(lhs != rhs) {
				if(lhs.prev == rhs.prev && lhs.next == rhs.next) {
					WavePoint start = lhs.prev;
					WavePoint end = lhs.next;

					start.next = lhs;
					lhs.next = rhs;
					rhs.prev = lhs;
					end.prev = rhs;

					return;
				}
			}
		}
	}

	public static WavePoint getEnd(WavePoint node) {
		return get().end;
	}

	public static void setEnd(WavePoint node) {
		get().end = node;
	}
}
