using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class MainCamControl : MonoBehaviour {
	
	
	public float sensitivity =1f;
	private Vector2 f0start;
	private Vector2 f1start;
	private Vector3 offset;
	private GameObject player;
	private bool initialaized;

	void Init (GameObject _player){
		offset = Vector3.forward * -50;

        player = _player;
        
		transform.LookAt(player.transform.position);
		initialaized=true;
	}

    void Update()
    {
        if (initialaized)
        {
            transform.position = player.transform.position + offset;

            if (Input.touchCount < 2 && Input.touchCount > 0)
            {
                //Touch began, save position
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    //				firstpoint = Input.GetTouch (0).position;
                }
                //Move finger by screen
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    //				secondpoint = Input.GetTouch (0).position;
                    //Mainly, about rotate camera. For example, for Screen.width rotate on 180 degree
                    //				yAngle = yAngTemp - (secondpoint.y - firstpoint.y) * 90.0f / Screen.height;
                    float x = Input.GetTouch(0).deltaPosition.x;
                    float y = Input.GetTouch(0).deltaPosition.y;

                    OrbitCamera(player.transform.position, x, y);

                    f0start = Vector2.zero;
                    f1start = Vector2.zero;
                }
            }
            if (Input.touchCount == 2)
            {
                Zoom();
            }
        }

    }

    void Zoom()
	{
		if (f0start == Vector2.zero && f1start == Vector2.zero)
		{
			f0start = Input.GetTouch(0).position;
			f1start = Input.GetTouch(1).position;
		}
		Vector2 f0position = Input.GetTouch(0).position;
		Vector2 f1position = Input.GetTouch(1).position;
//		float dir = Mathf.Sign(Vector2.Distance(f1start, f0start) - Vector2.Distance(f0position, f1position));
//		print (Vector2.Distance(f1start, f0start) - Vector2.Distance(f0position, f1position));
//		transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, dir*sensitivity * Time.deltaTime );
		transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, (Vector2.Distance(f1start, f0start) - Vector2.Distance(f0position, f1position))*sensitivity * Time.deltaTime );

	}
	private void OrbitCamera(Vector3 target, float y_rotate, float x_rotate)
	{
		Vector3 angles = transform.eulerAngles;
		transform.RotateAround(target,transform.up, -y_rotate);
		transform.RotateAround(target,transform.right , x_rotate);
		offset	= transform.position - player.transform.position ;
	}

}