using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    //Make the card selectable and usable without being public
    [SerializeField] private GameObject cardBack;

    private int _id;

    private SceneController controller;

    public void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<SceneController>();
    }

    public int id 
    {
        get { return _id; }
    }

    public void SetCard(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite =  image;
    }

    public void OnMouseDown()
    {
        //Debug.Log("click");
        if(cardBack.activeSelf && controller.canReveal){
            cardBack.SetActive(false);
            controller.CardRevealed(this);
        }
    }

    public void Unreveal()
    {
        cardBack.SetActive(true);
    }
}
