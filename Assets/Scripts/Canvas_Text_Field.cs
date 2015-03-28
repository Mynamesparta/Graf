using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Canvas_Text_Field : MonoBehaviour {
	public Transform LeftConer;
	public Transform RightConer;
	public InputField inputField;
	public InputField inputField_Left;
	public InputField inputField_Right;
	public Font font;
	//public Text textField;
	public Vector3 ownPos;
	//public Vector3 stream_ownPos;
	public int value;
	public int leftValue;
	public int rightValue;
	//public int stream;
	public bool isActiveOnTheBegin = true;

	private Text text;
	private State_of_Edge state;
	void Awake()
	{
		text = GetComponentInChildren<Text> ();
		//state = State_of_Edge.Normal;
		//_setValue ();
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
		/*/
		if (text.text != "")
			value = Convert.ToInt32 (text.text);
		else 
			value = 0;
		/*/
	}
	public void setEdit(bool b)
	{
		switch(state)
		{
		case State_of_Edge.Normal:
		{
			//print ("setEdit:" + b);
			if (inputField != null)
				inputField.gameObject.SetActive (b);
			if (inputField_Left != null)
				inputField_Left.gameObject.SetActive (false);
			if (inputField_Right != null)
				inputField_Right.gameObject.SetActive (false);
			break;
		}
		case State_of_Edge.Double:
		{
			//print ("setEdit:" + b);
			if (inputField != null)
				inputField.gameObject.SetActive (false);
			if (inputField_Left != null)
				inputField_Left.gameObject.SetActive (b);
			if (inputField_Right != null)
				inputField_Right.gameObject.SetActive (b);
			break;
		}
		case State_of_Edge.Left:
		{
			//print ("setEdit:" + b);
			if (inputField != null)
				inputField.gameObject.SetActive (false);
			if (inputField_Left != null)
				inputField_Left.gameObject.SetActive (b);
			if (inputField_Right != null)
				inputField_Right.gameObject.SetActive (false);
			break;
		}
		case State_of_Edge.Right:
		{
			//print ("setEdit:" + b);
			if (inputField != null)
				inputField.gameObject.SetActive (false);
			if (inputField_Left != null)
				inputField_Left.gameObject.SetActive (false);
			if (inputField_Right != null)
				inputField_Right.gameObject.SetActive (b);
			break;
		}
		}
	}
	public void setValue(int m=0)
	{
		if(m==0)
		{
			m = Convert.ToInt32 (text.text);
		}
		value = m;
		inputField.text = value.ToString ();
		switch (state) {
		case State_of_Edge.Normal:
			{
				//leftValue = 0;
				//rightValue = 0;
				//inputField.text = m.ToString ();
				//inputField_Left.text = "";
				//inputField_Right.text = "";
				break;
			}
		}
	}
	public void setLeftValue(int m)
	{
		m = Convert.ToInt32 (inputField_Left.text);
		leftValue = m;
		switch (state) {
		case State_of_Edge.Left:
			{
				//rightValue = 0;
				//value = 0;
				//inputField.text = "";
				//inputField_Left.text = m.ToString ();
				//inputField_Right.text = "";
				break;
			}
		}
	}
	public void setRightValue(int m)
	{
		m = Convert.ToInt32 (inputField_Right.text);
		rightValue = m;
		switch (state) {
		case State_of_Edge.Right:
			{
				//leftValue = 0;
				//value = 0;
				//inputField.text = "";
				//inputField_Left.text = "";
				//inputField_Right.text = m.ToString ();
				break;
			}
		}
	}

	void _setValue()
	{
		switch(state)
		{
		case State_of_Edge.Normal:
		{
			inputField.text = value.ToString ();
			if(inputField_Left!=null)
				inputField_Left.text= "";
			if(inputField_Right!=null)
				inputField_Right.text= "";
			break;
		}
		case State_of_Edge.Double:
		{
			inputField.text="";
			inputField_Left.text= leftValue.ToString ();
			inputField_Right.text= rightValue.ToString ();
			break;
		}
		case State_of_Edge.Left:
		{
			inputField.text="";
			inputField_Left.text= leftValue.ToString ();
			inputField_Right.text= "";
			break;
		}
		case State_of_Edge.Right:
		{
			inputField.text="";
			inputField_Left.text= "";
			inputField_Right.text= rightValue.ToString ();
			break;
		}
		}
	}
	public void setState(State_of_Edge _state)
	{
		print ("setState:"+_state.ToString ());
		state = _state;
		setEdit (true);
		_setValue ();
	}
}
