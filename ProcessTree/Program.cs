﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessTree
{
    static class Program
    {
        static void Main(string[] args){
            //System.Diagnostics.Debugger.Launch();
            var lines = ReadLines().ToArray();
            var map = new Dictionary<int, ICollection<ProcessLine>>(lines.Length);
            foreach (var line in lines){
                map.Add(line.Pid, null);
            }
            foreach (var line in lines){
                if(line.PPid == 0){
                    continue;
                }
                var children = map[line.PPid];
                if(children == null){
                    children = new List<ProcessLine>(32);
                    map[line.PPid] = children;
                }
                children.Add(line);
            }
            PrintTree(map);
        }

        private static void PrintTree(IDictionary<int, ICollection<ProcessLine>> tree) {
            var buffer = new BufferedStdOut();
            PrintTree(buffer, tree);
            buffer.Flush();
        }

        private static void PrintTree(BufferedStdOut buffer, IDictionary<int, ICollection<ProcessLine>> tree, int root = 1, int level = 0) {
            var children = tree[root];
            if (children != null) {
                foreach (var child in children) {
                    buffer.Write(' ', level);
                    child.WriteName(buffer);
                    buffer.Write('\n');
                    PrintTree(buffer, tree, child.Pid, level + 1);
                }
            }
        }

        private static IEnumerable<ProcessLine> ReadLines() {
            string line;
            Console.ReadLine();
            while ((line = Console.ReadLine()) != null){
                yield return new ProcessLine(line);
            }
        }
    }
}
