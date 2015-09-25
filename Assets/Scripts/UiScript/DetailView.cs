using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DetailView : MonoBehaviour {
    public Text text;
    public GameObject[] Heart;
    public GameObject Player;

    CharacterControlShoot script;
    private int currentHeart = -1;

    public void HeartEdit(int heart) {
        if(heart > 0) {
            currentHeart += 1;

            } else {
            currentHeart -= 1;
            }
        Heart[currentHeart].GetComponent<Renderer>().enabled = heart > 0;
        }

    void Start() {
        text.text = UserInfo.UserName;
        script = Player.GetComponent<CharacterControlShoot>();
        }

    void Update() {

        }
    }
