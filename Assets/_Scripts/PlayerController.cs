using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;


    public GameObject sword;

    public GameObject powerUpEffect;
    public GameObject teleportEffect;
    public GameObject attackEffect;

    public GameObject box;


    public bool ballTrigger;
    public bool powerUp;
    bool onGround;
    bool finish;
    #region Instance Method
    public static PlayerController Instance;
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
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void FixedUpdate()
    { 
        
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_run") 
            && HologramController.Instance.hologramAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_run"))
        {
          
            GameManager.Instance.hologramSpeedForward = 8;
            GameManager.Instance.playerSpeedForward = 8;
            
        }
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_run") && onGround && !finish)
        {
            print("aa");
            SoundManager.Instance.run.mute=false;
        }
        else
        {
            SoundManager.Instance.run.mute = true;
        }
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy2"))
        {

        GameManager.Instance.playerSpeedForward = 0;

            if (!HologramController.Instance.hologramAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_run"))
            {
                Attack(other.gameObject);
                print("player_Attk");
            }
            else
            {
                Death("Dead",2);
            }
           
        }

        else if (other.CompareTag("Ball"))
        {
            Death("Dead2",1);
        }

        else if (other.CompareTag("finish"))
        {
            Finish();
        }

        else if (other.CompareTag("power_up"))
        {
            PowerUp(other.gameObject);
        }

        else if (other.CompareTag("Balls_move"))
        {
            BallsMove();
        }

        else if (other.CompareTag("Pavement"))
        {
           float pavementPositionY= other.gameObject.transform.position.y;
           transform.position = new Vector3(transform.position.x,   pavementPositionY, transform.position.z);
          

        }
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag=="floor")
        {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            onGround = false;
        }
    }


    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Pavement") && GameManager.Instance.holding)
        {
         //  float pavementLocalScaleY = other.gameObject.transform.localScale.y;
         //  transform.position = new Vector3(transform.position.x, transform.position.y - pavementLocalScaleY, transform.position.z);
          // float pavemntPositionY = other.gameObject.transform.position.y;
          // transform.position = new Vector3(transform.position.x, transform.position.y - pavemntPositionY, transform.position.z);
        }


    }

    void PowerUp(GameObject other)
    {
        Destroy(other);
        EffectMaker(powerUpEffect, 0.5f,transform.position);
        powerUp = true;
        GameManager.Instance.hologramClampBacward = 5;
        teleportEffect = powerUpEffect;
    }

    public void TeleportMaker()
    {
        transform.position = HologramController.Instance.transform.position;

        EffectMaker(teleportEffect, 0.5f,transform.position);

    }

    void EffectMaker(GameObject effect, float y, Vector3 position)
    {
        Vector3 v3 = new Vector3(position.x, position.y + y, position.z);
        Destroy(Instantiate(effect, v3, Quaternion.identity), 1);
    }

    void BallsMove()
    {
        ballTrigger = true;
    }

    public bool PlayerAndHologramSamePosition()
    {
        // player animation is chance his local position, so we need that function
        if (HologramController.Instance.transform.position.x - PlayerController.Instance.transform.position.x > -0.1f &&
                HologramController.Instance.transform.position.x - PlayerController.Instance.transform.position.x < 0.1f)
        {
            return true;
        }

        else return false;
    }

    public void Death(string dead, float delay)
    {
        GameManager.Instance.hologramSpeedForward = 0;
        sword.SetActive(false);
        PlayerController.Instance.playerAnimator.SetTrigger(dead);
        HologramController.Instance.hologramAnimator.SetTrigger(dead);
        SoundManager.Instance.death.Play();
        SoundManager.Instance.run.mute = true;
        GameManager.Instance.GameLose(delay);

    }

    public void Attack(GameObject other)
    {
        GameObject enemyTop = GetBrotherGameObject(other, 0);
        enemyTop.GetComponent<Rigidbody>().useGravity = true;
        Destroy(enemyTop.transform.GetChild(0).transform.GetChild(0).gameObject);
        EffectMaker(attackEffect, 0 ,enemyTop.transform.position);

        SoundManager.Instance.attack2.Play();
        playerAnimator.SetTrigger(HologramController.Instance.attackAnimation + "_2");
        HologramController.Instance.hologramAnimator.SetTrigger(HologramController.Instance.attackAnimation + "_2");

    }

    GameObject GetBrotherGameObject(GameObject other, int index)
    {
        return other.transform.parent.transform.GetChild(index).gameObject;

    }

    public void Finish()
    {
        finish = true;
        SoundManager.Instance.run.mute = true;

        GameManager.Instance.GameWin();

    }




}
