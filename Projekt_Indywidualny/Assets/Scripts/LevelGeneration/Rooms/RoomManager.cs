using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> doors;
    public UnityEvent OnRoomActivated;

    public void ActivateRoom()
    {
        CloseAllDoors();
        OnRoomActivated.Invoke();
    }

    public void CloseAllDoors()
    {
        foreach(GameObject door in doors)
        {
            Doors doorScript = door.GetComponent<Doors>();
            doorScript.CloseDoor();
        }
    }

    public void OpenAllDoors()
    {
        foreach (GameObject door in doors)
        {
            Doors doorScript = door.GetComponent<Doors>();
            doorScript.OpenDoor();
        }
    }
}
