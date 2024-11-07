using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapController : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private RawImage mapImage;
    [SerializeField] private GameObject playerMarker;
    [SerializeField] private RectTransform mapPivot;
    [SerializeField] private Texture2D emptyMapTexture;

    [Header("Parameters")]
    [SerializeField] private float minimapScale;
    
    private GameObject player;
    Vector3 playerStartPos = new Vector3(0,0,0);

    private float gridsize = 80;
    private float gridScale = 4;

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
            playerMarker.SetActive(true);
            gridsize = LevelGrid.Instance.GetSize();
            gridScale = LevelGrid.Instance.GetScale();
        }
        else
        {
            mapImage.texture = emptyMapTexture;
            playerMarker.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            mapPivot.gameObject.transform.rotation =  new Quaternion(0, 0, player.transform.rotation.y, player.transform.rotation.w);
            mapImage.gameObject.transform.localPosition = 135 * minimapScale * -new Vector3(player.transform.position.x - playerStartPos.x + 8, player.transform.position.z - playerStartPos.z + 8, 0) / (gridsize * gridScale);

        }

    }
}
