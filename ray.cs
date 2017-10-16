using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ray : MonoBehaviour {

    // Use this for initialization
    public int gunDamage = 1;
    public float fireRate = 0.25f;
    public float weaponRange = 500f;
    public float hitForce = 100f;
    public Transform gunEnd;

    private GameObject fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private LineRenderer laserLine;
    private float nextFire;
    //private float timer = 0.0f;
    private float checktime;

    public GameObject ball;
    public bool time_flag;
    public float time;

    public int count;
    public Text countText;

    public Queue<GameObject> myQueue =  new Queue<GameObject>();
    public Queue<float> timeQueue = new Queue<float>();

    void Start () {
        laserLine = GetComponent<LineRenderer>();
        fpsCam = GameObject.Find("GunEnd");

        time_flag = false;
        time = 0.0f;

        count = 0;
        //countText.text = "Score:" + count.ToString();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Y_button"))
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = fpsCam.transform.position;
            RaycastHit hit;
            laserLine.SetPosition(0, gunEnd.position);
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit))
            {
                laserLine.SetPosition(1, hit.point);
                if (hit.collider.tag == "balls")
                {
                    ball = hit.collider.gameObject;
                    hit.collider.gameObject.SetActive(false);
                    time_flag = true;
                    myQueue.Enqueue(ball);
                    timeQueue.Enqueue(0.0f);


                    count = count + 1;
                    //countText.text = "Score:" + count.ToString();
                }
            }

            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
             
        }

        if(time_flag == true)
        {

            //time += Time.deltaTime;
            int itr = timeQueue.Count;
            while (itr > 0)
            {
                float time_accu = timeQueue.Dequeue();
                time_accu = time_accu + Time.deltaTime;
                timeQueue.Enqueue(time_accu);
                itr = itr - 1;

            }

            float time = timeQueue.Peek();

            if (time >= 5.0f)
            {
                ball = myQueue.Dequeue();
                timeQueue.Dequeue();
                ball.SetActive(true);
                //time_flag = false;
                //time = 0.0f;
                if(timeQueue.Count==0)
                {
                    time_flag = false;
                }
            }

        }

    }
    private IEnumerator ShotEffect()
    {
        //gunAudio.Play();
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }

}
