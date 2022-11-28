using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    //Animations
    private Animator Anim;
    //Animation_End


    //Movement
    private Vector2 _currentPos,_lastPos,_deltapos;
    private Vector3 _temp;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotSpeed;
    //Movement_End


    //TransformApproch
    Transform _myTransform;
    [SerializeField] Transform PlayerModel;
    //TransformApprochEnd



    //RigidBodyApproch
    //  Rigidbody _myrigidbody;
    //RigidBodyApproch_End


    //metadata
    [SerializeField] private float _vRange, _vpower, _vCapaciy;
    public int CurrentAmount;
    public int AmountInBag;
    public TextMeshProUGUI CashText;
    public bool IsBagFull = false;
    //metadata_End


    //VacuumLogicObjects
    private Vector3 VacCenter;
    public GameObject Box;
    public Quaternion Boxr;
    public List<GameObject> InrangeObjects;
    public GameObject temp2;
    //VacuumLogicObjects_End




    //UI
    public Image CapacityProgressBar;




    //TempraryCode
    public Material CanCollect;



    void Start()
    {
        _myTransform = this.transform;
        PlayerModel = transform.GetChild(1).transform;
        Anim = PlayerModel.GetComponent<Animator>();
        GridManager.Instance.ReturnGridIndex(_myTransform.position);
        GetCurrentValue();
        GetMoneyObjectsInsideRange();
    }

    void Update()
    {
        if(!IsBagFull)
            GetMoneyObjectsInsideRange();
#if UNITY_EDITOR
     
        if (Input.GetMouseButtonDown(0))
        {
            _lastPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Anim.SetBool("Run", true);
            _currentPos = Input.mousePosition;
            _deltapos = _currentPos - _lastPos;
            _deltapos = _deltapos.normalized;
            _temp.x = _deltapos.x * _speed * Time.deltaTime;
            _temp.z = _deltapos.y * _speed * Time.deltaTime;

            //TransformApproch
            _myTransform.position += _temp;
            PlayerModel.rotation = Quaternion.Lerp(PlayerModel.rotation, Quaternion.LookRotation(_temp), Time.deltaTime *_rotSpeed);

            //RigidBodyApproch
            // _myrigidbody.AddForce(_temp*_speed*Time.deltaTime, ForceMode.Acceleration);

        }
        if (Input.GetMouseButtonUp(0))
        {
            Anim.SetBool("Run", false);
        }
//#elif UNITY_IOS || UNITY_ANDROID

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _currentPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    _deltapos = touch.position - _currentPos;
                    _deltapos = _deltapos.normalized;
                    _temp.x = _deltapos.x * _speed * Time.deltaTime;
                    _temp.z = _deltapos.y * _speed * Time.deltaTime;
                    _myTransform.position += _temp;
                    PlayerModel.rotation = Quaternion.Lerp(PlayerModel.rotation, Quaternion.LookRotation(_temp), Time.deltaTime *_rotSpeed );
                    Anim.SetBool("Run", true);
                    break;

                case TouchPhase.Ended:
                    Anim.SetBool("Run", false);
                    break;

                case TouchPhase.Stationary:
                    _myTransform.position += _temp;
                    PlayerModel.rotation = Quaternion.Lerp(PlayerModel.rotation, Quaternion.LookRotation(_temp), Time.deltaTime * _rotSpeed);
                    break;
            }
        }
#endif
    }
    public void SetVacSize()
    {
        Vector3 temp = _myTransform.eulerAngles;
        _myTransform.eulerAngles = Vector3.zero;
        VacCenter = _myTransform.position;
        VacCenter += Vector3.forward * _vRange * 2;
        _myTransform.eulerAngles = temp;
        Box.transform.position = VacCenter;
        Box.transform.rotation = Boxr;
        Box.transform.localScale = Box.transform.parent.InverseTransformVector(Vector3.one * _vRange * 2);
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
            temp2.transform.position = Vector3.Lerp(temp2.transform.position, _myTransform.position, _vpower * Time.deltaTime);            
            if (Vector3.Distance(temp2.transform.position, _myTransform.position) < 0.5f)
            {
                InrangeObjects.RemoveAt(i);
                countt = InrangeObjects.Count;
                AmountInBag+=1;
                if (AmountInBag >= _vCapaciy)
                {
                    int len = InrangeObjects.Count;
                    for (int j = 0; j < len; j++)
                    {
                        InrangeObjects[j].tag = "CollectableObjects";
                    }
                    InrangeObjects.Clear();
                    IsBagFull = true;
                    break;
                }
                CapacityProgressBar.fillAmount = AmountInBag / _vCapaciy;
                Destroy(temp2.gameObject);
            }
        }
    }


    public void GetMoneyObjectsInsideRange()
    {
       
        Collider[] hitColliders = Physics.OverlapBox(Box.transform.position, Vector3.one*_vRange, Boxr);
        for (int i = 0; i < hitColliders.Length; i += 2)
        { 
         
            if (hitColliders[i].CompareTag("CollectableObjects"))
            {
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



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        _myTransform = this.transform;
        Gizmos.DrawWireCube(Box.transform.position, Vector3.one * _vRange * 2);
    }






}
