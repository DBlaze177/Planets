using System;
using Unity.VisualScripting;
using UnityEngine;

public class RotationStarPlanetMoon : MonoBehaviour
{
    
    
    
    public float rotationSpeed = 30f;
    public float axisInc = 0f;
    public float orbitSpeed = 15f;
    private float orbitRad;
    private float orbitAngle;
    private float angleRot;
    
    
    
    
    
    void Start()
    {
        
        if (transform.parent != null)
        {
            Vector3 offsetInicial = transform.position - transform.parent.position;
            orbitRad = new Vector2(offsetInicial.x, offsetInicial.z).magnitude;
            
            if (orbitRad > 0.001f)
            {
                orbitAngle = Mathf.Atan2(offsetInicial.z, offsetInicial.x) * Mathf.Rad2Deg;
            }

            
        }



    }

    // Update is called once per frame
    void Update()

    {
            
            
            
            //Llamo al método para ver las drawline
            
            VisualizarVectoresDebug();





        //Movimiento de traslación

        if(transform.parent != null && orbitRad > 0.001f)
        {
            orbitAngle += orbitSpeed * Time.deltaTime;
            float rad = orbitAngle * Mathf.Deg2Rad;

                //ahora se calcula la posición respecto a la posición actual del padre en el mundo
                Vector3 relativePosition = new Vector3(MathF.Cos(rad), 0f, Mathf.Sin(rad)) * orbitRad;
                Vector3 newPosition = transform.parent.position + relativePosition;
                newPosition.y = transform.parent.position.y;

                transform.position = newPosition;



        }

        //Movimiento de rotación   

        angleRot += rotationSpeed * Time.deltaTime;   
        //Asigno transform.rotation de manera global para eliminar el arrastre del padre
        Quaternion inclination = Quaternion.Euler(0f,0f,axisInc);
        Quaternion axialRotation = Quaternion.Euler(0f, angleRot, 0f);
        transform.rotation = inclination * axialRotation;



    }

        private void VisualizarVectoresDebug()
    {

        //aquí veo el eje de rotación
        float axisSize = 3f;
        Debug.DrawLine(transform.position, transform.up * axisSize, Color.green); //PoloN
        Debug.DrawLine(transform.position, -transform.up * axisSize, Color.green); //PoloS

        //El sol no orbita a nadie, no se calcula el vector orbital
        if (transform.parent ==null || orbitRad <= 0.001f) return;


        //Vector orbital

        Vector3 directionToCenter = (transform.parent.position - transform.position).normalized;
        Debug.DrawRay(transform.position, directionToCenter * 2f, Color.red);



        //Vector de movimiento; hacia dónde va


        float rad = orbitAngle * Mathf.Deg2Rad;
        float sentido = Mathf.Sign(orbitSpeed);
    Vector3 direccionTangencial = new Vector3(-Mathf.Sin(rad), 0f, Mathf.Cos(rad)) * sentido;

    Debug.DrawRay(transform.position, direccionTangencial * 2.5f, Color.cyan);







        
    }





}
