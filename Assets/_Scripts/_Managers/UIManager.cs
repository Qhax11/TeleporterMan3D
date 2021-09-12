using TMPro;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    #region Instance Method
    public static UIManager Instance;
    private void InstanceMethod()
    {
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    
    #endregion

    #region Constant
    [HideInInspector]public TextMeshProUGUI levelIndex;
    [HideInInspector]public GameObject startMenu,levelComplete,levelFailed,confetti;
    
    public GameObject myTutorial;
    [HideInInspector]public GameObject optionsMenu;
    [HideInInspector]public GameObject shopMenu;
    [HideInInspector]public GameObject backButton;
    #endregion

    public bool gameIsFinish = false;
    private void Awake()
    {
        #region Instance Method
        InstanceMethod();
        #endregion
    }

    private void Start()
    {
     
    }

    private void Update()
    {
        
    }

    public void _GameInStartMenu()
    {
        if (!gameIsFinish)
        {
            myTutorial.SetActive(false);
            levelIndex.enabled = true;
            startMenu.SetActive(false);


            GameManager.Instance.AnimationStart();
            GameManager.Instance.gameState = GameManager.GameState.Play;
            TimeManager.Instance.transform.DOMoveX(0, 0.1f).OnComplete(() => { GameManager.Instance.PlayerCanTeleported(); });
            print("zzzz");
        }
       
    }

    void Destroy()
    {
        Destroy(PlayerController.Instance.gameObject, 0.2f);
        Destroy(HologramController.Instance.gameObject, 0.2f);
    }

    public void _GameWin()
    {
        gameIsFinish = true;
        levelIndex.enabled = false;
        levelComplete.SetActive(true);
        confetti.SetActive(true);
        Destroy();
    }

    public void _GameLose()
    {
        levelIndex.enabled = false;
        levelFailed.SetActive(true);
        Destroy();
    }

    public void _OptionsMenu()
    {
        levelIndex.enabled = false;
        startMenu.SetActive(false);
        myTutorial.SetActive(false);
        shopMenu.SetActive(false);
        optionsMenu.SetActive(true);
        backButton.SetActive(true);
    }

    public void _ShopButton()
    {
        startMenu.SetActive(false);
        levelIndex.enabled = false;
        myTutorial.SetActive(false);
        shopMenu.SetActive(true);
        backButton.SetActive(true);
    }

    public void _BackButton()
    {
        levelIndex.enabled = true;
        startMenu.SetActive(true);
        myTutorial.SetActive(true);
        optionsMenu.SetActive(false);
        backButton.SetActive(false);
        shopMenu.SetActive(false);
    }
    
    public void SetLevelIndex()
    {
        levelIndex.text = "Level " + LevelManager.Instance.currentLevelNumber;
    }
}
