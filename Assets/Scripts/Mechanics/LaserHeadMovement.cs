using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LaserHeadMovement : MonoBehaviour
{
    public enum RadiationTypes {Gamma, Electron, Proton}
    
    public float Rotation;
    public RadiationTypes RadiationType { get; set; }
    public float[] Intensities = new float[]{0 , 2, 4, 6, 100};

    public GameObject Radioation;
    public GameObject RadiationPivot;
    public int IntensityId { get; private set; }
    public float Intensity{
        get{return Intensities[ IntensityId];}
    }
    private float sqrt2 = Mathf.Sqrt(2);


    // Start is called before the first frame update
    void Start()
    {
        Update();
        GameObject rad = GameObject.Instantiate(Radioation, RadiationPivot.transform.position, transform.rotation, transform);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 psc = transform.parent.lossyScale;
        transform.localPosition = new Vector3(
            Mathf.Clamp(Mathf.Sin(Rotation) * sqrt2 * 1, -1, 1) ,
            Mathf.Clamp(Mathf.Cos(Rotation) * sqrt2 , -1, 1)
        );
        float crot = Mathf.Atan2(-transform.localPosition.y, -transform.localPosition.x );
        transform.rotation = Quaternion.Euler(0, 0, crot * Mathf.Rad2Deg + 90);
    }

    public void Rotate(float f){
        Rotation += f;
    }

    public void SwitchIntensity(int i){
        IntensityId = Mathf.Clamp(IntensityId + i, 0, Intensities.Length-1);
    }
}
