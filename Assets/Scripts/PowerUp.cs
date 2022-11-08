using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public float multiplicador = 2f;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Char1"))
        {
            PickUp1(collision);
        }
        else {
            PickUp2(collision);
        }
    }

    private void PickUp1(Collider2D player) {

        Char1 stats = player.GetComponent<Char1>();
        int dmg = stats.getAttack();
        stats.setAttack(dmg + 1);

        Destroy(gameObject);    
    }
    private void PickUp2(Collider2D player)
    {

        Char2 stats = player.GetComponent<Char2>();
        int dmg = stats.getAttack();
        stats.setAttack(dmg + 1);

        Destroy(gameObject);
    }
}
