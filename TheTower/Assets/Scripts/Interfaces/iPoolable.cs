using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iPoolable
{
    void OnUnpool();

    void OnPool();
}
