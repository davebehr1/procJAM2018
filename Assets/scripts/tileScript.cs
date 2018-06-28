using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileScript : MonoBehaviour {

    public float fallDelay = 1.5F;
    public Material matAssign;
    public Collider collision;
    private Vector3 tmpPos;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
    void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            TileManager.Instance.SpawnTile();
            StartCoroutine(FallDown());
        }
    }

    IEnumerator FallDown()
    {
        yield return new WaitForSeconds(fallDelay);
        GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(1);
        switch(gameObject.name)
        {
            case "LeftTile":
                
                TileManager.Instance.LeftTiles.Push(gameObject);

                Debug.Log("position" + gameObject.transform.position);
                matAssign = gameObject.transform.GetChild(0).GetComponent<Renderer>().material;
                matAssign = TileManager.Instance.m_Material;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                collision = gameObject.transform.GetChild(0).GetComponent<Collider>();
                collision.enabled = true;
                collision = gameObject.transform.GetChild(1).GetComponent<Collider>();
                collision.enabled = true;
                //gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.blue);

                //collision = tmp.transform.GetChild(0).GetComponent<Collider>();
                gameObject.SetActive(false);
                break;
            case "topTile":
                
                TileManager.Instance.TopTiles.Push(gameObject);

                matAssign = gameObject.transform.GetChild(0).GetComponent<Renderer>().material;
                matAssign = TileManager.Instance.m_Material;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                collision = gameObject.transform.GetChild(0).GetComponent<Collider>();
                collision.enabled = true;
                collision = gameObject.transform.GetChild(1).GetComponent<Collider>();
                collision.enabled = true;

                //gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                gameObject.SetActive(false);
                break;
        }
    }
}