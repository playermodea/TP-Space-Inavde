using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{    
    public GameObject mainMenu;

    public GameObject stageSelectMenu;
    public GameObject optionMenu;
    public GameObject quitMenu;

    public GameObject guideMenu;
    public GameObject audioMenu;

    public Button stage2Btn;
    public Button stage3Btn;

    public Text stageOneScoreText; // 최고 점수 표시용 텍스트
    public Text stageTwoScoreText;
    public Text stageThreeScoreText;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.activeStage == 1)
        {
            stage2Btn.interactable = false;
            stage3Btn.interactable = false;
        }
        else if(GameManager.activeStage == 2)
        {
            stage3Btn.interactable = false;
        }
        stageOneScoreText.text = GameManager.stageOneScore.ToString();
        stageTwoScoreText.text = GameManager.stageTwoScore.ToString();
        stageThreeScoreText.text = GameManager.stageThreeScore.ToString();
    }

    public void onClickStartBtn()
    {
        mainMenu.gameObject.SetActive(false);
        stageSelectMenu.gameObject.SetActive(true);

        Debug.Log("Start Game Button Clicked");
    }

    public void onClickOptionBtn()
    {
        mainMenu.gameObject.SetActive(false);
        optionMenu.gameObject.SetActive(true);

        Debug.Log("Option Button Clicked");
    }

    public void onClickQuitBtn()
    {
        mainMenu.gameObject.SetActive(false);
        quitMenu.gameObject.SetActive(true);

        Debug.Log("Quit Button Clicked");
    }

    public void onClickStage1Btn()
    {
        SceneManager.LoadScene("scPlayStage1");
        Debug.Log("Stage1 Button Clicked");
    }

    public void onClickStage2Btn()
    {
        SceneManager.LoadScene("scPlayStage2");
        Debug.Log("Stage2 Button Clicked");
    }

    public void onClickStage3Btn()
    {
        SceneManager.LoadScene("scPlayStage3");
        Debug.Log("Stage3 Button Clicked");
    }

    public void onClickBackMainBtn()
    {
        stageSelectMenu.gameObject.SetActive(false);
        optionMenu.gameObject.SetActive(false);
        quitMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);

        Debug.Log("Back (to Main) Button Clicked");
    }

    public void onClickGuideBtn()
    {
        optionMenu.gameObject.SetActive(false);
        guideMenu.gameObject.SetActive(true);

        Debug.Log("Guide Button Clicked");
    }

    public void onClickAudioBtn()
    {
        optionMenu.gameObject.SetActive(false);
        audioMenu.gameObject.SetActive(true);

        Debug.Log("Audio Button Clicked");
    }

    public void onClickBackOptionBtn()
    {
        guideMenu.gameObject.SetActive(false);
        audioMenu.gameObject.SetActive(false);
        optionMenu.gameObject.SetActive(true);

        Debug.Log("Back (to Option) Button Clicked");
    }

    public void onClickYesOfQuitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Yes (of Quit) Button Clicked");
#else
        Application.Quit();
#endif
    }
}
