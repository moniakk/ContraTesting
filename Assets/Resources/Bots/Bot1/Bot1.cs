using UnityEngine;
using System.Collections;
namespace Bot
{
    public class Bot1 : Bot
    {
        public int MaxAmmo = 2;
        public float ReloadTime = 2f;
        public float bulletForce = 40f;
        public float bulletTime = 0.5f;
        public GameObject bullet;
        Transform FirePoint;
        int currentAmmo = 0;
        float TimeLastShoot;
        float ReloadResidueTime;
        Transform hand;
        void Start()
        {

            hand = (Transform)transform.FindChild("hand");

            currentAmmo = MaxAmmo;
            FirePoint = (Transform)transform.FindChild("hand").FindChild("FirePoint");
            if (FirePoint == null)
            {
                Debug.LogError("WTF? No Fire point");
            }
        }



        GameObject TargetObject;
        void Update()
        {


            //transform.Translate(Vector3.left * 0.01f);
            if (IsStarted)
            {
                var dir = TargetObject.transform.position - transform.position;
                if (dir.x < 0)
                {
                    transform.rotation = new Quaternion(0, 180, 0, 0);
                }
                else
                {
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                }
                if (dir.y > 0)
                {
                    // hand.rotation = new Quaternion(0, 0, hand.rotation.z + 0.01f, 1);
                }
                else
                {
                    //  hand.rotation = new Quaternion(0, 0, hand.rotation.z - 0.01f, 1);
                }



                if ((Time.time - TimeLastShoot) > bulletTime && currentAmmo > 0)
                {
                    TimeLastShoot = Time.time;
                    var newBullet = (GameObject)Instantiate(bullet, FirePoint.position, FirePoint.rotation);
                    Destroy(newBullet, 2);
                    currentAmmo -= 1;
                }

                if (currentAmmo == 0)
                {
                    ReloadResidueTime += Time.deltaTime;
                    if (ReloadResidueTime >= ReloadTime)
                    {
                        currentAmmo = MaxAmmo;
                        ReloadResidueTime = 0;
                    }
                }
            }
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "bullet")
            {
                Destroy(gameObject);
            }

        }
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Player")
            {
                IsStarted = true;
                TargetObject = col.gameObject;
            }

        }
    }
}