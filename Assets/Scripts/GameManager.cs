using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject HowToPlayImg;
    public GameObject Menu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey == true && HowToPlayImg.active == true)
        {
            HowToPlayImg.SetActive(false);
            Menu.SetActive(true);
        }
    }

    public void BeginGame()
    {
        Application.LoadLevel("intro");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HowToPlay()
    {
        HowToPlayImg.SetActive(true);
        Menu.SetActive(false);
    }
}
