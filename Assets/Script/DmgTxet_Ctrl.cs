using UnityEngine.UI;
using UnityEngine;

public class DmgTxet_Ctrl : MonoBehaviour
{
    
    public Text DamageText = null;



    void Start()
    {
        Destroy(gameObject, 1.4f);
    }

    public void InitDamage(float InDamage, EDamageType InEDamageType)
    {
        if (DamageText == null)
            DamageText = GetComponentInChildren<Text>();

        if (DamageText == null)
            return;

        DamageText.text = "" + (int)InDamage;
        

        Color textColor = new Color();
        switch(InEDamageType)
        {
            case EDamageType.Normal:
                textColor = Color.yellow;
                break;

            case EDamageType.Critical:
                textColor = Color.red;
                break;

            case EDamageType.Hill:
                textColor = Color.green;
                break;
        }
        
        DamageText.color = textColor;

    }
}
