﻿// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Windows;
using Flow.Launcher.Plugin;
using Flow.Plugin.WindowsSettings.Classes;
using Flow.Plugin.WindowsSettings.Properties;

namespace Flow.Plugin.WindowsSettings.Helper
{
    /// <summary>
    /// Helper class to easier work with context menu entries
    /// </summary>
    internal static class ContextMenuHelper
    {
        /// <summary>
        /// Return a list with all context menu entries for the given <see cref="Result"/>
        /// <para>Symbols taken from <see href="https://docs.microsoft.com/en-us/windows/uwp/design/style/segoe-ui-symbol-font"/></para>
        /// </summary>
        /// <param name="result">The result for the context menu entires</param>
        /// <param name="assemblyName">The name of the this assembly</param>
        /// <returns>A list with context menu entries</returns>
        internal static List<Result> GetContextMenu(in Result result, in string assemblyName)
        {
            if (result?.ContextData is not WindowsSetting entry)
            {
                return new List<Result>(0);
            }

            var list = new List<Result>(1)
            {
                new()
                {
                    Action = _ => TryToCopyToClipBoard(entry.Command),
                    Title = $"{Resources.CopyCommand} (Ctrl+C)",
                },
            };

            return list;
        }

        /// <summary>
        /// Copy the given text to the clipboard
        /// </summary>
        /// <param name="text">The text to copy to the clipboard</param>
        /// <returns><see langword="true"/>The text successful copy to the clipboard, otherwise <see langword="false"/></returns>
        private static bool TryToCopyToClipBoard(in string text)
        {
            try
            {
                Clipboard.Clear();
                Clipboard.SetText(text);
                return true;
            }
            catch (Exception exception)
            {
                Log.Exception("Can't copy to clipboard", exception, typeof(Main));
                return false;
            }
        }
    }
}
