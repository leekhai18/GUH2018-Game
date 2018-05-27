using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerController : MonoBehaviour {
	public float force;
	public float maxSpeed;
	private Rigidbody2D mR2d;
	private bool isActive=false;
	private bool isBelow=false;
	private Animator mAnim;
  

    bool isDynamic;

    enum State
	{
		start,one_silk_comming,swing_one_slik,drop_silk,two_silk_comming,
	}
	private State mState;
	// Use this for initialization
	void Start () {
		mState = State.start;
		mAnim = GetComponent<Animator> ();
		mR2d = GetComponent<Rigidbody2D> ();	
		//mR2d.velocity = Vector2.up * 500;
	}
	void Update()
	{
        if (!isDynamic)
        {
            if (GameManager.Instance.IsStarted)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                isDynamic = true;
            }
        }

        if (GameManager.Instance.IsStarted)
        {
            switch (mState)
            {
                case State.start:
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (!isActive)
                            isActive = true;
                        mAnim.ResetTrigger(mState.ToString());
                        mState = State.one_silk_comming;
                        mAnim.SetTrigger(mState.ToString());
                    }
                    break;
                case State.one_silk_comming:
                    if (Throw.curHook.GetComponent<RopeController>().isTouch)
                    {
                        mAnim.ResetTrigger(mState.ToString());
                        mState = State.swing_one_slik;
                        mAnim.SetTrigger(mState.ToString());
                    }
                    break;
                case State.swing_one_slik:
                    if (GetComponent<Throw>().numHook == 0)
                    {
                        mAnim.ResetTrigger(mState.ToString());
                        mState = State.drop_silk;
                        mAnim.SetTrigger(mState.ToString());
                    }
                    break;
                case State.drop_silk:
 

                    if (GetComponent<Throw>().numHook == 1)
                    {
                        mAnim.ResetTrigger(mState.ToString());
                        mState = State.one_silk_comming;
                        mAnim.SetTrigger(mState.ToString());
                    }

                    break;
                default:
                    break; ;
            }

        }

    }
	// Update is called once per frame
	void FixedUpdate () {
        if (GameManager.Instance.IsStarted)
        {
            if (Throw.curHook != null)
            {
                if (Throw.curHook.transform.position.y > transform.position.y)
                {
                    if (Throw.curHook.transform.position.x < transform.position.x)
                    {
                        mR2d.gravityScale = 100;
                        isBelow = true;
                    }
                    else
                        mR2d.gravityScale = 300;


                }
                else
                    isBelow = false;
            }
            else
                isBelow = false;
            if (isActive)
            {
                if (isBelow)
                {
                    if (mR2d.velocity.x > 0)
                    {
                        mR2d.AddForce(Vector2.right * force);
                        if (mR2d.velocity.x > maxSpeed)
                            mR2d.velocity = new Vector2(maxSpeed, mR2d.velocity.y);
                    }

                }
            }

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            GetComponent<Animator>().SetBool("IsDie", true);
            GameManager.Instance.GameOver();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Issue"))
        {
            collision.gameObject.GetComponent<IssueHandle>().SetCompleted();
            GameManager.Instance.score += 10;
            GameManager.Instance.SetTextScore();
            GameManager.Instance.helped++;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            GetComponent<Animator>().SetBool("IsDie", true);
            GameManager.Instance.GameOver();
        }
    }

}
