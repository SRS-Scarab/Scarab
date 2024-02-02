using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public float HP;
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

    public float getHP()
    {
        return HP;
    }
    public void damage(float damage)
    {
        // want to animate this
        HP -= damage;
        if (HP <= 0)
        {
            Application.Quit();
        }
    }
    public void heal(float heal)
    {
        HP += heal;
        if (HP > 100)
        {
            HP = 100;
        }
    }
}
