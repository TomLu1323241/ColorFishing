using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CFGameController : MonoBehaviour {

    [SerializeField] Text whatColor = null;
    [SerializeField] Image heathBar = null;
    [SerializeField] float drain = 0.0008f;
    [SerializeField] public Text score = null;

    CFSpawner spawner;

    public float healthPercent = 1;

    public string[] colors = { "Green", "Red", "Purple", "Yellow", "Orange" };// This needs to be changed with the colors on spawner
    public int colorLast = 0;
    public int color = 0;

	void Start () {
        spawner = GameObject.FindObjectOfType<CFSpawner>();
        NextColor();
        heathBar.rectTransform.position = new Vector2(-Camera.main.aspect * 5, -5);// Move health bar based on screen ratio
        score.rectTransform.position = new Vector2(Camera.main.aspect * 5, -5);// Move score based on screen ratio
    }
	
    public void NextColor()
    {
        while (colorLast == color)// Makes sure it is differnt color
        {
            color = (int)(Random.value * colors.Length);
        }
        whatColor.text = "Find " + colors[color] + " fish";
        whatColor.color = spawner.colors[color];
        colorLast = color;
    }

	// Update is called once per frame
	void Update () {
        if (healthPercent > 1)// Prevent going over 100% health
        {
            healthPercent = 1;
        } else if (healthPercent < 0)
        {
            for (int i = 0; i < spawner.fishInstance.Count; i++)// Removes collider to prevent clicking on fish after game as endded
            {
                if (spawner.fishInstance[i] == null)
                {
                    continue;
                }
                spawner.fishInstance[i].GetComponent<CircleCollider2D>().enabled = false;
            }
            spawner.StopAllCoroutines();// Stops the spawn of fish
            whatColor.text = "Good Game!";
            whatColor.color = new Color32(0, 0, 0, 255);
        }
        heathBar.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = healthPercent -= drain;// Drains hunger slowly
    }
}
