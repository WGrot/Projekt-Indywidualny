using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectStateDetector : MonoBehaviour
{
	public UnityEvent onDisable;
	private void OnDisable()
	{
		onDisable?.Invoke();
	}

}
