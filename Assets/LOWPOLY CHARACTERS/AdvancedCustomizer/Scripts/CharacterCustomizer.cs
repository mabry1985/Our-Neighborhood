using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomizer : MonoBehaviour
{
    [Header("Character gender")]
    public CharacterGender gender;
    [Header("this will automatically be set, just put the names of the children of this object iqual to the name of the type of clothing")]
    public GameObject[] parts;

    [Header("Add New Clothes",order = 1)]
    public GameObject ClothesToADD;


    public void AddClothes(GameObject newClothes)
    {
        //validate gender
        if (!ValidateGender(newClothes.GetComponent<Clothes>().characterGender))
        {
            Debug.LogError("this clothes no is compatible with character gender");
            return;
        }


        parts[(int)newClothes.GetComponent<Clothes>().type].GetComponent<SkinPart>().SetClothes(newClothes);

           }

    public void RemoveClothes(int slotID)
    {
        parts[slotID].GetComponent<SkinPart>().ResetClothes();
    }
    public bool ValidateGender(CharacterGender _gender)
    {
        if (_gender == gender)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}

