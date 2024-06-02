using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject playerObject;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerObject) {
            SceneManager.LoadScene(sceneName);
        }
    }
}
