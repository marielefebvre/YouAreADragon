using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractiveObject : MonoBehaviour {

    public string Action;
    public List<GameObject> Targets = new List<GameObject>();
    public SceneFadeInOut fade;

    private bool isActivate = false;
	void Test()
    {
        Debug.LogError("test");
    }

    void Save()
    {
        GameObject player = GameObject.Find("Player(Clone)");
        player.GetComponent<PlayerControl>().SetSavePos(this.transform.position);
    }

    void Death()
    {
        GameObject player = GameObject.Find("Player(Clone)");
        player.GetComponent<PlayerControl>().Death();
    }

    void ToggleActivation()
    {
        if (!isActivate)
        {
            this.isActivate = true;
            this.transform.localScale = new Vector3(-1, this.transform.localScale.y, this.transform.localScale.z);
            for (int i = Targets.Count - 1; i >= 0; i--)
            {
                Targets[i].SetActive(!(Targets[i].active));
            }
        }
    }

    void GetDoubleJump()
    {
        GameObject player = GameObject.Find("Player(Clone)");
        player.GetComponent<PlayerCharacter>().canDoubleJump = true;
        player.GetComponent<PlayerControl>().InteractiveIcon.SetActive(false);
        Targets[0].SetActive(true);
        Destroy(Targets[0], 8f);
        Destroy(this.gameObject);
    }

    void GetFire()
    {
        GameObject player = GameObject.Find("Player(Clone)");
        player.GetComponent<PlayerControl>().canFire = true;
        player.GetComponent<PlayerControl>().InteractiveIcon.SetActive(false);
        Targets[0].SetActive(true);
        Destroy(Targets[0], 8f);
        Destroy(this.gameObject);
    }

    void AffText()
    {
        Targets[0].SetActive(true);
        Destroy(Targets[0], 4f);
        Destroy(this.gameObject);
    }


    void NextLevel1()
    {
        fade.sceneEnding = true;
        fade.level = "Level1";
    }

    void NextLevel2()
    {
        fade.sceneEnding = true;
        fade.level = "Level2";
    }

    void NextLevel3()
    {
        fade.sceneEnding = true;
        fade.level = "Level3";
    }

    void NextLevel4()
    {
        fade.sceneEnding = true;
        fade.level = "Level4";
    }

    void NextLevel5()
    {
        fade.sceneEnding = true;
        fade.level = "Level5";
    }
}
