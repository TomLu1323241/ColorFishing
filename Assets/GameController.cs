using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] Text whatColor = null;
    [SerializeField] Image heathBar = null;

    Spawner spawner;

    public float width = 900;
    public float canvusScale = 0.00926f;// Canvus rez 2160 X 1080

    public string[] colors = { "Green", "Red", "Purple", "Yellow", "Orange" };
    public int color = 0;

	// Use this for initialization
	void Start () {
        spawner = GameObject.FindObjectOfType<Spawner>();
        NextColor();
        print(Camera.main.aspect * 1080 * canvusScale);
        heathBar.rectTransform.position = new Vector2(-Camera.main.aspect * 5, -5);
	}
	
    public void NextColor()
    {
        color = (int)(Random.value * colors.Length);
        whatColor.text = "Find" + System.Environment.NewLine + colors[color] + System.Environment.NewLine + "colored fish";
    }

	// Update is called once per frame
	void Update () {
		heathBar.rectTransform.sizeDelta = new Vector2(width -= 0.5f, 50);
    }
}
