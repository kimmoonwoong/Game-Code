using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public int helth;
    public int attackpower;
    public Transform target;
    Animator anim;
    Rigidbody2D rigid;
    public Transform pos;
    public Vector2 boxSize;
    public GameObject player;
    bool isStop = true;
    public Transform leftpos;
    public Transform rightpos;
    public Vector2 leftboxSize;
    public Vector2 rightboxSize;
    public string monstername;
    SpriteRenderer sprit;
    GameObject itemDB = null;
    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sprit = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Player play = player.GetComponent<Player>();
        Vector2 frontVec = new Vector2(transform.position.x , rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.left*4, new Color(0, 1, 0));
        Debug.DrawRay(frontVec, Vector3.right*4, new Color(0, 1, 0));
        RaycastHit2D leftrayHit = Physics2D.Raycast(frontVec, Vector3.left, 4, LayerMask.GetMask("Player"));
        RaycastHit2D rightrayHit = Physics2D.Raycast(frontVec, Vector3.right,4, LayerMask.GetMask("Player"));
        switch (monstername)
        {
            case "nomalmonster":
                if (leftrayHit.collider != null || rightrayHit.collider != null)
                {
                    if (isStop)
                    {
                        anim.SetBool("isRun", true);
                        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                    }
                    if (helth <= 0)
                    {
                        rigid.velocity = Vector2.zero;
                        isStop = false;
                    }

                }
                else
                {
                    anim.SetBool("isRun", false);
                    rigid.velocity = Vector2.zero;
                }   
                break;
            case "flymonster":
                float ch = Mathf.Abs(play.Lenght()) - Mathf.Abs(rigid.position.x);
                if (ch < 3f)
                {
                    if (isStop)
                    {
                        anim.SetBool("isRun", true);
                        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                    }
                    if (helth <= 0)
                    {
                        rigid.velocity = Vector2.zero;
                        isStop = false;
                    }
                }
                else if(ch>4f)
                {
                    anim.SetBool("isRun", false);
                    rigid.velocity = Vector2.zero;
                }
                break;
        }
      
    }
    public void Attack()
    {
        Player play = player.GetComponent<Player>();
        float ch = Mathf.Abs(play.Lenght()) - Mathf.Abs(rigid.position.x);
        float playerposcheck = rigid.position.x - play.Lenght();
        Collider2D[] collider2D = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        Collider2D[] leftcollider2D = Physics2D.OverlapBoxAll(leftpos.position, leftboxSize, 0);
        Collider2D[] rightcollider2D = Physics2D.OverlapBoxAll(rightpos.position, rightboxSize, 0);
        if (playerposcheck > 0)
            sprit.flipX = true;
        else
            sprit.flipX = false;
        foreach (Collider2D collider in leftcollider2D)
        {
                if (collider.tag == "Player")
                {
                    rigid.velocity = Vector2.zero;
                    anim.SetBool("isRun", false);
                    isStop = false;
                    if (ch < 2.4f)
                    {
                        anim.SetBool("isAttack", true);
                        if (anim.GetBool("isHit"))
                            anim.SetBool("isAttack", false);

                    }
                }
                if (ch > 2.4f)
                {
                    isStop = true;
                    anim.SetBool("isAttack", false);

                }
        }
        foreach (Collider2D collider in rightcollider2D)
        {
                if (collider.tag == "Player")
                {
                    rigid.velocity = Vector2.zero;
                    anim.SetBool("isRun", false);
                    isStop = false;
                    if (ch < 2.4f)
                    {
                        anim.SetBool("isAttack", true);
                        if (anim.GetBool("isHit"))
                        {
                            anim.SetBool("isAttack", false);
                            Debug.Log("dd");
                        }
                }
                }
                if (ch > 2.4f)
                {
                    anim.SetBool("isAttack", false);
                    isStop = true;
                }
        }
        
    }
    void Update()
    {
        Attack();
    }
    void Damage()
    {
        Player play = player.GetComponent<Player>();
        play.OnHit(attackpower);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
        Gizmos.DrawWireCube(leftpos.position, boxSize);
        Gizmos.DrawWireCube(rightpos.position, boxSize);
    }
    public void OnHit(int dmg)
    {
        helth -= dmg;
        Debug.Log("dd");
        anim.SetBool("isHit", true);
        anim.SetBool("isRun", false);
        Invoke("ReAnimation", 0.4f);
        if (helth<=0)
        {
            itemDB = GameObject.Find("ItemDateBase");
            ItemDateBase itemDateBase = itemDB.GetComponent<ItemDateBase>();
            GameObject go = Instantiate(itemDateBase.fieldItemPrefab, transform.position, Quaternion.identity);
            go.GetComponent<FIeldItem>().SetItem(itemDateBase.itemDB[Random.Range(0, 3)]);
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
}