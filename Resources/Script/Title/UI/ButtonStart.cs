using UnityEngine;
using UnityEngine.SceneManagement;
using UnityExtension;
using System.Collections;

public class ButtonStart : MonoBehaviourExtension
{
    public void OnClick()
    {
        SceneManager.LoadScene("puzzle");
    }
}
