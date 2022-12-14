using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GameManagerInstance;
    private Vector3 direction;
    private Vector2 lastMousePosition;
    private Camera cam;
    
    [SerializeField] private float playerSpeed;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private ParticleSystem dust;
    [SerializeField] private Transform[] Scores;
    private int index;
    public Transform CoinNextPos;
    public TextMeshProUGUI CoinCounter_txt;
    
    //*******************
    [SerializeField] private GameObject Coin_3d;
    public List<GameObject> CoinLst = new List<GameObject>();
    
    
    void Awake()
    {
        cam = Camera.main;
        playerAnimator = GetComponent<Animator>();

        GameManagerInstance = this;

        dust = transform.GetChild(3).gameObject.GetComponent<ParticleSystem>();

        for (int i = 0; i < 20; i++)
        {
            GameObject NewCoin = Instantiate(Coin_3d, Vector3.zero, quaternion.identity);
            CoinLst.Add(NewCoin);
        }

        CoinCounter_txt.text = PlayerPrefs.GetInt("Coin").ToString();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAnimator.SetBool("run",true);
            dust.Play();
        }

        if(Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up,transform.position);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if(plane.Raycast(ray, out var distance))
                direction = ray.GetPoint(distance);
         
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(direction.x, 0f, direction.z), playerSpeed * Time.deltaTime);

            var offset = direction -  transform.position;
            
            if (offset.magnitude > 1f)
                transform.LookAt(direction);
 
        }
        else if (Input.GetMouseButtonUp(0))
        {
            playerAnimator.SetBool("run",false);
            dust.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("coin"))
        {
            Scores[index].gameObject.SetActive(true);
            Scores[index].position = other.transform.position;

            if (index < 4)
                index++;
            else
                index = 0;
            
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.GetComponent<Collider>().isTrigger = true;
            other.GetComponent<scoreManager>().ScoreMode = true;

            if (!other.GetComponent<RectTransform>())
            {
                other.gameObject.AddComponent<RectTransform>();
                other.GetComponent<RectTransform>().anchorMin = Vector2.one; // new vector2(1f,1f)
                other.GetComponent<RectTransform>().anchorMax = Vector2.one; // new vector2(1f,1f)
            }

            other.transform.parent = CoinNextPos.parent;

           PlayerPrefs.SetInt("Coin",PlayerPrefs.GetInt("Coin") + 1);

           CoinCounter_txt.text = PlayerPrefs.GetInt("Coin").ToString();
        }
    }
}
