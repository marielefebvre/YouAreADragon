using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    /*Camera Settings:
     *  - Size : 4.8
     *  - Width : 16 & Height : 15
     *  - Resolution : Ration 16:15
     */

    private GameObject Character;
    private Vector3 startPos = new Vector3(5.11f, -4.8f, -10f);
    private float top = -4.18f;
    private float bottom;
    private float left = 4.48f;
    private float right;


    public int MapWidth = 0;
    public int MapHeight = 0;
    public bool isFollow = false;

	// Use this for initialization
	void Start () {
        this.transform.position = startPos;
        this.Character = GameObject.Find("Player(Clone)");
        this.bottom = ((MapHeight * 0.64f) * -1f) - (6.5f * -0.64f);
        this.right = (MapWidth * 0.64f) - (7f * 0.64f);
    }
	
	// Update is called once per frame
	void Update () {
        if (isFollow)
        {
            Vector3 pos = new Vector3(Character.transform.position.x, Character.transform.position.y, -10f);
            if (pos.x < left)
                pos.x = left;
            else if (pos.x > right)
                pos.x = right;
            if (pos.y < bottom)
                pos.y = bottom;
            else if (pos.y > top)
                pos.y = top;
            this.transform.position = pos;
        }
        else
        {
            float x = Character.transform.position.x / 9.92f;
            float y = Character.transform.position.y / 9.92f;

            // Debug.LogError("(x,y) is " + (int)x + "," + (int)y);

            this.transform.position = new Vector3(5.11f + 10.25f * (int)x, -4.8f + 9.6f * (int)y, -10f);
        }
	}
}
