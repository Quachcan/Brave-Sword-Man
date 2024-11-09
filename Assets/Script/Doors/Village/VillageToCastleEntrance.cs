using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VillageToCastleEntrance : MonoBehaviour
{
    [SerializeField]
    string sceneName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneController.Instance.LoadScene(sceneName);
        }
    }
}
