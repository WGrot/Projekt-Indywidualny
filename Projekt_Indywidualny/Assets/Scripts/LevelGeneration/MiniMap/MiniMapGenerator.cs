using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MiniMapGenerator : MonoBehaviour
{
    Texture2D miniMapSprite;
    Texture2D undiscoveredTexture;
    int size =256;
    public Texture2D MiniMapSprite { get => miniMapSprite; set => miniMapSprite = value; }

    public Texture2D UndiscoveredTexture { get => undiscoveredTexture; set => undiscoveredTexture = value; }

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
        size = LevelGrid.Instance.GetSize();
        miniMapSprite = new Texture2D(size,size);
        undiscoveredTexture = new Texture2D(size,size, TextureFormat.ARGB32, false);

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

        undiscoveredTexture = Texture2D.blackTexture;
        Debug.Log("Generated Minimap");
    }

    public void UpdateUndiscoveredTexture(int playerPosX, int playerPosZ, int radius)
    {
        int radiusSquared = radius * radius;

        // Ustalenie zakresu iteracji dla ograniczenia do prostok¹ta wokó³ okrêgu
        int rowStart = Math.Max(0, playerPosX - radius);
        int rowEnd = Math.Min(size - 1, playerPosX + radius);
        int colStart = Math.Max(0, playerPosZ - radius);
        int colEnd = Math.Min(size - 1, playerPosZ + radius);
        Debug.Log(rowStart + " " + rowEnd + " " + colStart + " " + colEnd);
        for (int row = rowStart; row <= rowEnd; row++)
        {
            for (int col = colStart; col <= colEnd; col++)
            {
                // Sprawdzamy, czy punkt (row, col) le¿y wewn¹trz okrêgu o œrodku (x, y)
                int dx = row - playerPosX;
                int dy = col - playerPosZ;

                if (dx * dx + dy * dy <= radiusSquared)
                {
                    // Wywo³ujemy podan¹ funkcjê dla tego pola
                    DiscoverPixel(row, col);
                }
            }
        }

        undiscoveredTexture.filterMode = FilterMode.Point;
        undiscoveredTexture.Apply();
    }



    public void DiscoverPixel(int x, int y)
    {
        Color transparentColor = new Color(0, 0, 0, 0);
        undiscoveredTexture.SetPixel(x, y, transparentColor);
    }

}
