using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    //-- Mechanic Variables
    public bool holding;

    private Vector3 pos1, pos2;

    public GameObject player;
    public GameObject hologram;
    public Animator playerAnimator;
    public Animator hologramAnimator;

    private Rigidbody playerRb;

    public float hologramSpeedX;
    public float hologramSpeedZ;
    public float hologramClampX;
    public float hologramClampForward;
    public float hologramClampBacward;


    //-- Mechanic Variables

    public GameObject teleportEffect;


    public bool canTeleport;

    public float hologramSpeedForward=10;
    public float playerSpeedForward = 10;

    #region Instance Method
    public static GameManager Instance;
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

    public enum GameState
    {
        Play,
        Pause,
        Win,
        Lose,
        StartMenu,
    }
    public GameState gameState;
    
        
    private void Awake()
    {
        #region Instance Method
        InstanceMethod();
        #endregion
        
        
    }
    
    private void Start()
    {
        playerRb = player.GetComponent<Rigidbody>();
        playerAnimator = player.GetComponent<Animator>();
        hologramAnimator = hologram.GetComponent<Animator>();
        canTeleport = false;
        
    }

    private void FixedUpdate()
    {
        if (gameState == GameState.Play)
        {
            player.transform.Translate(transform.forward * (Time.deltaTime * playerSpeedForward));
            hologram.transform.Translate(transform.forward * (Time.deltaTime * hologramSpeedForward));
            
        }

        if (player.transform.position.y < -3)
        {
            GameLose(0);
        }



    }

    private void Update()
    {
        if (gameState == GameState.Play)
        {
            if (Input.GetMouseButtonDown(0))
            {
                pos1 = GameManager.Instance.GetMousePosition();

                holding = true;
            }

            if (Input.GetMouseButton(0) && holding) //set players velocity on X axis and clamp value
            {
                pos2 = GameManager.Instance.GetMousePosition();

                Vector3 delta = pos1 - pos2;

                pos1 = pos2;

                hologram.transform.position = new Vector3(hologram.transform.position.x - delta.x * hologramSpeedX,
                    hologram.transform.position.y, hologram.transform.position.z - delta.y * hologramSpeedZ);
                  

            }

            if (Input.GetMouseButtonUp(0))
            {
                holding = false;

                playerRb.velocity = Vector3.zero;

                if(canTeleport) PlayerController.Instance.TeleportMaker();
               

            }

            hologram.transform.position = new Vector3(Mathf.Clamp(hologram.transform.position.x,-hologramClampX, hologramClampX),hologram.transform.position.y,
            Mathf.Clamp(hologram.transform.position.z, player.transform.position.z + hologramClampBacward, player.transform.position.z + hologramClampForward));
            
        }

    }

   

   

    public void GameWin()
    {
        gameState = GameState.Win;
        
        UIManager.Instance._GameWin();
    }

    public void GameLose(float delay)
    {
        gameState = GameState.Lose;

        UIManager.Instance.gameIsFinish = true;

        TimeManager.Instance.transform.DOMoveX(0, delay).OnComplete(() => { UIManager.Instance._GameLose(); });

    }
    
    public void AnimationStart()
    {
        PlayerController.Instance.playerAnimator.SetBool("RunBool", true);
        HologramController.Instance.hologramAnimator.SetBool("Run", true);
    }
    
    public void PlayerCanTeleported()
    {
        canTeleport = true;
    }

    #region Constant Methods
    
    public static float ClampAngle(float angle, float min, float max)
    {
        angle = Mathf.Repeat(angle, 360);
        min = Mathf.Repeat(min, 360);
        max = Mathf.Repeat(max, 360);
        var inverse = false;
        var timing = min;
        var tangle = angle;
        if (min > 180)
        {
            inverse = true;
            timing -= 180;
        }
        if (angle > 180)
        {
            inverse = !inverse;
            tangle -= 180;
        }
        var result = !inverse ? tangle > timing : tangle < timing;
        if (!result)
            angle = min;
        inverse = false;
        tangle = angle;
        var tax = max;
        if (angle > 180)
        {
            inverse = true;
            tangle -= 180;
        }
        if (max > 180)
        {
            inverse = !inverse;
            tax -= 180;
        }
        result = !inverse ? tangle < tax : tangle > tax;
        if (!result)
            angle = max;
        return angle;
    }
    
    public Vector2 GetMousePosition()
    {
        var pos = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

        return pos;
    }
    
    #endregion

}
