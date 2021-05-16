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

        [Tooltip("Speed increase for moving forward")]
        public float rotateAcceleration;

        [Tooltip("Speed decrease for moving to the sides")]
        public float rotateDeceleration;

        [Tooltip("Player  speed")]
        public float speed;

        [Tooltip("Player maximum speed")]
        public float maxSpeed;

        [Tooltip("Player minimum speed")]
        public float minSpeed;

        [Tooltip("Speed boost ammount")]
        public float speedBoost;
    }

    public PlayerStats playerStats;

    public KeyCode left, right, boost;


    Animator playerAnim;

    Rigidbody playerRB;

    public bool moving;
    bool onTheGround = true, boostActivated = false;

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Check for A/D input and rotate player
        CheckForInput();
    }

    private void FixedUpdate()
    {
        //Check max velocity and limits it
        SpeedCheck();

        //Maintaining acceleration
        ForwardSpeedMaintain();

        //Ground raycast
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
            AnimChangeBoolGrounded(onTheGround);
        }
        else
        {
            onTheGround = false;
            AnimChangeBoolGrounded(onTheGround);
        }
    }

    #region Player Controls
    void CheckForInput()
    {
        if (onTheGround && moving)
        {
            if (onTheGround && moving)
            {
                if (Input.GetKey(left))
                {
                    MoveLeft();
                }

                if (Input.GetKey(right))
                {
                    MoveRight();
                }

                if (Input.GetKey(boost))
                {
                    BoostSpeed();
                }
            }
        }

    }

    void MoveRight()
    {
        if (transform.eulerAngles.y > 91)
        {
            transform.Rotate(new Vector3(0, -playerStats.rotateSpeed * Time.deltaTime, 0), Space.Self);
        }
    }        
    
    void MoveLeft()
    {        
        if (transform.eulerAngles.y < 274)
        {
            transform.Rotate(new Vector3(0, playerStats.rotateSpeed * Time.deltaTime, 0), Space.Self);
        }
    }
    
    void BoostSpeed()
    {
        if (!boostActivated)
         {
            playerRB.AddForce(transform.forward * Time.fixedDeltaTime * playerStats.speedBoost,
                          ForceMode.Impulse);
            playerStats.maxSpeed *= 1.5f;

            boostActivated = true;
            StartCoroutine("WaitCoroutine", 5);           
        }
    }
    void SpeedCheck()
    {
        if(playerStats.speed >= playerStats.maxSpeed)
        {
            playerStats.speed = playerStats.maxSpeed;
            AnimChangeFloatSpeed(2);
        }

        if(playerStats.speed < playerStats.maxSpeed &&
            playerStats.speed > playerStats.minSpeed)
        {
            AnimChangeFloatSpeed(0.5f);
        }

        if(playerStats.speed <= playerStats.minSpeed)
        {
            playerStats.speed = playerStats.minSpeed;
            AnimChangeFloatSpeed(0);
        }
    }

    void ForwardSpeedMaintain()
    {
        if (moving)
        {
            float absAngle = Mathf.Abs(180 - transform.eulerAngles.y);
            playerStats.speed += Remap(0, 90, playerStats.rotateAcceleration, -playerStats.rotateDeceleration, absAngle);

            Vector3 velocity = transform.forward * playerStats.speed * Time.fixedDeltaTime;
            velocity.y = playerRB.velocity.y;
            playerRB.velocity = velocity;
        }
    }

    float Remap(float oldMin, float oldMax, float newMin, float newMax, float oldValue)
    {
        float oldRange = (oldMax - oldMin);
        float newRange = (newMax - newMin);
        float newValue = (((oldValue - oldMin) * newRange) / oldRange) + newMin;
        return newValue;
    }
    #endregion

    #region Animations
    void AnimChangeFloatSpeed(float parseFloat)
    {
        playerAnim.SetFloat("playerSpeed", parseFloat);
    }

    void AnimChangeBoolGrounded(bool parseBool)
    {
        playerAnim.SetBool("grounded", parseBool);
    }

    #endregion

    IEnumerator WaitCoroutine(float time)
    {
        yield return new WaitForSecondsRealtime(time); 
        boostActivated = false;
        playerStats.maxSpeed /= 1.5f;
    }
}
