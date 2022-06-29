using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PingPong : MonoBehaviour
{
    public bool isHorizontal = true;
    [SerializeField]private float m_time = 0.5f;
    Transform m_trans;
    [SerializeField]private Transform m_target;
    // Use this for initialization
    void Start()
    {
        m_trans = transform;
        MyPingPong(m_trans.localPosition, m_target.localPosition);
    }

    private void MyPingPong(Vector3 from, Vector3 to)
    {
        if (isHorizontal)
        {
            m_trans.DOLocalMoveX(to.x, m_time).OnComplete(() => MyPingPong(to, from));
        }
        else
        {
            m_trans.DOLocalMoveY(to.y, m_time).OnComplete(() => MyPingPong(to, from));
        }
      
    }

}

