using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public int HP;
    private Slider HPBar;
    // Start is called before the first frame update
    void Start()
    {
        HP = 100;
        HPBar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        HPBar.value = HP / 100f;
    }

    public int getHP()
    {
        return HP;
    }
    public void damage(int damage)
    {
        // want to animate this
        HP -= damage;
        if (HP <= 0)
        {
            // player death
        }
    }
    public void heal(int heal)
    {
        HP += heal;
        if (HP > 100)
        {
            HP = 100;
        }
    }
}
