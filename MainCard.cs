using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard : MonoBehaviour
{
    [SerializeField] private SceneController controller;
    [SerializeField] private GameObject Card_Back;
    [SerializeField] private TimerScript timer;

    public void OnMouseDown()
    {
        if (Card_Back.activeSelf && controller.canReveal && timer.stopGame)
        {
            Card_Back.SetActive(false);
            controller.CardRevealed(this);
        }
    }

    private int _id;
    public int id
    {
        get { return _id; }
    }

    public void ChangeSprite(int id, Sprite Image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = Image; //this gets the sprite renderer component and changes the property of its sprite

    }

    public void Unreveal()
    {
        Card_Back.SetActive(true);
    }
}
