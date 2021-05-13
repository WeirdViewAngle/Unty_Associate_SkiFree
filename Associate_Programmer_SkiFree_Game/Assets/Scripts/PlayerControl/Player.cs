using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [System.Serializable]
    public struct PlayerStats
    {
        [Tooltip("Player Rotate Speed")]
        public float rotateSpeed;

        [Tooltip("How much player gain moving towards center")]
        public float rotateAcceleration;

        [Tooltip("How much player lose moving to the side")]
        public float rotateDeceleration;

        [Tooltip("Player default forward speed")]
        public float speed;

        [Tooltip("Player maximum speed")]
        public float maxSpeed;
    }

    public PlayerStats playerStats;

    public KeyCode left, right;


    Animator playerAnim;

    Rigidbody playerRB;

    bool onTheGround = true, moving = true;

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Check for A/D input and rotate player
        CheckForInput();

        //Animations
        AnimationCheck();
    }

    private void FixedUpdate()
    {
        //Check max velocity and limits it
        MaxVelocityCheck();

        //Maintaining acceleration
        ForwardSpeedMaintain();

        //Check mid air state
        CheckForGround();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //UIManager._Instance.reduseHealthEvent.Invoke(1);
    }

    void CheckForGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 0.2f))
        {
            onTheGround = true;
        }
        else
        {
            onTheGround = false;
        }
    }

    #region Player Movement
    void CheckForInput()
    {        
        if (onTheGround && moving)  
        {
            if (Input.GetKey(left) && transform.rotation.eulerAngles.y > 275)
            {
                transform.Rotate(new Vector3(0, playerStats.rotateSpeed * Time.deltaTime, 0), Space.Self);
            }
            else if (Input.GetKey(right) && transform.rotation.eulerAngles.y < 90)
            {
                transform.Rotate(new Vector3(0, -playerStats.rotateSpeed * Time.deltaTime, 0), Space.Self);
                //transform.Rotate(Vector3.up, -playerStats.rotateSpeed * Time.deltaTime);
            }
        }
    }


    void MaxVelocityCheck()
    {
        if(playerRB.velocity.magnitude > playerStats.maxSpeed)
        {
            playerRB.velocity = playerRB.velocity.normalized * playerStats.maxSpeed;
        }
    }

    void ForwardSpeedMaintain()
    {
        if (moving)
        {
            playerRB.AddForce(transform.forward * playerStats.speed * Time.fixedDeltaTime,
                              ForceMode.Impulse);
        }
    }
    #endregion

    #region Animations
    
    void AnimationCheck()
    {
        if (playerRB.velocity.magnitude >= playerStats.maxSpeed)
            AnimChangeFloatSpeed(2);

        if(playerRB.velocity.magnitude < playerStats.maxSpeed)
            AnimChangeFloatSpeed(0.5f);

        if(!moving || playerRB.velocity == Vector3.zero)
            AnimChangeFloatSpeed(0);

        AnimChangeBoolGrounded(onTheGround);
    }

    void AnimChangeFloatSpeed(float parseFloat)
    {
        playerAnim.SetFloat("playerSpeed", parseFloat);
    }

    void AnimChangeBoolGrounded(bool parseBool)
    {
        playerAnim.SetBool("grounded", parseBool);
    }

    #endregion
}
