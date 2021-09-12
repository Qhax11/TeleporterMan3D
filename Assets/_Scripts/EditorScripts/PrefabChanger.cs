
#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class PrefabChanger : MonoBehaviour
{
    public enum MyEnum
    { 
        PickOne, 
        ChangeAllListWithSameObject,
        ChangeOneObject,
        ChangeListToList,
        InstantiateObject
    }
    [Space(10)][Title("Select Job Type")]public MyEnum whatWillIDo;
    
    
    [HideIfGroup("whatWillIDo",MyEnum.PickOne)]
    [Space(15)]
    [Title("To set parent")]
    [Tooltip("If you need Parent")] public Transform parent;
    
    
    [HideIfGroup("whatWillIDo",MyEnum.PickOne)]
    [Space(10)][Title("Object Name")]
    [DetailedInfoBox("Check bool if you want same name","Check bool if you want same name")]
    public bool whatIsObjectName;
    [HideIfGroup("whatWillIDo",MyEnum.PickOne)]public string objectName;

    [HideIfGroup("whatWillIDo",MyEnum.PickOne)]
    [Space(10)]
    [Title("Select component to add.")]
    [DetailedInfoBox("Check bool which component you want on your object.",
        "Check bool which component you want on your object.")]
    
    [HideIfGroup("whatWillIDo",MyEnum.PickOne)] [Space(7.5f)]public bool addRigidbody;
    [HideIfGroup("whatWillIDo",MyEnum.PickOne)][Space(7.5f)]public bool addBoxCollider;
    [HideIfGroup("whatWillIDo",MyEnum.PickOne)][Space(7.5f)]public bool addMeshCollider;
    [HideIfGroup("whatWillIDo",MyEnum.PickOne)][Space(7.5f)]public bool addCapsuleCollider;
    [FormerlySerializedAs("doTween")] [HideIfGroup("whatWillIDo", MyEnum.PickOne)] [Space(7.5f)] public bool addDoTween;
    
    [HideIfGroup("whatWillIDo",MyEnum.PickOne)]
    [Space(25f)][DetailedInfoBox("Add your tag first.","Add your tag first.")]public string objectTag;
    [HideIfGroup("whatWillIDo",MyEnum.PickOne)]
    [Space(7.5f)][DetailedInfoBox("Add your layer first.","Add your layer first.")]public string objectLayer;
    [HideIfGroup("whatWillIDo",MyEnum.PickOne)]
    [Space(25)][DetailedInfoBox("Write your script's name properly.","Write your script's name properly.")]public string myScript;
    
    
    [Space(25)][Title("ChangeAllListWithSameObject")]
    [ShowIf("whatWillIDo",MyEnum.ChangeAllListWithSameObject)]public List<GameObject> onScene = new List<GameObject>();
    [Space(10)][ShowIf("whatWillIDo",MyEnum.ChangeAllListWithSameObject)]public GameObject willSet;
    
    
    [Space(25)][Header("Instantiate List")]
    [ShowIf("whatWillIDo",MyEnum.ChangeListToList)]public List<GameObject> willDeleteList = new List<GameObject>();
    [Space(10)][ShowIf("whatWillIDo",MyEnum.ChangeListToList)]public List<GameObject> willSetList = new List<GameObject>();
    private int _listCount;
    
    [Space(25)][Header("Changing Objects")]
    [ShowIf("whatWillIDo",MyEnum.ChangeOneObject)]public GameObject willDelete;
    [Space(10)][ShowIf("whatWillIDo",MyEnum.ChangeOneObject)]public GameObject willSpawn;

    

    [Space(25)][Header("Instantiate Object")]
    [ShowIf("whatWillIDo", MyEnum.InstantiateObject)]public int objectCount;
    [ShowIf("whatWillIDo", MyEnum.InstantiateObject)]public bool myPosition;
    [ShowIf("whatWillIDo", MyEnum.InstantiateObject)]public Vector3 whereWillBe;
    [ShowIf("whatWillIDo", MyEnum.InstantiateObject)]public bool plusMyPosition;
    [ShowIf("whatWillIDo", MyEnum.InstantiateObject)]public float plusPosition;
    [ShowIf("whatWillIDo", MyEnum.InstantiateObject)]public float _myPosition;
    [ShowIf("whatWillIDo", MyEnum.InstantiateObject)]public bool xPos,yPos,zPos;
    [Space(10)][ShowIf("whatWillIDo",MyEnum.InstantiateObject)]public GameObject spawnPrefab;
    
    
    [ShowIf("whatWillIDo",MyEnum.ChangeAllListWithSameObject)]
    [DetailedInfoBox("This button will change your all list with same prefab(WillSet Object).","This button will change your all list with same prefab(WillSet Object).")]
    [Button(ButtonSizes.Large)]
    public void ChangeAllListWithSameObject()
    {
        for (int i = 0; i < onScene.Count; i++)
        {
            var pos1 = onScene[i].transform.position;
            var rot1 = onScene[i].transform.rotation;
            var name = onScene[i].name;

            DestroyImmediate(onScene[i]);

            var go = PrefabUtility.InstantiatePrefab(willSet) as GameObject;

            if (whatIsObjectName)
            {
                go.name = objectName + i;
            }
            else
            {
                go.name = willSet.name + i;
            }
            
            if (parent != null) go.transform.parent = parent;
            go.transform.position = pos1;
            go.transform.rotation = rot1;

            onScene[i] = go;
            
            AddMyComponents(go);
        }
        
    }

    
    [ShowIf("whatWillIDo",MyEnum.ChangeOneObject)]
    [DetailedInfoBox("This button will change your gameObject(WillDelete) with (WillSet) gameObject.","This button will change your gameObject(WillDelete) with (WillSet) gameObject.")]
    [Button(ButtonSizes.Large)]
    public void ChangeOneObject()
    {
        var pos1 = willDelete.transform.position;
        var rot1 = willDelete.transform.rotation;
        var name = willDelete.name;

        DestroyImmediate(willDelete);

         var go = PrefabUtility.InstantiatePrefab(willSpawn) as GameObject;
         
         if (whatIsObjectName)
         {
             go.name = objectName;
         }
         else
         {
             go.name = willSpawn.name;
         }
         
         if (parent != null) go.transform.parent = parent;
         go.transform.position = pos1;
         go.transform.rotation = rot1;
         
         willDelete = go;
         
         AddMyComponents(go);
    }

    
    [ShowIf("whatWillIDo",MyEnum.ChangeListToList)]
    [DetailedInfoBox("This button will switch your list(WillDeleteList to WillSetList).","This button will switch your list(WillDeleteList to WillSetList).")]
    [Button(ButtonSizes.Large)]
    public void ChangeListToList()
    {
        _listCount = willDeleteList.Count;

        for (int i = 0; i < _listCount; i++)
        {
            var pos1 = willDeleteList[i].transform.position;
            var rot1 = willDeleteList[i].transform.rotation;
            var name = willDeleteList[i].name;

            DestroyImmediate(willDeleteList[i]);

            var go = PrefabUtility.InstantiatePrefab(willSetList[i]) as GameObject;

            if (whatIsObjectName)
            {
                go.name = objectName + i;
            }
            else
            {
                go.name = willSetList[i].name + i;
            }
            
            if (parent != null) go.transform.parent = parent;
            go.transform.position = pos1;
            go.transform.rotation = rot1;

            willDeleteList[i] = go;
            
            AddMyComponents(go);
        }
    }
    
    [ShowIf("whatWillIDo",MyEnum.InstantiateObject)]
    [DetailedInfoBox("This button will instantiate gameObject as you click.","This button will instantiate gameObject as you click.")]
    [Button(ButtonSizes.Large)]
    public void InstantiateObject()
    {
        for (int i = 0; i < objectCount; i++)
        {
            var go = PrefabUtility.InstantiatePrefab(spawnPrefab) as GameObject;
            if (!myPosition)
                go.transform.position = new Vector3(0, 0, 0);
            
            
            go.transform.rotation = new Quaternion(0, 0, 0, 0);

            if (myPosition)
                go.transform.position = whereWillBe;

            if (plusMyPosition)
            {
                if (xPos)
                {
                    var objPos = go.transform.position;
                    go.transform.position = new Vector3(objPos.x + _myPosition, objPos.y, objPos.z);
                }

                if (yPos)
                {
                    var objPos = go.transform.position;
                    go.transform.position = new Vector3(objPos.x, objPos.y + _myPosition, objPos.z);
                }

                if (zPos)
                {
                    var objPos = go.transform.position;
                    go.transform.position = new Vector3(objPos.x, objPos.y, objPos.z + _myPosition);
                }
            }
            
            if (parent != null) go.transform.parent = parent;
            
            if (whatIsObjectName)
            {
                go.name = objectName + i;
            }
            else
            {
                go.name = spawnPrefab.name + i;
            }

            if (xPos)
                _myPosition = go.transform.position.x;
            if (yPos)
                _myPosition = go.transform.position.y;
            if (zPos)
                _myPosition = go.transform.position.z;
            
            _myPosition += plusPosition;
            
            AddMyComponents(go);
        }
    }
    
    
    private void AddMyComponents(GameObject go)
    {
        if (addRigidbody)
            go.AddComponent<Rigidbody>();

        if (addBoxCollider)
            go.AddComponent<BoxCollider>();

        if (addCapsuleCollider)
            go.AddComponent<CapsuleCollider>();

        if (addMeshCollider)
            go.AddComponent<MeshCollider>();

        if (addDoTween)
            go.AddComponent<DOTweenAnimation>();

        if (objectTag != "")
            go.tag = objectTag;

        if (objectLayer != "")
            go.layer = LayerMask.NameToLayer(objectLayer);
        
        if (myScript != "")
            go.AddComponent(Type.GetType(myScript));
    }

    
    [Button(ButtonSizes.Small)]
    private void ResetVariables()
    {
        whatWillIDo = MyEnum.PickOne;
        parent = null;
        whatIsObjectName = false;
        objectName = null;
        addRigidbody = false;
        addBoxCollider = false;
        addCapsuleCollider = false;
        addMeshCollider = false;
        addDoTween = false;
        objectTag = null;
        objectLayer = null;
        myScript = null;
        _myPosition = 0;
    }
}

#endif