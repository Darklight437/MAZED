#if (UNITY_EDITOR) 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapEditorScript : MonoBehaviour
{
    public GameObject[] m_paintSandStone;
    public GameObject[] m_paintSand;

    public int tileObj;

    public bool paintSandStone = true;
    
    public uint mapLength;
    public uint mapWidth;
    
    public GameObject world;
    
    public bool enablePaint = false;
    
    public void GenerateBasMap()
    {
        world = new GameObject();
        
        world.name = "World GameObject";
        world.tag = "World";

        tileObj = 1;


        for (int x = 0; x < mapLength; x++)
        {
            for (int z = 0; z < mapWidth; z++)
            {
                GameObject mapTile = Instantiate(paintSandStone == true ? m_paintSandStone[tileObj] : m_paintSand[tileObj], new Vector3(x * 3, 0, z * 3), Quaternion.identity);
                
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
        tileObj = 0;
    }

    public void SetPaintTypeFloorTile()
    {
        tileObj = 1;
    }

    public void SetPaintTypeWALLTile()
    {
        tileObj = 2;
    }

    public void SetPaintTypeWALLFONTTile()
    {
        tileObj = 3;
    }

    public void SetPaintTypeDoorTile()
    {
        tileObj = 4;
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
                    GameObject go = Instantiate(paintSandStone == true ? m_paintSandStone[tileObj] : m_paintSand[tileObj], hit.collider.transform.position, Quaternion.identity);
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
                    GameObject go = Instantiate(paintSandStone == true ? m_paintSandStone[tileObj] : m_paintSand[tileObj], 
                                                                    new Vector3(hit.point.x + ((hit.point.x % 3) > 1.5f ?  3- hit.point.x % 3 : 0 - hit.point.x % 3), 
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