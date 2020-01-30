using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using System;
using System.Reflection;
using System.Linq;
using System.IO;

public class ModifyEditorStyle
{
    //
    // Constant
    // - - - - - - - - - - - - - - - - - - - -

    // Built-in default font name.
    public const string DefaultFontName = "MS PGothic";
    //public const string WindownFontName_Jp = "MS PGothic";
    //public const string WindownFontName_En = "Segoe UI";

    public static readonly string[] AvailableFontList = new string[]
    {
        "Lucida Grande",
        DefaultFontName,
        "MS UI Gothic",
    };

    //
    // Props
    // - - - - - - - - - - - - - - - - - - - -

    // --- Settisgs ---

    public static bool FontChangeIsEnabled
    {
        get
        {
            return EditorPrefs.GetBool("ModifyEditorStyle_FontChangeIsEnabled", true);
        }
        set
        {
            EditorPrefs.SetBool("ModifyEditorStyle_FontChangeIsEnabled", value);
        }
    }

    public static int FontSize
    {
        get
        {
            return EditorPrefs.GetInt("ModifyEditorStyle_FontSize", 15);
        }
        set
        {
            EditorPrefs.SetInt("ModifyEditorStyle_FontSize", value);
        }
    }

    public static int SmallFontSize
    {
        get
        {
            return EditorPrefs.GetInt("ModifyEditorStyle_SmallFontSize", 14);
        }
        set
        {
            EditorPrefs.SetInt("ModifyEditorStyle_SmallFontSize", value);
        }
    }

    public static int BigFontSize
    {
        get
        {
            return EditorPrefs.GetInt("ModifyEditorStyle_BigFontSize", 16);
        }
        set
        {
            EditorPrefs.SetInt("ModifyEditorStyle_BigFontSize", value);
        }
    }

    public static int PaddingTop
    {
        get
        {
            return EditorPrefs.GetInt("ModifyEditorStyle_PaddingTop", 1);
        }
        set
        {
            EditorPrefs.SetInt("ModifyEditorStyle_PaddingTop", value);
        }
    }

    public static int PaddingBottom
    {
        get
        {
            return EditorPrefs.GetInt("ModifyEditorStyle_PaddingBottom", 2);
        }
        set
        {
            EditorPrefs.SetInt("ModifyEditorStyle_PaddingBottom", value);
        }
    }

    public static int Selected
    {
        get
        {
            string fontName = EditorPrefs.GetString("ModifyEditorStyle_Selected", DefaultFontName);
            return Array.IndexOf(Fonts, fontName);
        }
        set
        {
            EditorPrefs.SetString("ModifyEditorStyle_Selected", (value < Fonts.Length && value >= 0) ? Fonts[value] : DefaultFontName);
        }
    }

    public static int SelectedBold
    {
        get
        {
            string fontName = EditorPrefs.GetString("ModifyEditorStyle_SelectedBold", DefaultFontName);
            return Array.IndexOf(Fonts, fontName);
        }
        set
        {
            EditorPrefs.SetString("ModifyEditorStyle_SelectedBold", (value < Fonts.Length && value >= 0) ? Fonts[value] : DefaultFontName);
        }
    }

    public static string[] Fonts
    {
        get
        {
            return AvailableFontList;
        }
    }

    // --- Styles ---

    public static IEnumerable<GUIStyle> GUISkinStyles
    {
        get
        {
            GUISkin skin = GUI.skin;
            yield return skin.label;
            yield return skin.button;
            yield return skin.textArea;
            yield return skin.textField;
        }
    }

    public static IEnumerable<GUIStyle> EditorStylesGUIStyles
    {
        get
        {
            GUISkin skin = GUI.skin;
            yield return skin.label;
            yield return skin.textArea;
            yield return skin.textField;

            yield return EditorStyles.colorField;
            yield return EditorStyles.foldout;
            yield return EditorStyles.foldoutPreDrop;
            yield return EditorStyles.label;
            yield return EditorStyles.numberField; //textField
            yield return EditorStyles.objectField;
            yield return EditorStyles.objectFieldMiniThumb;
            yield return EditorStyles.radioButton;
            yield return EditorStyles.textArea; //textField
            yield return EditorStyles.textField; //textField
            yield return EditorStyles.toggle;
            yield return EditorStyles.whiteLabel;
            yield return EditorStyles.wordWrappedLabel;
            yield return EditorStyles.toolbarSearchField;
        }
    }

    public static IEnumerable<GUIStyle> GetNeedsPaddingStyles
    {
        get
        {
            GUISkin skin = GUI.skin;
            yield return skin.label;
            yield return skin.textArea;
            yield return skin.textField;
            yield return EditorStyles.foldout;
            yield return EditorStyles.foldoutPreDrop;
            yield return EditorStyles.label;
            yield return EditorStyles.textArea; //textField
            yield return EditorStyles.textField; //textField
            yield return EditorStyles.numberField; //textField

            yield return GUI.skin.FindStyle("TV Line");
            yield return GUI.skin.FindStyle("TV Insertion");
            yield return GUI.skin.FindStyle("TV Ping");
            yield return GUI.skin.FindStyle("TV Selection");

            //Styles in older version
            yield return GUI.skin.FindStyle("IN Foldout");
            yield return GUI.skin.FindStyle("PR Insertion");
            yield return GUI.skin.FindStyle("PR Label");
        }
    }

    public static IEnumerable<GUIStyle> EditorStylesBold
    {
        get
        {
            yield return EditorStyles.boldLabel;
            yield return EditorStyles.toggleGroup; //BoldToggle
            yield return EditorStyles.whiteBoldLabel;

            //Internal style
            yield return GUI.skin.FindStyle("TV LineBold");
        }

    }

    public static IEnumerable<GUIStyle> EditorStylesBig
    {
        get
        {
            yield return EditorStyles.largeLabel;
            yield return EditorStyles.whiteLargeLabel;
        }
    }

    public static IEnumerable<GUIStyle> EditorStylesSmall
    {
        get
        {
            yield return EditorStyles.centeredGreyMiniLabel; //Same as miniLabel
            yield return EditorStyles.helpBox;
            yield return EditorStyles.layerMaskField; //MiniPopup
            yield return EditorStyles.miniBoldLabel;
            yield return EditorStyles.miniButton;
            yield return EditorStyles.miniButtonLeft;
            yield return EditorStyles.miniButtonMid;
            yield return EditorStyles.miniButtonRight;
            yield return EditorStyles.miniLabel;
            yield return EditorStyles.miniTextField;
            yield return EditorStyles.objectFieldThumb;
            yield return EditorStyles.popup; //MiniPopup
            yield return EditorStyles.toolbar;
            yield return EditorStyles.toolbarButton;
            yield return EditorStyles.toolbarDropDown;
            yield return EditorStyles.toolbarPopup;
            yield return EditorStyles.toolbarTextField;
            yield return EditorStyles.whiteMiniLabel;
            yield return EditorStyles.wordWrappedMiniLabel;
            yield return EditorStyles.miniPullDown;

            //Internal styles
            yield return GUI.skin.FindStyle("GV Gizmo DropDown");
        }
    }

    // You can comment out what you don't wanna change
    public static IEnumerable<GUIStyle> InternalStyles
    {
        get
        {
            yield return GUI.skin.FindStyle("TV Line");
            yield return GUI.skin.FindStyle("TV Insertion");
            yield return GUI.skin.FindStyle("TV Ping");
            yield return GUI.skin.FindStyle("TV Selection");

            //Styles in older version
            yield return GUI.skin.FindStyle("IN Foldout");
            yield return GUI.skin.FindStyle("PR Insertion");
            yield return GUI.skin.FindStyle("PR Label");
        }
    }

    //
    // Menu Layout
    // - - - - - - - - - - - - - - - - - - - -

    [SettingsProvider]
    public static SettingsProvider ModifyEditorStyleSettingsProvider() => new ModifyEditorStyleProvider("Preferences/Modify Editor Style for Win");

    public class ModifyEditorStyleProvider : SettingsProvider
    {
        public ModifyEditorStyleProvider(string path, SettingsScope scopes = SettingsScope.User) : base(path, scopes) { }

        public override void OnGUI(string searchContext)
        {
            ModifyEditorStylePreference();
        }
    }

    public static void ModifyEditorStylePreference()
    {
        EditorGUILayout.HelpBox("Changing the font size works but unfortunately the line height used in various drawers was baked as a const 16, we could not change it as a const was baked throughout the compiled Unity source code. (The enlarged characters with hanging part like 'g' will clip.)\n\nAlso, some parts seems to not change immediately until you recompile something.", MessageType.Info);

        FontChangeIsEnabled = EditorGUILayout.BeginToggleGroup("Enable", FontChangeIsEnabled);

        Selected = EditorGUILayout.Popup("Font", Selected, Fonts);
        SelectedBold = EditorGUILayout.Popup("Bold Font", SelectedBold, Fonts);
        EditorGUILayout.Space();

        EditorGUILayout.HelpBox("Please do no change. These are reference values.", MessageType.Warning);

        FontSize = EditorGUILayout.IntField("Font Size", FontSize);
        SmallFontSize = EditorGUILayout.IntField("Small Font Size", SmallFontSize);
        BigFontSize = EditorGUILayout.IntField("Big Font Size", BigFontSize);
        //EditorGUILayout.Space();
        //EditorGUILayout.HelpBox("Applies custom paddings to certain UI", MessageType.Info);
        //PaddingTop = EditorGUILayout.IntField("Padding Top", PaddingTop);
        //PaddingBottom = EditorGUILayout.IntField("Padding Bottom", PaddingBottom);

        if (GUILayout.Button("Modify"))
        {
            IsExecModify = true;
            Modify();
        }

        EditorGUILayout.EndToggleGroup();
    }

    // These statics are cleared out so often including just on loading a new scene.. it makes the linked font disappear
    public static Font Font_Normal;
    public static Font Font_Big;
    public static Font Font_Small;
    public static Font Font_Bold;
    public static Font Fomt_SmaollBold;

    public static void Modify()
    {
        if (!FontChangeIsEnabled)
        {
            StyleManager.WriteStylesAll();
            return;
        }

        if (!IsExecModify)
        {
            return;
        }
        IsExecModify = false;

        string fontName = Selected >= 0 && Selected < Fonts.Length ? Fonts[Selected] : DefaultFontName;
        string boldFontName = SelectedBold >= 0 && SelectedBold < Fonts.Length ? Fonts[SelectedBold] : DefaultFontName;

        Font_Normal = Font.CreateDynamicFontFromOSFont(fontName, FontSize);
        Font_Big = Font.CreateDynamicFontFromOSFont(fontName, BigFontSize);
        Font_Small = Font.CreateDynamicFontFromOSFont(fontName, SmallFontSize);
        Font_Bold = Font.CreateDynamicFontFromOSFont(boldFontName, FontSize);
        Fomt_SmaollBold = Font.CreateDynamicFontFromOSFont(boldFontName, SmallFontSize);

        //GUISkin skin = GUI.skin;
        //Debug.Log($"- : {skin.font?.name} {skin.font?.fontSize}");
        //skin.font = Font_Normal;
        //GUI.skin = skin; //SetDefaultFont activated on this setter


        EditorStyles style = null;
        //EditorStyles static was pulled from s_Current which was populated from `EditorGUIUtility.GetBuiltinSkin` which we cannot interfere.
        //s_Current is internal and therefore we need to reflect to change the font. All other styles are accessible except the fonts.
        //Type eType = typeof(EditorStyles);

        //Debug.Log(EditorStyles.standardFont.name);

        //var es = (EditorStyles)(eType.GetField("s_Current", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null));
        //eType.GetField("m_StandardFont", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(es, Font_Normal);
        //eType.GetField("m_BoldFont", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(es, Font_Bold);
        //eType.GetField("m_MiniFont", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(es, Font_Small);
        //eType.GetField("m_MiniBoldFont", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(es, Fomt_SmaollBold);

        //We should not override font where there's no font in the first place, because that will make the fallback switch
        //to bold on override not working.

        AutoFontFixFunction.UpdateAll();

        // foreach (var z in GUISkinStyles)
        // {
        //     Debug.Log($"{z.name} : {z.font?.name} {z.font?.fontSize} {z.fontSize} {z.padding}");
        // }

        //foreach (GUIStyle x in EditorStylesGUIStyles)
        //{
        //if (x != null)
        //{
        //    if (x.font != null)
        //    {
        //        Debug.Log($"{x.name} : {x.font.name} {x.font.fontSize} {x.fontSize} {x.padding}");
        //    }
        //    else
        //    {
        //        Debug.Log($"{x.name} : NO FONT {x.fontSize} {x.padding}");
        //    }
        //}

        //x.fontSize = 15;
        //x.fixedHeight = 20;
        //x.padding.top = 4;
        //x.padding.bottom = 5;

        //RectOffset margin = x.margin;
        ////margin.left = 5;
        ////margin.right = 5;
        //margin.top = 6;
        //margin.bottom = 6;
        //}

        //foreach (GUIStyle x in GetNeedsPaddingStyles)
        //{
        //    if (x != null)
        //    {
        //        //Debug.Log($"{x.name} -> {x.padding}");
        //        var p = x.padding;
        //        p.top = PaddingTop;
        //        p.bottom = PaddingBottom;
        //        x.padding = p;
        //    }
        //}


        //// ComboBox like control
        //Skin.Style("MiniPopup", style =>
        //{
        //    style.font = Font_Normal;
        //    style.fixedHeight = 21;
        //    style.margin.top = 3;
        //    style.margin.bottom = 8;
        //    style.padding.bottom = 6;
        //});

        foreach (GUIStyle x in EditorStylesBig)
        {
            if (x != null)
            {
                // if(x.font != null)
                // {
                //     Debug.Log($"{x.name} : {x.font.name} {x.font.fontSize} {x.fontSize} {x.padding}");
                // }
                // else
                // {
                //     Debug.Log($"{x.name} : NO FONT {x.fontSize} {x.padding}");
                // }

                //x.fontSize = BigFontSize;
            }
        }

        foreach (GUIStyle x in EditorStylesSmall)
        {
            if (x != null)
            {
                // if(x.font != null)
                // {
                //     Debug.Log($"SMALL {x.name} : {x.font.name} {x.font.fontSize} {x.fontSize} {x.padding}");
                // }
                // else
                // {
                //     Debug.Log($"SMALL {x.name} : NO FONT {x.fontSize} {x.padding}");
                // }

                //x.fontSize = SmallFontSize;
            }
        }

        //foreach (var x in EditorStylesBold)
        //{
        //    if (x != null)
        //    {
        //        if (x.font != null)
        //        {
        //            Debug.Log($"{x.name} : {x.font.name} {x.font.fontSize} {x.fontSize} {x.padding}");
        //        }
        //        else
        //        {
        //            Debug.Log($"{x.name} : NO FONT {x.fontSize} {x.padding}");
        //        }
        //    }
        //}

        // foreach (var x in InternalStyles)
        // {
        //     if (x != null)
        //     {
        //         if(x.font != null)
        //         {
        //             Debug.Log($"{x.name} : {x.font.name} {x.font.fontSize} {x.fontSize} {x.padding}");
        //         }
        //         else
        //         {
        //             Debug.Log($"{x.name} : NO FONT {x.fontSize} {x.padding}");
        //         }
        //     }
        // }
        // Debug.Log($"Modified");

        //ForDebug.WriteStylesAll();
    }

    public static bool IsExecModify { get; set; }

    public static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect) => Modify();

    public static void OnPlayModeStateChanged(PlayModeStateChange state) => IsExecModify = true;

    public static void OnSceneOpening(string path, OpenSceneMode mode) => IsExecModify = true;

    public static void OnSceneOpened(Scene scene, OpenSceneMode mode)
    {
        EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyWindowItemOnGUI;
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
    }

    [InitializeOnLoad]
    public class Startup
    {
        static Startup()
        {
            // Debug.Log($"STARTUP!!!");
            EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyWindowItemOnGUI;
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;

            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;

            // Somehow loading a new scene clears the static variable that we stored the font?
            EditorSceneManager.sceneOpened -= OnSceneOpened;
            EditorSceneManager.sceneOpened += OnSceneOpened;
            EditorSceneManager.sceneOpening -= OnSceneOpening;
            EditorSceneManager.sceneOpening += OnSceneOpening;

            IsExecModify = true;
        }
    }

    public static class StyleManager
    {
        public static IEnumerable<GUIStyle> GetAllStyles()
        {
            IEnumerator enumerator = GUI.skin.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current is GUIStyle style)
                {
                    yield return style;
                }
            }
        }

        public static IEnumerable<GUIStyle> GetEditorStyles()
        {
            yield return EditorStyles.objectFieldThumb;
            yield return EditorStyles.objectFieldMiniThumb;
            yield return EditorStyles.colorField;
            yield return EditorStyles.layerMaskField;
            yield return EditorStyles.toggle;
            yield return EditorStyles.foldout;
            yield return EditorStyles.foldoutPreDrop;
            yield return EditorStyles.foldoutHeader;
            yield return EditorStyles.foldoutHeaderIcon;
            yield return EditorStyles.toggleGroup;
            yield return EditorStyles.toolbar;
            yield return EditorStyles.toolbarButton;
            yield return EditorStyles.toolbarPopup;
            yield return EditorStyles.toolbarDropDown;
            yield return EditorStyles.toolbarTextField;
            yield return EditorStyles.inspectorDefaultMargins;
            yield return EditorStyles.inspectorFullWidthMargins;
            yield return EditorStyles.objectField;
            yield return EditorStyles.popup;
            yield return EditorStyles.numberField;
            yield return EditorStyles.label;
            yield return EditorStyles.miniLabel;
            yield return EditorStyles.largeLabel;
            yield return EditorStyles.boldLabel;
            yield return EditorStyles.miniBoldLabel;
            yield return EditorStyles.centeredGreyMiniLabel;
            yield return EditorStyles.wordWrappedMiniLabel;
            yield return EditorStyles.wordWrappedLabel;
            yield return EditorStyles.linkLabel;
            yield return EditorStyles.whiteLabel;
            yield return EditorStyles.helpBox;
            yield return EditorStyles.whiteMiniLabel;
            yield return EditorStyles.whiteBoldLabel;
            yield return EditorStyles.radioButton;
            yield return EditorStyles.miniButton;
            yield return EditorStyles.miniButtonLeft;
            yield return EditorStyles.miniButtonMid;
            yield return EditorStyles.miniButtonRight;
            yield return EditorStyles.miniPullDown;
            yield return EditorStyles.textField;
            yield return EditorStyles.textArea;
            yield return EditorStyles.miniTextField;
            yield return EditorStyles.whiteLargeLabel;
            yield return EditorStyles.toolbarSearchField;
        }

        public static void WriteStylesAll()
        {
            const string path = @"d:\log.log";

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (StreamWriter sw = File.CreateText(path))
            {
                foreach (GUIStyle style in GetAllStyles())
                {
                    sw.WriteLine(ToString(style));
                }

                sw.WriteLine("------------------------------------------------");

                foreach (GUIStyle style in GetEditorStyles())
                {
                    sw.WriteLine(ToString(style));
                }
            }
        }

        public static string ToString(GUIStyle style)
        {
            if (style.name == "ToolbarTextField")
            {
                Console.WriteLine("");
            }

            if (style.font == null)
            {
                return $"Name=[{style.name}], margin={style.margin}, padding={style.padding}, ({style.fixedWidth}x{style.fixedHeight})";
            }
            else
            {
                return $"Name=[{style.name}], Font=[{style.font?.name}, {style.fontSize}pt({style.fontStyle})], Margin={style.margin}, Padding={style.padding}, ({style.fixedWidth}x{style.fixedHeight})";
            }
        }
    }

    public static class AutoFontFixFunction
    {
        public static void UpdateAll()
        {
            foreach (GUIStyle style in StyleManager.GetAllStyles())
            //foreach (GUIStyle style in StyleManager.GetEditorStyles()) // ‚±‚Á‚¿‚ÍŒø‚©‚È‚¢
            {
                //bool? isBold = style.font?.name.ToLower().Contains("bold");

                //if (style.font == null)
                //{
                //    style.font = Font_Normal;
                //    style.fontSize = Font_Normal.fontSize;
                //}

                //if (style != null && style.font != null)
                //{
                //    Debug.Log($"{style.name}={style.font.name}[{style.fontStyle}], {style.fontSize}pt,");
                //}
                //else
                //{
                //    Debug.Log($"{style.name} style is null.");
                //}

                //if (style.fontStyle == FontStyle.Bold)
                //{
                //    style.font = Font_Big;
                //    style.fontSize = Font_Big.fontSize;
                //    style.fontStyle = FontStyle.Normal;
                //}
                //else
                //{
                style.font = Font_Normal;
                style.fontSize = Font_Normal.fontSize;
                style.fontStyle = FontStyle.Normal;
                //}

                //switch (style.font.fontSize)
                //{
                //    case 9:
                //        {
                //            style.font = Font_Small;
                //            style.fontSize = Font_Small.fontSize;
                //            break;
                //        }
                //    case 11:
                //        {
                //            style.font = Font_Normal;
                //            style.fontSize = Font_Normal.fontSize;
                //            break;
                //        }
                //    case 12:
                //        {
                //            style.font = Font_Big;
                //            style.fontSize = Font_Big.fontSize;
                //            break;
                //        }
                //}
            }
        }
    }
}