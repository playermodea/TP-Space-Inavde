using UnityEngine;
using UnityEngine.SceneManagement;

// 점수와 게임 오버 여부를 관리하는 게임 매니저
public class GameManager : MonoBehaviour
{
    // 싱글톤 접근용 프로퍼티
    public static GameManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<GameManager>();
                //Debug.Log("Singleton GameManager");
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    private static GameManager m_instance; // 싱글톤이 할당될 static 변수

    private string sceneName;   // 현재 Scene 이름

    private int score = 0; // 현재 게임 점수

    public static int stageOneScore = 0; // Stage1의 최고 점수
    public static int stageTwoScore = 0; // Stage2의 최고 점수
    public static int stageThreeScore = 0; // Stage3의 최고 점수

    public bool isGameover { get; private set; } // 게임 오버 상태

    public static int activeStage = 1; // 플레이 가능한 최대 스테이지

    private void Awake()
    {
        // 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 현재 Scene에 따른 BGM 재생
        sceneName = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            activeStage = 3;
        }
    }

    // 점수를 추가하고 UI 갱신
    public void AddScore(int newScore)
    {
        // 게임 오버가 아닌 상태에서만 점수 증가 가능
        if (!isGameover)
        {
            // 점수 추가
            score += newScore;
            // 점수 UI 텍스트 갱신
            InGameUIManager.instance.UpdateScoreText(score);
        }
    }

    // 게임 최종 스코어가 현재 Stage의 최고 점수일 경우 최고 점수 갱신
    public void AddHighScore()
    {
        if (sceneName.Equals("scPlayStage1") && score > stageOneScore)
        {
            stageOneScore = score;
        }
        else if (sceneName.Equals("scPlayStage2") && score > stageTwoScore)
        {
            stageTwoScore = score;
        }
        else if (sceneName == "scPlayStage3" && score > stageThreeScore)
        {
            stageThreeScore = score;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        InGameUIManager.instance.SetActivePauseUI(true);
    }

    public void ClearGame()
    {
        AddHighScore(); // 최고 점수 갱신
        InGameUIManager.instance.clearGameScore(score);
        InGameUIManager.instance.SetActiveClearUI(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        InGameUIManager.instance.SetActivePauseUI(false);
    }

    // 게임 오버 처리
    public void EndGame()
    {
        // 게임 오버 상태를 참으로 변경
        isGameover = true;
        
        AddHighScore(); // 최고 점수 갱신

        // 게임 오버 UI를 활성화
        GameObject.Find("Main Camera").GetComponent<FollowCam>().enabled = false;
        GameObject.Find("HealthBar").GetComponent<HealthBar>().enabled = false;
        GameObject.Find("AmmoBar").GetComponent<AmmoBar>().enabled = false;
        InGameUIManager.instance.endGameScore(score);
        InGameUIManager.instance.SetActiveGameoverUI(true);
    }
}