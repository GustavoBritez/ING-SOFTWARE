using System;

namespace WinFormsApp10
{
    public interface IMemento
    {
        string getState();
        void setState(string state);
    }
}
