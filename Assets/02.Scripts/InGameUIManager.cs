using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드
using UnityEngine.UI; // UI 관련 코드

public class InGameUIManager : MonoBehaviour
{
    // 싱글톤 접근용 프로퍼티
    public static InGameUIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<InGameUIManager>();
                //Debug.Log("Singleton UIManager");
            }

            return m_instance;
        }
    }

    private static InGameUIManager m_instance; // 싱글톤이 할당될 변수
    private float cleanTime;

    public int destroyEnemyCount = 0;
    public int enemyCount = 0;
    public Text commandText; // 탄약 표시용 텍스트
    public Text scoreText; // 점수 표시용 텍스트
    public Text endGameScoreText;
    public Text clearGameScoreText;
    public Text enemytStateText; // 적 웨이브 표시용 텍스트
    public GameObject gameoverUI; // 게임 오버시 활성화할 UI 
    public GameObject pauseUI;
    public GameObject clearUI;

    public void Update()
    {
        cleanTime += Time.deltaTime;

        if (cleanTime >= 5.0f)
            UpdateState(0);
    }

    // 탄약 텍스트 갱신
    public void UpdateState(int State)
    {
        switch (State)
        {
            case 1:
                commandText.text = "Don't Over The Field!";
                break;
            case 2:
                commandText.text = "Repairing Ship!";
                break;
            case 3:
                commandText.text = "Charging Missile!";
                break;
            case 4:
                commandText.text = "Good Shot!";
                break;
            case 5:
                commandText.text = "Watch Out!";
                break;
            case 6:
                commandText.text = "Press the \"F\" button to start";
                break;
            default:
                commandText.text = "";
                break;
        }

        cleanTime = 0.0f;
    }

    // 점수 텍스트 갱신
    public void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score : " + newScore;
    }

    // 적 웨이브 텍스트 갱신
    public void UpdateEnemyText(int Destroy, int count)
    {
        destroyEnemyCount += Destroy;
        enemyCount += count;
        enemytStateText.text = "Enemy : " + enemyCount + "\nDestroyed Enemy : " + destroyEnemyCount;
    }

    public void endGameScore(int score)
    {
        endGameScoreText.text = "Your Score : " + score;
    }

    public void clearGameScore(int score)
    {
        clearGameScoreText.text = "Your Score : " + score;
    }

    // 게임 오버 UI 활성화
    public void SetActiveGameoverUI(bool active)
    {
        gameoverUI.SetActive(active);
    }
    public void SetActivePauseUI(bool active)
    {
        pauseUI.SetActive(active);
    }
    public void SetActiveClearUI(bool active)
    {
        clearUI.SetActive(active);
    }

    // 게임 재시작
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Click Restart");
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("scMain");
    }

    // 클리어 후 메뉴로 돌아가는 버튼에 적용
    public void ToStageMenu()
    {
        Time.timeScale = 1.0f;
        if(SceneManager.GetActiveScene().name == "scPlayStage1" && GameManager.activeStage == 1)
        {
            GameManager.activeStage = 2;
        }
        else if(SceneManager.GetActiveScene().name == "scPlayStage2" && GameManager.activeStage == 2)
        {
            GameManager.activeStage = 3;
        }
        SceneManager.LoadScene("scMain");
    }
}
