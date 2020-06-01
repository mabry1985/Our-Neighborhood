using UnityEditor;
using System.Collections;
using UnityEngine;

[CustomEditor(typeof(CharacterCustomizer))]
[CanEditMultipleObjects]
public class CharacterCustomizerEditor : Editor
{
    void OnEnable()
    {
        var script = target as CharacterCustomizer;

        //open space 
        script.parts = new GameObject[(int)ClothesType.lenght];
        //getting names of types
        string[] nameOfTypes = System.Enum.GetNames(typeof(ClothesType));

        //get children list
        string[] childrenNames= new string[script.transform.childCount];
        for (int i = 0; i < script.transform.childCount; i++) {
            childrenNames[i] = script.transform.GetChild(i).name;
                }
    

        for (int i = 0; i < nameOfTypes.Length-1; i++)
        {
            for (int ii = 0; ii < childrenNames.Length; ii++)
            {

                if(nameOfTypes[i] == childrenNames[ii])
                {
                    if (ValidatePart(script.transform.GetChild(ii).gameObject))
                    {
                        script.parts[i] = script.transform.GetChild(ii).gameObject;
                    }
                }
            }

        }
    }
    bool ValidatePart(GameObject part)
    {
        if (!part.GetComponent<SkinnedMeshRenderer>())
        {
            Debug.LogError("the " + part.name + "part does not contain a skinnedMeshRenderer!");
            return false;
        }

        if (!part.GetComponent<SkinPart>())
        {
            part.AddComponent<SkinPart>();
            Debug.Log("new skinpart component as been add");
        }


        return true;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CharacterCustomizer script = (CharacterCustomizer)target;

        //BUTTON: add clothing
        if (GUILayout.Button("ADD"))
        {
           if( script.ClothesToADD != null){
                script.AddClothes(script.ClothesToADD);
            }
        }

        //BUTTONS: remove clothing
        for (int i = 0; i < script.parts.Length; i++)
        {
            if (script.parts[i] != null) 
            {
                if (script.parts[i].GetComponent<SkinPart>().currentClothes != null &&
                    script.parts[i].GetComponent<SkinPart>().currentClothes != script.parts[i].GetComponent<SkinPart>().defaultClothes)
                {
                    if (GUILayout.Button("X " + script.parts[i].name))
                    {
                        script.RemoveClothes(i);
                    }
                }
            }
        }
    }
}
