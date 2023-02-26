using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float power;

    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchLeft;    
    public bool isTouchRight;

    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bullObjA;
    public GameObject bullObjB;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();   
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        Reload();
    }


    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
        {
            h = 0;
        }

        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && h == 1) || (isTouchBottom && h == -1))
        {
            v = 0;
        }
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int)h);
        }
    }

    void Fire()
    {
        if (!Input.GetButton("Fire1"))
            return;

        if (curShotDelay < maxShotDelay)
            return;

        switch (power)
        {
            //Power One
        case 1:
            GameObject bullet = Instantiate(bullObjA, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            break;
        case 2:
            GameObject bulletR = Instantiate(bullObjA, transform.position+Vector3.right*0.1f, transform.rotation);
            GameObject bulletL = Instantiate(bullObjA, transform.position+Vector3.left*0.1f, transform.rotation);
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();     
            rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            break;
        case 3:
            GameObject bulletRR = Instantiate(bullObjA, transform.position + Vector3.right * 0.35f, transform.rotation);
            GameObject bulletLL = Instantiate(bullObjA, transform.position + Vector3.left * 0.35f, transform.rotation);
            GameObject bulletCC = Instantiate(bullObjB, transform.position, transform.rotation);

            Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();

            rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            break;

        }
        curShotDelay = 0;
    
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

     

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
            }
        }
    }
}
