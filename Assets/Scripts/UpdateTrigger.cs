using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class UpdateTrigger : MonoBehaviour
{
    [Serializable]
    public class UpdateTriggerItem{
        [SerializeField]
        public MonoBehaviour SourceObject;
        [SerializeField]
        public string SourceMember;

        [SerializeField]
        public MonoBehaviour TargetObject;
        [SerializeField]
        public string TargeMember;

        public MemberInfo SourceInfo;
        public MethodInfo TargetInfo;
    }


    public UpdateTriggerItem[] Items;


    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in Items)
        {
            item.SourceInfo = item.SourceObject.GetType().GetMember(item.SourceMember).First();
            item.TargetInfo = item.TargetObject.GetType().GetMethod(item.TargeMember);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in Items)
        {
            object val = null;
            if(item.TargetInfo != null && item.SourceInfo != null){
                if(item.SourceInfo.MemberType == MemberTypes.Method) val = ((MethodInfo) item.SourceInfo).Invoke(item.SourceObject, new object[0]);
                if(item.SourceInfo.MemberType == MemberTypes.Property) val = ((PropertyInfo) item.SourceInfo).GetValue(item.SourceObject);

                if(val != null){
                    item.TargetInfo.Invoke(item.TargetObject, new object[]{val});
                }
            }
        }   
    }

    public float[] getData(){
        return new float[]{0.4f, 0.6f};
    }
}




[UnityEditor.CustomEditor(typeof(UpdateTrigger))]
 public class UpdateTriggerItemEditor : Editor{

     int itemcnt = 0;
    public override void OnInspectorGUI()
    {

        // UpdateTrigger trigger = (UpdateTrigger)target;
        // damage = EditorGUILayout.IntSlider("Damage", damage, 0, 100);
        // ProgressBar (damage / 100.0f, "Damage");

        // armor = EditorGUILayout.IntSlider ("Armor", armor, 0, 100);
        // ProgressBar (armor / 100.0f, "Armor");

        EditorGUILayout.BeginFoldoutHeaderGroup(true, "group");
        itemcnt = EditorGUILayout.IntField("Item count", itemcnt );

        EditorGUILayout.EndFoldoutHeaderGroup();




        /*bool allowSceneObjects = !EditorUtility.IsPersistent (target);
        mp.gun = (GameObject)EditorGUILayout.ObjectField ("Gun Object", mp.gun, typeof(GameObject), allowSceneObjects);*/
    }

    // Custom GUILayout progress bar.
    void ProgressBar (float value, string label)
    {
        // Get a rect for the progress bar using the same margins as a textfield:
        Rect rect = GUILayoutUtility.GetRect (18, 18, "TextField");
        EditorGUI.ProgressBar (rect, value, label);
        EditorGUILayout.Space ();
    }   
 }
