using System;
using System.Collections.Generic;
using UnityEngine;

public class RailManager : MonoBehaviour
{
    public static RailManager Instance = new RailManager();
    
    public List<Transform> rails = new List<Transform>();

    public event Action RailAdded;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 ScoreManager 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 레일 추가 메서드
    public void AddRail(Transform newRail)
    {
        
        rails.Add(newRail);
        //RailAdded?.Invoke();
    }

    // 특정 인덱스의 레일 반환 메서드
    public Transform GetRailAt(int index)
    {
        if (index >= 0 && index < rails.Count)
        {
            return rails[index];
        }
        return null;
    }

    // 총 레일 수 반환 메서드
    public int GetRailCount()
    {
        return rails.Count;
    }

}