using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Switch : MonoBehaviour {
    #region �ֶ�
    public List<Transform> m_tChildrens;
    /// <summary>
    /// ��Switch�������� ͬһ��ֻ��һ�����Ա�ѡ��
    /// </summary>
    public SwitchGroup m_sGroup;
    [SerializeField]
    private int m_iIndex = -1;
    [SerializeField]
    private int m_iValue = -9;
    /// <summary>
    /// ��ʶһ��״̬ �ص�ʱ�ش� ��ʱ���ڽ�� ���������ͳһ�����ʱ�� ����Զ�ѡ��Ļ�����Ҳ�Ქ��
    /// </summary>
    [HideInInspector]
    public int m_iStatus = 0;
    /// <summary>
    /// �Ƿ������ͬһ��������ѡ�в���Ӧ�¼�
    /// </summary>
    public bool m_bAllowSelectSameOne;
    #endregion

    #region  ����
    public int Index
    {
        get
        {
            return m_iIndex;
        }
        set
        {
            m_iIndex = value;
        }
    }
    public Action<int, int> OnValueChange
    {
        get;
        set;
    }
    public int Value
    {
        get
        {
            return m_iValue;
        }
        set
        {
            if (m_iValue == value)
            {
                //ѡ�����ж� �����ͬһ������ �����ж��Ƿ�����ѡ��ͬһ��
                //���������ѡ���¼�
                if (m_bAllowSelectSameOne)
                {
                    InvokeValueChange();
                }
                return;
            }

            Value2 = value;
        }
    }
    public int Value2
    {
        get
        {
            return m_iValue;
        }
        set
        {
            m_iValue = value;
            //�������е��Ӷ���
            foreach (Transform tf in m_tChildrens)
            {
                tf.SetActive(false);
            }

            //�޶���Χ ���ܳ���
            if (m_iValue >= m_tChildrens.Count)
                m_iValue = m_tChildrens.Count - 1;

            //��ʾѡ�е��Ӷ���
            if (m_iValue >= 0 && m_iValue < m_tChildrens.Count)
            {
                m_tChildrens[m_iValue].SetActive(true);
            }

            InvokeValueChange();
        }
    }

    #endregion

    void Awake () {
        if (m_iValue == -9) Value = 0;
        if (m_sGroup != null)
        {
            m_sGroup.AddItem(this);
        }
        //��ӵ���¼� �������Ϊѡ��״̬
        if (m_tChildrens != null)
        {
            Transform transform;
            for (int i = 0; i < m_tChildrens.Count; ++i)
            {
                transform = m_tChildrens[i];
                //����Ӷ����ǰ�ť ��Ϊÿ����ť��ӵ���¼� 
                //����ť���ʱ�����õ�ǰ�����ť��˳��ֵ����
                if (transform.GetButton() != null)
                {
                    int index = i;
                    transform.AddClickListener(() => {
                        SetValue(index);
                    });
                }
            }
        }
    }
    
    public void SetValue(int value, int status = 0)
    {
        //��ʶ״̬ �ص�ʱ�ش�
        this.m_iStatus = status;

        if (m_sGroup != null)
        {
            m_sGroup.SelectItem = this;
        }
        else
        {
            Value = value;
        }
    }

    public void SetValue2(int value, int status = 0)
    {
        //��ʶ״̬ �ص�ʱ�ش�
        this.m_iStatus = status;

        if (m_sGroup != null)
        {
            m_sGroup.SelectItem2 = this;
        }
        else
        {
            Value2 = value;
        }
    }

    public void InvokeValueChange()
    {
        //��Ӧ�ص�
        if (OnValueChange != null) OnValueChange(m_iValue, m_iStatus);
    }


    /// <summary>
    /// �������е���ʾ����
    /// </summary>
    public void HideChildren()
    {
        //�������е��Ӷ���
        foreach (Transform tf in m_tChildrens)
        {
            tf.SetActive(false);
        }
    }
    /// <summary>
    /// ����ָ������ʾ����
    /// </summary>
    public void HideChild(int index = -1)
    {
        if (index == -1)
        {
            index = m_iValue;
        }
        //����ѡ�е��Ӷ���
        if (index >= 0 && index < m_tChildrens.Count)
        {
            m_tChildrens[index].SetActive(false);
        }
    }
    /// <summary>
    /// ��ʾָ������ʾ����
    /// </summary>
    public void ShowChild(int index = -1)
    {
        if (index == -1)
        {
            index = m_iValue;
        }
        //��ʾѡ�е��Ӷ���
        if (index >= 0 && index < m_tChildrens.Count)
        {
            m_tChildrens[index].SetActive(true);
        }
    }

    /// <summary>
    /// ֱ���л�ѡ��״̬ �������¼�
    /// </summary>
    /// <param name="value"></param>
    public void IsOn(int value)
    {
        m_iValue = value;
        //�������е��Ӷ���
        foreach (Transform tf in m_tChildrens)
        {
            tf.SetActive(false);
        }

        //�޶���Χ ���ܳ���
        if (m_iValue >= m_tChildrens.Count)
            m_iValue = m_tChildrens.Count - 1;

        //��ʾѡ�е��Ӷ���
        if (m_iValue >= 0 && m_iValue < m_tChildrens.Count)
        {
            m_tChildrens[m_iValue].SetActive(true);
        }
    }

    /// <summary>
    /// ��������ʱ����
    /// </summary>
    void OnDestroy()
    {
        OnValueChange = null;
        m_tChildrens = null;
        m_sGroup = null;
    }

}
