using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

    //Префаб врага
    public Object EnemyPrefab;
    //Расстояние до игрока
    public float Distance = 2;
    //Угол занимаемый врагами (360 - вся окружность)
     float Angle = 360;
    //Количество врагов
     int count = 6;

    // Use this for initialization
    void Start()
    {
        //Определяем начальную точку
        Vector2 point = transform.position;
       
        Angle = Angle * Mathf.Deg2Rad;

        for (int i = 1; i <= count; i++)
        {
            //Рассчитываем координату Z для врага
            float _z = transform.position.y + Mathf.Cos(Angle / count * i) * Distance;
            //Рассчитываем координату X для врага
            float _x = transform.position.x + Mathf.Sin(Angle / count * i) * Distance;
            point.x = _x;
            point.y = _z;
            //Создаём врага
            Instantiate(EnemyPrefab, point, Quaternion.identity);
        }
    }
}