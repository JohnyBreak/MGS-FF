using System;
using System.Collections;
using System.Collections.Generic;
using DamageSystem;
using UnityEngine;

namespace SurveillanceCameraSystem
{
    public class StateMachine : IDisposable
    {
        private readonly Dictionary<int, IState> _statesMap = new();
        private readonly Action _onTargetSpotted;
        private readonly Action _onTargetLost;
        private readonly SurveillanceCameraView _view;
        private Transform _target;
        private Detection _detection;
        private Coroutine _resetRoutine;
        private IState _currentState;
        private DamageComposite _damageComposite;
        
        public StateMachine(
            SurveillanceCameraView view,
            Action onTargetSpotted,
            Action onTargetLost,
            float angle,
            float followSpeed,
            float patrolTime)
        {
            _view = view;
            _onTargetSpotted = onTargetSpotted;
            _onTargetLost = onTargetLost;
            _detection = new Detection(_view.LayerMask, _view.Collider, OnTarget);

            _damageComposite = new(view.transform, OnDamage);
            var follow = new FollowState(_view.Rotator, angle, followSpeed, GetTarget);
            var patrol = new PatrolState(_view.Rotator, patrolTime, angle);
            var broken = new BrokenState(_view.Rotator, _damageComposite);
            
            _statesMap[follow.GetKey()] = follow;
            _statesMap[patrol.GetKey()] = patrol;
            _statesMap[broken.GetKey()] = broken;
            
            ChangeToState(patrol.GetKey());
        }
        
        private void OnTarget(Transform target)
        {
            _target = target;
            if (_resetRoutine != null)
            {
                _view.StopCoroutine(_resetRoutine);
            }
            
            if (target == null)
            {
                _onTargetLost?.Invoke();
                _resetRoutine = _view.StartCoroutine(ResetRoutine());
                return;
            }
            
            ChangeToState(SCStateKeys.Follow);
            _onTargetSpotted?.Invoke();
        }

        private void ChangeToState(int stateKey)
        {
            if (!_statesMap.ContainsKey(stateKey))
            {
                return;
            }

            _currentState?.Exit();

            _currentState = _statesMap[stateKey];
            
            _currentState.Enter();
        }
        
        private void OnDamage(IDamage damage)
        {
            ChangeToState(SCStateKeys.Broken);
        }
        
        private IEnumerator ResetRoutine()
        {
            yield return new WaitForSeconds(3);
            
            ChangeToState(SCStateKeys.Patrol);
        }
        
        private Transform GetTarget()
        {
            return _target;
        }

        public void Dispose()
        {
            _detection?.Dispose();
            
            _damageComposite?.Dispose();
            
            foreach (var state in _statesMap.Values)
            {
                state?.Dispose();
            }
        }
    }
}