using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class RedBlink : MonoBehaviour
{
    private Renderer m_Renderer;

    private Color originalColor;

    private float currentR;

    private bool Back = true;

    private Transform[] transforms = null;
    private Color color = Color.red;
    private void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        // 通过递归来实现变色，因为基本的物件都是最低的层级
        transforms = GetComponentsInChildren<Transform>();
        if (m_Renderer == null)
        {
            
            for(int i =1;i<transforms.Length;i++)
            {
                
                transforms[i].AddComponent<RedBlink>();
            }
            Destroy(gameObject.GetComponent<RedBlink>());
        }
        else 
            originalColor = m_Renderer.material.color;
        if(transforms.Length >=2)
        {
            for (int i = 1; i < transforms.Length; i++)
            {
                transforms[i].AddComponent<RedBlink>();
            }
        }

    }

    private void Update()
    {
        m_Renderer.material.color = Color.Lerp(color, originalColor, Mathf.PingPong(Time.time, 1));
    }

    public void EndRedBlink()
    {
        m_Renderer.material.color = originalColor;
        Debug.Log(gameObject.name);
        if(transforms.Length>=2)
        {
            for (int i = 1; i < transforms.Length; i++)
            {
                transforms[i].GetComponent<RedBlink>().EndRedBlink();
            }
        }
        Destroy(gameObject.GetComponent<RedBlink>());
    }

}