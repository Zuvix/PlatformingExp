using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer sr;
    Rigidbody2D rb;

    //Movement varables
    [SerializeField]
    float speed=3f;
    float inputX;
    float inputY;

    //Shooting variables
    [SerializeField]
    float maxCd = 0.5f;
    float cd = 0;
    [SerializeField]
    Transform gun1;
    [SerializeField]
    Transform gun2;
    [SerializeField]
    GameObject bullet;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX= Input.GetAxisRaw("Horizontal");
        inputY= Input.GetAxisRaw("Vertical");
        Shoot();
    }
    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(inputX, inputY);
        movement = movement.normalized;
        movement = movement * speed;
        rb.velocity = movement;
    }
    void Shoot()
    {
        cd -= Time.deltaTime;
        if(cd<=0 && Input.GetKey(KeyCode.Space))
        {
            GameObject bullet1 =Instantiate(bullet);
            GameObject bullet2 = Instantiate(bullet);
            bullet1.transform.position = gun1.transform.position;
            bullet2.transform.position = gun2.transform.position;
            cd = maxCd;
        }
    }
}
