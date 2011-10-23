using System;

namespace ProcessTree
{
    internal sealed class ProcessLine
    {
        private readonly string _line;
        public readonly int Pid;
        public readonly int PPid;
        private readonly StringToken _name;

        public void WriteName(BufferedStdOut stdout) {
            _name.Write(stdout, _line);
        }
        
        public ProcessLine(string line){
            _line = line;
            var left = 0;
            var right = 0;
            for(right = left; right < line.Length && !Char.IsWhiteSpace(line[right]); right++){}
            Pid = ParseInt32(line, left, right);
            left = right+1;
            for(right = left; right < line.Length && !Char.IsWhiteSpace(line[right]); right++){}
            PPid = ParseInt32(line, left, right);
            left = right+1;
            right = line.Length;
            _name = new StringToken(left, right-1);
        }

        private static int ParseInt32(string line, int left, int right) {
            var value = 0;
            for (var i = left; i < right; i++) {
                value = (value * 10) + (line[i] - 48);
            }
            return value;
        }
    }
}