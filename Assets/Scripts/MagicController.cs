using UnityEngine;
using System.Collections;

public class MagicController : MonoBehaviour {

    private float speed = 2f;
    private Collider2D actual;
    private Collider2D[] player;

	// Use this for initialization
	void Start () {
        actual = this.gameObject.GetComponent<Collider2D>();
        player = GameObject.Find("Player(Clone)").GetComponents<Collider2D>();
        Physics2D.IgnoreCollision(actual, player[0], true);
        Physics2D.IgnoreCollision(actual, player[1], true);
    }
	
	// Update is called once per frame
	void Update () {
       this.transform.Translate(new Vector3(this.transform.localScale.x * speed * Time.deltaTime, 0f, 0f));
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag != "Player")
            Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Death")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

}
