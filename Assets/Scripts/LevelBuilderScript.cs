using UnityEngine;
using System.Collections;

public class LevelBuilderScript : MonoBehaviour {

    public GameObject[] obj;
    public TextAsset LevelCSV;
    public GameObject BackgroundBlock;

    public void BuildObject(int x, int y, int value)
    {
        GameObject clone = Instantiate(BackgroundBlock, new Vector3(x * 0.64f + 0.32f, -y * 0.64f + 0.32f, 0f), Quaternion.identity) as GameObject;
        clone.transform.parent = this.transform;
        if (obj[value] == null)
            return;
        clone = Instantiate(obj[value], new Vector3(x * 0.64f + 0.32f, -y * 0.64f + 0.32f, 0f), Quaternion.identity) as GameObject;
        clone.transform.parent = this.transform;
    }

    public void BuildLevel()
    {
        int maxY = 0;
        int maxX = 0;
        int y = 0;
        for (int i = this.transform.childCount; i > 0; i--)
            DestroyImmediate(this.transform.GetChild(i - 1).gameObject);
        string[] level = LevelCSV.text.Split("\n"[0]);
        foreach (string line in level)
        {
            string[] blocks = line.Split(","[0]);
            y++;
            for (int x = 0; x < blocks.Length; x++)
            {
                if (blocks.Length == 1)
                    continue;
                maxX = blocks.Length;
                BuildObject(x, y, int.Parse(blocks[x]));
            }
        }
        maxY = y;

        //Bordure de la map
        for (int j = 0; j <= maxY; j++)
        {
            if (j == 0 || j == maxY)
            {
                for (int i = -1; i <= maxX; i++)
                {
                    BuildObject(i, j, 3); // 3 Pour la valeur d'un block platform
                }
            }
            else
            {
                BuildObject(-1, j, 3); // 3 Pour la valeur d'un block platform
                BuildObject(maxX, j, 3); // 3 Pour la valeur d'un block platform
            }
        }
    }
}
