using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerSlider : MonoBehaviour {

    Slider slider;

	// Use this for initialization
	void Start () {
        slider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {

        if (GameManager.Instance.IsStarted)
        {
            slider.value -= Time.deltaTime;

            if (slider.value < slider.maxValue * 0.2f)
            {
                GetComponent<Animator>().SetBool("IsDanger", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("IsDanger", false);
            }

            if (slider.value <= 0)
            {
                GameManager.Instance.GameOver();
            }
        }
    }

    public void RefreshTime()
    {
        slider.value = slider.maxValue;
    }

}
