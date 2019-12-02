using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItem : MonoBehaviour
{
    private string[] ItemPath = {
        "Prefab/test1",
        "Prefab/test2",
        "Prefab/test3"
    };
    public List<GameObject> ItemList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vec =new Vector3(0, 0, 0);
        if (Input.GetKey("e"))
        {
            ItemSpawn(vec, 0);
        }
        if (Input.GetKey("f"))
        {
            if (ItemList.Count!=0)
            {
                ItemDelete(ItemList[0]);
            }
        }
    }

    bool ItemSpawn(Vector3 pos, int itemNo)
    {
        GameObject prefab = (GameObject)Resources.Load(ItemPath[itemNo]);

        ItemList.Add(Instantiate(prefab, new Vector3(Random.Range(-50.0f, 50.0f), 0, Random.Range(-50.0f, 50.0f)), Quaternion.identity));

        return true;
    }

    bool ItemDelete(GameObject obj)
    {
        if (ItemList.IndexOf(obj)!=-1)
        {
            if (ItemList.Remove(obj))
            {
                Destroy(obj);
                return true;
            }
        }
        return false;
    }
}
