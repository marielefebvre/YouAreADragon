using System;
using UnityEngine;


public class PlayerControl : MonoBehaviour
{
    private PlayerCharacter m_Character;
    private bool m_Jump;
    private Vector3 savePos;
	private float timer;

    public GameObject InteractiveIcon;
    public GameObject Fireball;
    public bool canFire = false;


    private void Awake()
    {
        m_Character = GetComponent<PlayerCharacter>();
        savePos = this.transform.position;
    }


    private void Update()
    {
		timer += Time.deltaTime;
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Jump = Input.GetButtonDown("Jump");
        }
        if (m_Character.isPrincess == true && canFire == true && Input.GetButtonDown("Fire") && timer >= 1f)
        {
			timer = 0;
            GameObject fireball = Instantiate(Fireball, this.transform.position, this.transform.rotation) as GameObject;
            fireball.transform.localScale = this.transform.localScale * 2f;
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), fireball.GetComponent<Collider2D>(), true);
            m_Character.PlayClip(AudioId.AUDIO_SHOOT_ID);
        }
    }


    private void FixedUpdate()
    {
        // Read the inputs.
        bool crouch = Input.GetKey(KeyCode.LeftControl);
        float h = Input.GetAxis("Horizontal");
        // Pass all parameters to the character control script.
        m_Character.Move(h, crouch, m_Jump);
        m_Jump = false;
    }

    public void SetSavePos(Vector3 pos)
    {
        this.savePos = pos;
    }

    public void Death()
    {
        Debug.Log("I'm dead :/");
        this.transform.position = this.savePos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Death")
        {
            Death();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (m_Character.isPrincess && other.gameObject.tag == "Interactive")
        {
            this.InteractiveIcon.SetActive(true);
            if (Input.GetButtonDown("Interaction") && m_Character.isPrincess == true)
            {
                m_Character.PlayClip(AudioId.AUDIO_SAVE_ID);
                other.gameObject.GetComponent<InteractiveObject>().SendMessage(other.gameObject.GetComponent<InteractiveObject>().Action);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Interactive")
        {
            this.InteractiveIcon.SetActive(false);
        }
    }
}
