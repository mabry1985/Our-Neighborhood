using UnityEngine;
using UnityEditor;


public class ClothesCreator : EditorWindow
{
    static ClothesType selectedClothesType;
    //Creates a new menu (Examples) with a menu item (Create Prefab)
    [MenuItem("ClothesCreator/Create")]
    static void CreatePrefab()
    {
        //Keep track of the currently selected GameObject(s)
        GameObject[] objectArray = Selection.gameObjects;

        //Loop through every GameObject in the array above
        foreach (GameObject gameObject in objectArray)
        {
            //Set the path as within the Assets folder, and name it as the GameObject's name with the .prefab format
            string localPath = "Assets/LOWPOLY CHARACTERS/IAdvancedCustomizer/Clothes/" + gameObject.name + ".prefab";

            //Check if the Prefab and/or name already exists at the path
            if (AssetDatabase.LoadAssetAtPath(localPath, typeof(GameObject)))
            {
                //Create dialog to ask if User is sure they want to overwrite existing Prefab
                if (EditorUtility.DisplayDialog("Are you sure?",
                    "The Prefab already exists. Do you want to overwrite it?",
                    "Yes",
                    "No"))
                //If the user presses the yes button, create the Prefab
                {
                    CreateNew(gameObject, localPath);
                }
            }
            //If the name doesn't exist, create the new Prefab
            else
            {
                Debug.Log(gameObject.name + " is not a Prefab, will convert");
                CreateNew(gameObject, localPath);
            }
        }
    }
    // Disable the menu item if no selection is in place
    [MenuItem("ClothesCreator/Create", true)]
    static bool ValidateCreatePrefab()
    {
        return Selection.activeGameObject != null;
    }
    static GameObject ConvertToClothe(GameObject obj)
    {
        //add required components
        obj.AddComponent<MeshFilter>();
        obj.AddComponent<MeshRenderer>();
        obj.AddComponent<Clothes>();
        //Get type 
        obj.GetComponent<Clothes>().type = selectedClothesType;
        //get mesh and materials from SkinnedMeshRenderer
        obj.GetComponent<MeshFilter>().sharedMesh = obj.GetComponent<SkinnedMeshRenderer>().sharedMesh;
        obj.GetComponent<MeshRenderer>().sharedMaterials = obj.GetComponent<SkinnedMeshRenderer>().sharedMaterials;
        //destroy SkinnedMeshRenderer
        DestroyImmediate(obj.GetComponent<SkinnedMeshRenderer>());
        //return converted obj
        return obj;
    }
    static void CreateNew(GameObject obj, string localPath)
    {
        //Create a new Prefab at the path given
        PrefabUtility.SaveAsPrefabAssetAndConnect(ConvertToClothe(obj), localPath, InteractionMode.AutomatedAction);
    }

    [MenuItem("ClothesCreator/Creator")]
    static void Init()
    {
        UnityEditor.EditorWindow window = GetWindow(typeof(ClothesCreator));
        window.Show();
    }
    void OnGUI()
    {
        selectedClothesType = (ClothesType)EditorGUILayout.EnumPopup("type to create:", selectedClothesType);

        if (GUILayout.Button("Create"))
        {
            CreatePrefab();
        }
    }

}













