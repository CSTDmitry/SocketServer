using System.Text;
using TCP.Interfaces;

namespace TCP.Fragments.Externals
{
  public class ByteBuffer : IDisposable, IFragment
  {
    private List<byte> Buffer;
    private byte[] ReadBuffer;
    private int ReadPoint;
    private bool Updated = false;

    public void Initialize()
    {
      Buffer = new List<byte>();
      ReadPoint = 0;
    }

    public long GetReadPoint => ReadPoint;
    public byte[] GetArray => Buffer.ToArray();
    public int GetCount => Buffer.Count;
    public int GetLength => Buffer.Count - ReadPoint;

    public void ClearBuffer()
    {
      Buffer.Clear();
      ReadPoint = 0;
    }

    public void Write(byte[] input)
    {
      Buffer.AddRange(input);
      Updated = true;
    }

    public void Write(short input)
    {
      Buffer.AddRange(BitConverter.GetBytes(input));
      Updated = true;
    }

    public void Write(int input)
    {
      Buffer.AddRange(BitConverter.GetBytes(input));
      Updated = true;
    }

    public void Write(float input)
    {
      Buffer.AddRange(BitConverter.GetBytes(input));
      Updated = true;
    }

    public void Write(long input)
    {
      Buffer.AddRange(BitConverter.GetBytes(input));
      Updated = true;
    }

    public void Write(string input)
    {
      Buffer.AddRange(BitConverter.GetBytes(input.Length));
      Buffer.AddRange(Encoding.ASCII.GetBytes(input));
      Updated = true;
    }


    public byte ReadByte()
    {
      byte result = Buffer[ReadPoint + 1];
      return result;
    }

    public byte[] ReadBytes(int length, bool peek = true)
    {
      if (Updated)
      {
        ReadBuffer = Buffer.ToArray();
        Updated = false;
      }

      byte[] result = Buffer.GetRange(ReadPoint, length).ToArray();

      if (peek)
      {
        ReadPoint += length;
      }

      return result;
    }

    public string ReadString(bool peek = true)
    {
      int length = ReadInteger(true);

      if (Updated)
      {
        ReadBuffer = Buffer.ToArray();
        Updated = false;
      }

      string result = Encoding.ASCII.GetString(ReadBuffer, ReadPoint, length);

      if (peek && Buffer.Count > ReadPoint)
      {
        ReadPoint += length;
      }

      return result;
    }

    public short ReadShort(bool peek = true)
    {
      if (Buffer.Count > ReadPoint)
      {
        if (Updated)
        {
          ReadBuffer = Buffer.ToArray();
          Updated = false;
        }

        short result = BitConverter.ToInt16(ReadBuffer, ReadPoint);

        if (peek && Buffer.Count > ReadPoint)
        {
          ReadPoint += 2;
        }

        return result;
      }
      else
      {
        throw new Exception("Byte buffer is past limit.");
      }
    }

    public float ReadFloat(bool peek = true)
    {
      if (Buffer.Count > ReadPoint)
      {
        if (Updated)
        {
          ReadBuffer = Buffer.ToArray();
          Updated = false;
        }

        float result = BitConverter.ToSingle(ReadBuffer, ReadPoint);

        if (peek && Buffer.Count > ReadPoint)
        {
          ReadPoint += 4;
        }

        return result;
      }
      else
      {
        throw new Exception("Byte buffer is past limit.");
      }
    }

    public long ReadLong(bool peek = true)
    {
      if (Buffer.Count > ReadPoint)
      {
        if (Updated)
        {
          ReadBuffer = Buffer.ToArray();
          Updated = false;
        }

        long result = BitConverter.ToInt64(ReadBuffer, ReadPoint);

        if (peek && Buffer.Count > ReadPoint)
        {
          ReadPoint += 8;
        }

        return result;
      }
      else
      {
        throw new Exception("Byte buffer is past limit.");
      }
    }

    public int ReadInteger(bool peek = true)
    {
      if (Buffer.Count > ReadPoint)
      {
        if (Updated)
        {
          ReadBuffer = Buffer.ToArray();
          Updated = false;
        }

        int result = BitConverter.ToInt32(ReadBuffer, ReadPoint);

        if (peek && Buffer.Count > ReadPoint)
        {
          ReadPoint += 4;
        }

        return result;
      }
      else
      {
        throw new Exception("Byte buffer is past limit.");
      }
    }


    private bool DisposedValue = false;

    protected virtual void Dispose(bool disposing)
    {
      if (!DisposedValue)
      {
        if (disposing)
        {
          Buffer.Clear();
        }
        ReadPoint = 0;
      }

      DisposedValue = true;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
  }
}
