using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rota : MonoBehaviour
{
    public GameObject player;
    float angle = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angle = 0.25f;
        transform.Rotate(new Vector3(0,angle,0));
    }

    void OnCollisionEnter(Collision col)
    {
        col.gameObject.transform.SetParent(transform);
    }

    void OnCollisionExit(Collision col)
    {
        col.gameObject.transform.SetParent(null);
    }
}
