using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{  
    [SerializeField] // 프라이베이트여도 유니티에서 설정 가능
    private float moveSpeed = 3f;

    [SerializeField]
    private float jumpForce = 300f;
    public bool isJump = false;

    [SerializeField]
    private GameObject bulletObj = null;

    private bool move = false;
    private float moveHorizontal = 0;

    [SerializeField]
    private GameObject InstantiateObj = null;

    // Update is called once per frame
    void Update()
    {
        if(move == true)
        {
            PlayerMove();
        }

        if(Input.GetButtonDown("Jump"))
        {
            PlayerJump();
        }

        if(Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }
    private void PlayerMove()
    {
       // float h = Input.GetAxis("Horizontal");
        float h = moveHorizontal;
        float playerSpeed = h * moveSpeed * Time.deltaTime;
        Vector3 vector3 = new Vector3();
        vector3.x = playerSpeed;

        transform.Translate(vector3);

        if (h < 0)
        {
            GetComponent<Animator>().SetBool("Walk", true);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (h == 0)
        {
            GetComponent<Animator>().SetBool("Walk", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Walk", true);
            transform.localScale = new Vector3(1, 1, 1);
        }

    }

    private void PlayerJump()
    {
        //점프상태 아닐때만 
        if (isJump == false)
        {
            GetComponent<Animator>().SetBool("Walk", false);
            GetComponent<Animator>().SetBool("Jump", true);

            Vector2 vector2 = new Vector2(0, jumpForce);
            GetComponent<Rigidbody2D>().AddForce(vector2);
            isJump = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Platform")
        {
            GetComponent<Animator>().SetBool("Jump", false);
            isJump = false;
        }
    }

    public void Fire()
    {
        AudioClip audioClip = Resources.Load<AudioClip>("RangedAttack.ogg") as AudioClip;
        GetComponent<AudioSource>().clip = audioClip;
        GetComponent<AudioSource>().Play();

        float direction = transform.localScale.x;
        Quaternion quaternion = new Quaternion(0, 0, 0, 0);

        Instantiate(bulletObj, InstantiateObj.transform.position, quaternion).GetComponent<Bullet>().IntantiateBullet(direction);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            Datamanager.instance.playerHP -= 1;
            if (Datamanager.instance.playerHP < 0)
            {
                Datamanager.instance.playerHP = 0;
            }
            UImanager.instance.PlayerHP();
        }
    }

    public void OnMove(bool _right)
    {
        if(_right)
        {
            moveHorizontal = 1;
        }
        else
        {
            moveHorizontal = 0;
        }
        move = true;
    }

    public void OffMove()
    {
        moveHorizontal = 0;
        move = false;
    }
}
