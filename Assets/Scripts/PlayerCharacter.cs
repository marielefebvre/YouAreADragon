using System;
using UnityEngine;

public enum AudioId
{
    AUDIO_JUMP_ID = 0,
    AUDIO_SPAWN_ID = 1,
    AUDIO_FINISH_LEVEL_ID = 2,
    AUDIO_DIE_ID = 3,
    AUDIO_SAVE_ID = 4,
    AUDIO_TRANSFORM_ID = 5,
    AUDIO_SHOOT_ID = 6,
    AUDIO_ACTIVATE_ID = 7,
    //AUDIO_ID_COUNT = 8,
};
public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    [SerializeField]
    private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
    [Range(0, 1)]
    [SerializeField]
    private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [SerializeField]
    private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
    [SerializeField]
    private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .1f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private Transform m_CeilingCheck;   // A position marking where to check for ceilings
    const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    private Animator m_Anim;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.

    public bool isPrincess = true;
    public bool canDoubleJump = true; // Ability for the double jump
    private bool isDoubleJumping = false; // Have already double jumping during the jump

    public BoxCollider2D BoxCollider;
    public CircleCollider2D CircleCollider;

    public AudioClip[] clipStoragePrincess;
    public AudioClip[] clipStorageDragon;
    public AudioSource myAudioSource;

    private void Awake()
    {
        // Setting up references.
        m_GroundCheck = transform.Find("GroundCheck");
        m_CeilingCheck = transform.Find("CeilingCheck");
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        PlayClip(AudioId.AUDIO_SPAWN_ID);
    }

    private void FixedUpdate()
    {
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                isDoubleJumping = false;
                m_Anim.SetFloat("Time", 0f);
            }
        }
        m_Anim.SetBool("Ground", m_Grounded);

        // Set the vertical animation
        
            m_Anim.SetFloat("Time", m_Anim.GetFloat("Time") + Time.deltaTime);
    }

    void Update()
    {
        if (Input.GetButtonDown("Transformation"))
        {
            PlayClip(AudioId.AUDIO_TRANSFORM_ID);
            if (m_Anim.GetBool("isPrincess"))
            {
                this.isPrincess = false;
                m_Anim.SetBool("isPrincess", false);
				this.BoxCollider.offset = new Vector2(0.08f, 0.07f);
				this.BoxCollider.size = new Vector2(0.3f, 0.45f);
				this.CircleCollider.offset = new Vector2(0.08f, -0.16f);
				this.CircleCollider.radius = 0.15f;
                /*this.BoxCollider.offset = new Vector2(0.079f, 0.02f);
                this.BoxCollider.size = new Vector2(0.29f, 0.33f);
                this.CircleCollider.offset = new Vector2(0.08f, -0.16f);
                this.CircleCollider.radius = 0.15f;*/
            }
            else
            {
                this.isPrincess = true;
                m_Anim.SetBool("isPrincess", true);
                this.BoxCollider.offset = new Vector2(0.08f, 0.07f);
                this.BoxCollider.size = new Vector2(0.3f, 0.45f);
                this.CircleCollider.offset = new Vector2(0.08f, -0.16f);
                this.CircleCollider.radius = 0.15f;
            }
        }
    }

    public void Move(float move, bool crouch, bool jump)
    {
        // If crouching, check to see if the character can stand up
        /*if (!crouch && m_Anim.GetBool("Crouch"))
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        // Set whether or not the character is crouching in the animator
        m_Anim.SetBool("Crouch", crouch);*/

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            // Reduce the speed if crouching by the crouchSpeed multiplier
            //move = (crouch ? move * m_CrouchSpeed : move);

            // The Speed animator parameter is set to the absolute value of the horizontal input.
            m_Anim.SetFloat("Speed", Mathf.Abs(move));

            // Move the character
            m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump && m_Anim.GetBool("Ground"))
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Anim.SetBool("Ground", false);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            PlayClip(AudioId.AUDIO_JUMP_ID);
        }
        else if(!isPrincess && jump && canDoubleJump && !isDoubleJumping)
        {
            // Add a second vertical force to the player
            m_Rigidbody2D.velocity = Vector3.zero;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            isDoubleJumping = true;
            PlayClip(AudioId.AUDIO_JUMP_ID);
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void PlayClip(AudioId id)
    {
        if (m_Anim.GetBool("isPrincess"))
        {
            myAudioSource.clip = clipStoragePrincess[(int)id];
        }
        else
        {
            myAudioSource.clip = clipStorageDragon[(int)id];
        }
        myAudioSource.Play();
    }
}
