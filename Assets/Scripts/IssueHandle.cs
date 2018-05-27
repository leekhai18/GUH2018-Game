using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IssueHandle : MonoBehaviour {

    Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetCompleted()
    {
        animator.SetBool("IssueCompleted", true);
    }
}
