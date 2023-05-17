using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JellyManager : MonoBehaviour
{
    public GameObject jellyPrefab = null;
    public string inputFilePath;
    public float spawnPosX = 3.0f;
    public float xScale = 1.0f;
    public float yScale = 1.0f;
    int mapDataReadCount = 0;
    private StreamReader sr;
    private List<string>mapData = new List<string>();
    
    void Awake()
    {
        ReadFile(inputFilePath);
    }
    void OnEnable()
    {
        //!!GameManger Awake보다 여기OnEnable이 먼저 실행되는 문제가 있어서 유니티 자체에서 실행순서를 설정해주었다.
        if (GameManager.Instance != null && GameManager.Instance.GameJellyManager == null)
        {
            GameManager.Instance.GameJellyManager = this;
        }
    }

    void ReadFile(string filePath)
    {
        Debug.Log(Application.dataPath + filePath);
        mapData.Clear();
        sr = new StreamReader(Application.dataPath + filePath);
        while(!sr.EndOfStream)
        {
            mapData.Add(sr.ReadLine());
        }
        mapDataReadCount =0;
    }
    public void SpawnJelly(int startNum, int count)
    {
        if(mapData.Count>0)
        {
            for(int i=mapDataReadCount; i<mapData.Count; i++)
            {
                int x = int.Parse(mapData[i].Split(' ')[0]);
                int y = int.Parse(mapData[i].Split(' ')[1]);
                if(x>=startNum && x<startNum+count)
                {
                    Instantiate(jellyPrefab, transform.position + Vector3.right * spawnPosX + Vector3.right * (x - startNum) * xScale + Vector3.up * y * yScale, transform.rotation, transform);
                    mapDataReadCount = i;
                }
            }
        }
    }
}
