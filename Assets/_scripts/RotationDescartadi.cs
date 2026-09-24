using UnityEngine;

public class Rotation : MonoBehaviour
{




    //No puedo usar un trasnform rotate porque los hijos heredan el movimiento de padres, así que padres arrastran a los hijos

      public Vector3 rotationSpeed = new Vector3(0f,15f,0f);

        //Pongo en el update la rotación porque es algo eterno
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, Space.Self);
    }
}
