using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffScript : MonoBehaviour
{
    public int randProperties;
    public float cooldownForCollider;
    public GameObject spawnPos, onCatchEffect, onDestroyEffect;
    public Animator animator;
    public CircleCollider2D buffCollider;
    public AudioSource instancingSound, onEnableSound, destroySound;
    void Start()
    {
        randProperties = Random.Range(0, 3);

        StartCoroutine(Respawn());

        if (SettingsScript.isBuffsEnabled == false)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        switch (randProperties)
        {
            case 0:
                {
                    gameObject.name = "Buff0";
                    break;
                }
            case 1:
                {
                    gameObject.name = "Buff1";
                    break;
                }
            case 2:
                {
                    gameObject.name = "Buff2";
                    break;
                }
            case 3:
                {
                    gameObject.name = "Buff3";
                    break;
                }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            Instantiate(onCatchEffect, transform.position, Quaternion.identity);
        
        if (collision.gameObject.tag == "bullet")
            Instantiate(onDestroyEffect, transform.position, Quaternion.identity);

        if (collision.gameObject.tag == "bullet" || collision.gameObject.tag == "Player")       
            transform.position = new Vector2(0, 100);                                           
                                                                                                
        buffCollider.enabled = false;                                                           
        randProperties = Random.Range(0, 4);                                                    
        StartCoroutine(Respawn());                                                              
                                                                                                
        destroySound.Play();                                                                    
        onEnableSound.Stop();                                                                   
    }                                                                                           
                                                                                                
    IEnumerator Respawn()                                                                       
    {                                                                                           
        yield return new WaitForSeconds(15f);
        transform.position = spawnPos.transform.position;
        animator.Play("Inst");
        StartCoroutine(ColliderCooldown());
        instancingSound.Play();
    }

    IEnumerator ColliderCooldown()
    {
        yield return new WaitForSeconds(cooldownForCollider);
        buffCollider.enabled = true;
        onEnableSound.Play();
    }
}
