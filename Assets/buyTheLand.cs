using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class buyTheLand : MonoBehaviour
{
 
    private IEnumerator CoinMaker;
      public int coinPrice;
      public GameObject land;
      [SerializeField] private TextMeshPro PriceTagTxt;
      [SerializeField] private GameObject wall;
    void Start()
    {
        CoinMaker = CountCoins(GameManager.GameManagerInstance.transform);
    }
    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerPrefs.GetInt("Coin") > 0)
            StartCoroutine(CoinMaker);
        else
            StopCoroutine(CoinMaker);
        
    }

    IEnumerator CountCoins(Transform player)
    {
        for (int counter = 0; counter < GameManager.GameManagerInstance.CoinLst.Count; counter++)
        {
            var newCoin = GameManager.GameManagerInstance.CoinLst[counter];

            if (coinPrice > 0 && PlayerPrefs.GetInt("Coin") > 0)
            {
                newCoin.transform.position = player.position;
                newCoin.SetActive(true);
                newCoin.transform.DOJump(transform.position, 3f, 1, 0.3f).SetEase(Ease.OutSine);
                SellTheLand();
            }
            else
            {
                counter = 0;
            }
            
            yield return new WaitForSecondsRealtime(0.1f);
        } 
    }

    private void SellTheLand()
    {
        PlayerPrefs.SetInt("Coin",PlayerPrefs.GetInt("Coin") - 1);

        coinPrice--;

        if (coinPrice == 0)
        {
            wall.SetActive(false);
            land.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
