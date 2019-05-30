using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {

    Spawner spawner;
    GameController gameController;
    [SerializeField] GameObject hook = null;

    // Use this for initialization
    void Start () {
        spawner = GameObject.FindObjectOfType<Spawner>();
        gameController = GameObject.FindObjectOfType<GameController>();
    }
	
	void HookBegin()
    {
        hook = GameObject.Instantiate(hook);
        float timeToFish = (6 - this.transform.position.y) / 10;
        hook.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -10);
        hook.transform.position = new Vector2(this.transform.position.x + this.GetComponent<Rigidbody2D>().velocity.x * timeToFish, 6);
        StartCoroutine(HookBack(timeToFish));
    }

    IEnumerator HookBack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        hook.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
        this.GetComponent<CircleCollider2D>().enabled = false;
    }

    private void OnMouseDown()
    {
        if (spawner.colors.IndexOf(GetComponent<SpriteRenderer>().color) == gameController.color)
        {
            gameController.width += 50;
            gameController.NextColor();
            HookBegin();
            //GameObject.Destroy(this.gameObject);
        } else
        {
            gameController.width -= 100;
        }
    }
}
