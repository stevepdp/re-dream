using System.Collections;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] float warnTimeout;
    [SerializeField] float hideTimeout;
    [SerializeField] float showTimeout;

    Color platformColourDefault;
    MeshCollider meshCollider;
    MeshRenderer meshRenderer;
    Renderer rend;

    void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
        rend = GetComponent<Renderer>();

        if (meshRenderer != null) platformColourDefault = meshRenderer.material.color;
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(hideTimeout);

        if (meshCollider != null) meshCollider.enabled = false;
        if (rend != null) rend.enabled = false;

        StartCoroutine(Show());
    }

    IEnumerator Show()
    {
        yield return new WaitForSeconds(showTimeout);

        if (meshRenderer != null) meshRenderer.material.color = platformColourDefault;
        if (meshCollider != null) meshCollider.enabled = true;
        if (rend != null) rend.enabled = true;
    }

    IEnumerator Warn()
    {
        yield return new WaitForSeconds(warnTimeout);

        if (meshRenderer != null) meshRenderer.material.color = Color.red;

        StartCoroutine(Hide());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Warn());
        }
    }
}