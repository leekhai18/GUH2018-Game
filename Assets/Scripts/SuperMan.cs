using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMan : MonoBehaviour {
    [SerializeField]
    float velocityJumpY;
    [SerializeField]
    float velocityJumpX;

    Rigidbody2D rb;
    bool isDynamic;

    bool isDie;


    public bool IsDie
    {
        get
        {
            return isDie;
        }
        set
        {
            isDie = value;
        }
    }

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        isDie = false;
        isDynamic = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isDynamic)
        {
            if (GameManager.Instance.IsStarted)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                isDynamic = true;
            }
        }

        if (!isDie)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb.velocity = new Vector2(velocityJumpX, velocityJumpY);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Town"))
        {
            isDie = true;
            GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Issue"))
        {
            collision.gameObject.GetComponent<IssueHandle>().SetCompleted();
            GameManager.Instance.RefreshTime();
            SoundManager.Instance.Play(SoundManager.Sounds.getScore);
            GameManager.Instance.score += 10;
            GameManager.Instance.SetTextScore();
            GameManager.Instance.helped++;
        }
    }
}
