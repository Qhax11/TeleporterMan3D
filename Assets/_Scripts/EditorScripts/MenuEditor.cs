#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace _Scripts.EditorScripts
{
    public static class MenuEditor
    {
        [MenuItem("PlayerPrefs/Clear PlayerPrefs", false, 1)]
        private static void NewMenuOption()
        {
            PlayerPrefs.SetInt("level" , 1);
            LevelManager.Instance.currentLevelNumber = PlayerPrefs.GetInt("level");
            UIManager.Instance.levelIndex.text = "Level " + LevelManager.Instance.currentLevelNumber;
        }
    }
}

#endif