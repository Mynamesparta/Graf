using UnityEngine;
using System.Collections;
enum State_of_Controller {Play,Edit,Normal};
public class Controller : MonoBehaviour {

	public GameObject clone_of_Vertex;
	public uint maxNumberOfVertex=10;

	private State_of_Controller state=State_of_Controller.Normal;
	private Stack currendIndex;
	void Awake()
	{
		currendIndex=new Stack(); 
		currendIndex.Push (1);
	}

	public void Edit()
	{
		switch (state) {
		case State_of_Controller.Edit:
			{
				state = State_of_Controller.Normal;
				break;
			}
		case State_of_Controller.Normal:
			{
				state = State_of_Controller.Edit;
				break;
			}
		}
		print (state);
	}
	public void Click()
	{
		print ("hello");
	}
}
