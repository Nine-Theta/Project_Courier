using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dev_PoolDisplay : MonoBehaviour
{
    public NpcScript Npc;
    public RectTransform UIContentHolder;
    public GameObject ContentObj;
     

    void Update()
    {
        foreach(RectTransform t in UIContentHolder.GetComponentInChildren<RectTransform>())
        {
            Destroy(t.gameObject);
        }
        
        for (int i = 0; i < 6; i++)
        {
            List<ScriptableDialogue> d = Npc._availableDialogues.GetPoolAsList(i);
            foreach (ScriptableDialogue s in d)
            {
                GameObject c = Instantiate(ContentObj);
                c.transform.SetParent(UIContentHolder);
                //c.transform.localPosition = ContentObj.transform.position;
                if(i == 5)
                {
                    c.GetComponentInChildren<TextMeshProUGUI>().text = "Backlog" + " => " + s.name;
                }
                else
                {
                    c.GetComponentInChildren<TextMeshProUGUI>().text = s.Priority + " => " + s.name;
                }
            }
        }
    }
}
