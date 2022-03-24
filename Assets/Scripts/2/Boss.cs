using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    SpriteRenderer sr;
    Rigidbody2D rb;
    [SerializeField]
    float speed = 2f;
    [SerializeField]
    int hp = 100;
    [SerializeField]
    GameObject bullet;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    IEnumerator Blink()
    {
        sr.enabled = false;
        yield return new WaitForSeconds(0.1f);
        sr.enabled = true;
    }
    IEnumerator Brain()
    {
        while (true)
        {
            float random = Random.Range(0, 100f);
            if (random < 30)
            {
                yield return StartCoroutine(Idle());
            }
            else if (random<50)
            {
                yield return StartCoroutine(Movement());
            }
            else
            {
                yield return StartCoroutine(MoveAndShoot());
            }
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator Idle()
    {
        print("Im idle");
        yield return new WaitForSeconds(2f);
        print("No longer idle");
    }
    IEnumerator Movement()
    {
        rb.velocity = new Vector2(-speed, 0);
        yield return new WaitForSeconds(2f);
        rb.velocity = new Vector2(speed, 0);
        yield return new WaitForSeconds(4f);
        rb.velocity = new Vector2(-speed, 0);
        yield return new WaitForSeconds(2f);
        rb.velocity = new Vector2(0, 0);
    }
    IEnumerator Shoot(float shootingTime)
    {
        float timePassed = 0;
        while (timePassed < shootingTime)
        {
            GameObject bullet1 = Instantiate(bullet);
            bullet1.transform.position = transform.position;
            timePassed += 0.5f;
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator MoveAndShoot()
    {
        StartCoroutine(Shoot(8f));
        StartCoroutine(Movement());
        yield return new WaitForSeconds(8f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            StartCoroutine(Blink());
            hp--;
            if (hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    private void Start()
    {
        StartCoroutine(Brain());
    }
}
