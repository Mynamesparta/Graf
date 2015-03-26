using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Canvas_Text_Field : MonoBehaviour {
	public Transform LeftConer;
	public Transform RightConer;
	public InputField inputField;
	public Font font;
	//public Text textField;
	public Vector3 ownPos;
	//public Vector3 stream_ownPos;
	public int value;
	//public int stream;
	public bool isActiveOnTheBegin = true;

	private Text text;
	void Awake()
	{
		text = GetComponentInChildren<Text> ();
		setEdit(isActiveOnTheBegin);
		//print ("hello");
		inputField.GetComponentInParent<Text> ().font = font;
	}
	void LateUpdate () 
	{
		if(LeftConer!=null&&RightConer!=null)
		{
			transform.position = ownPos + LeftConer.position + (RightConer.position - LeftConer.position) / 2;
		}
		if (text.text != "")
			value = Convert.ToInt32 (text.text);
		else 
			value = 0;
	}
	public void setEdit(bool b)
	{
		//print ("setEdit:" + b);
		if (inputField != null)
			inputField.gameObject.SetActive (b);
	}
	public void setValue(int m)
	{
		value = m;
		inputField.text = m.ToString ();
	}
}
