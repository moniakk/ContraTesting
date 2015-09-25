using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MenuStart : MonoBehaviour {
    public Text name;
 
    public void LoadScene() {
        UserInfo.UserName = name.text;
        Application.LoadLevel("Level1");

        }
    public void Exit() {
        Application.Quit();
        }
    }
