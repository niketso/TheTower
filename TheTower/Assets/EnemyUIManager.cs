using UnityEngine.UI;
using UnityEngine;

public class EnemyUIManager : MonoBehaviour {

    [SerializeField] private Slider enemyHpSlider;
    [SerializeField] private EnemyHealth enemyHp;


    
    private void Start()
    {
        enemyHpSlider.maxValue = enemyHp.GetStrength();
        enemyHpSlider.value = enemyHpSlider.maxValue;
    }

    private void Update()
    {   
        enemyHpSlider.value =  enemyHp.Health;
    }


}
