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
        
        public ProcessLine(string line, int pidCol, int ppidCol, int cmdCol){
            _line = line;
            for(int column = 0, left = 0, right = 0; left+(right-left) < line.Length; left = NextNonWhitespace(line, right), column++){
                right = NextToken(line, left);
                if(column == pidCol){
                    Pid = ParseInt32(line, left, right);
                } else if(column == ppidCol){
                    PPid = ParseInt32(line, left, right);
                } else if(column == cmdCol){
                    _name = new StringToken(left, right - 1);
                }
            }
        }

        private static int NextNonWhitespace(string line, int left){
            for(;left < line.Length && Char.IsWhiteSpace(line[left]); left++) {}
            return left;
        }

        private static int NextToken(string line, int left){
            int right;
            for (right = left; right < line.Length && !Char.IsWhiteSpace(line[right]); right++) { }
            return right;
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