using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class FallingCircles : MonoBehaviour {

    private float rand;
    public static int score;
    public static int coinCounter;
    public AudioClip s1;
    public AudioClip s2;
    public AudioClip s3;
    public GameObject pinkBlue;
    public GameObject bluePink;
    public GameObject pinkThird;
    public GameObject thirdPink;
    public GameObject blueThird;
    public GameObject thirdBlue;
    public GameObject coin;
    public GameObject left;
    public GameObject right;
    public GameObject parent;
    private GameObject fallingCircles;
    private bool needToZoom;
    private Vector3 startVector = new Vector3(1,0);
    public Text textScore;
    public Text textCoins;

    void Start () {
        parent.GetComponent<AudioSource>().volume = 0;
        score = 0;
        coinCounter = 0;
        textScore.text = score + "";
        textCoins.text = "Coins: " + coinCounter;
        needToZoom = false;
        for (int i = 0; i < 4; i++)
        {
            RandomCircles();
            Instantiate(fallingCircles, new Vector3(fallingCircles.transform.position.x, 6 + 4 * i), Quaternion.identity);
        }
	}
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.otherCollider.tag == "Swapping" && collision.gameObject.tag == "Coin")
        {
            coinCounter++;
            textCoins.text = "Coins: " + coinCounter;
            Destroy(collision.gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider.tag == "Swapping" && collision.gameObject.tag == "Coin")
        {
            coinCounter++;
            textCoins.text = "Coins: " + coinCounter;
            RandomCircles();
            Destroy(collision.gameObject);
            Instantiate(fallingCircles, new Vector3(fallingCircles.transform.position.x, collision.gameObject.transform.position.y + 16.0f), Quaternion.identity);
        }
        else if (collision.otherCollider.tag != "Swapping" && collision.gameObject.tag == "Coin")
        {
            RandomCircles();
            Instantiate(fallingCircles, new Vector3(fallingCircles.transform.position.x, collision.gameObject.transform.position.y + 16.0f), Quaternion.identity);
        }
        else if (collision.gameObject.tag == collision.otherCollider.tag)
        {
            RandomCircles();
            Destroy(collision.gameObject);
            Instantiate(fallingCircles, new Vector3(fallingCircles.transform.position.x, collision.gameObject.transform.position.y + 16.0f), Quaternion.identity);
            needToZoom = true;
            score += 1;
            textScore.text = score + "";
        } else
        {
            collision.otherCollider.GetComponent<Rigidbody2D>().useFullKinematicContacts = false;
            startVector = collision.otherCollider.transform.position;
            CirclesMovement.speed = -15;
        }
        left.GetComponent<AudioSource>().Play();
    }

    private void RandomCircles()
    {
        rand = Random.Range(0.0f, 6.4f);
        if (rand >= 0.0f && rand < 1.0f)
        {
            fallingCircles = pinkBlue;
            left.GetComponent<AudioSource>().clip = s1;
        }
        if (rand >= 1.0f && rand < 2.0f)
        {
            fallingCircles = bluePink;
            left.GetComponent<AudioSource>().clip = s1;
        }
        if (rand >= 2.0f && rand < 3.0f)
        {
            fallingCircles = pinkThird;
            left.GetComponent<AudioSource>().clip = s2;
        }
        if (rand >= 3.0f && rand < 4.0f)
        {
            fallingCircles = thirdPink;
            left.GetComponent<AudioSource>().clip = s2;
        }
        if (rand >= 4.0f && rand < 5.0f)
        {
            fallingCircles = blueThird;
            left.GetComponent<AudioSource>().clip = s3;
        }
        if (rand >= 5.0f && rand < 6.0f)
        {
            fallingCircles = thirdBlue;
            left.GetComponent<AudioSource>().clip = s3;
        }
        if (rand >= 6.0f && rand <= 6.4f)
        {
            fallingCircles = coin;
            left.GetComponent<AudioSource>().clip = s3;
        }
    }

    private void Update()
    {
        if (Mathf.Abs(parent.transform.position.y - startVector.y) >= 2 && startVector.x != 1)
        {
            CirclesMovement.speed = 25;
        }

        if (needToZoom)
        {
            left.transform.localScale = new Vector3 (left.transform.localScale.x + 0.01f, left.transform.localScale.y + 0.01f);
            right.transform.localScale = new Vector3(right.transform.localScale.x + 0.01f, right.transform.localScale.y + 0.01f);
            if (left.transform.localScale.x >= 0.45f && right.transform.localScale.y >= 0.45f)
            {
                needToZoom = false;
            }                            
        } else
        {  
            if (left.transform.localScale.x >= 0.4f && right.transform.localScale.y >= 0.4f)
            {
                left.transform.localScale = new Vector3(left.transform.localScale.x - 0.01f, left.transform.localScale.y - 0.01f);
                right.transform.localScale = new Vector3(right.transform.localScale.x - 0.01f, right.transform.localScale.y - 0.01f);
            }
        }

        parent.GetComponent<AudioSource>().volume += 0.0002f;
    }
}
