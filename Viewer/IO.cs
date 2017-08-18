﻿using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Viewer
{
    internal static class IO
    {
        private const string Filter = @"Grid files (*.grid)|*.grid|All Files (*.*)|*.*";

        public static void Save(IReadOnlyList<Cell> cells)
        {
            var dialog = new SaveFileDialog {Filter = Filter};

            if (dialog.ShowDialog().GetValueOrDefault())
            {
                using (var stream = dialog.OpenFile())
                using(var writer = new StreamWriter(stream))
                {
                    var data = JsonConvert.SerializeObject(cells);
                    writer.Write(data);
                }
            }            
        }

        public static IReadOnlyList<Cell> Load()
        {
            var dialog = new OpenFileDialog {Filter = Filter};
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                using (var stream = dialog.OpenFile())
                using(var reader = new StreamReader(stream))
                {
                    var data = reader.ReadToEnd();

                    return JsonConvert.DeserializeObject<List<Cell>>(data);
                }
            }

            return new List<Cell>(0);
        }
    }
}
