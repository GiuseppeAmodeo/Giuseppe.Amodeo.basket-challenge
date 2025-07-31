using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    [SerializeField]
    private ForceFieldMode forceFieldMode;

    [SerializeField]
    private float force = 5.0f;

    [SerializeField]
    private float radius = 0.3f;

    [SerializeField]
    private LayerMask layerMask;

    private void FixedUpdate()
    {
        Collider[] array = Physics.OverlapSphere(base.transform.position, this.radius, this.layerMask);
        for (int i = 0; i < array.Length; i++)
        {
            Rigidbody component = array[i].GetComponent<Rigidbody>();
            if (component != null)
            {
                Vector3 a = Vector3.zero;
                ForceFieldMode forceFieldMode = this.forceFieldMode;
                if (forceFieldMode != ForceFieldMode.Attractive)
                {
                    if (forceFieldMode == ForceFieldMode.Repulsive)
                    {
                        a = array[i].gameObject.transform.position - base.transform.position;
                    }
                }
                else
                {
                    a = base.transform.position - array[i].gameObject.transform.position;
                }
                component.AddForce(a * this.force);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = (this.forceFieldMode == ForceFieldMode.Attractive) ? Color.red : Color.green;
        Gizmos.DrawWireSphere(base.transform.position, this.radius);
    }
}
