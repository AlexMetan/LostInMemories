using UnityEngine;

public class CameraShaking : MonoBehaviour
{
    Transform camTransform;
	[SerializeField] float headAngle;
	[SerializeField] float headMove;
	[SerializeField] float positionTime;
	[SerializeField] float rotationTime;
	[SerializeField] float rotateToZeroTime;
	float idleMove;
	Vector3 originalPos;
	float defPostionY;

	
	void Start()
	{		
		camTransform = transform;		
		defPostionY=camTransform.localPosition.y;
	
	}
	
	

	void Update()
	{
		if(Player_Static.InventoryOpen||Player_Static.BlockMovement)
			Player_Static.ShakeValue=.2f;
		if(Player_Static.ShakeValue<2)
		{
			
			SetDefRotation();
			idleMove=headMove/4f;
		}			
		else 
		{
			ShakeRot();
			idleMove=headMove;
		}
			
        ShakePos();
	}
	void ShakePos()
	{
	
		var newPlayerPositionY=idleMove+defPostionY;
		if(camTransform.localPosition.y!=newPlayerPositionY)
		{		
			Vector3 newPos= new Vector3(camTransform.localPosition.x,newPlayerPositionY,camTransform.localPosition.z);
			camTransform.localPosition=Vector3.MoveTowards(camTransform.localPosition,newPos,positionTime*Time.deltaTime*Player_Static.ShakeValue);
		}
		else
			headMove*=-1;
		
	}
	void ShakeRot()
	{
		float eular=0;	
		if(headAngle<0) 
			eular=360+headAngle;
		else 
			eular=headAngle;
		if((int)camTransform.localRotation.eulerAngles.z!=eular)
		{			
			camTransform.localRotation=Quaternion.RotateTowards(camTransform.localRotation,Quaternion.AngleAxis(headAngle,Vector3.forward),Time.deltaTime*rotationTime*Player_Static.ShakeValue);
		}
		else 
			headAngle*=-1;
	}
	void SetDefRotation()
	{
		camTransform.localRotation=Quaternion.RotateTowards(camTransform.localRotation,Quaternion.AngleAxis(0,Vector3.forward),rotateToZeroTime*Time.deltaTime);
	}
}
