/****************************************************************************
 * Copyright (c) 2015 ~ 2022 liangxiegame UNDER MIT LICENSE
 * 
 * http://qframework.io
 * https://github.com/liangxiegame/QFramework
 * https://gitee.com/liangxiegame/QFramework
 ****************************************************************************/

using System;
using UnityEditor;
using UnityEngine;

namespace QFramework
{
    public class EditorPrefsBoolProperty : BindableProperty<bool>
    {
        private string mKey;

        public EditorPrefsBoolProperty(string key, bool initValue = false)
        {
            mKey = key;

            // 初始化
            mValue = EditorPrefs.GetBool(key, initValue);
            
            RegisterWithInitValue(value =>
            {
                EditorPrefs.SetBool(mKey, value);
            });
        }
    }

    public class LocaleKitEditor
    {
        /// <summary>
        /// 快捷访问 减少代码量
        /// </summary>


        private static Lazy<EditorPrefsBoolProperty> mIsCN =
            new Lazy<EditorPrefsBoolProperty>(() => new EditorPrefsBoolProperty("EDITOR_CN", true));

        public static IBindableProperty<bool> IsCN => mIsCN.Value;

        private static ILabel mENLabel;

        private static Lazy<IMGUIView> mSwitchToggleView = new Lazy<IMGUIView>(() =>
        {
            var languageToggle = EasyIMGUI.Toggle()
                .IsOn(() => !IsCN.Value);
            languageToggle.ValueProperty.Register(b => { IsCN.Value = !b; });

            mENLabel = EasyIMGUI.Label()
                .Text("EN");

            return EasyIMGUI.Custom().OnGUI(() =>
            {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                mENLabel.DrawGUI();
                languageToggle.DrawGUI();
                GUILayout.EndHorizontal();
            });
        });

        public static void DrawSwitchToggle(Color fontColor)
        {
            mENLabel?.ChangeFontColor(fontColor);
            mSwitchToggleView.Value.DrawGUI();
        }
    }
}