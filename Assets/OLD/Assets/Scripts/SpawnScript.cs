using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject spawn;
    public int x1, x2, y1, y2;
    void Start()
    {
        StartCoroutine(TimeBetwenRelocations());
    }

    IEnumerator TimeBetwenRelocations()
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = new Vector2(Random.Range(x1, x2), Random.Range(y1, y2));
        StartCoroutine(TimeBetwenRelocations());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.position = new Vector2(Random.Range(x1, x2), Random.Range(y1, y2));
        }
    }
}
