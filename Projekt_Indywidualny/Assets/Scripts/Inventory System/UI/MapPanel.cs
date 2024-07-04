using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPanel : MonoBehaviour
{
    [SerializeField] private RawImage mapImage;
    [SerializeField] private Texture2D emptyMapTexture;

    private void OnEnable()
    {   if(MiniMapGenerator.Instance != null)
        {
            mapImage.texture = MiniMapGenerator.Instance.MiniMapSprite;
        }
        else
        {
            mapImage.texture = emptyMapTexture;
        }
    }
}
