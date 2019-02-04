﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourEffect : MonoBehaviour
{
    public Color fullColor;
    public Color noColor;
    public float speed;
    GameObject child1, child2;
    bool enableEffect = false;

	void Start ()
    {
        //child1 = transform.GetChild(0).gameObject;
        //child2 = transform.GetChild(1).gameObject;
	}
	
	void Update ()
    {
		//if(Input.GetKeyDown(KeyCode.F))
  //      {
  //          enableEffect = !enableEffect;
  //      }

  //      if(enableEffect == true)
  //      {
  //          EnableColor();
  //      }
  //      else
  //      {
  //          DisableColor();
  //      }
	}

    public void EnableColor()
    {
        Color trueColor = child1.GetComponent<SpriteRenderer>().color;
        trueColor.a = Mathf.MoveTowards(trueColor.a, fullColor.a, Time.deltaTime * speed);
        child1.GetComponent<SpriteRenderer>().color = trueColor;

        Color trueNoColor = child2.GetComponent<SpriteRenderer>().color;
        trueNoColor.a = Mathf.MoveTowards(trueNoColor.a, noColor.a, Time.deltaTime * speed);
        child2.GetComponent<SpriteRenderer>().color = trueNoColor;
    }
    public void DisableColor()
    {
        Color trueColor = child1.GetComponent<SpriteRenderer>().color;
        trueColor.a = Mathf.MoveTowards(trueColor.a, noColor.a, Time.deltaTime * speed);
        child1.GetComponent<SpriteRenderer>().color = trueColor;

        Color trueNoColor = child2.GetComponent<SpriteRenderer>().color;
        trueNoColor.a = Mathf.MoveTowards(trueNoColor.a, fullColor.a, Time.deltaTime * speed);
        child2.GetComponent<SpriteRenderer>().color = trueNoColor;
    }
}
