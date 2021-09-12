using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class LevelManager : MonoBehaviour
{
    #region Instance Method
    public static LevelManager Instance;
    
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

    
    public enum LoaderType
    {
        Serial,
        Random
    }
    public List<GameObject> levels = new List<GameObject>();
    [HideInInspector]public int currentLevelNumber;

    private static GameObject _loadedLevel;
    private static bool _sameLevel;
    
    [Tooltip("Choose *Random* to have the levels come randomly." + "Choose *Serial* to have the levels come in order.")]
    [Space(10)]public LoaderType lt;
    private void Awake()
    {
        #region Instance Method
        InstanceMethod();
        #endregion
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("level") == 0)
        {
            PlayerPrefs.SetInt("level",1);
        }
        currentLevelNumber = PlayerPrefs.GetInt("level");

        if (!_sameLevel)
        {
            if (lt == LoaderType.Serial)
            {
                if (PlayerPrefs.GetInt("level") < levels.Count + 1)
                {
                    _loadedLevel = levels[PlayerPrefs.GetInt("level") - 1];
                }
                else
                {
                    _loadedLevel = levels[Random.Range(0, levels.Count)];
                }
            }

            if (lt == LoaderType.Random)
            {
                _loadedLevel = levels[Random.Range(0, levels.Count)];
            }
        }
        else
        {
            _sameLevel = false;
        }

        Instantiate(_loadedLevel);

        Time.timeScale = 1f;
        
        UIManager.Instance.SetLevelIndex();
    }

    public void NextLevel()
    {
        currentLevelNumber++;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        PlayerPrefs.SetInt("level", currentLevelNumber);

        UIManager.Instance.SetLevelIndex();
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        PlayerPrefs.SetInt("level" ,currentLevelNumber);

        _sameLevel = true;
        
        UIManager.Instance.SetLevelIndex();
    }
}
