using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    // Use this for initialization
    Animator anim;
    public float speed;
    public int jumpForce;
    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;
    private static playerController instance;
    private bool isDead = false;
    public GameObject resetBtn;
    public float threshold;
    private bool jump = false;
    private Vector3 dir;
    public float RotateSpeed = 30f;
    public bool direction = true;

    public static playerController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<playerController>();
            }
            return playerController.instance;
        }
    }

    public bool IsDead
    {
        get
        {
            return isDead;
        }

        set
        {
            isDead = value;
        }
    }

	void Start () {
        anim = GetComponent<Animator>();
        dir = Vector3.zero;
		
	}
	
	// Update is called once per frame
	void Update () {
        //float move = Input.GetAxis("Vertical");
        //Debug.Log(move);
        //

        if (Input.GetMouseButtonDown(0) && !IsDead)
        {
            
            if (jump == false)
            {
                
                anim.SetTrigger("Active");
                if (direction)
                {
                    
                    transform.eulerAngles = new Vector3(0, -90, 0);
                    Debug.Log("left");
                    dir = Vector3.forward;
                    direction = false;
                }
                else
                {
                    dir = Vector3.forward;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    direction = true;
                    Debug.Log("forward");
                }
            }

        }else if (Input.GetKeyDown("space") && !IsDead)
        {
            

            if (jump == false)
            {

                this.GetComponent<Rigidbody>().velocity = Vector3.up * jumpForce;
                jump = true;

            }
            anim.SetTrigger("jump");
        }
        float amountMove = speed * Time.deltaTime;
        //Debug.Log(amountMove);
        //anim.SetFloat("speed",amountMove);
        transform.Translate(dir * amountMove);
		
	}

    private void FixedUpdate()
    {

        if (this.transform.position.y < threshold)
        {
            //this.transform.position = new Vector3(-2.341488f, 3.6f, -9.1f);
            IsDead = true;
            resetBtn.SetActive(true);
            //mainCam = null;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Ground")
        {
            jump = false;
            //Debug.Log(collision.gameObject.tag);
        }
    }
}
