using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{

    public int countUnit, cnUnit;
    public float size, ted2,fx, fy,fz =1;
    public Vector2 posC, posC2, vtext, vtext2;    
  
    public GameObject Unit, clone;

    private bool colOn = false, enemy, neutral, my;

    public Vector3 Origposition, pos0, pos1, pos2, pos3, pos4;
    
   
    void OnGUI()
    {
        if (countUnit < 10)
        {
            fx = 3;
            fy = 20;
        }
        if (countUnit >= 10)
        {
            fx = 5;
            fy = 20;
        }
        if (countUnit >= 10)
        {
            fx = 7;
            fy = 20;
        }
        GUI.Label(new Rect(posC.x-fx ,Screen.height - posC.y-fy , 100, 20), countUnit.ToString());
    }
    void Start()
    {
      
       
    }
    void Awake()
    {
        cnUnit = 0;
      
        
       

    }

    public virtual void CreateUnit(GameObject UnitE)
    {
        if (Input.GetMouseButton(0) && !Input.GetMouseButtonUp(0))
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            if (collider2D == Physics2D.OverlapPoint(touchPos))
            {
                Origposition = transform.position;
              
                colOn = true;
            }
        }
        else if (colOn == true)
        {
            if (countUnit > 0)
            {
                Vector2 wp2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(wp2, -Vector2.up);

                if (hit.collider && hit.collider.gameObject.name != gameObject.name)
                {
                    StartCoroutine(goWaits(UnitE, hit, countUnit));
                }
            }
            colOn = false;
        }
    }

    public IEnumerator goWaits(GameObject UnitE2, RaycastHit2D hit2, int countUnit2)
    {
        float Angle = -20;
        Origposition.z = 5;
        pos4 = GameObject.Find(hit2.collider.gameObject.name).transform.position;
        for (int i = 1; i <= countUnit2; i++)
        {
            pos1 = Origposition;

            pos4.z = Origposition.z;

            pos2 = calc_pos(pos1, pos4, 2, Angle);
            pos3 = calc_pos(pos1, pos4, 3, Angle);

            ted2 -= 100;
            cnUnit += 1;

            clone = Instantiate(UnitE2, pos1, Quaternion.identity) as GameObject;
            clone.name = "unite_" + gameObject.name + "_" + cnUnit;
            clone.GetComponent<PathFind>().nameBase = hit2.collider.gameObject.name;
            clone.GetComponent<PathFind>().pos2 = pos2;
            clone.GetComponent<PathFind>().pos3 = pos3;
            clone.GetComponent<PathFind>().pos4 = pos4;
            Angle += 10;
            Origposition.z = Origposition.z + 1 * fz;

            int dig = i % 5;
            if (dig == 0)
            {
                Origposition.z = 5;
                Angle = -20;
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
    public Vector3 calc_pos(Vector3 p1, Vector3 p4, int type, float cAngle)
    {
        Vector3 cV, cV2;
        Vector3 calc;
        float _x, _y, angleU, c_angle;
        switch (type)
        {
            case 2:
                cV = p4 - p1;
                break;
            case 3:
                cV = p1 - p4;
                break;
            default:
                cV = Vector3.zero;
                break;
        }

        c_angle = Mathf.Atan2(cV.y, cV.x) * Mathf.Rad2Deg;

        if (c_angle < 70 && c_angle > -70 && type == 2) {
            //p1.z *= (-1); p4.z *= (-1); pos1.z *= (-1); pos4.z *= (-1);
            fz = -1;
        }
        switch (type)
        {
            case 2:
                angleU = (c_angle - cAngle) * Mathf.Deg2Rad; cV2 = p1;
                _x = p1.x + Mathf.Cos(angleU) * 1;
                _y = p1.y + Mathf.Sin(angleU) * 1;
                break;
            case 3:
                angleU = (c_angle + cAngle) * Mathf.Deg2Rad; cV2 = p4;               
                break;
            default:
                angleU = 0; _x = 0; _y = 0;cV2 = Vector3.zero;
                break;
        }
        _x = cV2.x + Mathf.Cos(angleU) * 1;
        _y = cV2.y + Mathf.Sin(angleU) * 1;
        calc.x = _x; calc.y = _y; calc.z = p4.z;
        return calc;
    }
    void FixedUpdate()
    {
        posC2 = GameObject.Find("cnUnit_" + gameObject.name).transform.position;

      
        posC = Camera.main.WorldToScreenPoint(posC2);

        if (countUnit < 100)
        {
            ted2++;
        }

        countUnit = (int)ted2 / 100;
      
        CreateUnit(Unit);
    }
}
