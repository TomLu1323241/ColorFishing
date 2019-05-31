using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFFish : MonoBehaviour {

    CFSpawner spawner;
    CFGameController gameController;
    [SerializeField] GameObject hook = null;

    void Start () {
        spawner = GameObject.FindObjectOfType<CFSpawner>();
        gameController = GameObject.FindObjectOfType<CFGameController>();
    }
	
	void HookBegin()
    {
        hook = GameObject.Instantiate(hook);
        float timeToFish = (6 - this.transform.position.y) / 20;// 20 is hook speed, 6 is spawn location which is out of the camera
        hook.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -20);// need to change here too if you wanna change hook speed
        hook.transform.position = new Vector2(this.transform.position.x + this.GetComponent<Rigidbody2D>().velocity.x * timeToFish, 6);// Calculated Velocity
        this.GetComponent<CircleCollider2D>().enabled = false;// Fish can't hit others on way up
        StartCoroutine(HookBack(timeToFish));
    }

    IEnumerator HookBack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        hook.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 20);
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 20);
        yield return new WaitForSeconds(3f);
        GameObject.Destroy(hook);
    }

    private void OnMouseDown()
    {
        if (spawner.colors.IndexOf(GetComponent<SpriteRenderer>().color) == gameController.color)// Checks color of fish
        {
            gameController.healthPercent += 0.1f;
            gameController.NextColor();
            HookBegin();
            gameController.score.text = "Score : " + (System.Int32.Parse(gameController.score.text.Substring(8, gameController.score.text.Length - 8)) + 100);
        } else
        {
            gameController.healthPercent -= 0.2f;
        }
    }
}
