using System;
using UnityEngine;

public class scoreManager : MonoBehaviour
{

    [SerializeField] private float radius;
    public bool ScoreMode;
    void Start()
    {
        
    }

    void Update()
    {
        FindThePlayer();
    }

    private void FindThePlayer()
    {

        if (!ScoreMode)
        {
            Collider[] CoinColl = Physics.OverlapSphere(transform.position, radius);

            foreach (var c in CoinColl)
            {
                if (c.CompareTag("Player"))
                {
                    transform.position = Vector3.MoveTowards(transform.position, c.transform.position
                                                                                 + new Vector3(0f, 2f, 0f),
                        Time.deltaTime * 50f);
                }
            }
        }
        else
            ConvertToScore();
    }

    private void ConvertToScore()
    {
        transform.position = Vector3.MoveTowards(transform.position,GameManager.GameManagerInstance.CoinNextPos.position,
            Time.deltaTime * 75f);

        transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime * 300f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider>().enabled = false;
            
            Invoke("InActiveCoin",0.5f);

        }
    }

    void InActiveCoin()
    {
       gameObject.SetActive(false); 
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,radius);
    }
}
