﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SpeechLib;

public class HandRayCast : MonoBehaviour
{
    private PersonManager manager;
    private FollowMouse bodyScript;
    private GameObject currentItem;
    private GameObject lastHandAboveItem = null;
    private bool itemCatched = false;
    //private SpVoice voice;


    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponentInParent<PersonManager>();
        bodyScript = manager.body.GetComponent<FollowMouse>();

        //voice = new SpVoice();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(bodyScript.transform.position.x - bodyScript.edge.x - 0.35f, transform.position.y, bodyScript.transform.position.z - bodyScript.edge.z);
        //Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.position = new Vector3(newPos.x - bodyScript.edge.z, newPos.y, newPos.z - bodyScript.edge.z);

        if (Time.timeScale > 0)
        {
            RaycastHit hit;
            if (!itemCatched && Physics.Raycast(transform.position, Vector3.down, out hit, 5))
            {
                //Debug.Log(hit.collider.tag);
                if (hit.collider.tag != "Table" && hit.collider.tag != "Untagged")
                {
                    if (lastHandAboveItem != hit.collider.gameObject)
                    {
                        if (lastHandAboveItem != null)
                            lastHandAboveItem.GetComponent<Outline>().enabled = false;
                        lastHandAboveItem = hit.collider.gameObject;
                        lastHandAboveItem.GetComponent<Outline>().enabled = true;
                    }

                    if (Input.GetKeyDown(Const.MOUSE_LEFT_BUTTON))
                    {
                        itemCatched = true;
                        currentItem = hit.collider.gameObject;
                        //voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
                        //voice.Speak(currentItem.tag);
                    }
                }
                else
                {
                    if (lastHandAboveItem != null)
                    {
                        lastHandAboveItem.GetComponent<Outline>().enabled = false;
                        lastHandAboveItem = null;
                    }
                }

            }
            else if (itemCatched && Input.GetKeyUp(Const.MOUSE_LEFT_BUTTON))
            {
                itemCatched = false;
                //currentItem.GetComponent<Outline>().enabled = false;
                currentItem.GetComponent<Rigidbody>().useGravity = true;
                currentItem = null;
            }
            else if (itemCatched && Input.GetKey(Const.MOUSE_LEFT_BUTTON))
            {
                currentItem.transform.position = transform.position;
                currentItem.GetComponent<Rigidbody>().useGravity = false;
            }

        }

    }


    void DiferentiateItem(string itemTag)
    {
        //switch (itemTag)
        //{
        //    case "Pan":

        //        break;

        //    case "Pizza":

        //        break;

        //    case "Plate":

        //        break;

        //    case "Cooked Pineapple Slice":

        //        break;

        //    case "Cooked Tomato Slice":

        //        break;

        //    case "Cheese":

        //        break;

        //    case "Cheese Slice":

        //        break;

        //    case "Pineapple Slice":

        //        break;

        //    case "Cooked Cheese Slice":

        //        break;

        //    case "Cooked Tomato":

        //        break;

        //    case "Tomato Slice":

        //        break;

        //    case "Pineapple":

        //        break;

        //    case "Tomato":

        //        break;

        //    case "Species":

        //        break;

        //    case "Cooked Cheese":

        //        break;

        //    case "Cooked Pineapple":

        //        break;

        //    //case "Wall Colliders":

        //    //    break;

        //    case "Board":

        //        break;

        //    case "Kitchen Knife":

        //        break;

        //    case "Cooked Carrot":

        //        break;

        //    case "Lettuce":

        //        break;

        //    case "Cooked Ham":

        //        break;

        //    case "Cooked Lettuce Leaf":

        //        break;

        //    case "Bacon Slice":

        //        break;

        //    case "Cooked Ham Slice":

        //        break;

        //    case "Cooked Fish":

        //        break;

        //    case "Bacon":

        //        break;

        //    case "Cooked Bacon Slice":

        //        break;

        //    case "Fish Slice":

        //        break;

        //    case "Fish":

        //        break;

        //    case "Sausage":

        //        break;

        //    case "Cooked Bacon":

        //        break;

        //    case "Cooked Fish Slice":

        //        break;

        //    case "Ham Slice":

        //        break;

        //    case "Sausage Slice":

        //        break;

        //    case "Cooked Sausage":

        //        break;

        //    case "Cooked Carrot Slice":

        //        break;

        //    case "Cooked Sausage Slice":

        //        break;

        //    case "Cooked Lettuce":

        //        break;

        //    case "Carrot Slice":

        //        break;

        //    case "Ham":

        //        break;

        //    case "Lettuce Leaf":

        //        break;

        //    case "Carrot":

        //        break;

        //    case "Oven":

        //        break;

        //    case "Table":

        //        break;

        //    default:
        //        break;
        //}
    }
}
