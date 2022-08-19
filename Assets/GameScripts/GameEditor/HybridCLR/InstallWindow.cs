using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor ;
using UnityEngine;
 

namespace HybridCLR
{
    public class InstallWindow : EditorWindow
    {
        private InstallController m_Controller;

        [MenuItem("HybridCLR/Install", false, 0)]
        public static void Open()
        {
            InstallWindow window = GetWindow<InstallWindow>("HybridCLR Builder", true);
            window.minSize = new Vector2(800f, 500f);
        }

        private void OnEnable()
        {
            m_Controller = new InstallController();
        }

        private void OnGUI()
        {
            GUILayout.Space(5f);
            EditorGUILayout.LabelField("Install HybridCLR��", EditorStyles.boldLabel);
            //EditorGUILayout.BeginVertical("box");
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField($"Unity version: {Application.unityVersion}", EditorStyles.boldLabel);
            EditorGUILayout.LabelField($"il2cpp_plus branch: {m_Controller.Il2CppBranch}", EditorStyles.boldLabel);
            GUISelectUnityDirectory("il2cpp��װ·��", "Select");
            GUIItem("��ʼ��HybridCLR�ֿⲢ��װ��������Ŀ��", "Install", InitHybridCLR);
            EditorGUILayout.EndVertical();
        }

        private void GUIItem(string content, string button, Action onClick)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(content);
            if (GUILayout.Button(button, GUILayout.Width(100)))
            {
                onClick?.Invoke();
                GUIUtility.ExitGUI();
            }
            EditorGUILayout.EndHorizontal();
            
        }


        private void GUISelectUnityDirectory(string content, string selectButton)
        {
            EditorGUILayout.BeginHorizontal();
            string il2cppInstallDirectory = m_Controller.Il2CppInstallDirectory = EditorGUILayout.TextField(content, m_Controller.Il2CppInstallDirectory);
            if (GUILayout.Button(selectButton, GUILayout.Width(100)))
            {
                string temp = EditorUtility.OpenFolderPanel(content, m_Controller.Il2CppInstallDirectory, string.Empty);
                if (!string.IsNullOrEmpty(temp))
                {
                    m_Controller.Il2CppInstallDirectory = temp;
                }
            }
            EditorGUILayout.EndHorizontal();

            InstallErrorCode err = m_Controller.CheckValidIl2CppInstallDirectory(m_Controller.Il2CppBranch, il2cppInstallDirectory);
            switch (err)
            {
                case InstallErrorCode.Ok:
                    {
                        if (!il2cppInstallDirectory.Contains(m_Controller.Il2CppBranch))
                        {
                            EditorGUILayout.HelpBox($"li2cpp ·��δ���� '{m_Controller.Il2CppBranch}',��ȷ��ѡ���� {m_Controller.Il2CppBranch} �汾�İ�װĿ¼ ", MessageType.Warning);
                        }
                        break;
                    }
                case InstallErrorCode.Il2CppInstallPathNotExists:
                    {
                        EditorGUILayout.HelpBox("li2cpp ·��������", MessageType.Error);
                        break;
                    }
                case InstallErrorCode.Il2CppInstallPathNotMatchIl2CppBranch:
                    {
                        EditorGUILayout.HelpBox($"il2cpp �汾��ƥ�䣬����Ϊ {m_Controller.Il2CppBranch} �汾��ӦĿ¼", MessageType.Error);
                        break;
                    }
            }
        }

        private void InitHybridCLR()
        {
            m_Controller.InitHybridCLR(m_Controller.Il2CppBranch, m_Controller.Il2CppInstallDirectory);
        }
    }
}
