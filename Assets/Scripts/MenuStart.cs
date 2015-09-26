using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MenuStart : MonoBehaviour {
    public Text name;
    public Toggle gender;

    public void LoadScene() {
        UserInfo.UserName = name.text;
        UserInfo.gender = gender.isOn;
        Application.LoadLevel("Level1");

    }
    public void Update() {

    }
    public void Exit() {
        Application.Quit();
    }
}
