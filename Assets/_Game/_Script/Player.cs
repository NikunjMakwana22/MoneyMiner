using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class Player : MonoBehaviour
{

    //Animations
    private Animator Anim;

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
    public int CurrentAmount;
    public int AmountInBag;
    public TextMeshProUGUI CashText;



    //TempraryCode
    private Vector3 VacCenter;


    public Transform PlayerModelTransform;



    public GameObject[] MoneyObjects;





    public List<GameObject> InrangeObjects;
    public GameObject temp2;




    [SerializeField] Transform PlayerModel;


    public Material CanCollect;


    public GameObject Box;
    public Quaternion Boxr;



  
 
    // Start is called before the first frame update
    void Start()
    {
        //Temp



        //  _myrigidbody = GetComponent<Rigidbody>();
       
       
        PlayerModelTransform = transform.GetChild(1).transform;
        _myTransform = this.transform;
        PlayerModel = transform.GetChild(1).transform;
        Anim = PlayerModel.GetComponent<Animator>();
        GridManager.Instance.ReturnGridIndex(_myTransform.position);
        GetCurrentValue();
        GetMoneyObjectsInsideRange();
    }

    // Update is called once per frame
    void Update()
    {
     GetMoneyObjectsInsideRange();
        if(Input.GetMouseButtonDown(0))
        {
            _lastPos = Input.mousePosition;
        }
        else if(Input.GetMouseButton(0))
        {
            Anim.SetBool("Run", true);
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
        {
            Anim.SetBool("Run", false);
           // GetMoneyObjectsInsideRange();
        }
    }




    private void OnDrawGizmos()
    {
        Gizmos.color=Color.black;
        _myTransform = this.transform;
   //     Gizmos.DrawWireSphere(_myTransform.position, _vRange);
        Gizmos.DrawWireCube(Box.transform.position,Vector3.one * _vRange*2);
    
    }



    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }





    public void GetCurrentValue()
    {
        _vRange = GameManager.Instance.CurrentRangeLevel;
        _vpower=GameManager.Instance.CurrentPowerLevel;
        _vCapaciy = GameManager.Instance.CurrentCapacityLevel;
        SetVacSize();
    }






    public void CollectObject()
    {
        int countt = InrangeObjects.Count;

        for (int i = 0; i < countt; i++)
        {
            temp2 = InrangeObjects[i];
            //  InrangeObjects[i].transform.position = Vector3.Lerp(_myTransform.position, InrangeObjects[i].transform.position, 1f * Time.deltaTime);
            temp2.transform.position = Vector3.Lerp(temp2.transform.position, _myTransform.position, _vpower * Time.deltaTime);            
            //Scale optiom
            //    temp2.transform.localScale = Vector3.Lerp(temp2.transform.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime);

            if (Vector3.Distance(temp2.transform.position, _myTransform.position) < 0.5f)
            {
                InrangeObjects.RemoveAt(i);
                countt = InrangeObjects.Count;
                AmountInBag++;
                Destroy(temp2.gameObject);
            }
        }
    }



    public void SetVacSize()
    {
        VacCenter = _myTransform.position;
        VacCenter.z += _vRange*2;
        Box.transform.position=VacCenter;
         Box.transform.rotation=Boxr;
        Box.transform.localScale=Box.transform.parent.InverseTransformVector(Vector3.one * _vRange * 2);
            //= Vector3.one * _vRange*2;
    }

    public void GetMoneyObjectsInsideRange()
    {
       
        Collider[] hitColliders = Physics.OverlapBox(Box.transform.position, Vector3.one*_vRange, Boxr);
        for (int i = 0; i < hitColliders.Length; i += 2)
        { 
         
            if (hitColliders[i].CompareTag("CollectableObjects"))
            {
         //       float angle = Vector3.Angle(PlayerModelTransform.position, hitColliders[i].transform.position);
               //if (Vector3.Distance(_myTransform.position, hitColliders[i].transform.position) < _vRange && angle<60)
                if (Vector3.Distance(_myTransform.position, hitColliders[i].transform.position) < _vRange *2)
               {
                    hitColliders[i].GetComponent<MeshRenderer>().material = CanCollect;
                    InrangeObjects.Add(hitColliders[i].gameObject);
                    hitColliders[i].tag = "Untagged";
                }
            }
        }

        CollectObject();
    }







   



}
