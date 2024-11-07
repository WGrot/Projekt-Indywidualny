using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapGenerator : MonoBehaviour
{
    Texture2D miniMapSprite;
    public Texture2D MiniMapSprite { get => miniMapSprite; set => miniMapSprite = value; }

    #region Singleton
    public static MiniMapGenerator Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("More than one MiniMapGenerator instance found!");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public void GenerateMiniMap()
    {
        int size = LevelGrid.Instance.GetSize();
        miniMapSprite = new Texture2D(size,size);

        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j< size; j++)
            {
                miniMapSprite.SetPixel(i, j, Color.red);
                int fieldValue = LevelGrid.Instance.GetFieldValue(i, j);
                if (fieldValue == ConstantValues.EMPTY_FIELD_VALUE)
                {
                    miniMapSprite.SetPixel(i, j, Color.black);
                }
                if (fieldValue == ConstantValues.ROOM_FIELD_VALUE)
                {
                    miniMapSprite.SetPixel(i, j, Color.red);
                }
                if (fieldValue == ConstantValues.CORRIDOR_FIELD_VALUE || fieldValue == ConstantValues.ENTRANCE_FIELD_VALUE)
                {
                    miniMapSprite.SetPixel(i, j, Color.white);
                }
            }


        }
        MiniMapSprite.filterMode= FilterMode.Point;
        MiniMapSprite.Apply();
        Debug.Log("Generated Minimap");
    }

}
