using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColliderSize : MonoBehaviour
{
    CapsuleCollider area;
    [SerializeField]
    Vector3 center;
    public float timeOut;
    private float timeElapsed;

    bool timeflag;

    // Start is called before the first frame update
    void Start()
    {
        area = GetComponent<CapsuleCollider>();
        center = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * area.radius;
        timeflag = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeflag == true)
        {
            area.radius -= Time.deltaTime * 10;
            area.center = Vector3.MoveTowards(area.center,center, Time.deltaTime*10);

        }
        if (timeElapsed >= timeOut)
        {
            center = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * area.radius;
            timeElapsed = 0.0f;
            timeflag = !timeflag;
        }
    }

    private void OnTriggerExit(Collider player)
    {
        //後から実装
    }
}

