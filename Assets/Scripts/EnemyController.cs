using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public bool IsStatic = false;
    public int top = 0;
    public int bottom = 0;
    public int left = 0;
    public int right = 0;


    private Vector3 startPos;
    private bool isReturn = false; // par default, va en haut ou a droite, le retour se fait dans l'autre sens

	// Use this for initialization
	void Start () {
        this.startPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	if (!IsStatic)
        {
            if (!isReturn)
            {
                if (top > 0)
                {
                    this.transform.Translate(new Vector3(0, 1 * Time.deltaTime, 0));
                    if (this.transform.position.y >= this.startPos.y + (top * 0.64f))
                        isReturn = true;
                }
                else if (right > 0)
                {
                    this.transform.Translate(new Vector3(1 * Time.deltaTime, 0, 0));
                    if (this.transform.position.x >= this.startPos.x + (right * 0.64f))
                        isReturn = true;
                }
            }
            else
            {
                if (bottom > 0)
                {
                    this.transform.Translate(new Vector3(0, -1 * Time.deltaTime, 0));
                    if (this.transform.position.y <= this.startPos.y - (bottom * 0.64f))
                        isReturn = false;
                }
                else if (left > 0)
                {
                    this.transform.Translate(new Vector3(-1 * Time.deltaTime, 0, 0));
                    if (this.transform.position.x <= this.startPos.x - (left * 0.64f))
                        isReturn = false;
                }
            }
        }
	}
}
