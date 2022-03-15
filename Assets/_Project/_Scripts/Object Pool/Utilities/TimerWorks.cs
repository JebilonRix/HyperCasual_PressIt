using System.Collections;
using UnityEngine;

namespace PressIt
{
    public class TimerWorks : MonoBehaviour
    {
        public delegate void TimerMethod();
        public TimerMethod _timerMethod;

        [SerializeField] bool _isDebug = false;

        private float _targetSeconds;
        private float _counter = 0;
        private bool _isStart;
        private bool _isLoop;

        public bool IsStart { get => _isStart; private set => _isStart = value; }

        private void Update()
        {
            if (!IsStart)
            {
                return;
            }

            if (_counter >= _targetSeconds)
            {
                if (_isLoop)
                {
                    if (_timerMethod == null)
                    {
                        return;
                    }

                    _timerMethod();
                    _counter = 0;
                }
                else
                {
                    StopTimer(_timerMethod);
                }
            }
            else
            {
                _counter += Time.deltaTime;
            }
        }

        public void StartTimer(TimerMethod voidMethod, float seconds, bool isLoop)
        {
            IsStart = true;
            _targetSeconds = seconds;
            _isLoop = isLoop;
            _timerMethod += voidMethod;

            Log("Timer is started");
        }
        public void StartTimer(TimerMethod voidMethod, float seconds, bool isLoop, float delaySeconds)
        {
            ResetTimer();
            StartCoroutine(WaitDelay(voidMethod, seconds, isLoop, delaySeconds));
        }
        public void StopTimer(TimerMethod method)
        {
            IsStart = false;
            _counter = 0;
            _timerMethod -= method;

            Log("Timer is stoped.");
        }
        public void ResetTimer()
        {
            IsStart = true;
            _counter = 0;
        }
        private IEnumerator WaitDelay(TimerMethod voidMethod, float seconds, bool isLoop, float delaySeconds)
        {
            yield return new WaitForSeconds(delaySeconds);
            StartTimer(voidMethod, seconds, isLoop);

            Log("delay ends");
        }
        private void Log(string message)
        {
            if (_isDebug)
            {
                Debug.Log(gameObject.name + " " + message);
            }
        }
    }
}