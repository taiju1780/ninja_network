using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraCtr : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
      

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        //transform.position = transform.parent.position + offset;
    }
}
