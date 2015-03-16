using UnityEngine;
using System.Collections;
public enum _NameAlgorithm{Test,Test2};
public class nameAlgorithm : MonoBehaviour {
	public Controller contr;
	public _NameAlgorithm state=_NameAlgorithm.Test;
	public void setAlgorithm_to_this()
	{
		contr.setAlgorithm (this);
	}
	public void Start()
	{
		switch (state) {
		case _NameAlgorithm.Test:
			{
				_Test();
				break;
			}
		}
	}
	static void _Test()
	{
		print ("Hello World of test");
	}
}
