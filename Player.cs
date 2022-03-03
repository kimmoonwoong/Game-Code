using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    public float maxspeed;
    public float jumppower;
    public Transform pos;
    public Vector2 boxSize;
    public Transform leftpos;
    public Vector2 leftboxSize;
    public int attackPower;
    public int health;
    public int jumpcount = 2;
    public int mp;
    public GameObject monster;
    bool isleftviwecheck;
    bool isrightviwecheck;
    bool isstopright;
    bool isstopleft;
    float maxPositionY;
    Animator anim;
    SpriteRenderer sprit;
    int count = 0;
    // Start is called before the first frame update

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprit = GetComponent<SpriteRenderer>();
        maxPositionY = transform.position.y;
    }
    void move()
    {
        if (Input.GetButtonUp("Horizontal"))
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        if (isrightviwecheck)
        {
            if (Input.GetKey(KeyCode.A))
            {
                sprit.flipX = true;
                isleftviwecheck = false;
            }
        }
        if (isleftviwecheck)
        {
            if (Input.GetKey(KeyCode.D))
            {
                sprit.flipX = false;
                isrightviwecheck = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            isrightviwecheck = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
            isleftviwecheck = true;

        if (Input.GetKey(KeyCode.D))
            isstopright = true;
        if (Input.GetKey(KeyCode.A))
            isstopleft = true;
        if (Input.GetKeyUp(KeyCode.A))
            isstopleft = false;
        else if (Input.GetKeyUp(KeyCode.D))
            isstopright = false;
        if (isstopleft == true && isstopright == true)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }
            if (rigid.velocity.normalized.x == 0)
                anim.SetBool("isrun", false);
            else
                anim.SetBool("isrun", true);
        if (jumpcount > 0)
        {
            if (Input.GetButtonUp("Jump"))
            {
                if(jumpcount == 1)
                {
                    rigid.velocity = new Vector2(0, 0);
                }
                rigid.AddForce(Vector2.up * jumppower, ForceMode2D.Impulse);
                anim.SetBool("Isjump", true);
                jumpcount--;
                if (Input.GetMouseButtonDown(0))
                {
                    anim.SetBool("Isjump", false);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("Isattack", false);
            count = 0;
            maxspeed = 5;
        }

        else if (Input.GetMouseButtonDown(0))
        {
            if (count == 0)
            {
                anim.SetBool("Isattack", true);
            }
            else
            {
                anim.SetBool("Isattack", false);
            }
        }
        if(count > 0)
        {
            anim.SetBool("Isattack", false);
        }
       
    }
    void PlusCount()
    {
        count += 1;
    }
    void Attack()
    {
        Collider2D[] collider2D = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        Collider2D[] leftcollider2D = Physics2D.OverlapBoxAll(leftpos.position, boxSize, 0);
        foreach (Collider2D collider in collider2D)
        {
            if (collider.tag == "Monster")
            {
                collider.GetComponent<Monster>().OnHit(attackPower);
            }
        }
        foreach (Collider2D collider in leftcollider2D)
        {
            if (collider.tag == "Monster")
            {
                collider.GetComponent<Monster>().OnHit(attackPower);
            }

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
        Gizmos.DrawWireCube(leftpos.position, leftboxSize);
    }
    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        //Move Speed
        rigid.AddForce(Vector2.right * x * 2, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxspeed)
            rigid.velocity = new Vector2(maxspeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxspeed * (-1))
            rigid.velocity = new Vector2(maxspeed * (-1), rigid.velocity.y);

        if (rigid.velocity.y < 0)
        {

            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1f)
                {
                    anim.SetBool("Isjump", false);
                    anim.SetBool("isDown", false);
                    jumpcount = 2;
                    Debug.Log("dd");
                }
            }
        }
        if (rigid.velocity.y < 0 && maxPositionY < transform.position.y)
        {
            maxPositionY = transform.position.y;
        }
        Debug.Log(maxPositionY);
        if(maxPositionY > transform.position.y && anim.GetBool("Isjump"))
        {
            Debug.Log("DD");
            anim.SetBool("isDown", true);
            anim.SetBool("Isjump", false);
        }
        else if(maxPositionY < transform.position.y && anim.GetBool("Isjump"))
        {
            anim.SetBool("Isjump", true);
            anim.SetBool("isDown", false);
        }
        
    }
    public void OnHit(int dmg)
    {
        health -= dmg;
        anim.SetBool("isHit", true);
        anim.SetBool("isrun", false);
        Invoke("ReAnimation", 0.4f);
        if(health > 5)
        {
            health = 5;
        }
        if (health <= 0)
        {
            anim.SetBool("isDead", true);
            Invoke("Destroy", 0.4f);
        }
    }
    void ReAnimation()
    {
        anim.SetBool("isHit", false);
    }
    void Destroy()
    {
        gameObject.SetActive(false);
    }
    public float Lenght()
    {
        float playerLenght = rigid.position.x;
        return playerLenght;
    }
    // Update is called once per frame
    void Update()
    {
        move();
    }
}
