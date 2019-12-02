using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndFadeAnimation()
    {
        Destroy(this.gameObject);
    }
}
