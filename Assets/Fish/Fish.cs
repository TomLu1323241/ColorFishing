using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {

    Spawner spawner;
    GameController gameController;

	// Use this for initialization
	void Start () {
        spawner = GameObject.FindObjectOfType<Spawner>();
        gameController = GameObject.FindObjectOfType<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        if (spawner.colors.IndexOf(GetComponent<SpriteRenderer>().color) == gameController.color)
        {
            gameController.width += 50;
            gameController.NextColor();
            GameObject.Destroy(this.gameObject);
        } else
        {
            gameController.width -= 100;
        }
    }
}
