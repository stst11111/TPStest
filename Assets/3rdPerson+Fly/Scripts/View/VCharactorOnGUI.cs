using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VCharactorOnGUI : MonoBehaviour {

    public Canvas CharCanvas;
    public GameObject Charactor;
    private Vector3 OnScreen;
    private Camera maincam;
    public Image Bar;
    //public GameObject pointer;
    //public GameObject Circle;
	// Use this for initialization
	void Start () {
        maincam = Camera.main;
        //Instantiate(pointer ,Circle.transform);
	}
	
	// Update is called once per frame
	void Update () {
        OnScreen = maincam.WorldToScreenPoint(Charactor.transform.position);
        if (OnScreen.x<Screen.width && OnScreen.x>0 && OnScreen.y<Screen.height && OnScreen.y>0 &&OnScreen.z>0)
            //在屏幕内
        {
            Ray r = maincam.ScreenPointToRay(OnScreen);
            RaycastHit[] hits =Physics.RaycastAll(r, Vector3.Distance(Charactor.transform.position, maincam.gameObject.transform.position));
            foreach (RaycastHit hit in hits )
            {
                GameObject hitobj = hit.transform.gameObject;
                if (hitobj.layer!=8&& hitobj.layer!=9 && hitobj != Charactor)
                    //如果有障碍物，不显示
                {
                    Bar.gameObject.SetActive(false);
                    return ;
                }
            }
            //显示图标
            if (!Bar.gameObject.activeInHierarchy)
            {
                Bar.gameObject.SetActive(true);
            }
            //if (!pointer.activeInHierarchy)
            //{
            //    pointer.SetActive(true);
            //}
            Bar.rectTransform .position= maincam.WorldToScreenPoint(Charactor.transform.position+Vector3.up);
            //pointer.transform.eulerAngles = new Vector3(0,0,
            //    -Mathf.Atan(-(Charactor.transform.position.x- Circle.transform.position.x)/(Charactor.transform.position.z -Circle. transform.position.z)));
            //print(-(Charactor.transform.position.x - Circle.transform.position.x) / (Charactor.transform.position.z - Circle.transform.position.z));
            return;
        }
        Bar.gameObject.SetActive(false);
        //pointer.SetActive(false);
    }
}
