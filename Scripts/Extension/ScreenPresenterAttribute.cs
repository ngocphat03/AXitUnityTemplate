﻿namespace UITemplate.Scripts.Extension
{
    using System;
    using AXitUnityTemplate.ScreenTemplate.Scripts.Interface;
    using UITemplate.Scripts.Extension.Ulties;
    using UnityEngine;
    using Zenject;

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ScreenPresenterAttribute : Attribute
    {
        private Type ScreenPresenterType { get; }

        public ScreenPresenterAttribute(Type screenPresenterType)
        {
            if (!typeof(IScreenPresenter).IsAssignableFrom(screenPresenterType))
            {
                Debug.LogError($"Can't get presenter with type: {screenPresenterType}");
            }
            this.ScreenPresenterType = screenPresenterType;
        }

        public IScreenPresenter ScreenPresenter()
        {
            if (!typeof(IScreenPresenter).IsAssignableFrom(this.ScreenPresenterType))
            {
                Debug.LogError($"Can't get presenter with type: {this.ScreenPresenterType}");

                return null;
            } 
            
            var screenPresenterInstance = this.GetCurrentContainer().Instantiate(this.ScreenPresenterType);
            return (IScreenPresenter)screenPresenterInstance;
        }
    }
}