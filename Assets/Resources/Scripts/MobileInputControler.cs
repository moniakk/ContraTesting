//using UnityEngine;
//using System.Collections;

//public class MobileInputControler : MonoBehaviour {
//    GameObject player;
//    UnityStandardAssets._2D.Platformer2DUserControl script;
//    UnityStandardAssets._2D.PlatformerCharacter2D m_Character;

//    public static float move;
//    void Start() {
//        player = GameObject.Find("CharacterRobotBoy");
//        if (player != null) {
//            script = player.GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>();
//            m_Character = script.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();
//        }

//    }


//    void Update() {


//        m_Character.Move(move, false, false);
//    }
//    public void up() {

//        m_Character.Move(0, false, true);
//    }
//    public void LeftDown() {
//        move = -0.1f;

//    }
//    public void LeftUp() {
//        move = 0;

//    }
//    public void RightDown() {
//        move = 0.1f;
//        Debug.Log("RightDown");

//    }
//    public void RightUp() {
//        move = 0f;

//    }
//    public void Down() {

//    }
//    public void Shoot() {
//        m_Character.Move(0, false, true);
//    }
//}
