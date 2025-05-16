using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] GameObject HPHealth;

   // private void Start(){
    //    HPHealth.transform.localScale = new Vector3(0.5f, 1f);
    //}
    public void SetHP(float hpNormalized){
        HPHealth.transform.localScale = new Vector3(hpNormalized,1f);
    }
}
