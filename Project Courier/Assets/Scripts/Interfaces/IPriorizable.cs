using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PriorityLevel { Critical, Important, Preferred, Normal, Low }
public interface IPriorizable
{
    public PriorityLevel Priority { get; set; } 

}
