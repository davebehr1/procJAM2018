using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float speed;
    //private bool jumpState = false;
    public int jumpForce;
    private bool jump = false;
    public float threshold;
    private bool isDead = false;

    public GameObject resetBtn;

    public Camera mainCam;

    private Vector3 dir;



    private static PlayerScript instance;

    public static PlayerScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerScript>();
            }
            return PlayerScript.instance;
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

    // Use this for initialization
	void Start ()
    {
        dir = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(Input.GetMouseButtonDown(0) && !IsDead){
            if (jump == false)
            {
                if (dir == Vector3.forward)
                {
                    dir = Vector3.left;
                }
                else
                {
                    dir = Vector3.forward;
                }
            }
            
        }else if (Input.GetKeyDown("space") && !IsDead)
        {
            if (jump == false)
            {
                
                this.GetComponent<Rigidbody>().velocity = Vector3.up * jumpForce;
                jump = true;

            }
        }
        float amountMove = speed * Time.deltaTime;
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
        
        if(collision.gameObject.tag == "Ground"){
            jump = false;
            //Debug.Log(collision.gameObject.tag);
        }
	}


}
