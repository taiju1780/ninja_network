using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadCsv : MonoBehaviour
{

    Dictionary<string, Dictionary<string, string>> nameDic = new Dictionary<string, Dictionary<string, string>>();

    public void ReadCSV(string test)
    {
        int csvNumberLines = 0;
        TextAsset csvFile = Resources.Load("CSV/" + test) as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        List<string[]> lisString = new List<string[]>();
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            lisString.Add(line.Split(','));

            SetDictionary(csvNumberLines, lisString);
            csvNumberLines++;
        }

    }


    void SetDictionary(int csvNumberLines, List<string[]> lisString)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        var NAME = lisString[csvNumberLines][0];
        var ATK = lisString[csvNumberLines][1];
        var HP = lisString[csvNumberLines][2];
        dic.Add(lisString[0][0], NAME);
        dic.Add(lisString[0][1], ATK);
        dic.Add(lisString[0][2], HP);
        nameDic.Add(NAME, dic);
    }


    //csvをDictionaryに変換したMonsterDataを返します
    public Dictionary<string, string> GetData(string name)
    {
        var dic = nameDic[name];
        return dic;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
