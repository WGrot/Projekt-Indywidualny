using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapController : MonoBehaviour
{
    [SerializeField] private RawImage mapImage;
    [SerializeField] private RectTransform mapPivot;
    [SerializeField] private Texture2D emptyMapTexture;
    [SerializeField] private float minimapScale;
    private GameObject player;
    Vector3 playerStartPos = new Vector3(0,0,0);

    private void OnEnable()
    {
        LevelGenerationV2.OnLevelGenerated += LoadMinimapTexture;
    }

    private void OnDisable()
    {
        LevelGenerationV2.OnLevelGenerated -= LoadMinimapTexture;
    }

    public void LoadMinimapTexture()
    {
        if (MiniMapGenerator.Instance != null)
        {
            Texture2D newMinimap = MiniMapGenerator.Instance.MiniMapSprite;
            mapImage.texture = newMinimap;
            mapImage.gameObject.transform.localScale = new Vector3(minimapScale, minimapScale, minimapScale);
            player = GameObject.FindGameObjectWithTag("Player");
            playerStartPos = player.transform.position; 
        }
        else
        {
            Debug.Log("Map generator not found");
            mapImage.texture = emptyMapTexture;
        }
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            float gridsize = LevelGrid.Instance.GetSize();
            float gridScale = LevelGrid.Instance.GetScale();

            mapPivot.gameObject.transform.rotation =  new Quaternion(0, 0, player.transform.rotation.y, player.transform.rotation.w);
            mapImage.gameObject.transform.localPosition = - new Vector3(player.transform.position.x - playerStartPos.x + 8 , player.transform.position.z - playerStartPos.z + 8, 0) * minimapScale * 135/(gridsize * gridScale);

        }

    }
}
