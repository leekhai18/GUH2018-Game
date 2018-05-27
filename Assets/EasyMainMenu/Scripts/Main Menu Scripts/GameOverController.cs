using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

    Animator anim;

    public int quickSaveSlotID;

    [Header("Options Panel")]
    public GameObject StartGameOptionsPanel;

    [SerializeField]
    Text score;

    [SerializeField]
    Text helped;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

        //new key
        PlayerPrefs.SetInt("quickSaveSlot", quickSaveSlotID);

        openStartGameOptions();
    }

    #region Open Different panels

    public void openOptions()
    {
        //enable respective panel
        StartGameOptionsPanel.SetActive(false);

        //play anim for opening main options panel
        anim.Play("buttonTweenAnims_on");

        //play click sfx
        playClickSound();

        //enable BLUR
        //Camera.main.GetComponent<Animator>().Play("BlurOn");
       
    }

    public void openStartGameOptions()
    {
        //enable respective panel
        StartGameOptionsPanel.SetActive(true);

        score.text = "Score : " + PlayerPrefs.GetInt("Score", 0);
        helped.text = "Helped : " + PlayerPrefs.GetInt("Helped", 0);

        //play anim for opening main options panel
        anim.Play("buttonTweenAnims_on");

        //play click sfx
        playClickSound();

        //enable BLUR
        //Camera.main.GetComponent<Animator>().Play("BlurOn");
        
    }

    public void Retry()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Retry", 0));
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }

    public void openOptions_Game()
    {
        //enable respective panel

        //play anim for opening game options panel
        anim.Play("OptTweenAnim_on");

        //play click sfx
        playClickSound();

    }
    public void openOptions_Controls()
    {
        //enable respective panel

        //play anim for opening game options panel
        anim.Play("OptTweenAnim_on");

        //play click sfx
        playClickSound();

    }
    public void openOptions_Gfx()
    {
        //enable respective panel

        //play anim for opening game options panel
        anim.Play("OptTweenAnim_on");

        //play click sfx
        playClickSound();

    }

    public void openSuper()
    {
        //enable respective panel

        //play anim for opening game options panel
        anim.Play("OptTweenAnim_on");

        SceneManager.LoadScene("SuperMan");
    }

    public void openSpider()
    {
        SceneManager.LoadScene("SpiderMan");
    }
    #endregion

    #region Back Buttons

    public void back_options()
    {
        //simply play anim for CLOSING main options panel
        anim.Play("buttonTweenAnims_off");

        //disable BLUR
       // Camera.main.GetComponent<Animator>().Play("BlurOff");

        //play click sfx
        playClickSound();
    }

    public void back_options_panels()
    {
        //simply play anim for CLOSING main options panel
        anim.Play("OptTweenAnim_off");
        
        //play click sfx
        playClickSound();

    }

    public void Quit()
    {
        Application.Quit();
    }
    #endregion

    #region Sounds
    public void playHoverClip()
    {
       
    }

    void playClickSound() {

    }


    #endregion
}
