using Runner.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] int price;
    [SerializeField] PowerUpTypeEnum powerUpType;

    bool available = false;

    public enum PowerUpTypeEnum { METEOR, WAVE, GLACIATION }

	public void OnPointerClick(PointerEventData eventData)
	{
        if (available)
		{
            switch (powerUpType)
            {
                case PowerUpTypeEnum.METEOR:
                    MeteorPower();
                    break;
                case PowerUpTypeEnum.WAVE:
                    WavePower();
                    break;
                case PowerUpTypeEnum.GLACIATION:
                    GlaciationPower();
                    break;

            }
        }
		
	}

	void Start()
    {
        Text txtPrice = gameObject.GetComponentInChildren<Text>();
        if (txtPrice != null)
		{
            txtPrice.text = "" + price;
		}
    }

    void Update()
    {
        if (price <= ScoreManager.instance.GetCoins())
		{
            gameObject.GetComponent<Image>().color = Color.white;
            available = true;
		}
		else
		{
            //le ponemos el sombreado o lo q sea
            gameObject.GetComponent<Image>().color = Color.black;
            available = false;
		}

    }

    void MeteorPower()
	{
        Debug.Log("Activando MeteorPower");
	}

    void WavePower()
	{
        Debug.Log("Activando WavePower");
    }

    void GlaciationPower()
	{
        Debug.Log("Activando GlacialPower");
    }
}
