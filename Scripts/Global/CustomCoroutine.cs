using System.Collections;
using UnityEngine;

public class CustomCoroutine 
{
    private MonoBehaviour _host;
    private Coroutine _coroutine;
    private IEnumerator _routine;

    private bool _isProcessing => _coroutine != null;

    public CustomCoroutine(MonoBehaviour host, IEnumerator routine)
    {
        _host = host;
        _routine = routine;
    }

    public void Start()
    {
        Stop();

        _coroutine = _host.StartCoroutine(_routine);
    }

    public void Stop()
    {
        if (_isProcessing)
        {
            _host.StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}
