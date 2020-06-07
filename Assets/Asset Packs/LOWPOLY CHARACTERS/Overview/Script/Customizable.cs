using UnityEngine;
using UnityEngine.UI;

public class Customizable : MonoBehaviour {

    public Button Next;
    public Button Previous;
    public Button Reset;
    public Text ClothesName;
    public GameObject[] Clothes;
    public GameObject DefaultClothes;
    
    public int currentItem;
	void Start () {
        Next.onClick.AddListener(delegate { NextItem(true); });
        Previous.onClick.AddListener(delegate { PreviousItem(true); });
        Reset.onClick.AddListener(delegate { InitializedItems(true); });

        AutoSetItems();
        InitializedItems(true);
    }
    void AutoSetItems() {
        Clothes = new GameObject[transform.childCount];

        for (int i = 0; i < Clothes.Length; i++)
        {
            Clothes[i] = transform.GetChild(i).gameObject;
        }
        }
    public void InitializedItems(bool clicke)
    {
        for (int i = 0; i < Clothes.Length; i++)
        {
            if (DefaultClothes != null)
            {
                if (Clothes[i].name == DefaultClothes.name)
                {
                    Clothes[i].SetActive(true);
                    ClothesName.text = Clothes[i].name.ToString();
                }
                else
                {
                    Clothes[i].SetActive(false);
                }
            }
            else
            {
                Clothes[i].SetActive(false);
            }
        }
    }

    public void NextItem(bool clicked)
    {
        currentItem++;
        if(currentItem == Clothes.Length)
        {
            currentItem = 0;
        }

        for(int i = 0; i < Clothes.Length; i++)
        {
            if(currentItem == i)
            {
                Clothes[i].SetActive(true);
                ClothesName.text = Clothes[i].name.ToString();
            }
            else
            {
                Clothes[i].SetActive(false);
            }
        }
    }

    public void PreviousItem(bool clicked)
    {
        if(currentItem > 0)
        {
            currentItem--;
        }
        else
        {
            currentItem = Clothes.Length - 1;
        }
        for (int i = 0; i < Clothes.Length; i++)
        {
            if (currentItem == i)
            {
                Clothes[i].SetActive(true);
                ClothesName.text = Clothes[i].name.ToString();
            }
            else
            {
                Clothes[i].SetActive(false);
            }
        }
    }
}
