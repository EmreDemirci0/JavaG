
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DusmanKontrol : MonoBehaviour
{
    // public float donmehiz = 0;
    // public float donmehiz = 0;
    public int resim;
    GameObject[] gidilecekNoktalar;
    GameObject karakter;
    bool mesafeAl = true;
    bool ileriMi = true;
    int aradakiMesafesayac = 0;
    public LayerMask layermask;
    public int dusman_hiz = 10;
    RaycastHit2D ray;
    Vector3 aradakiMesafe;
    void Start()
    {
        gidilecekNoktalar = new GameObject[transform.childCount];

        karakter = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < gidilecekNoktalar.Length; i++)
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);//giidlecek noktanın ebeynini ayarla ebebynle aynı yere koy
        }
    }
    void FixedUpdate()
    {
        beniGordumu();
        noktalaraGit();
    }
    void beniGordumu()
    {
        Vector3 rayYonum=karakter.transform.position-transform.position;
        ray = Physics2D.Raycast(transform.position,rayYonum,1000,layermask);
        Debug.DrawLine(transform.position,ray.point,Color.red);
    }
    void noktalaraGit()
    {
        //Debug.Log();
        if (mesafeAl)
        {
            //  transform.position += (gidilecekNoktalar[0].transform.position - transform.position) * Time.deltaTime;
            aradakiMesafe = (gidilecekNoktalar[aradakiMesafesayac].transform.position - transform.position).normalized;
            mesafeAl = false;
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[aradakiMesafesayac].transform.position);
        transform.position += aradakiMesafe * Time.deltaTime * dusman_hiz;

        if (mesafe < 0.5f)
        {
            mesafeAl = true;
            if (aradakiMesafesayac == gidilecekNoktalar.Length - 1)
            {
                ileriMi = false;
            }
            else if (aradakiMesafesayac == 0)
            {
                ileriMi = true;
            }

            if (ileriMi)
            {
                aradakiMesafesayac++;
            }
            else
            {
                aradakiMesafesayac--;
            }

        }
    }
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 0.4f);
        }
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(DusmanKontrol))]
[System.Serializable]
class DusmanKontrolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DusmanKontrol script = (DusmanKontrol)target;
        if (GUILayout.Button("ÜRET", GUILayout.MinWidth(100), GUILayout.Width(100)))
        {
            GameObject yeniObjem = new GameObject(/*"Buraya yeni gameobject adı yazılır"*/);
            yeniObjem.transform.parent = script.transform;
            yeniObjem.transform.position = script.transform.position;
            yeniObjem.name = script.transform.childCount.ToString();

        }
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("layermask"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("dusman_hiz"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }

}
#endif