using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SwitchGroup : MonoBehaviour {
    #region �ֶ�
    /// <summary>
    /// Switch����
    /// </summary>
    private List<Switch> m_sItems = new List<Switch>();
    /// <summary>
    /// ��ǰѡ����
    /// </summary>
    private Switch m_sSelectItem;
    /// <summary>
    /// �Ƿ���Startʱ�򿪵�һ��ѡ�� Ĭ��Ϊtrue
    /// </summary>
    [SerializeField]
    private bool m_bStartDefault = true;
    /// <summary>
    /// �Ƿ������ͬһ��������ѡ�в���Ӧ�¼�
    /// </summary>
    public bool m_bAllowSelectSameOne;
    #endregion
    #region  ����
    public Action<int> OnValueChange
    {
        get;
        set;
    }
    #endregion
    void Start()
    {
        if (m_bStartDefault && m_sItems != null)
        {
            //Ĭ��ѡ�е�һ��
            foreach (Switch item in m_sItems)
            {
                if (item.Index == 0)
                {
                    item.SetValue(1, 1);
                    break;
                }
            }
        }
    }
    /// <summary>
    /// ��һ��Switch��ӵ�������
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(Switch item)
    {
        m_sItems.Add(item);
    }

    public Switch SelectItem
    {
        get
        {
            return m_sSelectItem;
        }
        set
        {
            if (m_sSelectItem == value)
            {
                //�������ѡ��ͬһ�� ��ѡ��ʱ
                //ֻ�����ѡ���¼�
                if (m_bAllowSelectSameOne)
                {
                    m_sSelectItem.InvokeValueChange();
                    InvokeValueChange();
                }
                else
                {
                    //���SwitchGroup������ѡ��ͬһ��
                    //�����Switch�� �ж�
                    //����Valueʱ �����ͬ����赲
                    m_sSelectItem.Value = 1;
                }
                return;
            }

            SelectItem2 = value;
        } 
    }

    public Switch SelectItem2
    {
        get
        {
            return m_sSelectItem;
        }
        set
        {
            m_sSelectItem = value;

            foreach (Switch item in m_sItems)
                item.Value = 0;

            m_sSelectItem.Value = 1;

            InvokeValueChange();
        }
    }

    public void InvokeValueChange()
    {
        //��Ӧ�ص�
        if (OnValueChange != null)
        {
            OnValueChange(m_sSelectItem.Index);
        }
    }

    /// <summary>
    /// ��������ʱ����
    /// </summary>
    void OnDestroy()
    {
        OnValueChange = null;
        m_sItems = null;
        m_sSelectItem = null;
    }
}
