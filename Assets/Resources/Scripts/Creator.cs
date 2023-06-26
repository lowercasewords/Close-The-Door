using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Creator
{
    private static GameObject[] openableDoors;

    [RuntimeInitializeOnLoadMethod]
    private static void OnLoad()
    {
        //openableDoors = GameObject.FindGameObjectsWithTag("OpenableDoor");
        
        //foreach(GameObject door in openableDoors)
        //{
        //    //We don't check if component already exist
        //    //because we don't assign it in the first place
        //    door.AddComponent<DoorUse>();
             
        //}
    }


}
