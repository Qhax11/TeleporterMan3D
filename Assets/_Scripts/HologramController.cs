using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HologramController : MonoBehaviour
{
    public Animator hologramAnimator;
    public bool gameStart;

    public string attackAnimation;

    bool onPavement = false;

    float pavementLocalScaleY;
    float pavementPositionY;



    #region Instance Method
    public static HologramController Instance;
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
    private void Awake()
    {
        #region Instance Method
        InstanceMethod();
        #endregion
    }
    void Start()
    {
        hologramAnimator = GetComponent<Animator>();
    }
 

    // Update is called once per frame
    void Update()
    {
        // for fix animation bug and when player is dead
        if (!GameManager.Instance.holding && (GameManager.Instance.gameState == GameManager.GameState.Lose ||
            GameManager.Instance.gameState == GameManager.GameState.Play ||
            GameManager.Instance.gameState == GameManager.GameState.Pause) && !PlayerController.Instance.powerUp)
        {
            transform.position = PlayerController.Instance.transform.position;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") &&  !PlayerController.Instance.PlayerAndHologramSamePosition())
        {      
            GameManager.Instance.hologramSpeedForward = 0;
            attackAnimation = RandomAttackMaker();
            hologramAnimator.SetTrigger(attackAnimation);
            SoundManager.Instance.attack1.Play();
            print("hologram_attk");
        }
        

        if (other.CompareTag("Pavement"))
        {
          //  pavementLocalScaleY = other.gameObject.transform.localScale.y;
           // transform.position = new Vector3(transform.position.x + 0.6f, transform.position.y + pavementLocalScaleY, transform.position.z);
            pavementPositionY = other.gameObject.transform.position.y;
            transform.position = new Vector3(transform.position.x,  pavementPositionY, transform.position.z);
            onPavement = true;
            print(pavementPositionY);
        }
        
        else if (other.CompareTag("RightBorder") && onPavement)
        {
            transform.position = new Vector3(transform.position.x + 0.6f, transform.position.y , transform.position.z);
            print("b");

        }
        else if (other.CompareTag("LeftBorder") && onPavement)
        {
            transform.position = new Vector3(transform.position.x - 0.6f, transform.position.y , transform.position.z);
            print("c");

        }

    }

    private void OnTriggerStay(Collider other)
    {
        
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if(PlayerController.Instance.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_run"))
            {
                var var = PlayerController.Instance.playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime -
                    (int)PlayerController.Instance.playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                hologramAnimator.Play("Player_run", 0, var);
            }
            
        }

        if (other.CompareTag("Pavement"))
        {
          //  pavementPositionY = other.gameObject.transform.position.y;
          //  transform.position = new Vector3(transform.position.x, transform.position.y - pavementPositionY, transform.position.z);
            onPavement = false;
        }

    }
    

   string  RandomAttackMaker()
    {
        /*
        int x = Random.Range(0,3);
        if (x == 0)
        {
            return "Attack1";
        }
        else if (x == 2)
        {
            return "Attack2";
        }
        else
        {
            return "Attack3";
        }
        */
        return "Attack2";
    }

}
