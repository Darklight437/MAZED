#if (UNITY_EDITOR) 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapEditorScript))]
public class MapEditor : Editor
{

    /*
    * OnInspectorGUI
    * virtual function override
    * 
    * this overrides the default inspector for or MapEditorScript allowing
    * for our own custome buttons to be displayed there
    * 
    * @returns nothing
    */
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MapEditorScript myScript = (MapEditorScript)target;

        //adds in a button to generate a default map
        //of normal tiles when clicked
        if (GUILayout.Button("Generate base Map"))
        {
            myScript.GenerateBasMap();
        }
        
        //this button calls a function in MapEditorScript to toggle on and off paint mode
        if (GUILayout.Button("Toggle paint mode"))
        {
            myScript.TogglePaint();
        }
        
        //these buttons will only display if paint mode is on
        if (myScript.enablePaint)
        {
            
            if (GUILayout.Button("Slect Empty"))
            {
                myScript.SetPaintTypeEmptyTile();
            }
            
            if (GUILayout.Button("Slect Floor"))
            {
                myScript.SetPaintTypeFloorTile();
            }
            
            if (GUILayout.Button("Slect Wall"))
            {
                myScript.SetPaintTypeWALLTile();
            }
            
            if (GUILayout.Button("Slect Door"))
            {
                myScript.SetPaintTypeDoorTile();
            }

            if (GUILayout.Button("Slect Wall Font"))
            {
                myScript.SetPaintTypeWALLFONTTile();
            }
        }
    }

    /*
    * OnSceneGUI
    * 
    * this function is where we override unitys mouse controls 
    * to implament our own ones when in paint mode
    * 
    * @returns nothing
    */
    void OnSceneGUI()
    {
        MapEditorScript myScript = (MapEditorScript)target;

        //gets the event stuff needed to override the mouse
        Event e = Event.current;
        int controlID = GUIUtility.GetControlID(FocusType.Passive);

        //only overrides when enable paint is true
        if (myScript.enablePaint)
        {
            //switch statements for the override
            switch (e.GetTypeForControl(controlID))
            {
                //when we click down the paint
                case EventType.MouseDown:
                    GUIUtility.hotControl = controlID;
                    myScript.Paint();
                    e.Use();
                    break;

                case EventType.MouseUp:
                    GUIUtility.hotControl = 0;
                    e.Use();
                    break;

                //when we drag we also paint
                case EventType.MouseDrag:
                    GUIUtility.hotControl = controlID;
                    myScript.Paint();
                    e.Use();
                    break;

                case EventType.KeyDown:
                    break;
            }
        }
    }
}
#endif