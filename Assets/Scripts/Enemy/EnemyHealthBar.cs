using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{

    public LivingEntity target;
    public Camera cameraBrain;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cameraBrain.transform.position);

    }

    private void LateUpdate()
    {
        GetComponent<Slider>().value = target.currentHealth / target.originalHealth;
    }
}
