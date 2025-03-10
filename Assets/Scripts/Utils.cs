using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
    public static Vector2Int[] FourDir =
    {
        new Vector2Int(0,0),
        new Vector2Int(0,1),
        new Vector2Int(0,-1),
        new Vector2Int(1,0),
        new Vector2Int(-1,0)
    };
    public static T GetUI<T>(string _name = null) where T : MonoBehaviour
    {
        T component = null;
        if (component == null)
        {
            component = FindInCanvasChildren<T>();
        }
        else if (component == null)
        {
            Debug.Log(component.name + " found in the current scene.");

        }
        return component;
    }
    private static T FindInCanvasChildren<T>() where T : MonoBehaviour
    {
        T component = null;
        GameObject canvas = GameObject.Find("Canvas");

        if (canvas != null)
        {
            component = canvas.GetComponentInChildren<T>(true);
        }

        return component;
    }

    public static List<IPlayerUesableUI> SetPlayerMarcineOnUI()
    {
        var list = new List<IPlayerUesableUI>();
        GameObject canvas = GameObject.Find("Canvas");
        foreach (Transform child in canvas.GetComponentsInChildren<Transform>(true))
        {
            IPlayerUesableUI usableUI = child.GetComponent<IPlayerUesableUI>();
            if (usableUI != null)
            {
                Debug.Log(child.name);
                list.Add(usableUI);
            }
        }
        return list;
    }
    public static Vector3Int ToVector3Int(Vector3 v3)
    {
        return new Vector3Int(Mathf.FloorToInt(v3.x), Mathf.FloorToInt(v3.y), Mathf.FloorToInt(v3.z));
    }
    public static Vector3Int ToVector3Int(Vector2 v2)
    {
        return new Vector3Int(Mathf.FloorToInt(v2.x), 0, Mathf.FloorToInt(v2.y));
    }
    public static Vector2Int ToVector2Int(Vector3 v3)
    {
        return new Vector2Int(Mathf.FloorToInt(v3.x),Mathf.FloorToInt(v3.z));
    }
    public static Vector2Int ToVector2Int(Vector2 v2)
    {
        return new Vector2Int(Mathf.FloorToInt(v2.x), Mathf.FloorToInt(v2.y));
    }
}
