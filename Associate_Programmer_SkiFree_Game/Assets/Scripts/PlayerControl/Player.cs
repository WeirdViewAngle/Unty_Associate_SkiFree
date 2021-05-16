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

        [Tooltip("Player lives")]
        public int lives;

        [Tooltip("Player default forward speed")]
        public float forwardSpeed;

<<<<<<< Updated upstream
        [Tooltip("Player maximum velocity")]
        public float maxPlayerVelocity;
=======
        [Tooltip("Player maximum speed")]
        public float maxSpeed;

        [Tooltip("Player minimum speed")]
        public float minSpeed;

        [Tooltip("Speed boost ammount")]
        public float speedBoost;
>>>>>>> Stashed changes
    }

    public PlayerStats playerStats;

<<<<<<< Updated upstream
    [SerializeField] Rigidbody playerRB;
=======
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
>>>>>>> Stashed changes

    void Update()
    {
        //Check for A/D input and rotate player
        CheckForInput();
    }

    private void FixedUpdate()
    {
        //Check max velocity and limits it
        MaxVelocityCheck();

        //Maintaining acceleration
        ForwardSpeedMaintain();
    }

    private void OnCollisionEnter(Collision collision)
    {
        UIManager._Instance.reduseHealthEvent.Invoke(1);
    }

    #region Player Movement
    void CheckForInput()
    {
<<<<<<< Updated upstream
        if (Input.GetKey(KeyCode.A))
=======
        if (Physics.Raycast(transform.position, Vector3.down, 0.2f))
        {
            onTheGround = true;            
        }
        else
>>>>>>> Stashed changes
        {
            transform.Rotate(Vector3.up, playerStats.rotateSpeed * Time.deltaTime);
        }
<<<<<<< Updated upstream
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, -playerStats.rotateSpeed * Time.deltaTime);
        }
    }

    void MaxVelocityCheck()
    {
        if(playerRB.velocity.magnitude > playerStats.maxPlayerVelocity)
        {
            playerRB.velocity = playerRB.velocity.normalized * playerStats.maxPlayerVelocity;
        }
=======

        AnimChangeBoolGrounded(onTheGround);
    }

    #region Player Movement
    void CheckForInput()
    {        
        if (onTheGround && moving)  
        {
            if (Input.GetKey(left))
                MoveRight();

            else if (Input.GetKey(right))
                MoveLeft();

            if (Input.GetKey(boost))
                BoostSpeed();
        }
    }

    void MoveRight()
    {
        if (transform.eulerAngles.y < 275)
        {
            transform.Rotate(new Vector3(0, playerStats.rotateSpeed * Time.deltaTime, 0), Space.Self);
        }        
>>>>>>> Stashed changes
    }

    void MoveLeft()
    {
<<<<<<< Updated upstream
        playerRB.AddForce(transform.forward * playerStats.forwardSpeed * Time.deltaTime,
                          ForceMode.Impulse);
    }
    #endregion
=======
        if (transform.eulerAngles.y > 91) 
        {
            transform.Rotate(new Vector3(0, -playerStats.rotateSpeed * Time.deltaTime, 0), Space.Self);
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
    void MaxVelocityCheck()
    {
        if(playerStats.speed > playerStats.maxSpeed)
        {
            playerStats.speed = playerStats.maxSpeed;
            AnimChangeFloatSpeed(2);
        }

        if(playerStats.speed < playerStats.maxSpeed &&
            playerStats.speed > playerStats.minSpeed)
        {
            AnimChangeFloatSpeed(0.5f);
        }

        if(playerStats.speed < playerStats.minSpeed)
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
>>>>>>> Stashed changes
}
