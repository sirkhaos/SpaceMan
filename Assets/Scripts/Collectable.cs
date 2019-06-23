using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    HealthPotion,
    ManaPotion,
    Money
}

public class Collectable : MonoBehaviour
{
    public CollectableType type = CollectableType.Money;
    public int value = 1;


    private SpriteRenderer sprite;
    private CircleCollider2D itemCollider;

    bool hasBeenCollected = false;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            Collect();
        }
    }

    private void Show()
    {
        sprite.enabled = true;
        itemCollider.enabled = true;
        hasBeenCollected = false;
    }

    private void Hide()
    {
        sprite.enabled = false;
        itemCollider.enabled = false;
    }

    private void Collect()
    {
        Hide();
        hasBeenCollected = true;

        switch (this.type)
        {
            case CollectableType.Money:
                //TODO: logica de la moneda 
                break;
            case CollectableType.HealthPotion:
                //TODO: logica de la posion de vida
                break;
            case CollectableType.ManaPotion:
                //TODO: logica de la posion de mana
                break;
        }
    }
}
