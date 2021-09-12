using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Instance Method
    public static SoundManager Instance;
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

    public AudioSource run, death, attack1, attack2, sword_draw;
    
    private void Awake()
    {
        #region Instance Method
        InstanceMethod();
        #endregion
    }

    private void Start()
    {
       
    }
}
