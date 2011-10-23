using System;
using System.IO;
using System.Text;

namespace ProcessTree
{
    internal sealed class BufferedStdOut
    {
        private readonly StringBuilder _buffer;
        private readonly TextWriter _out;
        public BufferedStdOut(){
            _buffer = new StringBuilder(8*1024);
            _out = Console.Out;
        }
        public void Write(string value){
            if(_buffer.Length + value.Length > _buffer.Capacity){
                Flush();
            }
            _buffer.Append(value);
        }
        public void Write(char value, int count){
            if(_buffer.Length + count > _buffer.Capacity){
                Flush();
            }
            for(int i = 0; i < count; i++){
                _buffer.Append(value);
            }
        }
        public void Write(char value){
            if(_buffer.Length + 1 > _buffer.Capacity){
                Flush();
            }
            _buffer.Append(value);
        }
        public void Flush(){
            _out.Write(_buffer.ToString());
            _buffer.Length = 0;
        }
    }
}