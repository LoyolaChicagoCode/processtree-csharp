namespace ProcessTree
{
    internal sealed class StringToken
    {
        private readonly ushort _left;
        private readonly ushort _right;

        public StringToken(int left, int right){
            _left = (ushort)left;
            _right = (ushort)right;
        }

        public void Write(BufferedStdOut stdout, string line) {
            for (var i = _left; i <= _right; i++) {
                stdout.Write(line[i]);
            }
        }
    }
}