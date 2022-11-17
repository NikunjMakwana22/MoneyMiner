using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    //Movement
     private Vector2 _currentPos,_lastPos,_deltapos;
     private Vector3 _temp;
    [SerializeField] private float _speed;

    //TransformApproch
    Transform _myTransform;




 

    //RigidBodyApproch
    // Rigidbody _myrigidbody;




    //metadata
   [SerializeField] private float _vRange, _vpower, _vCapaciy;


    //TempraryCode
    public GameObject[] MoneyObjects;





    public List<GameObject> InrangeObjects;
    public GameObject temp2;




    [SerializeField] Transform PlayerModel;


    public Material CanCollect;

    public void CollectObject()
    {
        int countt = InrangeObjects.Count;
        for (int i = 0; i < countt; i++)
        {
            temp2 = InrangeObjects[i];
            //  InrangeObjects[i].transform.position = Vector3.Lerp(_myTransform.position, InrangeObjects[i].transform.position, 1f * Time.deltaTime);
            temp2.transform.position = Vector3.Lerp(temp2.transform.position, _myTransform.position, _vpower * Time.deltaTime);
            if (Vector3.Distance(temp2.transform.position, _myTransform.position) < 0.5f)
            {
                InrangeObjects.RemoveAt(i);
                countt = InrangeObjects.Count;
                Destroy(temp2.gameObject);
            }
        }
    }

    public void GetMoneyObjectsInsideRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(_myTransform.position, _vRange);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if(hitColliders[i].CompareTag("CollectableObjects"))
            {
                if (Vector3.Distance(_myTransform.position, hitColliders[i].transform.position) < _vRange)
                {
                    hitColliders[i].GetComponent<MeshRenderer>().material = CanCollect;
                    InrangeObjects.Add(hitColliders[i].gameObject);
                    hitColliders[i].tag = "Untagged";
                    
                }
            }
        }
    }




   
 
    // Start is called before the first frame update
    void Start()
    {
        //Temp



      //  _myrigidbody = GetComponent<Rigidbody>();
        _myTransform = this.transform;
        PlayerModel = transform.GetChild(0).transform;
        GridManager.Instance.ReturnGridIndex(_myTransform.position);
        GetMoneyObjectsInsideRange();

    }

    // Update is called once per frame
    void Update()
    {
        CollectObject();
        if(Input.GetMouseButtonDown(0))
        {
            _lastPos = Input.mousePosition;
        }
        else if(Input.GetMouseButton(0))
        {
            _currentPos = Input.mousePosition;
            _deltapos = _currentPos - _lastPos;
            _deltapos = _deltapos.normalized;
            _temp.x = _deltapos.x * _speed * Time.deltaTime;
            _temp.z = _deltapos.y *_speed * Time.deltaTime;


            //TransformApproch
            _myTransform.position += _temp;
            PlayerModel.rotation = Quaternion.Lerp(PlayerModel.rotation, Quaternion.LookRotation(_temp), Time.deltaTime * 10f);


            //RigidBodyApproch
            // _myrigidbody.AddForce(_temp*_speed*Time.deltaTime, ForceMode.Acceleration);

        }
        if (Input.GetMouseButtonUp(0))
            GetMoneyObjectsInsideRange();
    }




    private void OnDrawGizmos()
    {
        Gizmos.color=Color.red;
        _myTransform = this.transform;
        Gizmos.DrawWireSphere(_myTransform.position, _vRange);

    }



    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
