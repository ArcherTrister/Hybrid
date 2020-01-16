﻿// -----------------------------------------------------------------------
//  <copyright file="Singleton.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2014-07-18 17:55</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Hybrid.Data
{
    /// <summary>
    /// 定义一个指定类型的单例，该实例的生命周期将跟随整个应用程序。
    /// </summary>
    /// <typeparam name="T">要创建单例的类型。</typeparam>
    public class Singleton<T> : Singleton
    {
        private static T _instance;

        /// <summary>
        /// 获取指定类型的单例实例
        /// </summary>
        public static T Instance
        {
            get { return _instance; }
            set
            {
                _instance = value;
                AllSingletons[typeof(T)] = value;
            }
        }
    }

    /// <summary>
    /// 提供一个字典容器，按类型装载所有<see cref="Singleton{T}"/>的单例实例
    /// </summary>
    public class Singleton
    {
        static Singleton()
        {
            if (AllSingletons == null)
            {
                AllSingletons = new Dictionary<Type, object>();
            }
        }

        /// <summary>
        /// 获取 单例对象字典
        /// </summary>
        public static IDictionary<Type, object> AllSingletons { get; }
    }
}