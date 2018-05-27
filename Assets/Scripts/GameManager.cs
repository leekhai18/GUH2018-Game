using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
    [SerializeField]
    GameObject superMan;

    [SerializeField]
    GameObject timer;

    [SerializeField]
    GameObject mCamera;

    [SerializeField]
    GameObject world;

    [SerializeField]
    GameObject guidePanel;

    public int score;
    public GameObject scoreText;

    public int helped;

    public void SetTextScore()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.getScore);
        GameManager.Instance.scoreText.GetComponent<Text>().text = score.ToString();
    }

    int index;

    bool isStarted;

    public bool IsStarted
    {
        get
        {
            return isStarted;
        }
        set
        {
            isStarted = value;
        }
    }

	// Use this for initialization
	void Start () {
        isStarted = false;

        for (int i = 0; i < 3; i++)
        {
            AddPartWorld(new Vector3(1920 * i + 960, 533 * i, 0));
        }

        index = 2;
        score = 0;
        helped = 0;
    }

	
	// Update is called once per frame
	void Update ()
    {
        if (isStarted)
        {
            if (mCamera.transform.position.x > world.transform.GetChild(1).gameObject.transform.position.x + 480)
            {
                EasyObjectPool.Instance.ReturnObjectToPool(world.transform.GetChild(0).gameObject);

                world.transform.GetChild(0).gameObject.transform.SetParent(EasyObjectPool.Instance.transform);

                index++;
                AddPartWorld(new Vector3(1920 * index + 960, 533 * index, 0));
            }
        }
    }

    public void SuperManDie()
    {
        superMan.GetComponent<SuperMan>().IsDie = true;
    }

    public void RefreshTime()
    {
        timer.GetComponent<TimerSlider>().RefreshTime();
    }

    public void AddPartWorld(Vector3 position)
    {
        GameObject obj = EasyObjectPool.Instance.GetObjectFromPool("PartWorld" + Random.Range(1, 10), position, Quaternion.identity);
        obj.transform.SetParent(world.transform);
    }

    public void FaceOutGuide()
    {
        guidePanel.GetComponent<Animator>().SetBool("LetFaceOut", true);
        isStarted = true;
    }

    public void GameOver()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.die);
        StartCoroutine(delay());
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(3);
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Helped", helped);
        PlayerPrefs.SetInt("Retry", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("GameOver");
    }
}
