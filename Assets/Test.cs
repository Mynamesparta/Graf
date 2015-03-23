using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Test : MonoBehaviour {
	public int Hello;
	public GameObject clone;
	private List<Camera> vertexs;
	public Transform LeftConer=null;
	public Transform RightConer;
	public Vector3 rotation;
	public float DistanceToScele;
	void Start () 
	{
		vertexs=new List<Camera>();
		GameObject cube;
		vertexs.Add((Object.Instantiate(clone,new Vector3(10,10,10),Quaternion.Euler(0,0f,0f))
		          as GameObject).GetComponent<Camera>());
		Object.Destroy (vertexs[0].gameObject);

	}
	
	
	void LateUpdate () 
	{
		if(LeftConer!=null&&RightConer!=null)
		{
			transform.position = LeftConer.position + (RightConer.position - LeftConer.position) / 2;
			//
			transform.rotation=	Quaternion.FromToRotation(rotation , RightConer.position-LeftConer.position);
			//transform.rotation= Quaternion.Euler(transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y,90f);
			//
			transform.localScale=new Vector3(transform.localScale.x,
			                                 Vector3.Distance(LeftConer.position,RightConer.position)*DistanceToScele,
			                                 transform.localScale.z);
			//
		}
	}
}
