using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileManager : MonoBehaviour {

    //public GameObject leftTilePrefab;
    //public GameObject topTilePrefab;
    public GameObject currentTile;
    //private bool createGap = false;
    //private bool createIndex = true;
    public Renderer rend;
    public Renderer rendTwo;
    public Collider collision;
    public Material j_Material;
    public Material m_Material;
    private int randomIndex;
    private int jumpIndex = 0; // need to generate atleast 2 more blocks after a jump has been created
    public GameObject ps;
    private GameObject tmpObject = null;
    public AnimationCurve myCurve;
    public float movementSpeed = 100;
    private int jumpBlock = 0;
    public float speed = 5f;
    private Vector3 tmpPos;
    private int level = 1;
    private float timeLeft = 3.0f;
    public Camera cam;
    public Light tmpLight;
    private Vector3 lightPosition;
    private Quaternion lightRotation;
    private int UpperLimit = 20;
    private Light tempObject;
    //private Vector3 jumper = 
    //private int randomIndex;

    public GameObject[] tilePrefabs;

    private static TileManager instance;

    private Stack<GameObject> leftTiles = new Stack<GameObject>();
    private List<GameObject> jumps = new List<GameObject>();

    private Stack<GameObject> topTiles = new Stack<GameObject>();

    private Stack<GameObject> gaps = new Stack<GameObject>();

    public static TileManager Instance
    {
        get
        {
            if(instance == null){
                instance = GameObject.FindObjectOfType<TileManager>();
            }
            return TileManager.instance;
        }
    }


    public Stack<GameObject> LeftTiles
    {
        get
        {
            return this.leftTiles;
        }

        set
        {
            this.leftTiles = value;
        }
    }

    public Stack<GameObject> TopTiles
    {
        get
        {
            return topTiles;
        }

        set
        {
            topTiles = value;
        }
    }

    public Stack<GameObject> Gaps
    {
        get
        {
            return gaps;
        }
        set
        {
            gaps = value;
        }
    }

    public List<GameObject> Jumps
    {
        get
        {
            return jumps;
        }

        set
        {
            jumps = value;
        }
    }




    // Use this for initialization
    void Start () {
        CreateTiles(100);
        for (int i = 0; i < 50; ++i)
        {
            SpawnTile();
        }   
	}

    // Update is called once per frame
    void Update()
    {
        if (tmpObject != null)
        {
            for (int i = 0; i < Jumps.Count; ++i)
            {
                if (Jumps[i].transform.position.y < 3.3f)
                {
                    Jumps[i].transform.Translate(0, speed * Time.deltaTime, 0);
                }
            }
        }

        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            ++level;
            timeLeft = 30.0f;
        }
        if(IsInView(tmpLight, tmpLight)){
            Debug.Log("CAMERA IS IN VIEWPORT");

        }else{
            Debug.Log("CAMERA IS NOT IN VIEWPORT");
        }
        //IsInView(tmpLight, tmpLight);

    }
    public void CreateTiles(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            leftTiles.Push(Instantiate(tilePrefabs[0]));
            TopTiles.Push(Instantiate(tilePrefabs[1]));
           

            TopTiles.Peek().name = "topTile";
            TopTiles.Peek().SetActive(false);
            leftTiles.Peek().name = "LeftTile";
            leftTiles.Peek().SetActive(false);

        }
    }

    public void SpawnTile()
    {
        if(leftTiles.Count == 0 || TopTiles.Count == 0)
        {
            CreateTiles(10);
        }
            //Debug.Log("create index is true");
            if (jumpIndex == 0)
            {
            randomIndex = Random.Range(0, 2);
            }
            int randomGap = Random.Range(5, 10);
            jumpBlock = Random.Range(5, UpperLimit);


        if(randomIndex == 0)
        {
            
                GameObject tmp = leftTiles.Pop();
                //m_Material = tmp.transform.GetChild(0).GetComponent<Renderer>().material;
                tmp.SetActive(true);
                tmp.transform.position = currentTile.transform.GetChild(0).transform.GetChild(randomIndex).position;
                tmp.transform.position = new Vector3(tmp.transform.position.x, 0, tmp.transform.position.z);
                //tmpPos = tmp.transform.position;
                //tmpPos.y = 0;
                if (level > 1)
                {
                    //loop through collection and change each platform color
                    m_Material = tmp.transform.GetChild(1).GetComponent<Renderer>().material;
                    m_Material.color = Color.red;
                    //UpperLimit -= 5;
                }
                if(jumpIndex > 0){
                    --jumpIndex;
                }
          
                if(randomGap == 8 && jumpIndex == 0){
                    //tmp.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                    rend = tmp.transform.GetChild(0).GetComponent<Renderer>();
                    //rendTwo = tmp.transform.GetChild(1).GetComponent<Renderer>();
                    rend.enabled = false;
                    lightPosition = tmp.transform.GetChild(0).position;
                    lightPosition.y = lightPosition.y - 6;
                    lightRotation = tmp.transform.GetChild(0).rotation;
                    //lightRotation.x = 270;
                    tempObject = Instantiate(tmpLight,lightPosition,tmpLight.transform.rotation);
                    tempObject.transform.parent = tmp.transform.GetChild(0);
                    //rendTwo.enabled = false;
                    rend = tmp.transform.GetChild(1).GetComponent<Renderer>();
                    rend.enabled = false;
                    collision = tmp.transform.GetChild(0).GetComponent<Collider>();
                    collision.enabled = false;

                    collision = tmp.transform.GetChild(1).GetComponent<Collider>();
                    collision.enabled = false;
                    jumpIndex = 2;
                    randomIndex = 0;
                    
                }
            if (jumpBlock == 8)
            {
                tmpObject = tmp;
                Jumps.Add(tmpObject);

                m_Material = tmpObject.transform.GetChild(1).GetComponent<Renderer>().material;
                m_Material.color = Color.white;
                //Debug.Log(Jumps.Count);
            }
               
                currentTile = tmp;
        }
        else if(randomIndex == 1)
        {
            
                GameObject tmp = TopTiles.Pop();
                tmp.SetActive(true);
                tmp.transform.position = currentTile.transform.GetChild(0).transform.GetChild(randomIndex).position;
                tmp.transform.position = new Vector3(tmp.transform.position.x, 0, tmp.transform.position.z);
            //tmpPos = tmp.transform.position;
            //tmpPos.y = 0;  
            if (level > 1)
            {
                m_Material = tmp.transform.GetChild(1).GetComponent<Renderer>().material;
                m_Material.color = Color.red;
            }
                if (jumpIndex > 0)
                {
                    --jumpIndex;
                }
                if (randomGap == 6 && jumpIndex == 0)
                {
                //tmp.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    rend = tmp.transform.GetChild(0).GetComponent<Renderer>();
                    //rendTwo = tmp.transform.GetChild(1).GetComponent<Renderer>();
                    rend.enabled = false;
                    rend = tmp.transform.GetChild(1).GetComponent<Renderer>();
                    rend.enabled = false;
                    lightPosition = tmp.transform.GetChild(0).position;
                    lightPosition.y = lightPosition.y - 6;
                    lightRotation = tmp.transform.GetChild(0).rotation;
                    //lightRotation.x = lightRotation.x - 90;
                    tempObject = Instantiate(tmpLight, lightPosition,tmpLight.transform.rotation);
                    tempObject.transform.parent = tmp.transform.GetChild(0);
                    //rendTwo.enabled = false;
                    collision = tmp.transform.GetChild(0).GetComponent<Collider>();
                    collision.enabled = false;
                    collision = tmp.transform.GetChild(1).GetComponent<Collider>();
                    collision.enabled = false;
                    jumpIndex = 2;
                    randomIndex = 1;
                }
            if(jumpBlock == 7){
                tmpObject = tmp;
                Jumps.Add(tmpObject);

                m_Material=tmpObject.transform.GetChild(1).GetComponent<Renderer>().material;
                m_Material.color = Color.white;
                //Debug.Log(Jumps.Count);
            }
                currentTile = tmp;
               
            }

        }

    public void ResetGame()
    {
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene("SampleScene");
    } 

    public bool IsInView(Light origin, Light toCheck)
    {
        Vector3 pointOnScreen = cam.WorldToScreenPoint(toCheck.transform.position);
        //Debug.Log(pointOnScreen);
        //Debug.Log("Screen width: " + Screen.width);
        //Debug.Log("Screen width: " + Screen.height);

        //Is in front
        if (pointOnScreen.z < 0)
        {
            //Debug.Log("Behind: " + toCheck.name);
            return false;
        }

        //Is in FOV
        if ((pointOnScreen.x < 0) || (pointOnScreen.x > Screen.width) ||
                (pointOnScreen.y < 0) || (pointOnScreen.y > Screen.height))
        {
            //Debug.Log("OutOfBounds: " + toCheck.name);
            return false;
        }

        //RaycastHit hit;
        //Vector3 heading = toCheck.transform.position - origin.transform.position;
        //Vector3 direction = heading.normalized;// / heading.magnitude;

        //if (Physics.Linecast(cam.transform.position, toCheck.GetComponentInChildren<Renderer>().bounds.center, out hit))
        //{
        //    if (hit.transform.name != toCheck.name)
        //    {
        //        /* -->
        //        Debug.DrawLine(cam.transform.position, toCheck.GetComponentInChildren<Renderer>().bounds.center, Color.red);
        //        Debug.LogError(toCheck.name + " occluded by " + hit.transform.name);
        //        */
        //        //Debug.Log(toCheck.name + " occluded by " + hit.transform.name);
        //        return false;
        //    }
        //}
        return true;
    }

}
