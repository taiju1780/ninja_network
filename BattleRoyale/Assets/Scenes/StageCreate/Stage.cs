using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ステージクラス
public class Stage : MonoBehaviour
{
    // フィールド上に落ちているアイテムのリスト
    public List<GameObject> ItemList = new List<GameObject>();
    // アイテムフォルダに入っているプレハブのパスのリスト
    public List<string> PathList = new List<string>();
    
    public GameObject prefab;// 確認用

    // Start is called before the first frame update    
    void Start()
    {
        Init();// 初期化
    }
    
    // Update is called once per frame
    void Update()
    {
        // テスト用
        Vector3 vec =new Vector3(0, 0, 0);
        if (Input.GetKey("c"))
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

    // リストの初期化とアイテムのパスを取得する
    private void Init()
    {
        // 中身のクリア
        ItemList.Clear();
        // アイテムのprefabフォルダからファイルのパスを取得
        string[] FilePaths = System.IO.Directory.GetFiles("Assets/Resources/Prefab/Item/", "*prefab");
        for (int i = 0; i < FilePaths.Length; ++i)
        {
            string s = FilePaths[i].Substring("Assets/Resources/".Length);// リソースからの相対パスに変更
            s = s.Substring(0, s.LastIndexOf('.'));// 拡張子を外してパスのリストに格納
            PathList.Add(s);
        }
    }

    // アイテムを落とした時のフィールド処理
    // obj::落としたアイテムオブジェクト
    public bool ItemDrop(GameObject obj)
    {
        // オブジェクトのタグが「アイテム」だった時に追加する
        if (obj.tag == "Item")
        {
            ItemList.Add(obj);
            return true;
        }
        return false;
    }

    // アイテムの生成を行う関数
    // pos::アイテムの生成位置
    // itemNo::アイテムの識別番号
    public bool ItemSpawn(Vector3 pos, int itemNo)
    {
        prefab = (GameObject)Resources.Load(PathList[itemNo]);
        if (prefab)
        {
            // テスト用
            ItemList.Add(Instantiate(prefab, new Vector3(Random.Range(-50.0f, 50.0f), 50, Random.Range(-50.0f, 50.0f)), new Quaternion()));
            
            //ItemList.Add(Instantiate(prefab, pos,new Quaternion()));
        }
        return true;
    }

    // アイテムの(ステージからの)削除
    // obj::拾ったオブジェクトデータ
    public bool ItemDelete(GameObject obj)
    {
        // リストに存在するオブジェクトかのチェック
        if (ItemList.IndexOf(obj)!=-1)
        {
            // オブジェクトの削除
            if (ItemList.Remove(obj))
            {
                Destroy(obj);
                return true;
            }
        }
        return false;
    }

    // ステージに落ちているアイテムのリストを取得する
    public List<GameObject> GetItemList()
    {
        return ItemList;
    }
}
