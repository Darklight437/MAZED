#if (UNITY_EDITOR) 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapEditorScript : MonoBehaviour
{
    public GameObject[] m_paintType;
    
    public GameObject tileObj;
    
    public uint mapLength;
    public uint mapWidth;
    
    public GameObject world;
    
    public bool enablePaint = false;
    
    public void GenerateBasMap()
    {
        world = new GameObject();
        
        world.name = "World GameObject";
        world.tag = "World";

        tileObj = m_paintType[1];


        for (int x = 0; x < mapLength; x++)
        {
            for (int z = 0; z < mapWidth; z++)
            {
                GameObject mapTile = Instantiate(tileObj, new Vector3(x * 3, 0, z * 3), Quaternion.identity);
                
                mapTile.transform.SetParent(world.transform);
            }
        }
    }

    public void TogglePaint()
    {
        if (enablePaint == false)
        {
            enablePaint = true;
        }
        else
        {
            enablePaint = false;
        }
    }
    
    public void SetPaintTypeEmptyTile()
    {
        tileObj = m_paintType[0];
    }

    public void SetPaintTypeFloorTile()
    {
        tileObj = m_paintType[1];
    }

    public void SetPaintTypeWALLTile()
    {
        tileObj = m_paintType[2];
    }

    public void SetPaintTypeWALLFONTTile()
    {
        tileObj = m_paintType[3];
    }

    public void SetPaintTypeDoorTile()
    {
        tileObj = m_paintType[4];
    }
    
    public void Paint()
    {
        if (Event.current.type == EventType.MouseDown)
        {
            RaycastHit hit;
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (Event.current.button == 0)
                {
                    GameObject go = Instantiate(tileObj, hit.collider.transform.position, Quaternion.identity);
                    go.transform.SetParent(world.transform);

                    Transform topTransform = hit.transform.parent != null ? hit.transform.parent : hit.transform;

                    foreach (Transform tran in topTransform)
                    {
                        DestroyImmediate(hit.transform.gameObject);
                    }
                    DestroyImmediate(topTransform.gameObject);
                }
                else
                {
                    GameObject go = Instantiate(tileObj, new Vector3(hit.point.x + ((hit.point.x % 3) > 1.5f ?  3- hit.point.x % 3 : 0 - hit.point.x % 3), 
                                                                    hit.point.y  + ( 0 - (hit.point.y % 3)), 
                                                                    hit.point.z + ((hit.point.z % 3) > 1.5f ? 3 - hit.point.z % 3 : 0 - hit.point.z % 3)),
                                                                    Quaternion.identity);
                    go.transform.SetParent(world.transform);
                }

                Debug.Log("X point: " + hit.point.x + " : " +  ((hit.point.x % 3) > 1.5f ? 3 - hit.point.x % 3 : 0 - hit.point.x % 3));
                Debug.Log("Z point: " + hit.point.z + " : " + ((hit.point.z % 3) > 1.5f ? 3 - hit.point.z % 3 : 0 - hit.point.z % 3));
            }
        }
    }
}
#endif