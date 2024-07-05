using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MapPanel : MonoBehaviour
{
    [SerializeField] private RawImage mapImage;
    [SerializeField] private Texture2D emptyMapTexture;
    [SerializeField] private GameObject playerMarker;
    private GameObject player;

    [SerializeField] private float scaleIncrease;

    private InputActions inputActions;
    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
    }

    private void OnEnable()
    {
        inputActions.UI.Scroll.performed += ChangeScale;
        if(MiniMapGenerator.Instance != null)
        {
            mapImage.texture = MiniMapGenerator.Instance.MiniMapSprite;
        }
        else
        {
            mapImage.texture = emptyMapTexture;
        }
    }

    private void OnDisable()
    {
        inputActions.UI.Scroll.performed -= ChangeScale;
    }

    public void ChangeScale(InputAction.CallbackContext context)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (context.ReadValueAsButton())
        {
            mapImage.gameObject.transform.localScale = mapImage.gameObject.transform.localScale += new Vector3(scaleIncrease, scaleIncrease, 0);
        }
        else
        {
            mapImage.gameObject.transform.localScale = mapImage.gameObject.transform.localScale -= new Vector3(scaleIncrease, scaleIncrease, 0);
        }
    }
}
