using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {

	public static int stateHook = 0;
	public static GameObject curHook=null;
	public GameObject hook;
	private GameObject oldHook=null;
	public  float numHook=0;
	private bool ropeActive = false;
	// Use this for initialization
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (stateHook == 1)
				stateHook = 0;
			else
				stateHook = 1;
		}
		if (stateHook == 1)
			Hook_1 ();
		if (stateHook == 0)
			Hook_0 ();
		if (curHook != null) {
			if (curHook.GetComponent<RopeController> ().deleteHook) {
				if (curHook != oldHook && oldHook!=null) {
					GameObject.DestroyImmediate (oldHook);
					oldHook = curHook;
					numHook--;

				}
				if (curHook == oldHook) {
					Debug.Log ("ok");
				}
			}
		}

	}
	void Hook_1()
	{
		if (Input.GetMouseButtonDown (0)) {
			ropeActive = true;
			if (curHook != null)
				oldHook = curHook;
			Vector2 destiny = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			if (numHook != 2) 
			{
				curHook = (GameObject)Instantiate (hook, transform.position, Quaternion.identity);
				curHook.GetComponent<RopeController> ().destiny = destiny;
				Vector2 sideMove = (destiny - (Vector2)transform.position).normalized;

				curHook.GetComponent<RopeController> ().sideMove = sideMove;
				numHook++;
				if (oldHook == null)
					oldHook = curHook;
			}
		}

	}
	void Hook_0()
	{
		if (Input.GetMouseButtonDown (0)) {
			if (ropeActive == false) {
				Vector2 destiny = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				curHook = (GameObject)Instantiate (hook, transform.position, Quaternion.identity);
				Vector2 sideMove = (destiny - (Vector2)transform.position).normalized;
				curHook.GetComponent<RopeController> ().sideMove = sideMove;
				curHook.GetComponent<RopeController> ().destiny = destiny;
				ropeActive = true;
				numHook++;
			} else {

				Destroy (curHook);
				numHook--;
				ropeActive = false;
			}
		}
	}

}
