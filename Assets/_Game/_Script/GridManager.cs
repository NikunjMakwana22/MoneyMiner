using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridManager : MonoBehaviour
{


    public static GridManager Instance;

    [SerializeField] Transform _playerTransform;
    [SerializeField] float _GridSize;
    [SerializeField] float _LevelSize;
    [SerializeField] int _len;
    [SerializeField] float temp;

    Vector3[,] _gridCenter;

    public Vector2 CurrentPos;



    //TempVar
    public GameObject EmptyObject;
    public GameObject TempP;
    public GameObject TempPrefab;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CurrentPos = new Vector2(0, 0);
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void GenerateGrid()
    {
        //int count = TempP.transform.childCount;
        //Debug.Log(count);
        //for (int k = count-1; k >= 0; k--)
        //{   
        //    Destroy(TempP.transform.GetChild(k).gameObject);
        //}
        temp = _LevelSize - _GridSize * 0.5f;
        Vector3 pos = new Vector3(temp * -1f, 0, temp * -1f);
        Vector3 size = new Vector3(_GridSize, 0.01f, _GridSize);
        int i, j = 0;
        _len = (int)(_LevelSize / (_GridSize / 2));
        _gridCenter = new Vector3[_len, _len];
        for (i = 0; i < _len; i++)
        {
            pos.z = temp * -1f;
            for (j = 0; j < _len; j++)
            {
                _gridCenter[i, j] = pos;
                if (pos.z < temp)
                {
                    pos.z += _GridSize;
                }
            }
            pos.x += _GridSize;
        }
        
        //Debug2dArray();
    }

    private void Debug2dArray()
    {
        int i, j = 0;
        for (i = 0; i < _len; i++)
        {

            for (j = 0; j < _len; j++)
            {
                TempPrefab = Instantiate(EmptyObject, TempP.transform);
                TempPrefab.transform.position = _gridCenter[i, j];
                Debug.Log(_gridCenter[i, j]);
            }

        }
    }



    private void OnDrawGizmos()
    {
       // Gizmos.color = Color.gray;
     //  Gizmos.DrawCube(_gridCenter[(int)CurrentPos.x,(int)CurrentPos.y],new Vector3(_GridSize,_GridSize,_GridSize));
        Gizmos.color = Color.red;
        temp = _LevelSize - _GridSize * 0.5f;
        Vector3 pos = new Vector3(temp * -1f, 0, temp * -1f);
        Vector3 size = new Vector3(_GridSize, 0.01f, _GridSize);
        int i, j = 0;
        _len = (int)(_LevelSize / (_GridSize / 2));

        for (i = 0; i < _len; i++)
        {
            pos.z = temp * -1f;
            for (j = 0; j < _len; j++)
            {

                Gizmos.DrawWireCube(pos, size);

             
                
                //Handles.Label(pos, pos.ToString());
                Gizmos.DrawSphere(pos, 0.2f);
                if (pos.z < temp)
                {
                    pos.z += _GridSize;
                }
            }
            pos.x += _GridSize;
        }
    }


    public void ReturnGridIndex(Vector3 playerPos)
    {
        //Debug.Log(playerPos);
        for (int i = 0; i < _len; i++)
        {
          //  Debug.Log(playerPos.x + " CurrentGrid:" + _gridCenter[i, 0]+" index:"+i +" min:" +( _gridCenter[i, 0].x - _GridSize*0.5) + " max:" + (_gridCenter[i, 0].x + _GridSize*0.5));

            if (playerPos.x > _gridCenter[i, 0].x - (_GridSize*0.5) && playerPos.x < _gridCenter[i, 0].x + (_GridSize*0.5))
            {
              //  Debug.Log(i);
                CurrentPos.x = i;
                break;
            }
        }

        for (int i = 0; i < _len; i++)
        {
          //  Debug.Log(playerPos.z + " CurrentGrid:" + _gridCenter[0, i] + " index:" + i + " min:" + (_gridCenter[0, i].z - _GridSize * 0.5) + " max:" + (_gridCenter[0, i].z + _GridSize * 0.5));

            if (playerPos.z > _gridCenter[0, i].z - (_GridSize * 0.5) && playerPos.z < _gridCenter[0, i].z + (_GridSize * 0.5))
            {
            //    Debug.Log(i);
                CurrentPos.y = i;
            }
        }
        
    }
}

