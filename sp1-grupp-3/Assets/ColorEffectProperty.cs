using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColorEffectProperty : MonoBehaviour
{
    public Color fullColor;
    public Color noColor;
    public float speed;

    bool enable = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            enable = !enable;
        }
        
        var objecs = GameObject.FindObjectsOfType<ColourEffect>();

        for (int i = 0; i < objecs.Length; i++)
        {
            objecs[i].fullColor = fullColor;
            objecs[i].noColor = noColor;
            objecs[i].speed = speed;

            if (enable == true)
            {
                EnableColor(objecs[i].transform.GetChild(0).gameObject, objecs[i].transform.GetChild(1).gameObject);
            }
            else
            {
                DisableColor(objecs[i].transform.GetChild(0).gameObject, objecs[i].transform.GetChild(1).gameObject);
            }
        }
    }

    public void EnableColor(GameObject child1, GameObject child2)
    {
        Color trueColor = child1.GetComponent<SpriteRenderer>().color;
        trueColor.a = Mathf.MoveTowards(trueColor.a, fullColor.a, Time.deltaTime * speed);
        child1.GetComponent<SpriteRenderer>().color = trueColor;

        Color trueNoColor = child2.GetComponent<SpriteRenderer>().color;
        trueNoColor.a = Mathf.MoveTowards(trueNoColor.a, noColor.a, Time.deltaTime * speed);
        child2.GetComponent<SpriteRenderer>().color = trueNoColor;
    }
    public void DisableColor(GameObject child1, GameObject child2)
    {
        Color trueColor = child1.GetComponent<SpriteRenderer>().color;
        trueColor.a = Mathf.MoveTowards(trueColor.a, noColor.a, Time.deltaTime * speed);
        child1.GetComponent<SpriteRenderer>().color = trueColor;

        Color trueNoColor = child2.GetComponent<SpriteRenderer>().color;
        trueNoColor.a = Mathf.MoveTowards(trueNoColor.a, fullColor.a, Time.deltaTime * speed);
        child2.GetComponent<SpriteRenderer>().color = trueNoColor;
    }
}