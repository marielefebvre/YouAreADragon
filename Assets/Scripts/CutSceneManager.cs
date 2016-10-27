using UnityEngine;
using System.Collections;

public class CutSceneManager : MonoBehaviour {

    public float wait = 23f;
    public string scene;

    private float time;

	// Use this for initialization
	void Start () {
        time = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= wait)
        {
            Application.LoadLevel(scene);
        }
	}
}
