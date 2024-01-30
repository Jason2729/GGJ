using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCardGenerator : MonoBehaviour
{
    [SerializeField] private List<CardDataScriptableObject> CardData;
    [SerializeField] private Transform CustomCardHolder;
    [SerializeField] private GameObject customCardPrefab;

    [SerializeField] private int maxCards = 5;
    [SerializeField] private bool isClearingCards = false;

    [SerializeField] private int balloonsNeededForReroll = 3;

    public void GenerateCards(int cardsToGenerate)
    {
        int startingCards = CustomCardHolder.childCount;
        for (int i = 0; i < cardsToGenerate - startingCards; i++)
        {
            GenerateRandomCardByCardData();
        }
    }

    private GameObject GenerateRandomCardByCardData()
    {
        int id = Random.Range(0, CardData.Count);
        GameObject customCard = Instantiate(customCardPrefab, CustomCardHolder);
        customCard.GetComponent<CustomCard>().LinkCardData(CardData[id]);
        return customCard;
    }

    public void ClearCards(int cardsToClear)
    {
        for(int i = cardsToClear - 1; i >= 0; i--)
        {
            Destroy(CustomCardHolder.GetChild(i).gameObject);
        }
        isClearingCards = false;
    }

    public void ClearAllCards()
    {
        for (int i = CustomCardHolder.transform.childCount-1; i >= 0; i--)
        {
            Destroy(CustomCardHolder.GetChild(i).gameObject);
        }
        isClearingCards = false;
    }

    public void RerollCards(int cardsToReroll)
    {
        if(UIManager.Instance.GetBalloons() >= balloonsNeededForReroll)
        {
            UIManager.Instance.UseBalloons(balloonsNeededForReroll);
            StartCoroutine(Reroll(cardsToReroll));
        }
        else
        {

        }
    }

    private IEnumerator Reroll (int cardsToReroll)
    {
        isClearingCards = true;
        ClearCards(cardsToReroll);
        yield return new WaitUntil(() => !isClearingCards);
        GenerateCards(cardsToReroll);
    }

}
