using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Vector2 vecPlus, vecMinus, nVec, bulletPos;
    public int currentBulletRebound = 0, maxBulletReboud, reboundCoefficientUp, reboundCoefficientSide;

    public Rigidbody2D rigidBody;
    public PlayerScript playerScript;
    public BoxCollider2D bulletCollider;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public GameObject sparks, sparksForAnotherBullet;
    public AudioSource stuckSound, hitSound, swordCrossSound;

    void Start()
    {
        
    }

    void Update()
    {
        bulletPos = transform.localPosition;
        if (rigidBody.velocity.x == 0 && rigidBody.velocity.y == 0)
        {
            rigidBody.simulated = false;
            animator.Play("SimOff");
        }    
    }

    void FixedUpdate()
    {
        bulletPos = transform.localPosition;

        if (rigidBody.velocity.x < 0)
        {
            rigidBody.rotation += 0.1f;
            spriteRenderer.flipX = true;
        }

        if (rigidBody.velocity.x > 0)
        {
            rigidBody.rotation -= 0.1f;
            spriteRenderer.flipX = false;
        }
    }

    void Awake()
    {
        bulletPos = transform.localPosition;
        rigidBody.AddForce(nVec * speed, ForceMode2D.Impulse);
        StartCoroutine(Collider());

        maxBulletReboud = Random.Range(1, 5);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            rigidBody.AddForceAtPosition(Vector2.up * reboundCoefficientUp, Vector2.right * reboundCoefficientSide);
            currentBulletRebound++;
        }
        
        if (other.gameObject.tag == "bullet")
        {
            rigidBody.AddForceAtPosition(Vector2.up * 15, Vector2.right * 10);
            Instantiate(sparksForAnotherBullet, transform.position, Quaternion.identity);

            swordCrossSound.pitch = Random.Range(0.9f, 1.1f);
            swordCrossSound.Play();
        }

        if ((other.gameObject.tag == "Player") || (other.gameObject.tag == "Bot"))
            Destroy(gameObject);

        if (currentBulletRebound >= maxBulletReboud)
        {
            rigidBody.simulated = false;
            animator.Play("stuck");

            stuckSound.pitch = Random.Range(0.9f, 1.1f);
            stuckSound.Play();
        }

        if (currentBulletRebound < maxBulletReboud - 1)
        {
            hitSound.pitch = Random.Range(0.9f, 1.1f);
            hitSound.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "swordtrigger1" || other.gameObject.name == "swordtrigger2")
        {
            rigidBody.velocity = new Vector2(-rigidBody.velocity.x, rigidBody.velocity.y);
            Instantiate(sparks, transform.position, Quaternion.identity);
            
            swordCrossSound.pitch = Random.Range(0.9f, 1.1f);
            swordCrossSound.Play();
        }
    }

    IEnumerator Collider()
    {
        yield return new WaitForSeconds(0.025f);
        bulletCollider.enabled = true;
    }
}
