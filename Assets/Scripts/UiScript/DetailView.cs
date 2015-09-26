using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DetailView : MonoBehaviour {
    public Text text;
    public GameObject[] Heart;
    public GameObject Player;
    public Text Score;
    private CharacterControlShoot script = null;
    private int currentHeart;
    private int scoretmp;

    public void HeartEdit(int heart) {

        if (heart > 0) {
            Heart[currentHeart].GetComponent<Image>().enabled = heart > 0;
            currentHeart += 1;

        } else {
            currentHeart -= 1;
            Heart[currentHeart].GetComponent<Image>().enabled = heart > 0;
        }

    }

    void Start() {
        text.text = UserInfo.UserName;
        script = Player.GetComponent<CharacterControlShoot>();
    }

    void Update() {
        scoretmp += 1;
        Score.text ="Score: "+ scoretmp;
    }
}
