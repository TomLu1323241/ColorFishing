using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] List<GameObject> fish = new List<GameObject>();
    [SerializeField] public float spawnInterval = 2;
    [SerializeField] public List<Color> colors = new List<Color>();

    public List<GameObject> fishInstances = new List<GameObject>();
    public Coroutine spawningRoutine;

    float seperation;
    float seperationMid;

    // Use this for initialization
    void Start()
    {
        // Get 4 different lanes based off screen ratio
        seperation = Camera.main.orthographicSize * 2 / 4;
        seperationMid = Camera.main.orthographicSize * 2 / 8;

        spawningRoutine = StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        while (true)
        {
            SpawnSet();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnOne(int location, List<GameObject> SpawnList)// Spawns an gameobject from the given list
    {
        GameObject g = GameObject.Instantiate(SpawnList[(int)(Random.value * SpawnList.Count)]);
        g.transform.position = new Vector2(Camera.main.aspect * Camera.main.orthographicSize + 2,
            location * seperation + seperationMid - Camera.main.orthographicSize + 
            (Random.value - 0.5f) * 1f + 1);
        g.GetComponent<Rigidbody2D>().velocity = new Vector2(-4f - Random.value, 0);
        g.transform.SetParent(this.transform);
        int color = (int)(Random.value * colors.Count);
        g.GetComponent<SpriteRenderer>().color = colors[color];
        g.transform.GetChild(0).GetComponent<SpriteRenderer>().color = colors[color];
        fishInstances.Add(g);
        GameObject.Destroy(g, 6f);
    }

    void SpawnSet()
    {
        for (int i = (int)(Random.value + 0.5); i < 3; i++)
        {
            SpawnOne((int)(Random.value * 3), fish);
        }

    }
}
