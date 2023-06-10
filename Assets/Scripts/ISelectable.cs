using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable
{
    void Select(int i, int j);
    void Highlight(int i, int j);
}
