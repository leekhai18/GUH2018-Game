using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RopeController : MonoBehaviour {
	public bool isTouch = false;
	public Vector2 destiny;
	public float speed= 1;
	public Vector2 sideMove;
	public float distance = 2;
	public GameObject nodePrefab;
	public GameObject mShooter;
	public GameObject lastNode;
	public LineRenderer lr;
	public bool deleteHook = false;
	public static Collider2D col;
	public List<GameObject> Nodes = new List<GameObject>();
	private bool isDeleteForce = false;
	private bool done=false;
	private int vertexCount=2;

	// Use this for initialization
	void Start () {


		lr = GetComponent<LineRenderer> ();

		mShooter = GameObject.FindGameObjectWithTag ("Kaja");

		lastNode = transform.gameObject;


		Nodes.Add (transform.gameObject);


	}

	// Update is called once per frame
	void Update () {
		if (Vector2.Distance (transform.position, destiny) > 10)
			deleteHook = true;
	
		if(!isTouch)
			transform.position +=(Vector3) sideMove * speed;
		if(!isTouch)
		{
			if (Vector2.Distance (mShooter.transform.position, lastNode.transform.position) > distance) {
				CreateNode ();
			}
		} else if (done == false) {
			done = true;
			while(Vector2.Distance (mShooter.transform.position, lastNode.transform.position) > distance)
			{
				CreateNode ();
			}
			//lastNode.GetComponent<HingeJoint2D> ().connectedBody = mShooter.GetComponent<Rigidbody2D> ();
			lastNode.GetComponent<HingeJoint2D> ().connectedBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D> ();

		}


		RenderLine ();
	}
	void LateUpdate()
	{
		if (isDeleteForce) {
			isDeleteForce = false;
		//	mShooter.GetComponent<Rigidbody2D> ().velocity = new Vector2 (mShooter.GetComponent<Rigidbody2D> ().velocity.x, 0);
		}
	}

	void RenderLine()
	{

		lr.SetVertexCount (vertexCount);

		int i;
		for (i = 0; i < Nodes.Count; i++) {

			lr.SetPosition (i, Nodes [i].transform.position);

		}

		lr.SetPosition (i, mShooter.transform.position);

	}


	void CreateNode()
	{

		Vector2 pos2Create = mShooter.transform.position - lastNode.transform.position;
		pos2Create.Normalize ();
		pos2Create *= distance;
		pos2Create += (Vector2)lastNode.transform.position;

		GameObject go = (GameObject)	Instantiate (nodePrefab, pos2Create, Quaternion.identity);


		go.transform.SetParent (transform);

		lastNode.GetComponent<HingeJoint2D> ().connectedBody = go.GetComponent<Rigidbody2D> ();

		lastNode = go;

		Nodes.Add (lastNode);

		vertexCount++;

	}
	void OnTriggerEnter2D(Collider2D other)
	{
        if (Throw.stateHook == 1)
        {
            if (col != null && col == other)
            {
                return;
            }
        }
		if (other.gameObject.tag == "Town") {
			deleteHook = true;
			isTouch = true;
			if (Throw.stateHook == 1)
				isDeleteForce = true;
			col = other;

            GameManager.Instance.score++;
            GameManager.Instance.SetTextScore();
           
        }
    }

}

