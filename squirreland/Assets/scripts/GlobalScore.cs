using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScore : MonoBehaviour
{
    // Статическая переменная для хранения общего счёта игрока.
    // Так как переменная static, она доступна из любого другого скрипта через GlobalScore.score.
    // Пример: GlobalScore.score++;
    // Значение сохраняется между сценами, пока не будет обнулено вручную.
    public static int score = 0; 
}
