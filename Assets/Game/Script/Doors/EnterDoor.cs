using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{
    private bool enterAllowed;
    private bool isDoorEntering;
    private string sceneToLoad;

    private Dictionary<System.Type, string> doorToSceneMap = new Dictionary<System.Type, string>
    {
        { typeof(MotelRoomDoor), "TheMotel" },
        { typeof(MotelDoor), "MotelRoom" },
        { typeof(MotelToVillage), "Village" },
        { typeof(VillageToMotel), "TheMotel" },
        { typeof(VillageToCastleEntrance), "CastleEntrance" },
        { typeof(CastleEntranceToVillage), "Village" },
        { typeof(CastleEnTranceToCastle), "Castle" },
        { typeof(CastleToCastleEntrance), "CastleEntrance" },
        { typeof(CastleToBoss), "BossRoom" },
        { typeof(BossToCastle), "Castle" }
    };

    private HashSet<System.Type> exitCheckDoors = new HashSet<System.Type>
    {
        typeof(MotelRoomDoor),
        typeof(MotelDoor),
        typeof(MotelToVillage),
        typeof(VillageToMotel),
        typeof(VillageToCastleEntrance),
        typeof(CastleEntranceToVillage),
        typeof(CastleEnTranceToCastle),
        typeof(CastleToCastleEntrance),
        typeof(CastleToBoss),
        typeof(BossToCastle)
    };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        System.Type colliderType = collision.GetType();
        if (doorToSceneMap.ContainsKey(colliderType))
        {
            sceneToLoad = doorToSceneMap[colliderType];
            enterAllowed = true;
        }
        // if (collision.GetComponent<MotelRoomDoor>())
        // {
        //     sceneToLoad = "TheMotel";
        // }
        // else if (collision.GetComponent<MotelDoor>())
        // {
        //     sceneToLoad = "MotelRoom";
        // }
        // else if (collision.GetComponent<MotelToVillage>())
        // {
        //     sceneToLoad = "Village";
        // }
        // else if (collision.GetComponent<VillageToMotel>())
        // {
        //     sceneToLoad = "TheMotel";
        // }
        // else if (collision.GetComponent<VillageToCastleEntrance>())
        // {
        //     sceneToLoad = "CastleEntrance";
        // }
        // else if (collision.GetComponent<CastleEntranceToVillage>())
        // {
        //     sceneToLoad = "Village";
        // }
        // else if (collision.GetComponent<CastleEnTranceToCastle>())
        // {
        //     sceneToLoad = "Castle";
        // }
        // else if (collision.GetComponent<CastleToCastleEntrance>())
        // {
        //     sceneToLoad = "CastleEntrance";
        // }
        // else if (collision.GetComponent<CastleToBoss>())
        // {
        //     sceneToLoad = "BossRoom";
        // }
        // else if (collision.GetComponent<BossToCastle>())
        // {
        //     sceneToLoad = "Castle";
        // }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (exitCheckDoors.Contains(collision.GetType()))
        {
            enterAllowed = false;
        }
    }

    private void Update()
    {
        if (enterAllowed && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
